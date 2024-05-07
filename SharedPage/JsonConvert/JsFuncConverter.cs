
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
    /// 用于JS函数转译输出
    /// </summary>
    public class JsFuncConverter : JsonConverter<JsFunc>
    {
        public override JsFunc Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, JsFunc value, JsonSerializerOptions options)
        {
            writer.WriteRawValue(value.RAW, true);
        }
    }
}