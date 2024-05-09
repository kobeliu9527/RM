using SharedPage.JsonConvert;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace SharedPage.Ext
{
    public static class Ext
    {
        static JsonSerializerOptions op = new JsonSerializerOptions()
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs),
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true,
        };
        static JsonSerializerOptions op2 = new JsonSerializerOptions()
        {
            Converters = { new JsFuncConverter() },
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        static Ext()
        {
            // op2.Converters.Add(new ESerieBaseConvert());  
        }
        public static void Swap<T>(this List<T> list, int index1, int index2)
        {
            T temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }
        /// <summary>
        /// 转义后,忽略循环引用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize<T>(this T obj)
        {
            return System.Text.Json.JsonSerializer.Serialize(obj, op);
        }
        public static string Serialize2<T>(this T obj)
        {

            return System.Text.Json.JsonSerializer.Serialize(obj, op2);
        }
        public static List<object?[]> ToChatSource(this DataTable dt)
        {
            List<object?[]> list = new List<object?[]>();
            var colCount = dt.Columns.Count;
            object[] title = new object[colCount];
            for (int i = 0; i < colCount; i++)
            {
                title[i] = dt.Columns[i].ColumnName;
            }
            list.Add(title);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row.ItemArray);
            }

            return list;
        }
        public static List<object[]> ToChatSource(this DataSet dt)
        {
            List<object[]> list = new List<object[]>();

            return list;
        }
    }
}
