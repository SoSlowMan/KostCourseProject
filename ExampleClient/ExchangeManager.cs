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

    /*
    * Эта форма для управления поданными заявками
    * В dgv будут показываться заявки от людей на обмен сменами 
    * Если принять то обмен произойдет, а заявка, само собой, удалится из таблицы
    * Если отклонить то заявка просто удалится из таблицы
    */
    public partial class ExchangeManager : Form
    {
        public ExchangeManager()
        {
            InitializeComponent();
        }
    }
}
