using AutoServer.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using System;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(
    op =>
    {
        //op.Filters.Add<GlobalExceptionFilter>();
    })
.AddJsonOptions(op =>
{
    op.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs);
    op.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

});
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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
           Console.WriteLine(sql);
       };
   });
    return sqlSugar;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
