using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TouchSocket.Core;
using TouchSocket.Sockets;
using UfoBase;

namespace DLT645
{
    public class DLT645
    {
        public TcpClient client { get; set; }
        public ReceivedEventHandler<TcpClient> aa { get; private set; }

        public void Read(string address)
        {
            client.Send(address);
            client.Received += aa;
        }
        public DLT645()
        {

        }
    }
    public class OptionDLT645
    {
        /// <summary>
        /// 前导字符个数,国标规定默认4个,实际厂商为准
        /// </summary>
        public byte HeaderLength { get; set; } = 4;
        /// <summary>
        /// 前导字符,国标规定为 0xFE,实际厂商为准
        /// </summary>
        public byte Header { get; set; } = 0xFE;
        /// <summary>
        /// 通常在连接成功后,先发送获取设备地址的指令,然后赋值到这里,报错的是处理后的,就是我们实际看到的
        /// </summary>
        public byte[] DevAddress { get; set; } = new byte[6];
        /// <summary>
        /// 
        /// </summary>
        public virtual string ServerAddress { get; set; } = "localhost:9527";
    }
    /// <summary>
    /// 用于于电表通许的实例
    /// </summary>
    public class DLT645Client : DeviceBaseForEthernet
    {
        /// <summary>
        /// 自定义配置
        /// </summary>
        public OptionDLT645 Option { get; set; }=new OptionDLT645();
        public DLT645Client()
        {

        }

        public override void BatchRead()
        {

        }

        public override WaitingOptions WaitingOptions { get; set; } = new WaitingOptions()
        {
            FilterFunc = x => { return true; }
        };
        public override TouchSocketConfig Config { get; set; } = new TouchSocketConfig()
        {

        };

        public override void Init()
        {
            Client?.Setup(new TouchSocketConfig()
                .SetRemoteIPHost(ServerAddress)
                .SetTcpDataHandlingAdapter(() => new DLT645Adapter(Option.HeaderLength,Option.Header))
                );
            Client.Received += aa;
        }

        private async Task aa(TcpClient client, ReceivedDataEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override UResult<T> Read<T>(string address)
        {
            //ClientWaiter.send
            throw new NotImplementedException();
        }

        public override Task<byte[]> ReadBytes(string address)
        {
            throw new NotImplementedException();
        }
    }


}
