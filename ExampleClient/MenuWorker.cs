using System;
using System.Windows.Forms;

namespace ExampleClient {
	public partial class MenuWorker : FormForAuthorizedUser {

		public MenuWorker() {
			InitializeComponent();
		}

		private void button4_Click(object sender, EventArgs e) {
			new RaspW().Show();
		}

		private void button2_Click(object sender, EventArgs e) {
			OrdersW objfrm = new OrdersW();
			objfrm.Show();
		}

		private void button3_Click(object sender, EventArgs e) {
			Exchange_worker objfrm = new Exchange_worker();
			objfrm.Show();
		}

		private void MenuWorker_FormClosed(object sender, FormClosedEventArgs e) {
			OnCloseForm();
		}
	}
}
