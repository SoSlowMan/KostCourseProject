using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleClient {

	public partial class FormForAuthorizedUser : Form {
		protected Worker CurrentWorker = null;
		protected string CurrentAuthString = null;
		protected Form ParentFormF = null;


		public FormForAuthorizedUser SetAuthData(Worker worker, string authstr) {
			this.CurrentAuthString = authstr;
			this.CurrentWorker = worker;
			return this;
		}



		public FormForAuthorizedUser SetParent(Form form) {
			ParentFormF = form;
			ParentFormF.Hide();
			return this;
		}

		protected void OnCloseForm() {
			if (ParentFormF != null) {
				ParentFormF.Show();
			}

			

			if (ParentFormF.GetType() == typeof(LoginPage)) {
				onResponse<bool> h = delegate (bool data) {
					
				};

				List<KeyValuePair<string, string>> p = new List<KeyValuePair<string, string>> {
					new KeyValuePair<string, string>("authstr", CurrentAuthString)
				};

				new APIRequest().makeAPIRequest("dropToken", p, h);
			}
		}
	}
}
