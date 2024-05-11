using Dp.Server.Server.SignalR;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using SharedPage.Ext;
using SharedPage.JsonConvert;
using SharedPage.Model;
using System.Data;
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

        DataTable dt = new DataTable();

        // 定义表的列
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Age", typeof(int));

        // 添加数据行
        dt.Rows.Add(1, "Alice", 30);
        dt.Rows.Add(2, "Bob", 35);
        dt.Rows.Add(3, "Charlie", 25);
        var source = dt.ToChatSource();
        JsonSerializerOptions op = new JsonSerializerOptions()
        {
            Converters = { new ListConvert() }
        };
        //var s = JsonSerializer.Serialize(dt);
    }
}