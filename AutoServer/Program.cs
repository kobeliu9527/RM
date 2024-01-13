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
                   Console.WriteLine(sql);
               };
           });
            return sqlSugar;
        });

        builder.Services.AddScoped<ICrudBase<SysModule>, ModuleServerByDB>();
        builder.Services.AddScoped<ICrudBase<CompanyGroup>, CompanyGroupServerByDB>();
        builder.Services.AddScoped<ICrudBase<FunctionPage>, FunctionPageServerByDB>();
        var app = builder.Build();

        //卸载这后面吗?

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
        //    // 指定文件的访问路径，允许与 FileProvider 中的文件夹不同名
        //    // 如果不指定，则可通过 http://localhost:5000/file.json 获取，
        //    // 如果指定，则需要通过 http://localhost:5000/files/file.json 获取
        //    RequestPath = "/files",
        //    OnPrepareResponse = ctx =>
        //    {
        //        // 配置前端缓存 600s（为了后续示例的良好运行，建议先不要配置该Header）
        //        //ctx.Context.Response.Headers.Add(HeaderNames.CacheControl, "public,max-age=600");
        //    }
        //});
        //app.UseDirectoryBrowser(new DirectoryBrowserOptions
        //{
        //    // 如果指定了没有在 UseStaticFiles 中提供的文件目录，虽然可以浏览文件列表，但是无法访问文件内容
        //    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "files")),
        //    // 这里一定要和 StaticFileOptions 中的 RequestPath 一致，否则会无法访问文件
        //    RequestPath = "/files"
        //});

        app.MapControllers();
        //app.UseAuthentication();
        //app.UseAuthorization();
        app.Run();
    }
}