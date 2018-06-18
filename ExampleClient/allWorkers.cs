using System;
using System.Collections.Generic;

namespace ExampleClient {
	public partial class AllWorkers : FormForAuthorizedUser {
		public AllWorkers() {
			InitializeComponent();
		}

		private void addRow(Worker item) {
			dgv.Rows.Add(item.name, item.surname, item.midname);
		}

		private void updateWorkers() {
			onResponse<List<Worker>> h = delegate (List<Worker> data) {
				dgv.Rows.Clear();
				foreach (Worker item in data) {
					addRow(item);
				}
			};

			new APIRequest().makeAPIRequest("getSchedule", new List<KeyValuePair<string, string>> {
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