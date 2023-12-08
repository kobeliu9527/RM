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

#region ��־����
//string json = "{user}";
//var mssql = new MSSqlServerSinkOptions() { AutoCreateSqlTable = true, TableName = "hhhhhh" };
//var columnOptions = new ColumnOptions();
//columnOptions.Store.Remove(StandardColumn.Properties);
Log.Logger = new LoggerConfiguration()
    //.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .MinimumLevel.Debug()//����������־�ȼ�,��������̫��,�ᵼ�º��������е�������Ķ�û��Ч��
    .WriteTo.Console()//���������̨
                      //logger.LogInformation("{pp}:asdfas","11");�����pp���ռλ��ֵ����11,��ô�ͻ����к����writeTo()
                      // .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(Matching.WithProperty<string>("pp", p => p == "11")).WriteTo.Async(a => a.File("����1//123456.txt")))
                      //.WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(Matching.WithProperty<string>("pp", p => p == "22")).WriteTo.Async(a => a.File("����2//1���ֶ���.txt")))
                      //����ȼ�ΪWarning,�ͻ�д�뵽Ŀ¼Ϊ"���ȼ�2"��,�ļ���"1���ֶ���.txt"
                      // .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(x => x.Level == LogEventLevel.Warning).WriteTo.Async(a => a.File("���ȼ�2//1���ֶ���.txt")))
                      //д�뵽�ļ�asdf//log.txt ��������
                      // .WriteTo.File("asdf//log.txt", rollingInterval: RollingInterval.Day)
                      // .WriteTo.File("log2222277.json", outputTemplate: "{Message:lj}", restrictedToMinimumLevel: LogEventLevel.Fatal)
                      //.WriteTo.File(new ExpressionTemplate("{{MSG: @m}}"), "asdf//log.json", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Fatal)
                      //��json�ĸ�ʽ����
                      //.WriteTo.File(new CompactJsonFormatter(), $"{DateTime.Now.Month}{DateTime.Now.Day}//log.txt",
                      //rollingInterval: RollingInterval.Minute, //��������
                      //rollOnFileSizeLimit: true,//�ļ��Ƿ����
                      //fileSizeLimitBytes: null,//�����ļ���С
                      //retainedFileCountLimit: null,//�����ļ�����
                      //shared: true//���̹���
                      //)
                      //.WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(Matching.WithProperty<string>("dev", p => p != "")).WriteTo.MSSqlServer("Server = .;Database = LCM;User ID = sa;Password = 1;Encrypt=true;TrustServerCertificate=true", mssql
                      // , restrictedToMinimumLevel: LogEventLevel.Fatal
                      //))

    .CreateLogger();
Log.Warning("����������........");
User user = new User() {  RealName = "adasdafasf", Roles = new List<Role>() { } };
Log.Fatal("{dev}", JsonSerializer.Serialize(user));
#endregion
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(); // ��־����

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
            //ע��:  ���AOP���ò�����
            EntityService = (c, p) =>
            {
                /***�Ͱ汾C#д��***/
                // int?  decimal?���� isnullable=true ��֧��string(����.NET 7֧��)
                //if (p.IsPrimarykey == false && c.PropertyType.IsGenericType &&
                //c.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                //{
                //    p.IsNullable = true;
                //}

                /***�߰�C#д��***/
                //֧��string?��string  
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
       //�����������ã�������������Ч
       db.Aop.OnLogExecuting = (sql, pars) =>
       {
           Debug.WriteLine(sql);
       };
   });
    return sqlSugar;
});
builder.Services.AddOpenApiDocument((settings) =>
{
    settings.DocumentName = "����:�ӿ��ĵ�";
    settings.Version = "�汾:v0.0.1";
    settings.Title = "����:���Խӿ���Ŀ";
    settings.Description = "����:�ӿ��ĵ�˵��";
    settings.UseControllerSummaryAsTagDescription = true;
    //����JwtBearer��֤��ʽһ
    settings.AddSecurity("JwtBearer", Enumerable.Empty<string>(), new OpenApiSecurityScheme()
    {
        Description = "���Ƿ�ʽһ(ֱ�����������������֤��Ϣ������Ҫ�ڿ�ͷ���Bearer)",
        Name = "Authorization",//jwtĬ�ϵĲ�������
        In = OpenApiSecurityApiKeyLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
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
