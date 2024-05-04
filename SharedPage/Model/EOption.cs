using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedPage.Model
{
    public class EOption
    {
        public EdataSet dataset { get; set; }
        public ExAxis? xAxis { get; set; } = new();
        public EyAxis? yAxis { get; set; } = new();
        public Etitle title { get; set; } = new();
        public Legend legend { get; set; } = new();
        public Grid grid { get; set; } = new();
        /// <summary>
        /// 提示框组件
        /// </summary>
        public Etooltip? tooltip { get; set; } = new();
        public List<ESerie>? series { get; set; }
        [DisplayName("背景色"),Description("背景颜色,可能的值:blue red")]
        public string backgroundColor { get; set; } = "#65d778";
        //tooltip = new { },
    }
}
