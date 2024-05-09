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

internal class Program
{
    public static string aaa="1111111111111111";
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
        builder.Services.AddHttpClient();
        //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddBootstrapBlazor();
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationStateProviderForWasm>();
        builder.Services.AddSingleton<JsInterOp, JsInterOp>();


        await builder.Build().RunAsync();
    }

    private static void Test()
    {

      
     
    //EOption option = new EOption();
    //    JsonSerializerOptions op = new JsonSerializerOptions() {
    //        Converters = { new ListConvert () }
    //    };
    //    var s=JsonSerializer.Serialize(option,op);
    }
}