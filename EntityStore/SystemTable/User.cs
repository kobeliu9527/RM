using SqlSugar;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace EntityStore.SystemTable
{
    /// <summary>
    /// 用户表
    /// </summary>
    [SugarTable("sys_" + nameof(User), IsCreateTableFiledSort = true)]
    //[SugarIndex("{table}_SysName_PassWord",
    //                            nameof(User.SysName), OrderByType.Asc,
    //                            nameof(User.PassWord), OrderByType.Asc,
    //                            nameof(User.RealName), OrderByType.Asc,
    //                            nameof(User.Icon), OrderByType.Asc,
    //                            nameof(User.ModifyTime), OrderByType.Asc,
    //                            nameof(User.CreateTime), OrderByType.Asc)]
    public class User : EntityBaseIdTime
    {
        /// <summary>
        /// 用户名,全局唯一,通常有系统根据规则分配
        /// </summary>
        [DisplayName("系统名"), SugarColumn(Length = Constants.NameLen)]
        public string SysName { get; set; } = "";
        /// <summary>
        /// 真实名字,人员自主设置
        /// </summary>
        [DisplayName("真实名字"), SugarColumn(Length = Constants.NameLen)]
        public string RealName { get; set; } = "";
        /// <summary>
        /// 密码
        /// </summary>
        [DisplayName("密码"), SugarColumn(Length = 500)]
        public string PassWord { get; set; } = "";
        /// <summary>
        /// 角色列表
        /// </summary>
        [Navigate(typeof(UserRole), nameof(UserRole.UserId), nameof(UserRole.RoleId))]
        public List<Role>? Roles { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [DisplayName("图标"), Description("系统采用知名的fontawesome图标,网址:https://fontawesome.com.cn/v5"), SugarColumn(IsNullable = true)]
        public string? Icon { get; set; }
    }
}
