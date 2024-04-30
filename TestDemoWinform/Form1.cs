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

namespace TestDemoWinform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private  void button1_Click(object sender, EventArgs e)
        {
            DLT645Client dLT645Client = new DLT645Client();
            dLT645Client.ServerAddress = "192.3.23.3:5555";
            dLT645Client.Start();
        }
    }
}
