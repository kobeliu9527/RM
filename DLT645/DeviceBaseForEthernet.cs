using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TouchSocket.Core;
using TouchSocket.Sockets;
using UfoBase;
namespace DLT645
{
    public abstract class DeviceBaseForEthernet
    {
        /// <summary>
        /// 连接线程控制器
        /// </summary>
        public CancellationTokenSource? CancellSourceForConnect { get; set; }
        /// <summary>
        /// 读取线程控制器
        /// </summary>
        public CancellationTokenSource? CancellSourceForRead { get; set; }
        /// <summary>
        /// 服务器地址 192.168.1.2:5555
        /// </summary>
        public virtual string ServerAddress { get; set; } = "localhost:9527";
        /// <summary>
        /// 不仅仅是建立通讯连接,应该是连接+额外握手完成
        /// </summary>
        public bool Connected { get; set; }
        /// <summary>
        /// 是否在自动读取中
        /// </summary>
        public bool IsStarting { get; set; }
        /// <summary>
        /// 是否在自动连接中
        /// </summary>
        public bool IsConnecting { get; set; }
        /// <summary>
        /// 发生异常
        /// </summary>
        public Action<DeviceBaseForEthernet, Exception>? ExceptionHander { get; set; }
        /// <summary>
        /// 日志输出
        /// </summary>
        public Action<DeviceBaseForEthernet, string>? Loger { get; set; }
        /// <summary>
        /// 握手连接
        /// </summary>
        public Func<TcpClient, IWaitingClient<TcpClient>, bool>? OnTcpConnected { get; set; }
        /// <summary>
        /// 自动重连间隔多少毫秒
        /// </summary>
        public int ReConnectInterval { get; set; } = 1000;
        /// <summary>
        /// Socket对象
        /// </summary>
        public TcpClient? Client { get; set; }
        /// <summary>
        /// 设备名字+变量名为key  变量为value
        /// </summary>
        public ConcurrentDictionary<string, Variable> Variables { get; set; } = new();
        /// <summary>
        /// 同步socket对象
        /// </summary>
        public IWaitingClient<TcpClient>? ClientWaiter { get; set; }
        /// <summary>
        /// 创建同步socket对象时候,需要的option
        /// </summary>
        public virtual WaitingOptions WaitingOptions { get; set; } = new WaitingOptions()
        {
            FilterFunc = response =>
            {
                return true;
            }
        };
        public abstract TouchSocketConfig Config { get; set; }
        public byte[] AddresToSendBytes(string address)
        {
            throw new NotImplementedException();
        }

        public abstract void BatchRead();
        /// <summary>
        /// new TcpClient之后必须调用这个方法
        /// </summary>
        public abstract void Init();
        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public virtual Res Connect()
        {
            Res res = new();
            try
            {
                Client.SafeDispose();
                ClientWaiter.SafeDispose();
                Client = new TcpClient();
                Init();
                Client.Connect();
                ClientWaiter = Client.CreateWaitingClient(WaitingOptions);
            }
            catch (Exception ex)
            {
                return res.HasException(ex);
            }
            return res.Ok();
        }
        /// <summary>
        /// 一直连接,直到连接ok;会先尝试断开当前连接,在重新new连接对象
        /// <para>1.先断开</para>
        /// </summary>
        /// <returns></returns>
        public virtual void ConnectUntilOk()
        {
            IsConnecting = true;
            Connected = false;
            Client.SafeDispose();
            ClientWaiter.SafeDispose();
            Client = new TcpClient();
            CancellSourceForRead = new CancellationTokenSource();
            CancellSourceForConnect = new CancellationTokenSource();
            Init();
            while (!Connected && !CancellSourceForConnect.Token.IsCancellationRequested)
            {
                var result = Client.TryConnect(4000);//调用连接，当连接不成功时，会抛出异常。
                if (result.IsSuccess())
                {
                    ClientWaiter = Client.CreateWaitingClient(WaitingOptions);
                    if (OnTcpConnected == null)
                    {
                        Connected = true;
                    }
                    else
                    {
                        if (OnTcpConnected.Invoke(Client, ClientWaiter))
                        {
                            Connected = true;
                        }
                        else
                        {
                            Client.SafeClose();
                            Thread.Sleep(ReConnectInterval);
                        }
                    }
                }
                else
                {
                    Thread.Sleep(ReConnectInterval);
                }
            }
            IsConnecting = false;
        }
        /// <summary>
        /// 断开连接,只是释放socket,不过线程没停,那么还会自动连接
        /// </summary>
        public void DisConnect()
        {
            try
            {
                CancellSourceForRead?.Cancel();
                CancellSourceForConnect?.Cancel();
                Client.SafeDispose();
                ClientWaiter.SafeDispose();
                Connected = false;
            }
            catch (Exception ex)
            {
                ExceptionHander?.Invoke(this, ex);
                return;
            }
            Loger?.Invoke(this, "断开连接成功");
        }


        /// <summary>
        /// 读指定数据类型
        /// </summary>
        /// <param name="address"></param>
        public abstract UResult<T> Read<T>(string address);
        //{
        //    var res = await waitClient.SendThenResponseAsync(Encoding.UTF8.GetBytes("RRQM"));
        //    tcpClient.sen
        //    var res2 = res.RequestInfo as DLT645RequestInfo;
        //    var ss = res2.Body;
        //}
        /// <summary>
        /// 读字节
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public abstract Task<byte[]> ReadBytes(string address);
        

        /// <summary>
        /// 开线程不停的读取
        /// </summary>
        public virtual void Start()
        {
            CancellSourceForRead = new CancellationTokenSource();
            Task.Factory.StartNew(() =>
                {
                    IsConnecting = true;
                    while (!CancellSourceForRead.Token.IsCancellationRequested)
                    {
                        if (Connected)
                        {
                            //不停的读取
                        }
                        else
                        {
                            ConnectUntilOk();
                        }
                        ;
                    }
                    IsConnecting = false;
                }, TaskCreationOptions.LongRunning);
        }
        /// <summary>
        /// 停止读取线程
        /// </summary>
        public void StopAsync()
        {
            CancellSourceForRead?.Cancel();
        }
    }
}