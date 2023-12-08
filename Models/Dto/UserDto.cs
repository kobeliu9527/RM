using SqlSugar;

namespace Models.Dto
{
    public class UserDto
    {
        /// <summary>
        /// 用户名,全局唯一
        /// </summary>
        public string? SysName { get; set; }
        /// <summary>
        /// 真实名字
        /// </summary>
        public string? RealName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string? PassWord { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string NewPassWord { get; set; }
        /// <summary>
        /// 模块名字
        /// </summary>
        public string ModuleName { get; set; }
    }
}
