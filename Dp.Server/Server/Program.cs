using Dp.Server.Server.Hubs;
using EntityStore;
using EntityStore.JsonConvert;
using EntityStore.SystemTable;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SharedPage.Ext;
using SharedPage.JsonConvert;
using SharedPage.Model;
using SqlSugar;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using static System.Net.Mime.MediaTypeNames;

internal partial class Program
{
    private static void Main(string[] args)
    {
        Testt();
        var builder = WebApplication.CreateBuilder(args);
        SnowFlakeSingle.WorkId = 1;



        builder.Services.AddControllersWithViews().AddJsonOptions(op =>
        {
            op.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs);
            op.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            op.JsonSerializerOptions.Converters.Add(new EntityStore.DataTableJsonConverter());
            op.JsonSerializerOptions.WriteIndented=false;
            op.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

        });
        builder.Services.AddRazorPages();
        builder.Services.AddSignalR();
        builder.Services.AddResponseCompression(opts =>
        {
            opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                  new[] { "application/octet-stream" });
        });
        builder.Services.AddCors();
        builder.Services.AddSingleton<ISqlSugarClient>(s =>
        {
            SqlSugarScope sqlSugar = new SqlSugarScope(new ConnectionConfig()
            {
                DbType = SqlSugar.DbType.Sqlite,
                //DbType = DbType.SqlServer,
                //ConnectionString = Appsetting.SqlSugarOption.ConnectString,
                ConnectionString = "DataSource=Dp.db",
                //ConnectionString = "Server = .;Database = dp;User ID = sa;Password = 1;TrustServerCertificate=true",
                IsAutoCloseConnection = true,
                MoreSettings = new ConnMoreSettings()
                {
                    SqlServerCodeFirstNvarchar = true,
                },
                ConfigureExternalServices = new ConfigureExternalServices
                {
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




        //var hubContext = builder.Services.GetService(typeof(IHubContext<ChatHub>));
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        //app.UseWebAssemblyDebugging();
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        //if (!app.Environment.IsDevelopment())
        //{
        //    app.UseResponseCompression();
        //}
        app.UseSwagger(options =>
        {
            options.RouteTemplate = "api/{documentName}/swagger.{json|yaml}";
        });
        app.UseSwaggerUI(op =>
        {
            op.RoutePrefix = "api";
            //op.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        });
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
    public class Test
    {
        [JsonConverter(typeof(objectConverter))]
        public object? Source { get; set; }
    }
    public class TestConverter : JsonConverter<Test>
    {
        public override Test Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return new Test(); // Return default Test object if null

            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var test = new Test();
                test.Source = doc.RootElement.Clone();
                return test;
            }
        }

        public override void Write(Utf8JsonWriter writer, Test value, JsonSerializerOptions options)
        {
            if (value.Source == null)
            {
                writer.WriteNullValue();
                return;
            }

            JsonSerializer.Serialize(writer, value.Source, options);
        }
    }
    class UnquotedStringConverter : JsonConverter<string>
    {
        private HashSet<string> CorrectlyQuotedFields = new HashSet<string> { "city" };

        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                return reader.GetString();
            }
            else if (reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString();
                reader.Read(); // Move to the property value
                if (CorrectlyQuotedFields.Contains(propertyName))
                {
                    return reader.GetString();
                }
                else
                {
                    // ���ڲ���ȷ��ʽ���ֶΣ��������
                    return $"\"{reader.GetString()}\"";
                }
            }
            else
            {
                throw new JsonException("Unexpected token type.");
            }
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }


    private static void Testt()
    {
       
    }
}