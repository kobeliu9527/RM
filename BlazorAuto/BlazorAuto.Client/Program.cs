using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Models;
using Models.Services.Base;
using Models.Services.ServerByHttp;
using Models.System;
using Shared.AuthenticationStateCustom;



var builder = WebAssemblyHostBuilder.CreateDefault(args);
var Configuration = builder.Configuration;
var Appsetting = Configuration.GetSection(nameof(Appsettings)).Get<Appsettings>()!;
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationStateProviderByClient>();
builder.Services.AddScoped<IAuthService, AuthenticationStateProviderByClient>();

builder.Services.AddHttpClient(
    "http",
    client =>
    {
        client.BaseAddress = new Uri("http://localhost:5049/api/");
        // Add a user-agent default request header.http://*:2222
        client.DefaultRequestHeaders.UserAgent.ParseAdd("dotnet-docs");
    });
builder.Services.AddScoped<ICrudBase<Company>, CompanyServerByHttp>();
builder.Services.AddScoped<ICrudBase<CompanyGroup>, CompanyGroupServerByHttp>();
builder.Services.AddScoped<ICrudBase<FunctionPage>, FunctionPageServerByHttp>();
builder.Services.AddBootstrapBlazor();

builder.Services.AddTableDemoDataService();

await builder.Build().RunAsync();
