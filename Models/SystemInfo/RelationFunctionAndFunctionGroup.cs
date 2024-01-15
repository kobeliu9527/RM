using SqlSugar;

namespace Models.SystemInfo
{
    public class RelationFunctionAndFunctionGroup
    {
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]//中间表可以不是主键
        public int FunctionPageId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]//中间表可以不是主键
        public int FunctionGroupId { get; set; }
    }
}
