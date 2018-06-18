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
    public partial class allWorkers : Form
    {
        public allWorkers()
        {
            InitializeComponent();
        }

        private void addRow(Worker item)
        {
            dgv.Rows.Add(item.name, item.surname, item.midname);
        }

        private void updateWorkers()
        {
            onResponse<List<Worker>> h = delegate (List<Worker> data) {
                dgv.Rows.Clear();
                foreach (Worker item in data)
                {
                    addRow(item);
                }
            };

            new APIRequest().makeAPIRequest("getSchedule", null, h);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            newUsers objfrm = new newUsers();
            objfrm.Show();
        }
    }
}
