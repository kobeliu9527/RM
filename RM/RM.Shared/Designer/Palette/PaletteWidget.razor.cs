using Microsoft.AspNetCore.Components;
using RM.Shared.Models;

namespace RM.Shared.Designer.Palette
{
    /// <summary>
    /// 表示左侧工具箱中的一个工具
    /// </summary>
    public partial class PaletteWidget
    {
        /// <summary>
        /// 表示一个工具箱组件
        /// </summary>
        [Parameter]
        public PaletteWidgetDto WidgetItem { get; set; }

        /// <summary>
        /// 由3部分组成,左侧工具箱,中间设计器,右侧属性栏
        /// </summary>
        [CascadingParameter(Name = "FormDesigner")]
        public FormDesigner FormDesigner { get; set; }

        /// <summary>
        /// 控件开始拖动的时候会触发
        /// </summary>
        /// <param name="widgetItem"></param>
        /// <returns></returns>
        private async Task DragStartAsync(PaletteWidgetDto widgetItem)
        {
            await FormDesigner.SetDraggedPaletteWidgetAsync(widgetItem);
        }
    }
}
