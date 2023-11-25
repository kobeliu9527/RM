using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.System
{
    /// <summary>
    /// 登录信息
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string? UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string? PassWord { get; set; }
        /// <summary>
        /// 工厂
        /// </summary>
        public Factory? Factory { get; set; }
        /// <summary>
        /// 模块
        /// </summary>
        public Module? Module { get; set; }

    }
}
