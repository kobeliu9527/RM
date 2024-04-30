using FTP;
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
    public partial class Ftp : Form
    {
        public Ftp()
        {
            InitializeComponent();
        }
        FtpDev ftpDev = new FtpDev();
        private void button1_Click(object sender, EventArgs e)
        {
            //[FTP:192.168.28.158,USER:OrBit ,PASSWORD:Me*Septcl!39681,FOLDER:FtpSite,N]
            ftpDev.FtpClient = new FluentFTP.FtpClient();
            ftpDev.FtpClient.Host = this.textBox1.Text;
            ftpDev.FtpClient.Port = Convert.ToInt32(this.numericUpDown1.Value);
            ftpDev.FtpClient.Credentials = new System.Net.NetworkCredential() { 
            UserName = this.textBox3.Text,
            Password = this.textBox4.Text
            };
            var ss = ftpDev.Connect();
            //{ 
            //Ip=this.textBox1.Text,
            //Port=Convert.ToInt32(this.numericUpDown1.Value),
            //UserName=this.textBox3.Text,
            //PassWord=this.textBox4.Text,
            //};

        }

        public int sfd( int sdf)
        {
            return 1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var ss= ftpDev.Get();
            int[] a = { 1,2,3,4};
         var sssss=   a.Select(sfd);



            this.treeView1.Nodes.Clear();
            foreach (var item in ss) 
            { 
                TreeNode treeNode = new TreeNode();
                treeNode.Text = item.Name;
                
            }
        }
    }
}
