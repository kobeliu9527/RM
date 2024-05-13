using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedPage.Model
{
    /// <summary>
    /// 用于大屏设计器级联参数
    /// </summary>
    public class BigScreenDesigner
    {
        /// <summary>
        /// 当前正被设计的
        /// </summary>
        public BigScreen? Selected { get; set; }
        /// <summary>
        /// 当前用户所拥有的所有大屏
        /// </summary>
        public List<BigScreen> BigScreens { get; set; } = new();
        /// <summary>
        /// 要执行刷新大屏委托
        /// </summary>
        public Action<bool>? RefreshHandel { get; set; }
        /// <summary>
        /// 当正在被设计的大屏被改变时执行的委托
        /// </summary>
        public Action<long>? SelectedChangeHandel { get; set; }
    }
}
