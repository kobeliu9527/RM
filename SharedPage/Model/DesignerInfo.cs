using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedPage.Model
{
    public class DesignerInfo
    {
        /// <summary>
        /// 设计器中当前被选中的组件
        /// </summary>
        public ComponentInfo? SelectedComponent { get; set; }
        /// <summary>
        /// 当前大屏的信息 
        /// 
        /// </summary>
        public BigScreen BigScreen { get; set; } = new();
    }
}
