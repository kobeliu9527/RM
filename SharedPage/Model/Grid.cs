using System.ComponentModel;

namespace SharedPage.Model
{
    public class Grid
    {
        /// <summary>
        /// https://echarts.apache.org/zh/option.html#grid.left
        /// </summary>
        [DisplayName("左边距"), Description("可以是数字,百分比,或者'top', 'middle', 'bottom'")]
        ///
        public string? left { get; set; } //= "10%";
        [DisplayName("右边距"), Description("可以是数字,百分比,或者'top', 'middle', 'bottom'")]
        ///
        public string? right { get; set; } //= "10%";

        [DisplayName("上边距"), Description("可以是数字,百分比")]
        ///
        public string? top { get; set; } //= "60";
        [DisplayName("下边距"), Description("可以是数字,百分比")]
        ///
        public string? bottom { get; set; } //= "60";
        [DisplayName("宽度"), Description("可以是数字,百分比")]
        ///
        public string? width { get; set; } //= "auto";
        [DisplayName("高度"), Description("可以是数字,百分比")]
        ///
        public string? height { get; set; } //= "auto";
        /// <summary>
        /// https://echarts.apache.org/zh/option.html#grid.containLabel
        /// </summary>
        [DisplayName("是否强制显示坐标轴标签"), Description("是否强制显示坐标轴标签")]
        ///
        public bool?   containLabel { get; set; } //= true;
    }
}
