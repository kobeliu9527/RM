using Auto.Shared.Models.SystemInfo;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ServicesExtensions
    {
        /// <summary>
        /// 选项配置注册
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomOption(this IServiceCollection services)
        {
            services.AddConfigurableOptions<SystemInfoOptions>();
            return services;
        }
    }
}
