using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;

using APIParams = System.Collections.Generic.Dictionary<string, string>;

namespace ExampleServer {


	/**
	 * Абстрактный метод API
	 * От него наследуются все методы
	 */
	abstract class APIMethod {

		// Абстрактный метод класса - то, что будет выполнять конкретный метод
		// Реализация описана в конкретном методе
		public abstract object Execute(APIParams paramz, SqlConnection connection);

	}

	/**
	 * Абстрактный метод API, который можно вызвать только авторизованному пользователю
	 * То есть, в запросе обязательно должен быть параметр "authstr" к ключом, полученным
	 * при авторизации
	 */
	abstract class APIUserMethod : APIMethod {

		protected Worker worker = null;

		protected virtual void CheckAuth(APIParams paramz, SqlConnection connection) {
			if (!paramz.ContainsKey("authstr")) {
				throw new APIError("no authstr passed");
			}

			SqlCommand command = new SqlCommand("SELECT * FROM [workers] WHERE [authstr] = @astr", connection);
			command.Parameters.Add("@astr", SqlDbType.VarChar);
			command.Parameters["@astr"].Value = paramz["authstr"];

			SqlDataReader reader = command.ExecuteReader();

			if (!reader.Read()) {
				throw new APIError("session invalid");
			}

			this.worker = new Worker(reader);
			reader.Close();
		}

	}

	abstract class APIWorkerMethod : APIUserMethod {

		protected override void CheckAuth(APIParams paramz, SqlConnection connection) {
			base.CheckAuth(paramz, connection);

			if (worker.status != "worker") {
				throw new APIError("this is worker method");
			}
		}
	}

	abstract class APIManagerMethod : APIUserMethod {

		protected override void CheckAuth(APIParams paramz, SqlConnection connection) {
			base.CheckAuth(paramz, connection);

			if (worker.status != "manager") {
				throw new APIError("this is manager method");
			}
		}
	}

	/**
	 * Регистрация
	 */
	class SignIn : APIMethod {

		public override object Execute(APIParams paramz, SqlConnection connection) {
			SqlCommand command = new SqlCommand("SELECT * FROM [workers] WHERE [login] = @login AND [password] = @pass", connection);
			command.Parameters.Add("@login", SqlDbType.VarChar);
			command.Parameters.Add("@pass", SqlDbType.VarChar);

			command.Parameters["@login"].Value = paramz["login"];
			command.Parameters["@pass"].Value = Program.hash(paramz["password"]);

			SqlDataReader reader = command.ExecuteReader();
			LoginResult sign = new LoginResult();

			if (reader.Read()) {
				sign.isSuccess = true;
				sign.worker = new Worker(reader);
			}

			reader.Close();

			if (sign.isSuccess) {

				sign.authstr = Program.hash(sign.worker.login + DateTimeOffset.Now.Ticks);

				command = new SqlCommand("UPDATE [workers] SET [authstr] = @authstr WHERE [id_worker] = @id_worker", connection);

				command.Parameters.Add("@authstr", SqlDbType.VarChar);
				command.Parameters.Add("@id_worker", SqlDbType.Int);

				command.Parameters["@authstr"].Value = sign.authstr;
				command.Parameters["@id_worker"].Value = sign.worker.id_worker;

				command.ExecuteNonQuery();
			}

			return sign;
		}

	}

	/**
	 * Возвращает массив работников
	 */
	class GetWorkers : APIManagerMethod {

		public override object Execute(APIParams paramz, SqlConnection connection) {
			CheckAuth(paramz, connection);

			SqlDataReader reader = new SqlCommand("SELECT * FROM [workers]", connection).ExecuteReader();
			List<Worker> workers = new List<Worker>();

			while (reader.Read()) {
				workers.Add(new Worker(reader));
			}

			reader.Close();
			return workers;

		}
	}

	/**
	 * Возвращает расписание
	 */
	class GetSchedule : APIUserMethod {

		public override object Execute(APIParams paramz, SqlConnection connection) {
			CheckAuth(paramz, connection);

			SqlCommand cmd;

			if (!paramz.ContainsKey("onlyMe")) {
				cmd = new SqlCommand("SELECT [workers].[id_worker], [workers].[name], [workers].[surname], [workers].[midname], [workers].[login], [workers].[status], [rasp].*, [rasp_work].[id_smena] FROM [workers], [rasp], [rasp_work] WHERE [workers].[id_worker] = [rasp_work].[id_worker] AND [rasp_work].[id_rasp] = [rasp].[id_rasp]", connection);
			} else {
				cmd = new SqlCommand("SELECT [workers].[id_worker], [workers].[name], [workers].[surname], [workers].[midname], [workers].[login], [workers].[status], [rasp].*, [rasp_work].[id_smena] FROM [workers], [rasp], [rasp_work] WHERE [workers].[id_worker] = [rasp_work].[id_worker] AND [rasp_work].[id_rasp] = [rasp].[id_rasp] AND [rasp_work].[id_worker] = @id_worker", connection);
				cmd.Parameters.Add("@id_worker", SqlDbType.Int);
				cmd.Parameters["@id_worker"].Value = worker.id_worker;
			}

			SqlDataReader reader = cmd.ExecuteReader();

			List<WorkShift> items = new List<WorkShift>();

			while (reader.Read()) {
				items.Add(new WorkShift(reader));
			}

			reader.Close();

			return items;
		}
	}

	class GetAllSmena : APIManagerMethod {

		public override object Execute(APIParams paramz, SqlConnection connection) {
			CheckAuth(paramz, connection);

			SqlDataReader reader;
			try {
				reader = new SqlCommand("SELECT * FROM [rasp]", connection).ExecuteReader();
			} catch (SqlException e) {
				return new APIError(e.ToString());
			}

			List<Smena> smens = new List<Smena>();

			while (reader.Read()) {
				smens.Add(new Smena(reader));
			}

			reader.Close();

			return smens;
		}
	}

	class AddSmena : APIManagerMethod {

		public override object Execute(APIParams paramz, SqlConnection connection) {
			CheckAuth(paramz, connection);

			SqlCommand command  = new SqlCommand("INSERT INTO [rasp_work] ([id_worker], [id_rasp]) VALUES (@worker, @rasp); SELECT SCOPE_IDENTITY();", connection);
			command.Parameters.Add("@worker", SqlDbType.Int);
			command.Parameters["@worker"].Value = Int32.Parse(paramz["id_worker"]);

			command.Parameters.Add("@rasp", SqlDbType.Int);
			command.Parameters["@rasp"].Value = Int32.Parse(paramz["id_rasp"]);

			return Convert.ToInt32(command.ExecuteScalar());
		}
	}

	class DeleteSmena : APIManagerMethod {
		public override object Execute(APIParams paramz, SqlConnection connection) {
			CheckAuth(paramz, connection);

			SqlCommand command = new SqlCommand("DELETE FROM [rasp_work] WHERE [id_smena] = @id_smena", connection);
			command.Parameters.Add("@id_smena", SqlDbType.Int);
			command.Parameters["@id_smena"].Value = Int32.Parse(paramz["id_smena"]);

			return command.ExecuteNonQuery();

		}
	}

	/**
	 * Возвращает все заказы
	 */
	class GetOrders : APIUserMethod {

		public override object Execute(APIParams paramz, SqlConnection connection) {
			CheckAuth(paramz, connection);

			SqlCommand cmd;

			if (worker.status == "worker") {
				cmd = new SqlCommand("SELECT * FROM [orders], [workers] WHERE [orders].[id_worker] = @id_worker AND [orders].[id_worker] = [workers].[id_worker]", connection);
				cmd.Parameters.Add("@id_worker", SqlDbType.Int);
				cmd.Parameters["@id_worker"].Value = worker.id_worker;
			} else {
				cmd = new SqlCommand("SELECT * FROM [orders], [workers] WHERE [orders].[id_worker] = [workers].[id_worker]", connection);
			}

			SqlDataReader reader = cmd.ExecuteReader();
			List<OrderWithWorker> orders = new List<OrderWithWorker>();

			while (reader.Read()) {
				orders.Add(new OrderWithWorker(reader));
			}

			reader.Close();

			return orders;
		}

	}

	/**
	 * Создает заказ
	 */
	class AddOrder : APIManagerMethod {

		public override object Execute(APIParams paramz, SqlConnection connection) {
			CheckAuth(paramz, connection);

			SqlCommand command = new SqlCommand("INSERT INTO [orders] ([id_worker], [address], [order]) VALUES (@id_worker, @address, @order); SELECT SCOPE_IDENTITY();", connection);
			command.Parameters.Add("@id_worker", SqlDbType.Int);
			command.Parameters["@id_worker"].Value = Int32.Parse(paramz["id_worker"]);

			command.Parameters.Add("@order", SqlDbType.VarChar);
			command.Parameters["@order"].Value = paramz["order"];

			command.Parameters.Add("@address", SqlDbType.VarChar);
			command.Parameters["@address"].Value = paramz["address"];

			return Convert.ToInt32(command.ExecuteScalar());
		}
	}
	
	/**
	 * Удаляет заказ
	 */
	class DeleteOrder : APIManagerMethod {

		public override object Execute(APIParams paramz, SqlConnection connection) {
			CheckAuth(paramz, connection);

			SqlCommand command = new SqlCommand("DELETE FROM [orders] WHERE [id_order] = @id_order", connection);
			command.Parameters.Add("@id_order", SqlDbType.Int);
			command.Parameters["@id_order"].Value = Int32.Parse(paramz["id_order"]);
			return command.ExecuteNonQuery();
		}
	}

	/**
	 * Реквестует обмен сменами
	 */
	class AddRequestExchange : APIWorkerMethod {

		public override object Execute(APIParams paramz, SqlConnection connection) {
			CheckAuth(paramz, connection);

			SqlCommand command = new SqlCommand("INSERT INTO [smena_ch] ([id_smena], [id_rasp]) VALUES (@my_id, @desired_id); SELECT SCOPE_IDENTITY();", connection);
			
			command.Parameters.Add("@my_id", SqlDbType.Int);
			command.Parameters["@my_id"].Value = Int32.Parse(paramz["id_from"]);

			command.Parameters.Add("@desired_id", SqlDbType.Int);
			command.Parameters["@desired_id"].Value = Int32.Parse(paramz["id_to"]);

			return Convert.ToInt32(command.ExecuteScalar());
		}

	}

	/**
	 * Выборка заявок на обмен сменами
	 */
	class GetRequestsExchange : APIManagerMethod {

		public override object Execute(APIParams paramz, SqlConnection connection) {
			CheckAuth(paramz, connection);

			SqlDataReader reader = new SqlCommand("SELECT [s].[id_zay], [rwm].[id_rasp] AS [cur_id_rasp], [rwm].[id_smena] AS [cur_id_smena], [rwd].[id_rasp] AS [des_id_rasp], [rwd].[id_smena] AS [des_id_smena], [wc].[id_worker] AS [cur_id_worker], [wc].[name] AS [cur_name], [wc].[surname] AS [cur_surname], [wc].[midname] AS [cur_midname], [wc].[login] AS [cur_login], [wc].[status] AS [cur_status], [wf].[id_worker] AS [des_id_worker], [wf].[name] AS [des_name], [wf].[surname] AS [des_surname], [wf].[midname] AS [des_midname], [wf].[login] AS [des_login], [wf].[status] AS [des_status], [rc].[date] AS [cur_date], [rc].[start] AS [cur_start], [rc].[end] AS [cur_end], [rf].[date] AS [des_date], [rf].[start] AS [des_start], [rf].[end] AS [des_end] FROM [dbo].[smena_ch] [s], [dbo].[rasp_work] [rwm], [dbo].[rasp_work] [rwd], [dbo].[workers] [wc], [dbo].[workers] [wf], [dbo].[rasp] [rc], [dbo].[rasp] [rf] WHERE [s].[id_smena] = [rwm].[id_smena] AND [rwm].[id_worker] = [wc].[id_worker] AND [rwm].[id_rasp] = [rc].[id_rasp] AND [s].[id_rasp] = [rwd].[id_smena] AND [rwd].[id_worker] = [wf].[id_worker] AND [rwd].[id_rasp] = [rf].[id_rasp]", connection).ExecuteReader();

			List<ExchangeRequest> items = new List<ExchangeRequest>();

			while (reader.Read()) {
				items.Add(new ExchangeRequest(reader));
			}

			reader.Close();

			return items;
		}
	}

	class AcceptRequestExchange : APIManagerMethod {

		public override object Execute(APIParams paramz, SqlConnection connection) {
			CheckAuth(paramz, connection);

			SqlCommand cmd = new SqlCommand("SELECT	[rwm].[id_worker] AS [ciw], [rwm].[id_smena] AS[cis], [rwd].[id_worker] AS[diw], [rwd].[id_smena] AS[dis] FROM [dbo].[smena_ch] [s], [dbo].[rasp_work] [rwm], [dbo].[rasp_work] [rwd] WHERE [s].[id_smena] = [rwm].[id_smena] AND [s].[id_rasp] = [rwd].[id_smena] AND [s].[id_zay] = @id_request", connection);
			cmd.Parameters.Add("@id_request", SqlDbType.Int);
			cmd.Parameters["@id_request"].Value = Convert.ToInt32(paramz["id_request"]);

			SqlDataReader reader = cmd.ExecuteReader();

			if (reader.Read()) {
				int currentWorker = Convert.ToInt32(reader["ciw"]);
				int current = Convert.ToInt32(reader["cis"]);
				int desiredWorker = Convert.ToInt32(reader["diw"]);
				int desired = Convert.ToInt32(reader["dis"]);

				reader.Close();

				string sql = "UPDATE [rasp_work] SET [id_worker] = @id_worker WHERE [id_smena] = @id_smena";

				// Тот, кто хотел другую смену, получает ее
				cmd = new SqlCommand(sql, connection);
				cmd.Parameters.Add("@id_worker", SqlDbType.Int);
				cmd.Parameters["@id_worker"].Value = desiredWorker;
				cmd.Parameters.Add("@id_smena", SqlDbType.Int);
				cmd.Parameters["@id_smena"].Value = current;
				cmd.ExecuteNonQuery();

				// Тот, кто был на желаемой смене, получает смену, которая была у того, кто хотел эту
				cmd = new SqlCommand(sql, connection);
				cmd.Parameters.Add("@id_worker", SqlDbType.Int);
				cmd.Parameters["@id_worker"].Value = currentWorker;
				cmd.Parameters.Add("@id_smena", SqlDbType.Int);
				cmd.Parameters["@id_smena"].Value = desired;
				cmd.ExecuteNonQuery();
			} else {
				reader.Close();
				return new APIError("specified request not found");
			}

			// Удаляем заявку
			cmd = new SqlCommand("DELETE FROM [smena_ch] WHERE [id_zay] = @id_request", connection);
			cmd.Parameters.Add("@id_request", SqlDbType.Int);
			cmd.Parameters["@id_request"].Value = Convert.ToInt32(paramz["id_request"]);

			return cmd.ExecuteNonQuery() > 0;
		}

	}

	class RejectRequestExchange : APIManagerMethod {

		public override object Execute(APIParams paramz, SqlConnection connection) {
			CheckAuth(paramz, connection);

			SqlCommand cmd = new SqlCommand("DELETE FROM [smena_ch] WHERE [id_zay] = @id_request", connection);
			cmd.Parameters.Add("@id_request", SqlDbType.Int);
			cmd.Parameters["@id_request"].Value = Convert.ToInt32(paramz["id_request"]);

			return cmd.ExecuteNonQuery() > 0;
		}

	}

	// Он вообще нужен? Не нашел использования на клиенте
	class GetAllRasp : APIUserMethod {
		public override object Execute(APIParams paramz, SqlConnection connection) {
			CheckAuth(paramz, connection);

			SqlDataReader reader = new SqlCommand("SELECT [rasp].* FROM [workers], [rasp], [rasp_work], [auth] WHERE [workers].[id_worker] = [rasp_work].[id_worker] AND [rasp_work].[id_rasp] = [rasp].[id_rasp] AND [workers].[id_worker] = [auth].[id_worker]", connection).ExecuteReader();
			List<Smena> rasps = new List<Smena>();

			while (reader.Read()) {
				rasps.Add(new Smena(reader));
			}

			reader.Close();

			return rasps;
		}
	}

	/**
	 * Регистрирует нового пользователя
	 */
	class AddUser : APIManagerMethod {

		public override object Execute(APIParams paramz, SqlConnection connection) {
			CheckAuth(paramz, connection);

			if (paramz["password"].Length == 0 || paramz["name"].Length == 0 || paramz["surname"].Length == 0 || paramz["login"].Length == 0 || paramz["status"].Length == 0) {
				return new APIError("не все поля указаны");
			}

			SqlCommand command = new SqlCommand("INSERT INTO [workers] ([name], [surname], [midname], [login], [password], [status]) VALUES (@name, @surname, @midname, @login, @password, @status); SELECT SCOPE_IDENTITY();", connection);
			command.Parameters.Add("@name", SqlDbType.VarChar);
			command.Parameters["@name"].Value = paramz["name"];

			command.Parameters.Add("@surname", SqlDbType.VarChar);
			command.Parameters["@surname"].Value = paramz["surname"];

			command.Parameters.Add("@midname", SqlDbType.VarChar);
			command.Parameters["@midname"].Value = paramz["midname"];

			command.Parameters.Add("@login", SqlDbType.VarChar);
			command.Parameters["@login"].Value = paramz["login"];

			command.Parameters.Add("@password", SqlDbType.VarChar);
			command.Parameters["@password"].Value = Program.hash(paramz["password"]);

			command.Parameters.Add("@status", SqlDbType.VarChar);
			command.Parameters["@status"].Value = paramz["status"];

			return Convert.ToInt32(command.ExecuteScalar());
		}

	}

	/**
	 * Стирает текущую сессию
	 * По переданному authstr более нельзя будет совершать запросы
	 */
	class DropToken : APIUserMethod {

		public override object Execute(APIParams paramz, SqlConnection connection) {
			base.CheckAuth(paramz, connection);

			SqlCommand c = new SqlCommand("UPDATE [workers] SET [authstr] = NULL WHERE [authstr] = @str", connection);

			c.Parameters.Add("@str", SqlDbType.VarChar);
			c.Parameters["@str"].Value = paramz["authstr"];

			return c.ExecuteNonQuery() > 0;
		}
	}
}