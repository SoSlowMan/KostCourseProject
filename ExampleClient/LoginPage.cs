using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace ExampleClient {

	public partial class LoginPage : Form {

		public LoginPage() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {
			List<KeyValuePair<string, string>> parms = new List<KeyValuePair<string, string>> {
				new KeyValuePair<string, string>("login", textBox1.Text.Trim()),
				new KeyValuePair<string, string>("password", textBox2.Text.Trim())
			};

			// Получили ответ от API
			onResponse<LoginResult> h = delegate (LoginResult data) {

				// Если авторизация успешна
				if (data.isSuccess) {

					// Создаем переменную для формы (мы еще пока не заем, какую именно)
					FormForAuthorizedUser neededForm;

					// Узнаем кто этот юзера
					if (data.worker.status == "manager") {
						neededForm = new MenuManager();
					} else {
						neededForm = new MenuWorker();
					}
					// Обе эти формы/класса наследуются от FormForAuthorizedUser

					// Пихаем в них данные о нашем пользователе, который авторизовался
					// Теперь эти формы знают кто мы есть
					neededForm.SetAuthData(data.worker, data.authstr);
					neededForm.SetParent(this);

					// Открываем форму
					neededForm.Show();

					// Скрываем эту форму
					this.Hide();
				} else {
					MessageBox.Show("Проверьте логин или пароль");
				}
			};

			new APIRequest().makeAPIRequest("SignIn", parms, h);
		}
	}
}