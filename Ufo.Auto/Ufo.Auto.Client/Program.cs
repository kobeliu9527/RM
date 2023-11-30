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
//var ss=builder.Build();

await builder.Build().RunAsync();
