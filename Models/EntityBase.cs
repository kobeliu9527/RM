using SqlSugar;

namespace Models
{
    /// <summary>
    /// 只有主键的基类
    /// </summary>
    public class EntityBase
    {
        /// <summary>
        /// 主键,自增
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }
    }
}
