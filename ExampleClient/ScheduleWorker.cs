using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ExampleClient {
	public partial class ScheduleWorker : FormForAuthorizedUser {
		public ScheduleWorker() {
			InitializeComponent();
		}

		private void OnLoad(object sender, EventArgs e) {
			dgv.ColumnCount = 6;
			dgv.Columns[0].Name = "ID";
			dgv.Columns[1].Name = "Имя";
			dgv.Columns[2].Name = "Фамилия";
			dgv.Columns[3].Name = "Отчество";
			dgv.Columns[4].Name = "Время";
			dgv.Columns[5].Name = "Дата";

			UpdateSchedule();
		}

		private void UpdateSchedule() {
			onResponse<List<WorkShift>> h = delegate (List<WorkShift> data) {
				dgv.Rows.Clear();
				foreach (WorkShift item in data) {
					addRow(item);
				}
			};

			new APIRequest().makeAPIRequest("getSchedule", new List<KeyValuePair<string, string>> {
				new KeyValuePair<string, string>("authstr", CurrentAuthString)
			}, h);
		}

		private void MySchedule() {
			onResponse<List<WorkShift>> h = delegate (List<WorkShift> data) {
				dgv.Rows.Clear();
				foreach (WorkShift myitem in data) {
					addRow(myitem);
				}
			};

			new APIRequest().makeAPIRequest("getSchedule", new List<KeyValuePair<string, string>> {
				new KeyValuePair<string, string>("onlyMe", "1"),
				new KeyValuePair<string, string>("authstr", CurrentAuthString)
			}, h);
		}

		private void addRow(WorkShift item) {
			dgv.Rows.Add(item.smena.id_smena, item.worker.name, item.worker.surname, item.worker.midname, item.smena.start + "-" + item.smena.end, item.smena.date);
		}


		private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e) {

		}

		private void button1_Click(object sender, EventArgs e) {
			UpdateSchedule();
		}

		private void button2_Click(object sender, EventArgs e) {
			MySchedule();
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e) {
			OnCloseForm();
		}
	}
}
