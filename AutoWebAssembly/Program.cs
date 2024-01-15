using AutoWebAssembly;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Models.CustomConverter;
using Models.Services.Base;
using Models.Services.ServerByDb;
using Models.Services.ServerByHttp;
using Models.SystemInfo;
using Shared;
using System.Text.Json;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBootstrapBlazor();
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//var ss = builder.HostEnvironment.BaseAddress;
var ss = JsonSerializerOptions.Default;
//JsonSerializerOptions.Default.Converters.Add(new JsonTextConverter("yyyy-MM-dd HH:mm:ss"));

builder.Services.AddHttpClient(
    "http",
    client =>
    {
        client.BaseAddress = new Uri("http://localhost:9527/api/");
        // Add a user-agent default request header.http://*:2222
        client.DefaultRequestHeaders.UserAgent.ParseAdd("dotnet-docs");
    });

builder.Services.AddScoped<ICrudBase<SysModule>, ServerByHttpBase<SysModule>>();

await builder.Build().RunAsync();
