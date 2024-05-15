using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedPage.Model
{
    /// <summary>
    /// https://echarts.apache.org/zh/option.html#dataset
    /// </summary>
    public class EdataSet
    {
        ///
        public string? Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(EntityStore.DataTableJsonConverter))]public DataTable? source { get; set; } = new();
        ///
        public List<EDimension>? dimensions { get; set; }//sourceHeader
        /// <summary>
        /// 本系统简化数据源,这里统一设置为false,表示数据源中没有表头
        /// </summary>
        public bool? sourceHeader { get; set; } //sourceHeader
    }
}
