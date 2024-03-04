using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Shared.AuthenticationStateCustom;
using SqlSugar;
using System.Reflection;

namespace RMApp
{
    public static class MauiProgram
    {
       
        public static string ConnectionString2 = @"DataSource=" + Path.Combine(FileSystem.AppDataDirectory, "TodoSQLite.db3");
        public static MauiApp CreateMauiApp()
        {
            SnowFlakeSingle.WorkId = 1;
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddBootstrapBlazor();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<ISqlSugarClient>(s =>
            {
                SqlSugarScope sqlSugar = new SqlSugarScope(new ConnectionConfig()
                {
                    DbType = DbType.Sqlite,
                    // ConnectionString = "DataSource=SqlSugar4xTest.sqlite",
                    ConnectionString = ConnectionString2,
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
                            //if (p.IsPrimarykey == false && new NullabilityInfoContext()
                            // .Create(c).WriteState is NullabilityState.Nullable)
                            //{
                            //    p.IsNullable = true;
                            //}
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
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationStateProviderByClient>();
            builder.Services.AddScoped<IAuthService, AuthenticationStateProviderByClient>();
            
            return builder.Build();
        }
    }
}
