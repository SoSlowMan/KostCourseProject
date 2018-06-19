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
	public partial class ExchangeWorker : FormForAuthorizedUser {

		public ExchangeWorker() {
			InitializeComponent();
		}

		private void OnLoad(object sender, EventArgs e) {
			RequestAllList();
		}

		private void RequestAllList() {
			new APIRequest().makeAPIRequest("getSchedule", new List<KeyValuePair<string, string>> {
				new KeyValuePair<string, string>("authstr", CurrentAuthString)
			}, delegate (List<WorkShift> items) {
				List<ComboItem> otherSmenas = new List<ComboItem>(); // source
				List<ComboItem> mySmenas = new List<ComboItem>(); // target

				foreach (WorkShift w in items) {
					ComboItem i = new ComboItem("#" + w.smena.id_smena + ": " + w.worker.name + " " + w.worker.surname + " (" + w.smena.date + " с " + w.smena.start + " до " + w.smena.end + ")", w.smena.id_smena);

					if (w.worker.id_worker == CurrentWorker.id_worker) {
						mySmenas.Add(i);
					} else {
						otherSmenas.Add(i);
					}
				}

				variantsCombo.DataSource = otherSmenas;
				variantsCombo.DisplayMember = "display";
				variantsCombo.ValueMember = "value";

				mySmenasCombo.DataSource = mySmenas;
				mySmenasCombo.DisplayMember = "display";
				mySmenasCombo.ValueMember = "value";
			});
		}

		private void OnFormSubmit(object sender, EventArgs e) {
			new APIRequest().makeAPIRequest("addRequestExchange", new List<KeyValuePair<string, string>> {
				new KeyValuePair<string, string>("id_from", mySmenasCombo.SelectedValue.ToString()),
				new KeyValuePair<string, string>("id_to", variantsCombo.SelectedValue.ToString()),
				new KeyValuePair<string, string>("authstr", CurrentAuthString)
			}, delegate (int data) {
				MessageBox.Show("Обмен запрошен");
			});
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e) {
			OnCloseForm();
		}
	}
}
