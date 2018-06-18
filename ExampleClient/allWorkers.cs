using System;
using System.Collections.Generic;

namespace ExampleClient {
	public partial class AllWorkers : FormForAuthorizedUser {
		public AllWorkers() {
			InitializeComponent();
		}

		private void AllWorkers_Load(object sender, EventArgs e) {
			dgv.ColumnCount = 5;
			dgv.Columns[0].Name = "ID";
			dgv.Columns[1].Name = "Имя";
			dgv.Columns[2].Name = "Фамилия";
			dgv.Columns[3].Name = "Отчество";
			dgv.Columns[4].Name = "Логин";

			UpdateUsers();
		}

		private void addRow(Worker item) {
			dgv.Rows.Add(item.id_worker, item.name, item.surname, item.midname, item.login);
		}

		private void UpdateUsers() {
			onResponse<List<Worker>> h = delegate (List<Worker> data) {
				dgv.Rows.Clear();
				foreach (Worker item in data) {
					addRow(item);
				}
			};

			new APIRequest().makeAPIRequest("getAllUsers", new List<KeyValuePair<string, string>> {
				new KeyValuePair<string, string>("authstr", CurrentAuthString)
			}, h);
		}

		private void button1_Click(object sender, EventArgs e) {

		}

		private void button2_Click(object sender, EventArgs e) {
			new CreateUser().SetAuthData(CurrentWorker, CurrentAuthString).SetParent(this).Show();
		}

		private void AllWorkers_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e) {
			OnCloseForm();
		}

		
	}
}