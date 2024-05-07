using SharedPage.Base;
using SharedPage.JsonConvert;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedPage.Model
{
   
    public class EOption
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ExAxis>? xAxis { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<EyAxis>? yAxis { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<Grid>? grid { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ESerieBase>? series { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public EdataSet? dataset { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Etitle? title { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Legend? legend { get; set; }

        /// <summary>
        /// 提示框组件
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] 
        public Etooltip? tooltip { get; set; } 
        
        [DisplayName("背景色"),Description("背景颜色,可能的值:blue red")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] 
        public string backgroundColor { get; set; } = "transparent";
    }
}
