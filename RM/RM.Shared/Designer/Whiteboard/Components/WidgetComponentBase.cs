using Microsoft.AspNetCore.Components;
using RM.Shared.Models;
using System.Diagnostics.CodeAnalysis;

namespace RM.Shared.Designer.Whiteboard.Components
{
    /// <summary>
    /// 组件基类,有属性 <see cref="Component"/>
    /// </summary>
    public partial class WidgetComponentBase : ComponentBase
    {
        /// <summary>
        /// 组件数据
        /// </summary>
        [Parameter]
        [NotNull]
        public ComponentDto? Component { get; set; }
        /// <summary>
        /// 表示跟页面
        /// </summary>
        [CascadingParameter(Name = "FormDesigner")]
        public FormRuning? FormDesigner { get; set; }
        /// <summary>
        /// 组件基类
        /// </summary>
        public WidgetComponentBase()
        {
        }
    }
}
