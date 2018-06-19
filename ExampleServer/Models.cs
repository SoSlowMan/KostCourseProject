using System;
using System.Data.SqlClient;

namespace ExampleServer {

	public class APIError : Exception {
		public int error = 1;
		public string message;

		public APIError(string msg) {
			message = msg;
		}
	}

	public class Worker {
		public int id_worker { get; set; }
		public string name { get; set; }
		public string surname { get; set; }
		public string midname { get; set; }
		public string login { get; set; }
		public string status { get; set; }

		public Worker(SqlDataReader d) : this(d, "") {}

		public Worker(SqlDataReader d, string prefix) {
			id_worker = Int32.Parse(d[prefix + "id_worker"].ToString());
			name = d[prefix + "name"].ToString();
			surname = d[prefix + "surname"].ToString();
			midname = d[prefix + "midname"].ToString();
			login = d[prefix + "login"].ToString();
			status = d[prefix + "status"].ToString();
		}
	}

	public class Smena {
		public int id_smena { get; set; }
		public int id_rasp { get; set; }
		public string start { get; set; }
		public string end { get; set; }
		public string date { get; set; }

		public Smena(SqlDataReader d) : this(d, "") {}

		public Smena(SqlDataReader d, string prefix) {
			try {
				id_smena = Int32.Parse(d[prefix + "id_smena"].ToString());
			} catch (Exception) { }
			try {
				id_rasp = Int32.Parse(d[prefix + "id_rasp"].ToString());
			} catch (Exception) { }
			start = d[prefix + "start"].ToString();
			end = d[prefix + "end"].ToString();
			date = DateTime.Parse(d[prefix + "date"].ToString()).ToShortDateString();
		}
	}

	public class WorkShift {

		public Worker worker;
		public Smena smena;

		public WorkShift(SqlDataReader d) : this(d, "") {}

		public WorkShift(SqlDataReader d, string prefix) {
			worker = new Worker(d, prefix);
			smena = new Smena(d, prefix);
		}

	}

	public class LoginResult {
		public bool isSuccess { get; set; } = false;
		public Worker worker { get; set; }
		public string authstr { get; set; }
	}

	public class Order {
		public int id_order { get; set; }
		public int id_worker { get; set; }
		public string order { get; set; }
		public string address { get; set; }
		public string date { get; set; }

		public Order(SqlDataReader d) {
			id_order = Int32.Parse(d["id_order"].ToString());
			id_worker = Int32.Parse(d["id_worker"].ToString());
			order = d["order"].ToString();
			address = d["address"].ToString();
			date = d["date"].ToString();
		}
	}

	public class OrderWithWorker {
		public Order order;
		public Worker worker;

		public OrderWithWorker(SqlDataReader d) {
			order = new Order(d);
			worker = new Worker(d);
		}
	}

	public class ExchangeRequest {

		public int id_request { get; set; }
		public WorkShift current { get; set; }
		public WorkShift desired { get; set; }

		public ExchangeRequest(SqlDataReader d) {
			id_request = Convert.ToInt32(d["id_zay"].ToString());
			current = new WorkShift(d, "cur_");
			desired = new WorkShift(d, "des_");
		}
	}
}
