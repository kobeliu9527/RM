using AutoWebAssembly;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
//builder.RootComponents.Add<App>();
builder.Services.AddBootstrapBlazor();
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
var ss = builder.HostEnvironment.BaseAddress;

builder.Services.AddHttpClient(
    "http",
    client =>
    {
        client.BaseAddress = new Uri("http://localhost:9527/api/");
        // Add a user-agent default request header.http://*:2222
        client.DefaultRequestHeaders.UserAgent.ParseAdd("dotnet-docs");

    });
await builder.Build().RunAsync();
