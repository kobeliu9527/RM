using FluentFTP;
using System;
using System.Text;
using UfoBase;

namespace FTP
{
    public class FtpDev
    {
        public FtpClient FtpClient { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public Encoding Encoding { get; set; } = Encoding.UTF8;
        /// <summary>
        /// 同步文件
        /// </summary>
        /// <param name="path"></param>
        public void SynchronizeFiles(string path)
        {
            FtpClient = new FtpClient()
            {
                Host = Ip,
                Port = Port,
                Credentials = new System.Net.NetworkCredential() { UserName = UserName, Password = PassWord }
            };
        }
        public Res Connect()
        {
            Res res = new Res();
            try
            {
                FtpClient?.Connect();

                return res.Result(FtpClient.IsConnected);
            }
            catch (Exception ex)
            {
                return res.HasException(ex);
            }
        }
        public FtpListItem[] Get()
        {
          return  FtpClient.GetListing("/",FtpListOption.Recursive);
        }
        public Res DisConnect()
        {
            Res res = new Res();
            FtpClient?.Disconnect();
            FtpClient?.Dispose();
            return res;
        }
    }
}
