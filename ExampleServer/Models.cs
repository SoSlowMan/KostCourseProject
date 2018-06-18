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

		public Worker(SqlDataReader d) {
			id_worker = Int32.Parse(d["id_worker"].ToString());
			name = d["name"].ToString();
			surname = d["surname"].ToString();
			midname = d["midname"].ToString();
			login = d["login"].ToString();
			status = d["status"].ToString();
		}
	}

	public class Smena {
		public int id_smena { get; set; }
		public int id_rasp { get; set; }
		public string start { get; set; }
		public string end { get; set; }
		public string date { get; set; }

		public Smena(SqlDataReader d) {
			try {
				id_smena = Int32.Parse(d["id_smena"].ToString());
			} catch (Exception e) { }
			try {
				id_rasp = Int32.Parse(d["id_rasp"].ToString());
			} catch (Exception e) { }
			start = d["start"].ToString();
			end = d["end"].ToString();
			date = DateTime.Parse(d["date"].ToString()).ToShortDateString();
		}
	}

	public class WorkShift {

		public Worker worker;
		public Smena smena;

		public WorkShift(SqlDataReader d) {
			worker = new Worker(d);
			smena = new Smena(d);
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

		public Order(SqlDataReader d) {
			id_order = Int32.Parse(d["id_order"].ToString());
			id_worker = Int32.Parse(d["id_worker"].ToString());
			order = d["order"].ToString();
			address = d["address"].ToString();
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
}
