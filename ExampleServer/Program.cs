using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace ExampleServer {

	class Program {

        public static SqlConnection mConnection = new SqlConnection(@"Data Source=SOSLOWMAN\KOSTYA_SERVER;Initial Catalog=rasp_kurs2;Integrated Security=True");
		//public static SqlConnection mConnection = new SqlConnection(@"Data Source=vlad805-w10\sqlexpress;Initial Catalog=dss;Integrated Security=True");
        
		/**
         * Порт, который будет слушать сервер
         */
		static int port = 8005;

		/**
         * Главная функция программы
         */
		static void Main(string[] args) {
			/**
             * Получаем адреса для запуска сокета: указываем локальный
             * адрес (127.0.0.1) и порт, который хотим слушать
             */
			IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);

			/**
             * создаем сокет
             */
			Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			mConnection.Open();
			Console.WriteLine("Подключены к БД.");

			// Создали методы API
			createMethodsAPI();

			try {
				/**
                 * связываем сокет с локальной точкой, по которой будем принимать данные
                 */
				listenSocket.Bind(ipPoint);

				/**
                 * начинаем прослушивание
                 */
				listenSocket.Listen(10);

				Console.WriteLine("Сервер запущен. Ожидание подключений...");

				/**
                 * Крутим постоянный цикл: типа постоянно слушаем соединения
                 * В начале цикла Accept() будет ждать запрос. Как только
                 * Accept() дожидается запроса, тело цикла выполняется.
                 * После отработки и завершения сокета мы снова возвращаемся
                 * в начало тела цикла и ждем, пока Accept() дождется
                 * следующего запроса. Так примерно и работают все сервера.
                 */
				while (true) {
					// Ожидаем и принимаем сокет
					Socket handler = listenSocket.Accept();

					// Создаем строку, в которую будем получать данные от клиента
					StringBuilder builder = new StringBuilder();

					int bytes = 0; // количество полученных байтов
					byte[] data = new byte[256]; // буфер для получаемых данных

					do {
						bytes = handler.Receive(data); // Получение байт от клиента
						builder.Append(Encoding.UTF8.GetString(data, 0, bytes)); // Конвертация байт в строку
					}
					while (handler.Available > 0); // Выполняем до тех пор, пока не закончатся данные от клиента

					// Конвертируем в строку то, что мы получили
					string request = builder.ToString();

					// Выводим то, что нам прислал клиент в консоль
					Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + request);

					// Далее обрабатываем запрос и пытаемся сгенерировать ответ
					string response;

					try {
						response = JsonConvert.SerializeObject(handleRequest(request));
					} catch (Exception e) {
						response = JsonConvert.SerializeObject(new APIError(e.ToString()));
					}

					Console.WriteLine(response);
					/**
                     * Отправляем ответ клиенту
                     */
					// Конвертируем строку в массив байтов
					data = Encoding.UTF8.GetBytes(response);

					// Отправляем клиенту назад через тот же сокет
					handler.Send(data);

					// Закрываем сокет
					handler.Shutdown(SocketShutdown.Both);
					handler.Close();

					// Ждем следующий запрос...
				}
			} catch (Exception ex) {
				Console.WriteLine(ex.Message);
			}
		}

		static Dictionary<string, string> parseParams(string[] split) {
			Dictionary<string, string> paramz = new Dictionary<string, string>();

			// Если в запросе есть параметры (то есть то, что передано после "?"), то разбираем их
			if (split.Length > 1 && split[1].Length > 0) {
				string[] paramzList = split[1].Split('&'); // Делим строку по "&"

				foreach (string item in paramzList) // Пробегаемся по каждой строке формата "name=value", делим по "=" и суем с paramz
				{
					string[] i = item.Split('='); // Слева от "=" - ключ, справа - значение параметра

					if (i.Length != 2) {
						return null;
					}

					paramz.Add(i[0], i[1]);
				}
			}
            return paramz;
		}

		public static string hash(string text) {
			return BitConverter.ToString(new SHA256Managed().ComputeHash(Encoding.Default.GetBytes(text))).Replace("-", "").ToLower();
		}

		static Dictionary<string, APIMethod> mMethods;

		static void createMethodsAPI() {
			mMethods = new Dictionary<string, APIMethod>();
			mMethods.Add("SignIn", new SignIn());
            mMethods.Add("getWorkers", new GetWorkers());
            mMethods.Add("GetSchedule", new GetSchedule());
            mMethods.Add("GetAllSmena", new GetAllSmena());
            mMethods.Add("addSmena", new addSmena());
            mMethods.Add("deleteSmena", new deleteSmena());
            mMethods.Add("getOrders", new getOrders());
            mMethods.Add("addOrder", new addOrder());
            mMethods.Add("deleteOrder", new deleteOrder());
            mMethods.Add("addZay", new addZay());
            mMethods.Add("getAllRasp", new getAllRasp());
            mMethods.Add("addUser", new addUser());
            //mMethods.Add("addauthUser", new addauthWorker());
            mMethods.Add("dropToken", new dropToken());
        }

		static object handleRequest(string data) {
			// Разбиваем строку, присланную клиентом на наш формат (см. клиент, там описание)
			string[] split = data.Split('?');
			string method = split[0].Substring(1);

			Dictionary<string, string> paramz = parseParams(split);


			if (mMethods.ContainsKey(method)) {
				return mMethods[method].execute(paramz, mConnection);
			} else {
				return null;
			}
		}
	}
}
