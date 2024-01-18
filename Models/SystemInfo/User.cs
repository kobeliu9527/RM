using SqlSugar;
using System.ComponentModel;

namespace Models.SystemInfo
{
    public class User : EntityBase
    {
        /// <summary>
        /// 用户名,全局唯一
        /// </summary>
        [DisplayName("系统名")]
        public string SysName { get; set; } = "";
        /// <summary>
        /// 真实名字
        /// </summary>
        [DisplayName("真实名字")]
        public string RealName { get; set; } = "";
        /// <summary>
        /// 密码
        /// </summary>
        [DisplayName("密码")]
        public string PassWord { get; set; } = "";
        /// <summary>
        /// 角色列表
        /// </summary>
        [Navigate(typeof(UserRole), nameof(UserRole.UserId), nameof(UserRole.RoleId))]
        public List<Role>? Roles { get; set; }
    }
}
