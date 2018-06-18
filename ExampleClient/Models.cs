namespace ExampleClient {

	public class APIError {
		public int error = 1;
		public string message;
	}

	public class LoginResult {
		public bool isSuccess { get; set; }
		public Worker worker { get; set; }
	}

	public class Worker {
		public int id_worker { get; set; }
		public string name { get; set; }
		public string surname { get; set; }
		public string midname { get; set; }
        public string login { get; set; }
        public string status { get; set; }
    }


	public class Smena {
		public int id_smena { get; set; }
		public string start { get; set; }
		public string end { get; set; }
		public string date { get; set; }
	}

	public class WorkShift {

		public Worker worker;
		public Smena smena;

	}

	delegate void onResponse<T>(T data);

	public class Order {
		public int id_order { get; set; }
		public int id_worker { get; set; }
		public string order { get; set; }
		public string address { get; set; }
	}

	public class OrderWithWorker {
		public Order order;
		public Worker worker;
	}

	public class ComboItem {

		private object mDisplay;
		private object mValue;

		public ComboItem(object d, object v) {
			mDisplay = d;
			mValue = v;
		}
		public object display {
			get {
				return mDisplay;
			}
		}
		public object value {
			get {
				return mValue;
			}
		}

	}

}
