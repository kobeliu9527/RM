using Dp.Server.Server.SignalR;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using SharedPage.JsonConvert;
using SharedPage.Model;
using System.Text.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        Test();
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();
        builder.Services.AddSignalR();
        builder.Services.AddResponseCompression(opts =>
        {
            opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                  new[] { "application/octet-stream" });
        });
        builder.Services.AddCors();
        //var hubContext = builder.Services.GetService(typeof(IHubContext<ChatHub>));
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        if (!app.Environment.IsDevelopment())
        {
            app.UseResponseCompression();
        }
        app.UseHttpsRedirection();
        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseCors(op => { op.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });


        app.MapRazorPages();
        app.MapControllers();
        app.MapHub<ChatHub>("/chathub");
        app.MapFallbackToFile("index.html");

        app.Run();
    }

    private static void Test()
    {
        EOption option = new EOption();
        option.series = new List<SharedPage.Base.ESerieBase>() { 
         new SharedPage.Base.ESerieBase()
         {

         }
        };
        JsonSerializerOptions op = new JsonSerializerOptions()
        {
            Converters = { new ListConvert() }
        };
        var s = JsonSerializer.Serialize(option);
    }
}