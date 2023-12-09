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
        Title = "API���Խӿ�",
        Description = $"���нӿڶ�������",
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
        Description = "ֱ������token�ͺ�,����Ҫ�κ���������",
        Name = "Authorization",//jwtĬ�ϵĲ�������
        In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
        Type = SecuritySchemeType.Http,

        Scheme = "bearer"
    });
    //����JwtBearer��֤��ʽ��
    //options.AddSecurityDefinition("JwtBearer", new OpenApiSecurityScheme()
    //{
    //    Description = "���Ƿ�ʽ��(JWT��Ȩ(���ݽ�������ͷ�н��д���) ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�)",
    //    Name = "Authorization",//jwtĬ�ϵĲ�������
    //    In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
    //    Type = SecuritySchemeType.ApiKey
    //});
    //����һ��Scheme��ע�������IdҪ������AddSecurityDefinition�еĲ���nameһ��
    var scheme = new OpenApiSecurityScheme()
    {
        Reference = new OpenApiReference() { Type = ReferenceType.SecurityScheme, Id = "JwtBearer" }
    };
    //ע��ȫ����֤�����еĽӿڶ�����ʹ����֤��
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
