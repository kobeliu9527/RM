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
   /// <summary>
   /// EChart配置
   /// </summary>
    public class EOption
    {
        /// <summary>
        /// X轴集合
        /// </summary>
        public List<ExAxis>? xAxis { get; set; }
        /// <summary>
        /// Y轴集合
        /// </summary>
        public List<ExAxis>? yAxis { get; set; }
        /// <summary>
        /// 直角坐标系的底板
        /// </summary>
        public List<Grid>? grid { get; set; }
        /// <summary>
        /// 序列集合 一条曲线就是一个序列
        /// </summary>
        public List<ESerieBase>? series { get; set; }
        ///
        public EdataSet? dataset { get; set; } = new();
        ///
        public Etitle? title { get; set; }
        ///
        public Legend? legend { get; set; }

        /// <summary>
        /// 提示框组件
        /// </summary>
        /// 
        public Etooltip? tooltip { get; set; } 
        
        [DisplayName("背景色"),Description("背景颜色,可能的值:blue red")]
        public string backgroundColor { get; set; } = "transparent";
        [DisplayName("全局颜色"), Description("全局颜色,可能的值:blue red #c23531 rgb(128, 128, 128) rgba(128, 128, 128, 0.5)")]
        public List<string>? color { get; set; }
    }
}
