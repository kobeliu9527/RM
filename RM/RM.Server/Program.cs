using RM.Shared.Data;
using SqlSugar;
using System.Diagnostics;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args).Inject(); ;

// Add services to the container.
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
builder.Services.AddCorsAccessor();

builder.Services.AddControllersWithViews().AddInject();
builder.Services.AddAuthentication();
builder.Services.AddRemoteRequest();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBootstrapBlazor();

builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddHttpClient(name: "HttpClient", c =>
{
    //https://kjkqffn5-5000.asse.devtunnels.ms/
    //http://localhost:5000
    // 192.168.125.13
    c.BaseAddress = new Uri("https://kjkqffn5-5000.asse.devtunnels.ms/");
}
);
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
// 增加 Table 数据服务操作类
builder.Services.AddTableDemoDataService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();
app.UseCorsAccessor();
app.UseInject();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapDefaultControllerRoute();
app.Run();
