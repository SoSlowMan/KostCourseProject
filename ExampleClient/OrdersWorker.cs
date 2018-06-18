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
	public partial class OrdersWorker : FormForAuthorizedUser {
		public OrdersWorker() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {

		}

		private void OrdersW_Load(object sender, EventArgs e) {

		}

		private void OrdersW_FormClosed(object sender, FormClosedEventArgs e) {
			OnCloseForm();
		}
	}
}
