using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedPage.Model
{
    /// <summary>
    /// 非原创,来自于github上一个叫Blazor.ECharts项目
    /// </summary>
    public record JsFunc
    {
        /// <summary>
        /// 
        /// </summary>
        public JsFunc() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="raw"></param>
        public JsFunc(string raw)
        {
            RAW = raw;
        }
        /// <summary>
        /// 
        /// </summary>
        public string RAW { get; set; } = "";
    }
}
