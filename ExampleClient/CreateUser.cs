using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ExampleClient {
	public partial class CreateUser : FormForAuthorizedUser {
		public CreateUser() {
			InitializeComponent();
		}

		private void button_update_Click(object sender, EventArgs e) {
			onResponse<int> h = delegate (int data) {
				MessageBox.Show("Юзер регнут с идентификатором " + data);
			};

			new APIRequest().makeAPIRequest("addUser", new List<KeyValuePair<string, string>> {
				new KeyValuePair<string, string>("login", textBox1.Text.ToString()),
				new KeyValuePair<string, string>("password", textBox2.Text.ToString()),
				new KeyValuePair<string, string>("name", textBox3.Text.ToString()),
				new KeyValuePair<string, string>("surname", textBox4.Text.ToString()),
				new KeyValuePair<string, string>("midname", textBox5.Text.ToString()),
				new KeyValuePair<string, string>("status", textBox6.Text.ToString()),
				new KeyValuePair<string, string>("authstr", CurrentAuthString)
			}, h);
		}

		private void CreateUser_FormClosed(object sender, FormClosedEventArgs e) {
			OnCloseForm();
		}
	}
}
