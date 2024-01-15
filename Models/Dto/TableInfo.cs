namespace Models.Dto
{
    public class TableInfo
    {
        /// <summary>
        /// 获取数据源的存储过程名字
        /// </summary>
        public string DataSourse { get; set; } = "GetTableDemo";
        /// <summary>
        /// 存储过程需要的参数名字
        /// </summary>
        public string Parameter { get; set; } = "";
        /// <summary>
        /// 每一页显示多少条
        /// </summary>
        public int PageItems { get; set; } = 15;
        /// <summary>
        /// 一共有多少条
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 当前显示第几页
        /// </summary>
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// 一共有多少页
        /// </summary>
        public int PageCount { get; set; }
    }
}
