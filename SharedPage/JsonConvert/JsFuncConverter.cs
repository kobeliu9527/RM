
using SharedPage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedPage.JsonConvert
{
    /// <summary>
    /// 用于JS函数转译输出;非原创,来自于github上一个叫Blazor.ECharts项目
    /// </summary>
    public class JsFuncConverter : JsonConverter<JsFunc>
    {
        public override JsFunc Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new JsFunc("asdf");
            var st = reader.GetString();
            if (st == null)
            {
                return new JsFunc(st);
            }
            else {
                return null;
            }
        }

        public override void Write(Utf8JsonWriter writer, JsFunc value, JsonSerializerOptions options)
        {
            if (value.RAW!=null)
            {
                writer.WriteRawValue(value.RAW, true);
            }
        }
    }
}