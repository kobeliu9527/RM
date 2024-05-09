namespace SharedPage.Model
{
    public class ETextStyle
    {
        /// <summary>
        /// 文字颜色
        /// </summary>
        public string? color { get; set; }
        public EFontStyle? fontStyle { get; set; }
        public EFontWeight? fontWeight { get; set; }
        public string? fontFamily { get; set; }
        public int? fontSize { get; set; }
        public EAlign? align { get; set; }
        public EVerticalAlign? verticalAlign { get; set; }
        public int? lineHeight { get; set; }
        public string? backgroundColor { get; set; }
        public string? borderColor { get; set; }
        public double? borderWidth { get; set; }
        /// <summary>
        /// 文字块边框描边类型。;(本次未实现)自 v5.0.0 开始，也可以是 number 或者 number 数组，用以指定线条的 dash array，配合 borderDashOffset 可实现更灵活的虚线效果。https://echarts.apache.org/zh/option.html#xAxis.nameTextStyle.borderType
        /// </summary>
        public EBorderType? borderType { get; set; }
        public int? borderDashOffset { get; set; }
        public int? borderRadius { get; set; }
        /// <summary>
        /// 文字块的内边距。例如： padding: [3, 4, 5, 6]：表示[上, 右, 下, 左] 的边距。padding: 4：表示 padding: [4, 4, 4, 4]。padding: [3, 4]：表示 padding: [3, 4, 3, 4]。注意，文字块的 width 和 height 指定的是内容高宽，不包含 padding。
        /// </summary>
        public List<int>? padding { get; set; }
        /// <summary>
        /// 文字块的背景阴影颜色。
        /// </summary>
        public string? shadowColor { get; set; }
        /// <summary>
        /// 文字块的背景阴影长度。
        /// </summary>
        public string? shadowBlur { get; set; }
        /// <summary>
        /// 文字块的背景阴影 x 偏移。
        /// </summary>
        public int? shadowOffsetX { get; set; }
        //文字块的背景阴影 Y 偏移。
        public int? shadowOffsetY { get; set; }
        /// <summary>
        /// 文本显示高度。
        /// </summary>
        public int? width { get; set; }
        /// <summary>
        /// 文本显示高度。
        /// </summary>
        public int? height { get; set; }
        public string? textBorderColor { get; set; }
        public int? textBorderWidth { get; set; }
        public EBorderType? textBorderType { get; set; }
        /// <summary>
        /// 用于设置虚线的偏移量，可搭配 textBorderType 指定 dash array 实现灵活的虚线效果。
        /// </summary>
        public int? textBorderDashOffset { get; set; }
        public string? textShadowColor { get; set; }
        public int? textShadowBlur { get; set; }
        public int? textShadowOffsetX { get; set; }
        public int? textShadowOffsetY { get; set; }
        /// <summary>
        /// 文字超出宽度是否截断或者换行。配置width时有效'truncate' 截断，并在末尾显示ellipsis配置的文本，默认为...'break' 换行'breakAll' 换行，跟'break'不同的是，在英语等拉丁文中，'breakAll'还会强制单词内换行
        /// </summary>
        public EOverflow? overflow { get; set; }
        /// <summary>
        /// 在overflow配置为'truncate'的时候，可以通过该属性配置末尾显示的文本。
        /// </summary>
        public string? ellipsis { get; set; }
        /// <summary>
        /// todo  在 rich 里面，可以自定义富文本样式。利用富文本样式，可以在标签中做出非常丰富的效果。https://echarts.apache.org/zh/tutorial.html#%E5%AF%8C%E6%96%87%E6%9C%AC%E6%A0%87%E7%AD%BE
        /// </summary>nameGap
        public string? rich { get; set; }
        
    }
}