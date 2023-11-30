using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SqlSugar;
using System.Diagnostics;
using System.Reflection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddBootstrapBlazor();
builder.Services.AddAntDesign();
builder.Services.AddSingleton<ISqlSugarClient>(s =>
{
    SqlSugarScope sqlSugar = new SqlSugarScope(new ConnectionConfig()
    {
        DbType = SqlSugar.DbType.SqlServer,
        //
        ConnectionString = "Server = .;Database = LCM;User ID = sa;Password = 1;",
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
//var ss=builder.Build();

await builder.Build().RunAsync();
