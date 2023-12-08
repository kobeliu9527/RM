using Blazored.LocalStorage;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.Services.AuthenticationStateCustom;
using Models.Services.Base;
using Models.Services.ServerByDb;
using Models.System;
using Newtonsoft.Json.Linq;
using NSwag;
using Serilog;
using Serilog.Events;
using Serilog.Filters;
using Serilog.Formatting.Compact;
using Serilog.Sinks.MSSqlServer;
using Serilog.Templates;
using SqlSugar;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Text.Unicode;
using Ufo.Auto.Client.Pages;
using Ufo.Auto.Client.Services.AuthenticationStateCustom;
using Ufo.Auto.Client.Services.Base;
using Ufo.Auto.Client.Services.CompanyGroupServer;
using Ufo.Auto.Client.Services.CompanyServer;
using Ufo.Auto.Components;
using Ufo.Auto.Controllers;
using Ufo.Auto.Filters;

#region 日志配置
//string json = "{user}";
//var mssql = new MSSqlServerSinkOptions() { AutoCreateSqlTable = true, TableName = "hhhhhh" };
//var columnOptions = new ColumnOptions();
//columnOptions.Store.Remove(StandardColumn.Properties);
Log.Logger = new LoggerConfiguration()
    //.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .MinimumLevel.Debug()//框架整体的日志等级,这里设置太高,会导致后面设置中低于这个的都没有效果
    .WriteTo.Console()//输出到控制台
                      //logger.LogInformation("{pp}:asdfas","11");如果有pp这个占位且值等于11,那么就会制行后面的writeTo()
                      // .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(Matching.WithProperty<string>("pp", p => p == "11")).WriteTo.Async(a => a.File("分类1//123456.txt")))
                      //.WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(Matching.WithProperty<string>("pp", p => p == "22")).WriteTo.Async(a => a.File("分类2//1啊手动阀.txt")))
                      //如果等级为Warning,就会写入到目录为"按等级2"中,文件名"1啊手动阀.txt"
                      // .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(x => x.Level == LogEventLevel.Warning).WriteTo.Async(a => a.File("按等级2//1啊手动阀.txt")))
                      //写入到文件asdf//log.txt 按天生成
                      // .WriteTo.File("asdf//log.txt", rollingInterval: RollingInterval.Day)
                      // .WriteTo.File("log2222277.json", outputTemplate: "{Message:lj}", restrictedToMinimumLevel: LogEventLevel.Fatal)
                      //.WriteTo.File(new ExpressionTemplate("{{MSG: @m}}"), "asdf//log.json", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Fatal)
                      //以json的格式生成
                      //.WriteTo.File(new CompactJsonFormatter(), $"{DateTime.Now.Month}{DateTime.Now.Day}//log.txt",
                      //rollingInterval: RollingInterval.Minute, //滚动周期
                      //rollOnFileSizeLimit: true,//文件是否滚动
                      //fileSizeLimitBytes: null,//限制文件大小
                      //retainedFileCountLimit: null,//限制文件个数
                      //shared: true//进程共享
                      //)
                      //.WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(Matching.WithProperty<string>("dev", p => p != "")).WriteTo.MSSqlServer("Server = .;Database = LCM;User ID = sa;Password = 1;Encrypt=true;TrustServerCertificate=true", mssql
                      // , restrictedToMinimumLevel: LogEventLevel.Fatal
                      //))

    .CreateLogger();
Log.Warning("程序启动中........");
User user = new User() {  RealName = "adasdafasf", Roles = new List<Role>() { } };
Log.Fatal("{dev}", JsonSerializer.Serialize(user));
#endregion
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(); // 日志服务

//builder.Logging.ClearProviders();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddBootstrapBlazor();
builder.Services.AddAntDesign();
builder.Services.AddControllers(
    op =>
    {
        op.Filters.Add<GlobalExceptionFilter>();
    })
.AddJsonOptions(op =>
{
    op.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs);
    op.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

});
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationStateProviderByClient>();

builder.Services.AddHttpClient(
    "http",
    client =>
    {
        client.BaseAddress = new Uri("https://localhost:5000/api/");

        // Add a user-agent default request header.http://*:2222
        client.DefaultRequestHeaders.UserAgent.ParseAdd("dotnet-docs");

    });
builder.Services.AddEndpointsApiExplorer();//

builder.Services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, op =>
{
    op.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(LoginController.key))
    };
});
builder.Services.AddAuthorization();

builder.Services.AddSingleton<ISqlSugarClient>(s =>
{
    SqlSugarScope sqlSugar = new SqlSugarScope(new ConnectionConfig()
    {
        DbType = SqlSugar.DbType.SqlServer,
        //
        ConnectionString = "Server = .;Database = Ufo;User ID = sa;Password = 1;TrustServerCertificate=true",
        // ConnectionString = "DataSource=WMS.db",
        IsAutoCloseConnection = true,
        ConfigureExternalServices = new ConfigureExternalServices
        {
            //注意:  这儿AOP设置不能少
            EntityService = (c, p) =>
            {
                /***低版本C#写法***/
                // int?  decimal?这种 isnullable=true 不支持string(下面.NET 7支持)
                //if (p.IsPrimarykey == false && c.PropertyType.IsGenericType &&
                //c.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                //{
                //    p.IsNullable = true;
                //}

                /***高版C#写法***/
                //支持string?和string  
                if (p.IsPrimarykey == false && new NullabilityInfoContext()
                 .Create(c).WriteState is NullabilityState.Nullable)
                {
                    p.IsNullable = true;
                }
            }
        }
    },
   db =>
   {
       //单例参数配置，所有上下文生效
       db.Aop.OnLogExecuting = (sql, pars) =>
       {
           Debug.WriteLine(sql);
       };
   });
    return sqlSugar;
});
builder.Services.AddOpenApiDocument((settings) =>
{
    settings.DocumentName = "名字:接口文档";
    settings.Version = "版本:v0.0.1";
    settings.Title = "标题:测试接口项目";
    settings.Description = "描述:接口文档说明";
    settings.UseControllerSummaryAsTagDescription = true;
    //定义JwtBearer认证方式一
    settings.AddSecurity("JwtBearer", Enumerable.Empty<string>(), new OpenApiSecurityScheme()
    {
        Description = "这是方式一(直接在输入框中输入认证信息，不需要在开头添加Bearer)",
        Name = "Authorization",//jwt默认的参数名称
        In = OpenApiSecurityApiKeyLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
        Type = OpenApiSecuritySchemeType.Http,
        Scheme = "bearer"
    });
});

builder.Services.AddScoped<ICrudBase<Company>, CompanyServerByDB>();
builder.Services.AddScoped<ICrudBase<CompanyGroup>, CompanyGroupServerByDB>();
builder.Services.AddScoped<ICrudBase<FunctionPage>, FunctionPageServerByDB>();
//builder.Services.AddScoped<ICompanyManger, CompanyManger>();

var app = builder.Build();

app.UseSerilogRequestLogging(); // <-- Add this line

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();

app.UseOpenApi(settings =>
{

}); ; // serve documents (same as app.UseSwagger())

app.UseSwaggerUi(op =>
{
    op.Path = "/api";
});

//app.UseReDoc(); // serve ReDoc UI

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.MapDefaultControllerRoute();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

app.Run();
