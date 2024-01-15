using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Models;
using Models.SystemInfo;
using SqlSugar;
using System.Diagnostics;
using System.Reflection;
using Ufo.Auto.Client.Services.AuthenticationStateCustom;
using Ufo.Auto.Client.Services.Base;
using Ufo.Auto.Client.Services.CompanyGroupServer;
using Ufo.Auto.Client.Services.CompanyServer;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddBootstrapBlazor();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddHttpClient(
    "http",
    client =>
    {
        //client.BaseAddress = new Uri("https://localhost:5000/api/");
        client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);

        // Add a user-agent default request header.http://*:2222
        client.DefaultRequestHeaders.UserAgent.ParseAdd("dotnet-docs");

    });
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationStateProviderByClient>();
builder.Services.AddSingleton<ISqlSugarClient>(s =>
{
    SqlSugarScope sqlSugar = new SqlSugarScope(new ConnectionConfig()
    {
        DbType = SqlSugar.DbType.SqlServer,
        //
        ConnectionString = "Server = .;Database = Ufo;User ID = sa;Password = 1;",
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

builder.Services.AddScoped<ICrudBase<Company>, CompanyServerByHttp>();
builder.Services.AddScoped<ICrudBase<CompanyGroup>, CompanyGroupServerByHttp>();
builder.Services.AddScoped<ICrudBase<FunctionPage>, FunctionPageServerByHttp>();

//builder.Services.AddScoped<ICompanyManger, CompanyManger>();
//var ss=builder.Build();

await builder.Build().RunAsync();
