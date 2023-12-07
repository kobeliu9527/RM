
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AutoServer.CustomConverter
{
    /// <summary>
    /// 设置Json默认DateTime格式化
    /// </summary>
    public class JsonTextConverter : JsonConverter<DateTime>
    {
        private readonly string Format;
        public JsonTextConverter(string format)
        {
            Format = format;
        }
        public override void Write(Utf8JsonWriter writer, DateTime date, JsonSerializerOptions options)
        {
            writer.WriteStringValue(date.ToString(Format));
        }
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), Format, null);
        }
    }
}
