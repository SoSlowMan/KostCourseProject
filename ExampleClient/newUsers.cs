using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;

namespace ExampleClient
{
    public partial class newUsers : Form
    {
        public newUsers()
        {
            InitializeComponent();
        }

        static string hash(string text)
        {
            return BitConverter.ToString(new SHA256Managed().ComputeHash(Encoding.Default.GetBytes(text))).Replace("-", "").ToLower();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            APIRequest req = new APIRequest();

            List<KeyValuePair<string, string>> parms = new List<KeyValuePair<string, string>>();
            parms.Add(new KeyValuePair<string, string>("login", textBox1.Text.ToString()));
            parms.Add(new KeyValuePair<string, string>("password", hash(textBox2.Text.ToString())));
            parms.Add(new KeyValuePair<string, string>("name", textBox3.Text.ToString()));
            parms.Add(new KeyValuePair<string, string>("surname", textBox4.Text.ToString()));
            parms.Add(new KeyValuePair<string, string>("midname", textBox5.Text.ToString()));
            parms.Add(new KeyValuePair<string, string>("status", textBox6.Text.ToString()));

            onResponse<int> h = delegate (int data) {
            };

            req.makeAPIRequest("addUser", parms, h);
        }
    }
}
