using SharedPage.Base;

namespace SharedPage.Model
{
    public class SeriePie : ESerieBase
    {
        public override string? type { get; set; } = "pie";
        /// <summary>
        /// 数据格式可以是如下:[400, 300]或者['50%', '50%'],默认[ "50%", "50%" ]radius
        /// </summary>
        public List<object> center { get; set; } = new() { "50%", "50%" };
        /// <summary>
        /// 数据格式可以是如下:[400, 300]或者['50%', '50%'],默认[0, '75%']radius
        /// </summary>
        public List<object> radius { get; set; } = new() { 0, "75%" };
        /// <summary>
        /// radius area 设置这两个中的一个,将会变成南丁格尔图
        /// </summary>
        public string? roseType { get; set; }
        public override List<object>? data { get; set; } = new List<object>()
                {
                    new { value=335,name="直接访问" },
                    new { value=234,name="联盟广告" },
                    new { value=1548,name="搜索引擎" },
                };
    }
    //public string? type { get; set; }
    //public string? name { get; set; }
    //public List<object>? data { get; set; }

}
