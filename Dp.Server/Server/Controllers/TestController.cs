using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Dp.Server.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TestController : ControllerBase
    {

        [HttpGet]
        public a Getstudent()
        {
            a aa = new a();
            b assd=new b();
            aa.b = assd;
            var options = new JsonSerializerOptions
            {
               // Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
               // EscapeHandling = EscapeHandling.Default
                
            };
           // aa.name=System.Text.Json.JsonSerializer.Serialize(assd, options);
            aa.name = Newtonsoft.Json.JsonConvert.SerializeObject(assd); ;
            return aa;
        }
    }
    public class a
    {
        public int MyProperty { get; set; }
        public string name { get; set; } = "aaaa";
        public b b { get; set; }
    }
    public class b
    {
        public int MyProperty { get; set; } =1111111;
        public string name { get; set; } = "bbbbbbbbbbbbb";
    }
}
