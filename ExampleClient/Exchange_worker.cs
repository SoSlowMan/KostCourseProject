using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleClient
{
    public partial class Exchange_worker : Form
    {
        public Exchange_worker()
        {
            InitializeComponent();
        }

        private void button_add_Click(object sender, EventArgs e)
        {

        }

        private void Exchange_worker_Load(object sender, EventArgs e)
        {
            showWorkersInAddShiftForm();
            showSmensInAddShiftForm();
            showSmensInAddShiftForm2();
        }

        private void showWorkersInAddShiftForm()
        {
            onResponse<List<Worker>> h = delegate (List<Worker> items) {
                List<ComboItem> src = new List<ComboItem>();

                foreach (Worker w in items)
                {
                    src.Add(new ComboItem(w.name + " " + w.surname + " " + w.midname, w.id_worker));
                }

                //add_worker.DataSource = src;
                //add_worker.DisplayMember = "display";
                //add_worker.ValueMember = "value";
            };
            new APIRequest().makeAPIRequest("getWorkers", null, h);
        }

        private void showSmensInAddShiftForm()
        {
            onResponse<List<Smena>> h = delegate (List<Smena> items) {
                List<ComboItem> src = new List<ComboItem>();

                foreach (Smena w in items)
                {
                    src.Add(new ComboItem(w.start + "-" + w.end + ":" + w.date, w.id_smena));
                }

                add_rasp.DataSource = src;
                add_rasp.DisplayMember = "display";
                add_rasp.ValueMember = "value";
            };
            new APIRequest().makeAPIRequest("getAllRasp", null, h);
        }

        private void showSmensInAddShiftForm2()
        {
            onResponse<List<Smena>> h = delegate (List<Smena> items) {
                List<ComboItem> src = new List<ComboItem>();

                foreach (Smena w in items)
                {
                    src.Add(new ComboItem(w.start + "-" + w.end + ":" + w.date, w.id_smena));
                }

                comboBox1.DataSource = src;
                comboBox1.DisplayMember = "display";
                comboBox1.ValueMember = "value";
            };
            new APIRequest().makeAPIRequest("getAllSmena", null, h);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            APIRequest req = new APIRequest();

            List<KeyValuePair<string, string>> parms = new List<KeyValuePair<string, string>>();
            parms.Add(new KeyValuePair<string, string>("id_rasp", comboBox1.SelectedValue.ToString()));
            parms.Add(new KeyValuePair<string, string>("id_smena", add_rasp.SelectedValue.ToString()));
            onResponse<int> h = delegate (int data) { };

            req.makeAPIRequest("addZay", parms, h);
        }
    }
}
