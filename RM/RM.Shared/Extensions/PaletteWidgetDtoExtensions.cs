using RM.Shared.Models;

namespace RM.Shared.Extensions
{
    public static class PaletteWidgetDtoExtensions
    {
        /// <summary>
        /// 重新生成(new)一个控件实例,根据在工具箱中拖动的类型
        /// </summary>
        /// <param name="paletteWidget"></param>
        /// <returns></returns>
        public static ComponentDto CreateComponent(this PaletteWidgetDto paletteWidget)
        {
            var componentData = new ComponentDto(paletteWidget.ComponentType);
            componentData.Props = paletteWidget.Props;
            componentData.Id = $"{paletteWidget.ComponentType}-{DateTime.Now.ToString("F")}";
            return componentData;
        }
    }
}
