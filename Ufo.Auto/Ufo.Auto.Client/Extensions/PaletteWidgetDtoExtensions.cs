using Models;

namespace Ufo.Auto.Client.Extensions
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
            componentData.MutLanguage.zh_CN = paletteWidget.Name;
            componentData.Name = paletteWidget.Name; 
            foreach (var item in paletteWidget.Props)
            {
                Property p = new Property()
                {
                    DisplayName = item.Value.DisplayName,
                    PType = item.Value.PType,
                    StringValue = item.Value.StringValue,
                    DataSourse = item.Value.DataSourse,
                    BoolVal = item.Value.BoolVal,
                    IntlVal = item.Value.IntlVal,
                    StringListValue=new List<string>()
                };
                componentData.Props.Add(item.Key, p);
            }
            //componentData.Props = paletteWidget.Props;
            componentData.Id = Guid.NewGuid().ToString();
            return componentData;
        }
    }
}
