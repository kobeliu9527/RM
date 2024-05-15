using SqlSugar;

namespace EntityStore.SystemTable
{
    /// <summary>
    /// 用户和角色的关系表
    /// </summary>
    [SugarTable("sys_" + nameof(UserRole))]
    public class UserRole:EntityBaseIdTime
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]//中间表可以不是主键
        public long UserId { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]//中间表可以不是主键
        public long RoleId { get; set; }
    }
}
