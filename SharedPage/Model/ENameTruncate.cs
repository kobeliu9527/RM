namespace SharedPage.Model
{
    /// <summary>
    /// 坐标轴名字的截断。
    /// </summary>
    public class ENameTruncate
    {
        /// <summary>
        /// 截断文本的最大长度，超过此长度会被截断。
        /// </summary>
        public int? maxWidth { get; set; }
        /// <summary>
        /// 截断后文字末尾显示的内容。
        /// </summary>
        public string? ellipsis { get; set; }
    }
}