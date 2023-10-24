using RM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RM.Shared.Core
{
    public class ComponentUtils
    {
        public const int MaxColumnWidth = 12;
        public const string BlankIconDataString = @"<svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 50 20""></svg>";

        public static string GetComponentColumnCssClasses(ComponentDto componentData)
        {
            return $@"col-{CalculateColumnWidth(componentData)}";
        }

        public static int CalculateColumnWidth(ComponentDto componentData)
        {
            return componentData?.Width ?? MaxColumnWidth;
        }
        /// <summary>
        /// todo 重新计算一行中每个控件所占用的大小
        /// </summary>
        /// <param name="componentInRow"></param>
        public static void ComputeEachItemSizeInRow(List<ComponentDto> componentInRow)
        {
            //debug 直接整除就好了
            var columnSize = Math.Floor((double)(MaxColumnWidth / componentInRow.Count));//每一个占用多少
            var lastColumnSize = MaxColumnWidth % columnSize;//评分后剩余多少

            var rowSize = componentInRow.Aggregate(0, (colSize, component) => colSize + component.Width);
            var lastComponent = componentInRow.Last();

            if (rowSize < MaxColumnWidth)//所有控件的width之和小于12,直接把最后一个的width设为fill
            {
                // last component will fill remaining space
                lastComponent.Width = MaxColumnWidth;
                return;
            }
            else//控件sum>12了
            {
                //we iterate over the component to resize  them
                foreach (var component in componentInRow)
                {
                    component.Width = (int)columnSize;//将宽度设置为12/控件总数
                }

                // We stop if all column size are equal and there rest is 0
                //如果所有列大小相等，并且其余为0，则停止
                if (lastColumnSize == 0)
                {
                    return;
                }

                // we distribute the rest of number-of-components / 12 to
                // columns starting from the end
                //将多余的控件
                for (var index = componentInRow.Count - (int)lastColumnSize; index < componentInRow.Count - 1; index++)
                {
                    componentInRow[index].Width += 1;
                }
            }
        }
        public static string GetIconDataString(string iconAsString)
        {
            var encodedIconDataString = Uri.EscapeDataString(string.IsNullOrEmpty(iconAsString) ? BlankIconDataString : iconAsString);
            var iconDataString = $@"data:image/svg+xml,{encodedIconDataString}";
            return iconDataString;
        }
    }
}
