using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using WithAuto.Client.Pages;
using WithAuto.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddBootstrapBlazor();

builder.Services.AddControllers(
    op =>
    {
        //op.Filters.Add<GlobalExceptionFilter>();
    })
.AddJsonOptions(op =>
{
    op.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs);
    op.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

    //op.JsonSerializerOptions.Converters.Add(new JsonTextConverter("yyyy-MM-dd HH:mm:ss"));

});
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
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly,typeof(Shared.Core.ComponentUtils).Assembly);

app.Run();
