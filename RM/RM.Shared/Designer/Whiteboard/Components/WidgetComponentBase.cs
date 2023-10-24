using Microsoft.AspNetCore.Components;
using RM.Shared.Models;

namespace RM.Shared.Designer.Whiteboard.Components
{
    /// <summary>
    /// 组件基类,有属性<see cref="Component"/> 
    /// </summary>
    public partial class WidgetComponentBase : ComponentBase
    {
        [Parameter]
        public ComponentDto? Component { get; set; }

        public WidgetComponentBase()
        {

        }
    }
}
