using SqlSugar;

namespace Models.Dto
{
    /// <summary>
    /// 模块
    /// </summary>
    public class ModuleDto : EntityBase
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 所属的工厂Id
        /// </summary>
        public int FactoryId { get; set; }
    
    }
}
