using SharedPage.Base;
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
        public List<ExAxis>? xAxis { get; set; }
        public List<EyAxis>? yAxis { get; set; }
        public List<Grid>? grid { get; set; }
        public List<ESerieBase>? series { get; set; }
        public EdataSet? dataset { get; set; }

        public Etitle? title { get; set; } 
        public Legend? legend { get; set; }
        
        /// <summary>
        /// 提示框组件
        /// </summary>
        public Etooltip? tooltip { get; set; } 
        
        [DisplayName("背景色"),Description("背景颜色,可能的值:blue red")]
        public string backgroundColor { get; set; } = "transparent";
    }
}
