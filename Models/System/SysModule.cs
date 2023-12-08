using SqlSugar;

namespace Models.System
{
    /// <summary>
    /// 模块
    /// </summary>
    public class SysModule : EntityBase
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 所属的工厂Id
        /// </summary>
        public int FactoryId { get; set; }
        /// <summary>
        /// 功能组
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(FunctionGroup.ModuleId))]
        public List<FunctionGroup>? FunctionGroups { get; set; }
    }
}
