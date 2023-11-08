using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RM.Shared;
using RM.Shared.Data;
using System;
using System.Net.Http;
using MatBlazor;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient(name: "HttpClient", c =>
{
    //https://kjkqffn5-5000.asse.devtunnels.ms/
    //http://192.168.125.13:5000
    //builder.HostEnvironment.BaseAddress
    c.BaseAddress = new Uri("https://kjkqffn5-5000.asse.devtunnels.ms/");
}
);
// 增加 BootstrapBlazor 组件
builder.Services.AddBootstrapBlazor(op =>
{
    // 设置组件默认使用中文
    op.DefaultCultureInfo = "zh";
});
//builder.Services.AddMatToaster();
builder.Services.AddSingleton<WeatherForecastService>();

// 增加 Table 数据服务操作类
builder.Services.AddTableDemoDataService();

await builder.Build().RunAsync();
