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

			onResponse<LoginResult> h = delegate (LoginResult data) {
				if (data.isSuccess) {
					Form neededForm;
					if (data.worker.status == "manager") {
						neededForm = new MenuManager();
					} else {
						neededForm = new MenuWorker();
					}
					neededForm.Show();
				} else {
					MessageBox.Show("Проверьте логин или пароль");
				}
			};

			new APIRequest().makeAPIRequest("SignIn", parms, h);
		}
	}
}