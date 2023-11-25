using System;

namespace Models
{
    /// <summary>
    /// 控件的类型,用于区分是文本框还是下拉框等等
    /// </summary>
    public enum ComponentType
    {
        SingleLine = 0,
        MultiLine = 1,
        Link = 2,
        Email = 3,
        Checkbox = 4,
        Number = 5,
        File = 6,
        DateTime = 7,
        Image = 8,
        Choice = 9,
        Tabs = 10,
        Select = 11,
        /// <summary>
        /// DataGridView
        /// </summary>
        Table = 12,
        /// <summary>
        /// 绝对定位容器控件
        /// </summary>
        AbsPanel = 13,
        /// <summary>
        /// 按钮
        /// </summary>
        Button = 14,
    }

    public static class ComponentTypeExtensions
    {
        /// <summary>
        /// 返回控件的名字描述
        /// </summary>
        /// <param name="componentType"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string GetName(this ComponentType componentType)
        {
            return componentType.ToString();
            //var result = componentType switch
            //{
            //    ComponentType.SingleLine => "Single Line",
            //    ComponentType.MultiLine => "Multi Line",
            //    ComponentType.Link => "Link",
            //    ComponentType.Email => "Email",
            //    ComponentType.Checkbox => "Check",
            //    ComponentType.Number => "Number",
            //    ComponentType.File => "File",
            //    ComponentType.DateTime => "DateTime",
            //    ComponentType.Image => "Image",
            //    ComponentType.Choice => "Choice",
            //    ComponentType.Tabs => "Tabs",
            //    ComponentType.Select => "Select",
            //    _ => throw new NotImplementedException(@$"There is no defined name for '{componentType}'"),
            //};
            //return result;
        }
    }
}
