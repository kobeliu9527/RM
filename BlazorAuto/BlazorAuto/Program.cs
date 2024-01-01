global using SqlSugar;
global using Mapster;
using BlazorAuto.Components;
using BlazorAuto.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models;
using Models.Services.Base;
using Models.Services.ServerByDb;
using Models.System;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.AuthenticationStateCustom;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Models.Dto.SVG;
using Blazor.Diagrams;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);
SnowFlakeSingle.WorkId = 1;

var Configuration = builder.Configuration;
var Appsetting = Configuration.GetSection(nameof(Appsettings)).Get<Appsettings>()!;
builder.Services.Configure<Appsettings>(Configuration.GetSection(nameof(Appsettings)));
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationStateProviderByClient>();
builder.Services.AddScoped<IAuthService, AuthenticationStateProviderByClient>();

TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);
TypeAdapterConfig<BlazorDiagram, WorkFlow>
        .ForType()
        //.Ignore(d=>d.Options)
        ;
////.MapToConstructor(true);
//builder.Services.AddMapster();
//.Ignore(dest => dest.Age)
//.Map(dest => dest.FullName,
//    src => string.Format("{0} {1}", src.FirstName, src.LastName));
builder.Services.AddHttpClient(
    "http",
    client =>
    {
        client.BaseAddress = new Uri("http://localhost:5049/api/");
        // Add a user-agent default request header.http://*:2222
        client.DefaultRequestHeaders.UserAgent.ParseAdd("dotnet-docs");
    });
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddBootstrapBlazor();

builder.Services.AddTableDemoDataService();

builder.Services.AddControllers(
    op =>
    {
        op.Filters.Add<GlobalExceptionFilter>();
    })
.AddJsonOptions(op =>
{
    op.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs);
    op.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

    //op.JsonSerializerOptions.Converters.Add(new JsonTextConverter("yyyy-MM-dd HH:mm:ss"));

});
builder.Services.AddAuthentication()
         .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, op =>
         {
             op.TokenValidationParameters = new TokenValidationParameters()
             {
                 ValidateIssuer = false,
                 ValidateAudience = false,
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Appsetting.JwtOption.SecurityKey))
             };
         })
         ;
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v0.0.1",
        Title = "API测试接口",
        Description = $"所有接口都在这里",
        Contact = new OpenApiContact()
        {
            Name = "ufo",
            Email = "490384184@qq.com",
            Url = new Uri("http://www.baidu.com")
        }
    });
    var ss = AppContext.BaseDirectory;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.AddSecurityDefinition("JwtBearer", new OpenApiSecurityScheme()
    {
        Description = "直接输入token就好,不需要任何其他东西",
        Name = "Authorization",//jwt默认的参数名称
        In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
        Type = SecuritySchemeType.Http,

        Scheme = "bearer"
    });
    //定义JwtBearer认证方式二
    //options.AddSecurityDefinition("JwtBearer", new OpenApiSecurityScheme()
    //{
    //    Description = "这是方式二(JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）)",
    //    Name = "Authorization",//jwt默认的参数名称
    //    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
    //    Type = SecuritySchemeType.ApiKey
    //});
    //声明一个Scheme，注意下面的Id要和上面AddSecurityDefinition中的参数name一致
    var scheme = new OpenApiSecurityScheme()
    {
        Reference = new OpenApiReference() { Type = ReferenceType.SecurityScheme, Id = "JwtBearer" }
    };
    //注册全局认证（所有的接口都可以使用认证）
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        [scheme] = new string[0]
    });
});
builder.Services.AddSingleton<ISqlSugarClient>(s =>
{
    SqlSugarScope sqlSugar = new SqlSugarScope(new ConnectionConfig()
    {
        DbType = DbType.SqlServer,

        ConnectionString = Appsetting.SqlSugarOption.ConnectString,
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
           Console.WriteLine(sql);
       };
   });
    return sqlSugar;
});



builder.Services.AddScoped<ICrudBase<SysModule>, ModuleServerByDB>();
builder.Services.AddScoped<ICrudBase<CompanyGroup>, CompanyGroupServerByDB>();
builder.Services.AddScoped<ICrudBase<FunctionPage>, FunctionPageServerByDB>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}
app.UseSwagger(options =>
{
    options.RouteTemplate = "api/{documentName}/swagger.{json|yaml}";
});
app.UseSwaggerUI(op =>
{
    op.RoutePrefix = "api";
    //op.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});
//app.UseStaticFiles();
//app.UseDirectoryBrowser();
app.UseCors(op => { op.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorAuto.Client.Routes).Assembly, typeof(Shared.Designer.FormDesigner).Assembly);

app.Run();
