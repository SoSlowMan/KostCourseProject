using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace ExampleClient {
	class APIRequest {

		// Destination IP
		private string mDestIP = "127.0.0.1";

		// Port
		private int mPort = 8005;

		public APIRequest() {}

		public void makeAPIRequest<T>(string method, List<KeyValuePair<string, string>> paramz, onResponse<T> handler) {
			try {
				IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(mDestIP), mPort);

				Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

				// Подключение к удаленному хосту
				socket.Connect(ipPoint);

				// Создаем пустую строку для параметров
				StringBuilder sb = new StringBuilder("/").Append(method).Append("?");

				// Если есть параметры (вдруг у нас есть метод, где параметров нет)
				if (paramz != null) {
					// Загоняем все параметры запроса в строку
					foreach (KeyValuePair<string, string> item in paramz) {
						sb.Append(item.Key).Append("=").Append(item.Value).Append("&");
					}
				}

				string queryString = sb.ToString();
				queryString = queryString.Remove(queryString.Length - 1);

				// Строку с параметрами переводим в массив с байтами для
				// последующей передачи через сокеты
				byte[] data = Encoding.UTF8.GetBytes(queryString);

				// Отправляем данные
				socket.Send(data);

				// Получаем ответ от сервера API
				data = new byte[256]; // Буфер для ответа
				StringBuilder builder = new StringBuilder();
				int bytes = 0; // количество полученных байт

				// Буфферизация ответа
				do {
					bytes = socket.Receive(data, data.Length, 0);
					builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
				} while (socket.Available > 0); // ... до тех пор, пока доступно еще что-то для получения

				// Закрываем сокет
				socket.Shutdown(SocketShutdown.Both);
				socket.Close();

				/**
                 * Мы получили ответ от сервера, а теперь хотим с ним что-то сделать,
                 * ну, например, показать пользователю ответ
                 */
				handler(JsonConvert.DeserializeObject<T>(builder.ToString()));
			} catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

	}
}