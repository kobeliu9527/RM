using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using RM.Shared.Models.System;

namespace RM.Server.Controller
{
    /// <summary>
    /// ..
    /// </summary>
    public class LoginController: IDynamicApiController
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="info"></param>
        [HttpPost]
        public void LoginIn(LoginDto info)
        {


        }
    }
}
