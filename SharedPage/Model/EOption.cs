using System;
using System.Collections.Generic;
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
        public Etitle? title { get; set; } = new();
        /// <summary>
        /// 提示框组件
        /// </summary>
        public Etooltip? tooltip { get; set; } = new();
        public List<ESerie>? series { get; set; }
        //tooltip = new { },
    }
}
