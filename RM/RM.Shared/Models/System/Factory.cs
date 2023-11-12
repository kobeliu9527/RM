namespace RM.Shared.Models.System
{
    /// <summary>
    /// 工厂信息
    /// </summary>
    public class Factory : EntityBase
    {
        /// <summary>
        /// 工厂名
        /// </summary>
        public string? Name { get; set; }
    }
    /// <summary>
    /// 模块
    /// </summary>
    public class Module : EntityBase
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string? Name { get; set; }
    }
}
