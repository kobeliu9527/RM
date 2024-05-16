
using SharedPage.Ext;
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
    /// 用于JS函数转译输出;非原创,来自于github上一个叫Blazor.ECharts项目,只能是在传给js的时候用一下;保存在数据库的时候不能用
    /// </summary>
    public class JsFuncConverter : JsonConverter<JsFunc>
    {
        public override JsFunc Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {

            return new JsFunc("asdf");

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, JsFunc value, JsonSerializerOptions options)
        {
            if (!string.IsNullOrWhiteSpace(value.RAW))
            {
                writer.WriteRawValue(value.RAW, true);
            }
        }
    }
    public class JsNumStringConverter : JsonConverter<JsNumString>
    {
        public override JsNumString? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, JsNumString value, JsonSerializerOptions options)
        {
            if (decimal.TryParse(value.RAW, out decimal res))
            {
                writer.WriteRawValue(value.RAW, true);
            }
            else
            {
                writer.WriteStringValue(value.RAW);
            }
        }
    }
    /// <summary>
    /// 取消函数和数字类型的引号:
    /// </summary>
    public class JsFuncNumStringConverter : JsonConverter<JsFuncNumString>
    {
        public override JsFuncNumString? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, JsFuncNumString value, JsonSerializerOptions options)
        {
            switch (value.JsType)
            {
                case JsType.Num:
                    writer.WriteRawValue(value.RAW, true);
                    break;
                case JsType.String:
                    writer.WriteStringValue(value.RAW);
                    break;
                case JsType.Function:
                    writer.WriteRawValue(value.RAW, true);
                    break;
                case JsType.Array:
                    writer.WriteRawValue(value.RAW, true);
                    break;
                case JsType.Object:
                    writer.WriteRawValue(value.RAW, true);
                    break;
                case JsType.StringArray:
                    if (value.RAW.StartsWith("[") && value.RAW.EndsWith("]"))
                    {
                        writer.WriteRawValue(value.RAW, true);
                    }
                    else
                    {
                        writer.WriteStringValue(value.RAW);
                    }
                    break;
                default:
                    break;
            }

        }
    }


}