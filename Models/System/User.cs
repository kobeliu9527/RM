using SqlSugar;

namespace Models.System
{
    public class User : EntityBase
    {
        /// <summary>
        /// 用户名,全局唯一
        /// </summary>
        public string SysName { get; set; } = "";
        /// <summary>
        /// 真实名字
        /// </summary>
        public string RealName { get; set; } = "";
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; } = "";
        /// <summary>
        /// 角色列表
        /// </summary>
        [Navigate(typeof(UserRole), nameof(UserRole.UserId), nameof(UserRole.RoleId))]
        public List<Role>? Roles { get; set; }
    }
}
