using SqlSugar;

namespace Models.SystemInfo
{
    public class UserRole
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
