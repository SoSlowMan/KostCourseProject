using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ExampleClient {
	public partial class OrdersWorker : FormForAuthorizedUser {
		public OrdersWorker() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {

		}

		private void OrdersW_Load(object sender, EventArgs e) {
			dgv.ColumnCount = 4;
			dgv.Columns[0].Name = "ID";
			dgv.Columns[1].Name = "Описание";
			dgv.Columns[2].Name = "Адрес";
			dgv.Columns[3].Name = "Дата";

			UpdateOrders();
		}

		private void UpdateOrders() {
			new APIRequest().makeAPIRequest("getOrders", new List<KeyValuePair<string, string>> {
				new KeyValuePair<string, string>("authstr", CurrentAuthString)
			}, delegate (List<OrderWithWorker> data) {
				dgv.Rows.Clear();
				foreach (OrderWithWorker item in data) {
					addRow(item);
				}
			});
		}

		private void addRow(OrderWithWorker item) {
			dgv.Rows.Add(item.order.id_order, item.order.order, item.order.address, item.order.date);
		}

		private void OrdersW_FormClosed(object sender, FormClosedEventArgs e) {
			OnCloseForm();
		}
	}
}
