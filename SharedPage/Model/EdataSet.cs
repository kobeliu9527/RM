﻿using SharedPage.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedPage.Model
{
    public class Etitle 
    {
        public string text { get; set; } = "标题";
        public bool show { get; set; } = true;
        public textStyle textStyle { get; set; }=new textStyle();
    }
    public class textStyle
    {
        public string color { get; set; } = "#333";
    }

    public class Legend 
    {
        [DisplayName("图例类型"),Description("图例的类型。可选值:'plain'：普通图例。缺省就是普通图例。'scroll'：可滚动翻页的图例。当图例数量较多时可以使用。 https://echarts.apache.org/zh/option.html#legend ")]
        public string type { get; set; } = "plain";
        public bool show { get; set; }=true;
    }
    public class Grid
    {
        /// <summary>
        /// https://echarts.apache.org/zh/option.html#grid.left
        /// </summary>
        [DisplayName("左边距"), Description("可以是数字,百分比,或者'top', 'middle', 'bottom'")]
        public string left { get; set; } = "10%";
        [DisplayName("右边距"), Description("可以是数字,百分比,或者'top', 'middle', 'bottom'")]
        public string right { get; set; } = "10%";

        [DisplayName("上边距"), Description("可以是数字,百分比")]
        public string top { get; set; } = "60";
        [DisplayName("下边距"), Description("可以是数字,百分比")]
        public string bottom { get; set; } = "60";
        [DisplayName("宽度"), Description("可以是数字,百分比")]
        public string width { get; set; } = "auto";
        [DisplayName("高度"), Description("可以是数字,百分比")]
        public string height { get; set; } = "auto";
        /// <summary>
        /// https://echarts.apache.org/zh/option.html#grid.containLabel
        /// </summary>
        [DisplayName("是否强制显示坐标轴标签"), Description("是否强制显示坐标轴标签")]
        public bool containLabel { get; set; } = true;
    }
    public class ExAxis
    {
        public string type { get; set; } = "category";
        public List<object> data { get; set; } = new();
    }
    public class EyAxis
    {
        public string type { get; set; } = "value";
    }
    public class Etooltip
    {
       
    }
    /// <summary>
    /// https://echarts.apache.org/zh/option.html#dataset
    /// </summary>
    public class EdataSet
    {
        public string? Id { get; set; }
        public List<object[]>? source { get; set; }
        public List<EDimension>? dimensions { get; set; }//sourceHeader
        public bool? sourceHeader { get; set; }//sourceHeader
    }
    public class EDimension
    {
        public string? name { get; set; }
        public string? type { get; set; }
        public string? displayName { get; set; }
    }
    public class SerieLine : ESerieBase
    {
        //public string? type { get; set; }
        //public string? name { get; set; }
        //public List<object>? data { get; set; }
    }
    public class EEncode
    {
        /// <summary>
        /// 使用 “名为 product 的维度” 和 “名为 score 的维度” 的值在 tooltip 中显示;tooltip: ['product', 'score']
        /// </summary>
        public List<string>? tooltip { get; set; }

        /// <summary>
        /// 使用第一个维度和第三个维度的维度名连起来作为系列名。（有时候名字比较长，这可以避免在 series.name 重复输入这些名字）        seriesName: [1, 3],
        /// </summary>
        public List<int>? seriesName { get; set; }
        /// <summary>
        /// 表示使用第二个维度中的值作为 id。这在使用 setOption 动态更新数据时有用处，可以使新老数据用 id 对应起来，从而能够产生合适的数据更新动画。        itemId: 2,
        /// </summary>
        public int itemId { get; set; }
        /// <summary>
        /// 指定数据项的名称使用第三个维度在饼图等图表中有用，可以使这个名字显示在图例（legend）中。 itemName: 3,
        /// </summary>
        public int itemName { get; set; }
        /// <summary>
        /// 指定数据项的组 ID (groupId)。当全局过渡动画功能开启时，setOption 前后拥有相同 groupId 的数据项会进行动画过渡。 itemGroupId: 4,
        /// </summary>
        public int itemGroupId { get; set; }
        /// <summary>
        /// 指定数据项对应的子数据组 ID (childGroupId)，用于实现多层下钻和聚合。详见 childGroupId。
        /// 从 v5.5.0 开始支持 itemChildGroupId: 5
        /// </summary>
        public int itemChildGroupId { get; set; }
    }

    public class EEncodeForXY: EEncode
    {
        public List<object>? x { get; set; }
        public int y { get; set; }
    }
}
