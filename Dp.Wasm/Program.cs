using Dp.Wasm;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SharedPage;
using SharedPage.Model;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Timers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Components.Authorization;
using SharedPage.JsonConvert;
using Dp.Wasm.IServices;
using BootstrapBlazor.Components;

internal class Program
{
    public static string aaa = "1111111111111111";
    private static async Task Main(string[] args)
    {
        Test();
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        WebAssemblyHostConfiguration Configuration = builder.Configuration!;
        aaa = Configuration.GetSection("wasma")!.Value!;
        //JsonSerializerOptions.Default = new JsonSerializerOptions() { };
        //JsonSerializerOptions op= new JsonSerializerOptions() {
        //    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs)
        //};
        //var Appsetting = Configuration.GetSection(nameof(Appsettings)).Get<Appsettings>()!;
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");
        var ss = builder.HostEnvironment.BaseAddress;
        
        builder.Services.AddHttpClient("http", op =>
        {
            op.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
        });
        //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddBootstrapBlazor(null, op =>
        {
            op.IgnoreLocalizerMissing = true;
        });
        builder.Services.Configure<BootstrapBlazorOptions>(options =>
        {
            options.ToastDelay = 4000;
            options.ToastPlacement = Placement.TopCenter;
        });
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationStateProviderForWasm>();
        builder.Services.AddSingleton<JsInterOp, JsInterOp>();
        builder.Services.AddSingleton<IBigScreenService, BigScreenByLocal>(); 
       // builder.Services.AddSingleton<IBigScreenService, BigScreenByHttp>(); 

        await builder.Build().RunAsync();
    }

    private static void Test()
    {
        int? a = 33;
        var ss=default(int?);
        var sss = ss;
    }
}