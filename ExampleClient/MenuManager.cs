﻿using System;
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
    public partial class MenuManager : Form
    {
        public MenuManager()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ScheduleManage objfrm = new ScheduleManage();
            objfrm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewOrders objfrm = new NewOrders();
            objfrm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            allWorkers objfrm = new allWorkers();
            objfrm.Show();
        }
    }
}