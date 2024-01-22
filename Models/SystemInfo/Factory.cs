using SqlSugar;
using System.Diagnostics.CodeAnalysis;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Models.SystemInfo
{
    /// <summary>
    /// 工厂信息
    /// </summary>
    [SqlSugar.SugarTable("sys_"+nameof(Factory))]
    public class Factory : EntityBase
    {
        /// <summary>
        /// 工厂名
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 所属公司Id
        /// </summary>
        public long CompanyId { get; set; }
        /// <summary>
        /// 所有的模块
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(Module.FactoryId))]
        public List<Module>? Modules { get; set; }
    }
}
