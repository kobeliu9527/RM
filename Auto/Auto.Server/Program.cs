using Auto.Shared.Data;
using Auto.Shared.Models.SystemInfo;
using System.Text;

var builder = WebApplication.CreateBuilder(args).Inject();
Console.WriteLine($"Tunnel URL: {Environment.
          GetEnvironmentVariable("VS_TUNNEL_URL")}");
Console.WriteLine($"API project tunnel URL: {Environment.
    GetEnvironmentVariable("VS_TUNNEL_URL_MyWebApi")}");
// Add services to the container.
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
builder.Services.AddCustomOption();
builder.Services.AddCorsAccessor();
builder.Services.AddControllersWithViews().AddInject();

builder.Services.AddAuthentication();
builder.Services.AddRemoteRequest();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddBootstrapBlazor();

builder.Services.AddSingleton<WeatherForecastService>();

// 增加 Table 数据服务操作类
builder.Services.AddTableDemoDataService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();
app.UseCorsAccessor();
app.UseAuthorization();
app.UseAuthentication();

app.UseInject();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapDefaultControllerRoute();
app.Run();
