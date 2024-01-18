using SqlSugar;
using System.Text.Json.Serialization;

namespace Models.SystemInfo
{
    /// <summary>
    /// 功能组,把多个功能页面形成一个功能组
    /// </summary>
    public class FunctionGroup : EntityBase
    {
        /// <summary>
        /// 功能组的名字
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 所属的模块Id
        /// </summary>
        public long ModuleId { get; set; }
        /// <summary>
        /// 这个功能组包括的所有页面
        /// </summary>
        [Navigate(typeof(RelationFunctionAndFunctionGroup), nameof(RelationFunctionAndFunctionGroup.FunctionGroupId), nameof(RelationFunctionAndFunctionGroup.FunctionPageId))]
        public List<FunctionPage>? FunctionPages { get; set; }
    }
}
