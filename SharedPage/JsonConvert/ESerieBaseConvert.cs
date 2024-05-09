using SharedPage.Base;
using SharedPage.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedPage.JsonConvert
{
    public class ESerieBaseConvert : JsonConverter<ESerieBase> //where T : ICollection
    {
        public override ESerieBase Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                // 根据 JSON 对象中的类型鉴别器进行处理
                if (doc.RootElement.TryGetProperty("series", out JsonElement typeElement))
                {
                    string type = typeElement.GetString();
                    switch (type)
                    {
                        case "SeriePie":
                            return JsonSerializer.Deserialize<SeriePie>(doc.RootElement.GetRawText(), options);
                        case "SerieLine":
                            return JsonSerializer.Deserialize<SerieLine>(doc.RootElement.GetRawText(), options);
                        default:
                            throw new JsonException($"Unknown type: {type}");
                    }
                }
                else
                {
                    throw new JsonException("Missing type discriminator");
                }
            }
        }

        public override void Write(Utf8JsonWriter writer, ESerieBase value, JsonSerializerOptions options)
        {
            //JsonSerializer.Serialize(writer, value, value.GetType(), options);
            writer.WriteStartObject();
            
            writer.WriteEndObject();
        }
    }
}
