using System;
using System.Windows.Forms;

namespace ExampleClient {
	public partial class MenuWorker : FormForAuthorizedUser {

		public MenuWorker() {
			InitializeComponent();
		}

		private void button4_Click(object sender, EventArgs e) {
			new ScheduleWorker().SetAuthData(CurrentWorker, CurrentAuthString).SetParent(this).Show();
		}

		private void button2_Click(object sender, EventArgs e) {
			OrdersW objfrm = new OrdersW();
			objfrm.Show();
		}

		private void button3_Click(object sender, EventArgs e) {
			new ExchangeWorker().SetAuthData(CurrentWorker, CurrentAuthString).SetParent(this).Show();
		}

		private void MenuWorker_FormClosed(object sender, FormClosedEventArgs e) {
			OnCloseForm();
		}
	}
}
