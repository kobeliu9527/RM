using DLT645;
using System.Reflection.PortableExecutable;
using UfoBase;

internal class Program
{
    private static void Main(string[] args)
    {
        byte[] a = new byte[6];
        
        a[0] = 0;
        a[1] = 0;
        a[2] = 0;
        a[3] = 0;
        a[4] = 0x15;
        a[5] = 0x15;
        var res = a.BcdByteArrayToDecimal();
        Console.WriteLine(a.ToHexString());;

        DLT645Client c = new DLT645Client();
        var val = "";
        if (val.ToLower()=="true")
        {
            val = "1";
        }
        if (val.ToUpper() == "false")
        {
            val = "0";
        }
        if (false)
        {

        }
        c.Start();
        Console.ReadLine();
    }
    private static async Task Main2(string[] args)
    {
        CancellationTokenSource source = new CancellationTokenSource();
        CancellationToken token = source.Token;
        Console.WriteLine("主:" + Thread.CurrentThread.ManagedThreadId);
        var aa = Task.Factory.StartNew(async () =>
              {
                  while (!token.IsCancellationRequested)
                  {
                      Console.WriteLine("新2(循环await前):" + Thread.CurrentThread.ManagedThreadId);
                      // await Task.Delay(100);
                      Thread.Sleep(100);
                      Console.WriteLine("新3(循环await后):" + Thread.CurrentThread.ManagedThreadId);
                  }
                  Console.WriteLine("新4(循环完成):" + Thread.CurrentThread.ManagedThreadId);
                  await Task.Delay(1000);
                  Console.WriteLine("新5(循环完成,await之后):" + Thread.CurrentThread.ManagedThreadId);
              }, TaskCreationOptions.LongRunning);
        Console.WriteLine("新6(Task完成):" + Thread.CurrentThread.ManagedThreadId);
        await Task.Delay(1000);
        source.Cancel();
        Console.WriteLine("新7(Task完成,await之后,执行停止循环):" + Thread.CurrentThread.ManagedThreadId);
        Console.ReadLine();
    }



    public static async Task Test()
    {


    }
}