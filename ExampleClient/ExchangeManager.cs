using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ExampleClient {

	/*
    * Эта форма для управления поданными заявками
    * В dgv будут показываться заявки от людей на обмен сменами 
    * Если принять то обмен произойдет, а заявка, само собой, удалится из таблицы
    * Если отклонить то заявка просто удалится из таблицы
    */
	public partial class ExchangeManager : FormForAuthorizedUser {
		public ExchangeManager() {
			InitializeComponent();
		}

		private void ExchangeManager_Load(object sender, EventArgs e) {
			dgv.ColumnCount = 5;
			dgv.Columns[0].Name = "RID";
			dgv.Columns[1].Name = "ТекФИО";
			dgv.Columns[2].Name = "ЖелФИО";
			dgv.Columns[3].Name = "ТекСмена";
			dgv.Columns[4].Name = "ЖелСмена";

			UpdateTable();
		}

		/**
		 * Обновление раписания
		 */
		private void UpdateTable() {
			new APIRequest().makeAPIRequest("getRequestsExchange", new List<KeyValuePair<string, string>> {
				new KeyValuePair<string, string>("authstr", CurrentAuthString)
			}, delegate (List<ExchangeRequest> data) {
				dgv.Rows.Clear();
				foreach (ExchangeRequest item in data) {
					addRow(item);
				}
			});
		}

		private void addRow(ExchangeRequest item) {
			dgv.Rows.Add(
				item.id_request,
				getWorkerName(item.current.worker),
				getWorkerName(item.desired.worker),
				getSmenaString(item.current.smena),
				getSmenaString(item.desired.smena)
			);
		}

		private string getWorkerName(Worker w) {
			return w.name + " " + w.surname + " " + w.midname;
		}

		private string getSmenaString(Smena s) {
			return s.date + " " + s.start + "-" + s.end;
		}

		private int getSelectedRequestId() {
			if (dgv.SelectedRows.Count != 1) {
				return -1;
			}

			object s = dgv.SelectedRows[0].Cells[0].Value;

			return s != null ? Convert.ToInt32(s.ToString()) : -1;
		}

		private void ExchangeManager_FormClosed(object sender, FormClosedEventArgs e) {
			OnCloseForm();
		}

		private void OnAccept(object sender, EventArgs e) {
			int rid = getSelectedRequestId();
			if (rid == -1) {
				MessageBox.Show("Выделите одну строку сбоку");
				return;
			}

			new APIRequest().makeAPIRequest("acceptRequest", new List<KeyValuePair<string, string>> {
				new KeyValuePair<string, string>("authstr", CurrentAuthString),
				new KeyValuePair<string, string>("id_request", rid.ToString())
			}, delegate (bool data) {
				UpdateTable();
			});
		}

		private void OnReject(object sender, EventArgs e) {
			int rid = getSelectedRequestId();
			if (rid == -1) {
				MessageBox.Show("Выделите одну строку сбоку");
				return;
			}

			new APIRequest().makeAPIRequest("rejectRequest", new List<KeyValuePair<string, string>> {
				new KeyValuePair<string, string>("authstr", CurrentAuthString),
				new KeyValuePair<string, string>("id_request", rid.ToString())
			}, delegate (bool data) {
				UpdateTable();
			});
		}
	}
}
