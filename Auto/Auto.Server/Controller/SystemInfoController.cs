using Auto.Shared.Models.SystemInfo;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Auto.Server.Controller
{
    /// <summary>
    /// 获取系统信息
    /// </summary>
    public class SystemInfoController : IDynamicApiController
    {
        private readonly IOptions<SystemInfoOptions> options;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// 获取系统信息
        /// </summary>
        /// <param name="options"></param>
        /// <param name="httpContextAccessor"></param>
        public SystemInfoController(IOptions<SystemInfoOptions> options, IHttpContextAccessor httpContextAccessor)
        {
            this.options = options;
            this.httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// 获取系统信息
        /// </summary>
        /// <returns></returns>
        public string Get()
        {
            // ipv4
            var ipv4 = httpContextAccessor.HttpContext.GetLocalIpAddressToIPv4();

            // ipv6
            var ipv6 = httpContextAccessor.HttpContext.GetLocalIpAddressToIPv6();

            // ipv4
            var ipv41 = httpContextAccessor.HttpContext.GetRemoteIpAddressToIPv4();

            // ipv6
            var ipv61 = httpContextAccessor.HttpContext.GetRemoteIpAddressToIPv6();
            return $"{options.Value.Name}";
        }
    }
}
