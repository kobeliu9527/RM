namespace SharedPage.Model
{
    /// <summary>
    /// 坐标轴轴线相关设置。
    /// </summary>
    public class axisLine
    {
        /// <summary>
        /// 轴线两边的箭头。可以是字符串，表示两端使用同样的箭头；或者长度为 2 的字符串数组，分别表示两端的箭头。默认不显示箭头，即 'none'。两端都显示箭头可以设置为 'arrow'，只在末端显示箭头可以设置为 ['none', 'arrow']。
        /// </summary>
        public JsFuncNumString? symbol { get; set; }
    }
}