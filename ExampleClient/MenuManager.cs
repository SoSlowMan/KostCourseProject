using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleClient {

	public partial class MenuManager : FormForAuthorizedUser {

		public MenuManager() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {
			new ScheduleManage().SetAuthData(CurrentWorker, CurrentAuthString).SetParent(this).Show();
		}

		private void button2_Click(object sender, EventArgs e) {
			new OrdersManager().SetAuthData(CurrentWorker, CurrentAuthString).SetParent(this).Show();
		}

		private void button3_Click(object sender, EventArgs e) {
            //new ExchangeManager().SetAuthData(CurrentWorker, CurrentAuthString).SetParent(this).Show();
        }

		private void button4_Click(object sender, EventArgs e) {
			new AllWorkers().SetAuthData(CurrentWorker, CurrentAuthString).SetParent(this).Show();
		}

		private void MenuManager_FormClosed(object sender, FormClosedEventArgs e) {
			OnCloseForm();
		}
	}
}