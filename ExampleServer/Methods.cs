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

	abstract class APIUserMethod : APIMethod {

		private Worker worker = null;

		protected void CheckAuth(APIParams paramz, SqlConnection connection) {
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

	/*class addauthWorker : APIMethod
    {
        public override object execute(APIParams paramz, SqlConnection connection)
        {
            SqlCommand command;
            command = new SqlCommand("INSERT INTO [auth] ([id_worker]) VALUES (SELECT [id_worker] FROM [workers] WHERE ); SELECT SCOPE_IDENTITY();", connection);
            command.Parameters.Add("@worker", SqlDbType.Int);
            command.Parameters["@worker"].Value = Int32.Parse(paramz["id_worker"]);

            int id = Convert.ToInt32(command.ExecuteScalar());
            return id;
        }
    }*/

	class GetWorkers : APIUserMethod {

		public override object Execute(APIParams paramz, SqlConnection connection) {
			base.CheckAuth(paramz, connection);

			SqlDataReader reader;
			try {
				reader = new SqlCommand("SELECT * FROM [workers]", connection).ExecuteReader();
			} catch (SqlException e) {
				return new APIError(e.ToString());
			}

			List<Worker> workers = new List<Worker>();

			while (reader.Read()) {
				workers.Add(new Worker(reader));
			}

			reader.Close();
			return workers;

		}
	}

	class GetSchedule : APIUserMethod {

		public override object Execute(APIParams paramz, SqlConnection connection) {
			base.CheckAuth(paramz, connection);

			SqlDataReader reader;
			try {
				reader = new SqlCommand("SELECT [workers].[name], [workers].[surname], [workers].[midname], [rasp].[id_rasp], [rasp].[start], [rasp].[end], [rasp].[date], [rasp_work].[id_smena] FROM [workers], [rasp], [rasp_work] WHERE [workers].[id_worker] = [rasp_work].[id_worker] AND [rasp_work].[id_rasp] = [rasp].[id_rasp]", connection).ExecuteReader();
			} catch (SqlException e) {
				return new APIError(e.ToString());
			}

			List<WorkShift> items = new List<WorkShift>();

			while (reader.Read()) {
				items.Add(new WorkShift(reader));
			}

			reader.Close();

			return items;
		}
	}

	class GetAllSmena : APIUserMethod {

		public override object Execute(APIParams paramz, SqlConnection connection) {
			base.CheckAuth(paramz, connection);

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

	class addSmena : APIUserMethod {

		public override object Execute(APIParams paramz, SqlConnection connection) {
			base.CheckAuth(paramz, connection);

			SqlCommand command;
			command = new SqlCommand("INSERT INTO [rasp_work] ([id_worker], [id_rasp]) VALUES (@worker, @rasp); SELECT SCOPE_IDENTITY();", connection);
			command.Parameters.Add("@worker", SqlDbType.Int);
			command.Parameters["@worker"].Value = Int32.Parse(paramz["id_worker"]);

			command.Parameters.Add("@rasp", SqlDbType.Int);
			command.Parameters["@rasp"].Value = Int32.Parse(paramz["id_rasp"]);

			int id = Convert.ToInt32(command.ExecuteScalar());
			return id;
		}
	}

	class deleteSmena : APIMethod {
		public override object Execute(APIParams paramz, SqlConnection connection) {
			SqlCommand command;
			command = new SqlCommand("DELETE FROM [rasp_work] WHERE [id_smena] = @id_smena", connection);
			command.Parameters.Add("@id_smena", SqlDbType.Int);
			command.Parameters["@id_smena"].Value = Int32.Parse(paramz["id_smena"]);
			return command.ExecuteNonQuery();

		}
	}

	class getOrders : APIMethod {
		public override object Execute(APIParams paramz, SqlConnection connection) {
			SqlDataReader reader;
			try {
				reader = new SqlCommand("SELECT * FROM [orders], [workers] WHERE [orders].[id_worker] = [workers].[id_worker]", connection).ExecuteReader();
			} catch (SqlException e) {
				return new APIError(e.ToString());
			}

			List<OrderWithWorker> orders = new List<OrderWithWorker>();

			while (reader.Read()) {
				orders.Add(new OrderWithWorker(reader));
			}

			reader.Close();

			return orders;
		}
	}

	class addOrder : APIMethod {
		public override object Execute(APIParams paramz, SqlConnection connection) {
			SqlCommand command;
			command = new SqlCommand("INSERT INTO [orders] ([id_worker], [address], [order]) VALUES (@id_worker, @address, @order); SELECT SCOPE_IDENTITY();", connection);
			command.Parameters.Add("@id_worker", SqlDbType.Int);
			command.Parameters["@id_worker"].Value = Int32.Parse(paramz["id_worker"]);

			command.Parameters.Add("@order", SqlDbType.Int);
			command.Parameters["@order"].Value = Int32.Parse(paramz["order"]);

			command.Parameters.Add("@address", SqlDbType.Int);
			command.Parameters["@address"].Value = Int32.Parse(paramz["address"]);

			int id_ord = Convert.ToInt32(command.ExecuteScalar());
			return id_ord;
		}
	}

	class deleteOrder : APIMethod {
		public override object Execute(APIParams paramz, SqlConnection connection) {
			SqlCommand command;
			command = new SqlCommand("DELETE FROM [orders] WHERE [id_order] = @id_order", connection);
			command.Parameters.Add("@id_order", SqlDbType.Int);
			command.Parameters["@id_order"].Value = Int32.Parse(paramz["id_order"]);
			return command.ExecuteNonQuery();

		}
	}

	class addZay : APIMethod {
		public override object Execute(APIParams paramz, SqlConnection connection) {
			SqlCommand command;
			command = new SqlCommand("INSERT INTO [smena_ch] ([id_smena], [id_rasp]) VALUES (@smena, @rasp); SELECT SCOPE_IDENTITY();", connection);
			command.Parameters.Add("@smena", SqlDbType.Int);
			command.Parameters["@smena"].Value = Int32.Parse(paramz["id_smena"]);

			command.Parameters.Add("@rasp", SqlDbType.Int);
			command.Parameters["@rasp"].Value = Int32.Parse(paramz["id_rasp"]);

			int idz = Convert.ToInt32(command.ExecuteScalar());
			return idz;
		}
	}

	class getAllRasp : APIMethod {
		public override object Execute(APIParams paramz, SqlConnection connection) {
			SqlDataReader reader;
			try {
				reader = new SqlCommand("SELECT [rasp].* FROM [workers], [rasp], [rasp_work], [auth] WHERE [workers].[id_worker] = [rasp_work].[id_worker] AND [rasp_work].[id_rasp] = [rasp].[id_rasp] AND [workers].[id_worker] = [auth].[id_worker]", connection).ExecuteReader();
			} catch (SqlException e) {
				return new APIError(e.ToString());
			}

			List<Smena> rasps = new List<Smena>();

			while (reader.Read()) {
				rasps.Add(new Smena(reader));
			}

			reader.Close();

			return rasps;
		}
	}

	class addUser : APIMethod {
		public override object Execute(APIParams paramz, SqlConnection connection) {
			SqlCommand command;
			command = new SqlCommand("INSERT INTO [workers] ([name], [surname], [midname], [login], [password], [status]) VALUES (@name, @surname, @midname, @login, @password, @status); SELECT SCOPE_IDENTITY();", connection);
			command.Parameters.Add("@name", SqlDbType.VarChar);
			command.Parameters["@name"].Value = paramz["name"];

			command.Parameters.Add("@surname", SqlDbType.VarChar);
			command.Parameters["@surname"].Value = paramz["surname"];

			command.Parameters.Add("@midname", SqlDbType.VarChar);
			command.Parameters["@midname"].Value = paramz["midname"];

			command.Parameters.Add("@login", SqlDbType.VarChar);
			command.Parameters["@login"].Value = paramz["login"];

			command.Parameters.Add("@password", SqlDbType.VarChar);
			command.Parameters["@password"].Value = paramz["password"];

			command.Parameters.Add("@status", SqlDbType.VarChar);
			command.Parameters["@status"].Value = paramz["status"];

			command = new SqlCommand("INSERT INTO [auth] ([id_worker]) VALUES (SELECT [id_worker] FROM [workers] WHERE [login]=@login); SELECT SCOPE_IDENTITY();", connection);
			command.Parameters.Add("@worker", SqlDbType.Int);
			command.Parameters["@worker"].Value = Int32.Parse(paramz["id_worker"]);

			int id = Convert.ToInt32(command.ExecuteScalar());
			return id;
		}
	}

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