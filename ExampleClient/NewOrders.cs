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
    public partial class NewOrders : Form
    {
        public NewOrders()
        {
            InitializeComponent();
        }

        private void NewOrders_Load(object sender, EventArgs e)
        {
            dgv.ColumnCount = 3;
            dgv.Columns[0].Name = "Описание";
            dgv.Columns[1].Name = "Адрес";
            dgv.Columns[2].Name = "ФИО";

        }

        private void updateOrders()
        {
            onResponse<List<OrderWithWorker>> h = delegate (List<OrderWithWorker> data) {
                dgv.Rows.Clear();
                foreach (OrderWithWorker item in data)
                {
                    addRow(item);
                }
            };

            new APIRequest().makeAPIRequest("getOrders", null, h);
        }

        private void addRow(OrderWithWorker item)
        {
            dgv.Rows.Add(item.order.order, item.order.address, item.worker.name + " " + item.worker.surname + " " + item.worker.midname);
        }

        private void showWorkersInAddShiftForm()
        {
            onResponse<List<Worker>> h = delegate (List<Worker> items) {
                List<ComboItem> src = new List<ComboItem>();

                foreach (Worker w in items)
                {
                    src.Add(new ComboItem(w.name, w.id_worker));
                }

                add_worker.DataSource = src;
                add_worker.DisplayMember = "display";
                add_worker.ValueMember = "value";
            };
            new APIRequest().makeAPIRequest("getWorkers", null, h);
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            updateOrders();
        }

        private void button_edit_Click(object sender, EventArgs e)
        {

        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count != 1)
            {
                return;
            }

            List<KeyValuePair<string, string>> p = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("id_order", dgv.SelectedRows[0].Cells[0].Value.ToString())
            };

            onResponse<int> handle = delegate (int data) {
                updateOrders();
            };

            new APIRequest().makeAPIRequest("deleteOrder", p, handle);
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            APIRequest req = new APIRequest();

            List<KeyValuePair<string, string>> parms = new List<KeyValuePair<string, string>>();
            parms.Add(new KeyValuePair<string, string>("id_worker", add_worker.SelectedValue.ToString()));
            parms.Add(new KeyValuePair<string, string>("address", textBox1.Text.ToString()));
            parms.Add(new KeyValuePair<string, string>("order", textBox2.Text.ToString()));

            onResponse<int> h = delegate (int data) {
                updateOrders();
            };

            req.makeAPIRequest("addOrder", parms, h);
        }
    }
}
