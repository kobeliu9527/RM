using SqlSugar;

namespace Models.SystemInfo
{
    [SqlSugar.SugarTable("sys_"+nameof(RoleFunction))]
    public class RoleFunction
    {
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]//中间表可以不是主键
        public long RoleId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]//中间表可以不是主键
        public long FunctionPageId { get; set; }
    }
}
