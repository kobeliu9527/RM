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
    public class ListConvert : JsonConverter<List<ExAxis>> //where T : ICollection
    {
        public override List<ExAxis> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, List<ExAxis> value, JsonSerializerOptions options)
        {
            if (value != null && value.Count > 0)
            {
                JsonSerializer.Serialize(writer, value, options); // 序列化非空集合
            }
        }
        //public override List<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        //{
        //    throw new NotImplementedException(); 
        //}

        //public override void Write(Utf8JsonWriter writer, List<T> value, JsonSerializerOptions options)
        //{
        //    if (value != null && value.Count > 0)
        //    {
        //        JsonSerializer.Serialize(writer, value, options); // 序列化非空集合
        //    }
        //}
        //public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        //{
        //    throw new NotImplementedException();
        //}

        //public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        //{
        //    if (value != null && value.Count > 0)
        //    {
        //        JsonSerializer.Serialize(writer, value, options); // 序列化非空集合
        //    }
        //}
    }
}
