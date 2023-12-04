using SqlSugar;

namespace Models.System
{
    public class UserRole
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]//中间表可以不是主键
        public int UserId { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]//中间表可以不是主键
        public int RoleId { get; set; }
    }
}
