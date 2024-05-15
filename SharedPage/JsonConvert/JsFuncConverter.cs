
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
    /// <summary>
    /// 
    /// </summary>
    public class JsFunc2Converter : JsonConverter<string>
    {
        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.None:
                    break;
                case JsonTokenType.StartObject:
                    break;
                case JsonTokenType.EndObject:
                    break;
                case JsonTokenType.StartArray:
                    break;
                case JsonTokenType.EndArray:
                    break;
                case JsonTokenType.PropertyName:
                    break;
                case JsonTokenType.Comment:
                    break;
                case JsonTokenType.String:
                    break;
                case JsonTokenType.Number:
                    break;
                case JsonTokenType.True:
                    break;
                case JsonTokenType.False:
                    break;
                case JsonTokenType.Null:
                    break;
                default:
                    break;
            }
            return null;
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            if (value != null)
            {
                writer.WriteRawValue(value);
            }
            else
            {
                writer.WriteNullValue();
            }

        }
    }



}