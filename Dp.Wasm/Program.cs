using Dp.Wasm;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SharedPage;
using SharedPage.Model;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Timers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
//JsonSerializerOptions.Default = new JsonSerializerOptions() { };
System.Text.Json.JsonSerializerOptions op= new JsonSerializerOptions() {
    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs)
};
ApiData apiData = new ApiData();
apiData.Data.Add("≤‚ ‘1", new List<object[]>() { new object[] { "1", "2" }, new object[] { "3", "4" } });
apiData.Data.Add("≤‚ ‘2", new List<object[]>() { new object[] { "11", "21" }, new object[] { "13", "14" } });
string json = System.Text.Json.JsonSerializer.Serialize(apiData, op);
var obj = System.Text.Json.JsonSerializer.Deserialize<ApiData>(json, op);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBootstrapBlazor();
builder.Services.AddSingleton<JsEcharts, JsEcharts>();

await builder.Build().RunAsync();
