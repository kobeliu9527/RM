using SqlSugar;

namespace Models.SystemInfo
{
    [SqlSugar.SugarTable("sys_"+nameof(ModuleAndFunctionGroup))]
    public class ModuleAndFunctionGroup
    {
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]//中间表可以不是主键
        public long ModuleId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]//中间表可以不是主键
        public long FunctionGroupId { get; set; }
    }
}
