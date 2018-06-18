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
    public partial class RaspW : Form
    {
        public RaspW()
        {
            InitializeComponent();
        }

        private void RaspW_Load(object sender, EventArgs e)
        {
            dgv.ColumnCount = 6;
            dgv.Columns[0].Name = "ID";
            dgv.Columns[1].Name = "Имя";
            dgv.Columns[2].Name = "Фамилия";
            dgv.Columns[3].Name = "Отчество";
            dgv.Columns[4].Name = "Время";
            dgv.Columns[5].Name = "Дата";

            updateSchedule();
        }

        private void updateSchedule()
        {
            onResponse<List<WorkShift>> h = delegate (List<WorkShift> data) {
                dgv.Rows.Clear();
                foreach (WorkShift item in data)
                {
                    addRow(item);
                }
            };

            new APIRequest().makeAPIRequest("getSchedule", null, h);
        }

        private void MySchedule()
        {
            onResponse<List<WorkShift>> h = delegate (List<WorkShift> data) {
                dgv.Rows.Clear();
                foreach (WorkShift myitem in data)
                {
                    addRow(myitem);
                }
            };

            new APIRequest().makeAPIRequest("getMySchedule", null, h);
        }

        private void addRow(WorkShift item)
        {
            dgv.Rows.Add(item.smena.id_smena, item.worker.name, item.worker.surname, item.worker.midname, item.smena.start + "-" + item.smena.end, item.smena.date);
        }


        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            updateSchedule();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySchedule();
        }
    }
}
