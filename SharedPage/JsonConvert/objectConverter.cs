using SharedPage.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EntityStore.JsonConvert
{
    /// <summary>
    /// 
    /// </summary>
    public class objectConverter : JsonConverter<object>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.None:
                    reader.TrySkip();
                    return null;
                case JsonTokenType.StartObject:
                  return  JsonDocument.ParseValue(ref reader).RootElement.Clone();
                    return null;
                case JsonTokenType.EndObject:
                    reader.TrySkip();
                    return null;
                case JsonTokenType.StartArray:
                    reader.TrySkip();
                    return null;
                case JsonTokenType.EndArray:
                    reader.TrySkip();
                    return null;
                case JsonTokenType.PropertyName:
                    reader.TrySkip();
                    return null;
                case JsonTokenType.Comment:
                    reader.TrySkip();
                    return null;
                case JsonTokenType.String:
                    return reader.GetString();
                case JsonTokenType.Number:
                    return reader.GetDecimal();
                case JsonTokenType.True:
                    return reader.GetBoolean();
                case JsonTokenType.False:
                    return reader.GetBoolean();
                case JsonTokenType.Null:
                    return null;
                default:
                    return null;
            }


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            writer.WriteRawValue(value.ToString(), true);
            //if (value is JsFunc s)
            //{
            //    writer.WriteRawValue(s.RAW, true);
            //}
            //else {
            //    JsonSerializer.Serialize(writer, value, options);
            //}
            
        }
    }
}