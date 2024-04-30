using DLT645;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLT电能表通讯
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DLT645Client dLT645Client = new DLT645Client();
        private void 连接(object sender, EventArgs e)
        {
            var res = dLT645Client.Connect();
            if (res.IsSucceeded)
            {
                MessageBox.Show("连接成功");
            }
            else
            {
                MessageBox.Show("连接失败");
                
            }
        }

        private void 断开连接(object sender, EventArgs e)
        {
            dLT645Client?.DisConnect();
        }
    }
}
