using SharedPage.Base;
using SharedPage.JsonConvert;
using SharedPage.Model;
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
        static JsonSerializerOptions OpForJsFun = new JsonSerializerOptions()
        {
            Converters = { new JsFuncConverter() },
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        static Ext()
        {
            // op2.Converters.Add(new ESerieBaseConvert());  
        }
        /// <summary>
        /// 将集合中的两个元素互换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        public static void Swap<T>(this List<T> list, int index1, int index2)
        {
            T temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }
        /// <summary>
        /// 转义后,为null不会序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize<T>(this T obj)
        {
            return System.Text.Json.JsonSerializer.Serialize(obj, op);
        }
        /// <summary>
        /// 为echart的函数准备的转换器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeForJsFun<T>(this T obj)
        {
            return System.Text.Json.JsonSerializer.Serialize(obj, OpForJsFun);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [Obsolete("不用了", false)]
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [Obsolete("不用了", false)]
        public static List<object[]> ToChatSource(this DataSet dt)
        {
            List<object[]> list = new List<object[]>();

            return list;
        }
        /// <summary>
        /// 测试数据源
        /// </summary>
        public static DataTable pietable { get; } = GetDataTable();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static DataTable GetDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("产品种类", typeof(string));
            table.Columns.Add("总产量", typeof(int));
            table.Columns.Add("ok的", typeof(int));
            table.Columns.Add("Ng的", typeof(int));
            table.Columns.Add("时间", typeof(DateTime));

            // 添加数据行
            table.Rows.Add("衣服", 1000, 600, 400, DateTime.Now.AddHours(1));
            table.Rows.Add("电视", 2000, 1300, 700, DateTime.Now.AddHours(1));
            table.Rows.Add("鞋子", 3000, 2000, 1000, DateTime.Now.AddHours(1));
            table.Rows.Add("美铝", 4000, 1600, 2400, DateTime.Now.AddHours(1));

            return table;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ComponentInfo ToComponent(this ComponentType type)
        {
            ComponentInfo chart = new ComponentInfo();
            chart.ComponentType = type;
            switch (type)
            {
                case ComponentType.Line:
                    chart.DataName = "test";
                    chart.Option = new EOption()
                    {
                        dataset = new EdataSet()
                        {
                            source = pietable,
                            // dimensions = new List<EDimension>() {
                            //     new EDimension() { name = "列1" },
                            //     new EDimension() { name = "列2" },
                            //     new EDimension() { name = "列3" },
                            //     new EDimension() { name = "列4" },
                            //     new EDimension() { name = "列5" },
                            // },
                            sourceHeader = false
                        },

                    };
                    chart.Option.series = new List<ESerieBase>() { new SeriePie() { type = ESeriesType.line }, new SeriePie() { type = ESeriesType.line }, new SeriePie() { type = ESeriesType.line } };
                    chart.Option.xAxis = new() { new ExAxis() { type = EAxisType.category } };
                    chart.Option.yAxis = new List<ExAxis>() { new ExAxis() };
                    chart.Option.tooltip = new();
                    chart.Option.legend = new();
                    break;
                case ComponentType.Pie:
                    chart.Option = new EOption()
                    {
                        dataset = new EdataSet()
                        {
                            source = pietable,
                            //dimensions = new List<EDimension>() {
                            //        new EDimension() { name = "列1" },
                            //        new EDimension() { name = "列2" },
                            //        new EDimension() { name = "列3" },
                            //        new EDimension() { name = "列4" },
                            //        new EDimension() { name = "列5" },
                            //    },
                            //sourceHeader = false
                        },

                    };
                    chart.Option.series = new List<ESerieBase>() { new SeriePie() {
                        type = ESeriesType.pie,
                        //encode = new EEncode() { itemName = "产品种类", value = 2 }
                    } };
                    chart.Option.tooltip = new();
                    chart.Option.legend = new();
                    break;
                case ComponentType.Other:
                    break;
                default:
                    break;
            }
            return chart;
        }
    }
}
