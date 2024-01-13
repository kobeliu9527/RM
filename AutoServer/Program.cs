global using  Models.Dto;
global using Mapster;
using AutoServer;
using AutoServer.Controllers;
using AutoServer.CustomConverter;
using AutoServer.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Models.NotEntity;
using System;
using System.Configuration;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Models.Services.Base;
using Models.System;
using Models.Services.ServerByDb;
using Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var Configuration = builder.Configuration;
        var Appsetting = Configuration.GetSection(nameof(Appsettings)).Get<Appsettings>()!;
        //Configuration.GetSection(nameof(Appsettings)).Bind(Appsetting);
        builder.Services.Configure<Appsettings>(Configuration.GetSection(nameof(Appsettings)));
        if (Appsetting.IsDebug)
        {
            IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
            var ip2 = ipEntry.AddressList.FirstOrDefault(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            if (ip2 != null)
            {
                builder.WebHost.UseUrls(new[] { $"http://*:9527" });
            }
        }
        builder.Services.AddDirectoryBrowser();
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

        builder.Services.AddAuthorization(options =>
        {
            //options.FallbackPolicy = new AuthorizationPolicyBuilder()
            //    .RequireAuthenticatedUser()
            //    .Build();
        });
        builder.Services.AddCors();
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
                DbType = DbType.SqlServer,

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

        builder.Services.AddScoped<ICrudBase<SysModule>, ModuleServerByDB>();
        builder.Services.AddScoped<ICrudBase<CompanyGroup>, CompanyGroupServerByDB>();
        builder.Services.AddScoped<ICrudBase<FunctionPage>, FunctionPageServerByDB>();
        var app = builder.Build();

        //ж���������?

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
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
        //app.UseStaticFiles();
        //app.UseDirectoryBrowser();
        app.UseCors(op => { op.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
        app.UseAuthentication();
        app.UseAuthorization();
        //app.UseStaticFiles(new StaticFileOptions
        //{
        //    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "files")),
        //    // ָ���ļ��ķ���·���������� FileProvider �е��ļ��в�ͬ��
        //    // �����ָ�������ͨ�� http://localhost:5000/file.json ��ȡ��
        //    // ���ָ��������Ҫͨ�� http://localhost:5000/files/file.json ��ȡ
        //    RequestPath = "/files",
        //    OnPrepareResponse = ctx =>
        //    {
        //        // ����ǰ�˻��� 600s��Ϊ�˺���ʾ�����������У������Ȳ�Ҫ���ø�Header��
        //        //ctx.Context.Response.Headers.Add(HeaderNames.CacheControl, "public,max-age=600");
        //    }
        //});
        //app.UseDirectoryBrowser(new DirectoryBrowserOptions
        //{
        //    // ���ָ����û���� UseStaticFiles ���ṩ���ļ�Ŀ¼����Ȼ��������ļ��б������޷������ļ�����
        //    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "files")),
        //    // ����һ��Ҫ�� StaticFileOptions �е� RequestPath һ�£�������޷������ļ�
        //    RequestPath = "/files"
        //});

        app.MapControllers();
        //app.UseAuthentication();
        //app.UseAuthorization();
        app.Run();
    }
}