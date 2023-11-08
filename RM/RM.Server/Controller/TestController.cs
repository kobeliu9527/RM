using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;

namespace RM.Server.Controller
{
    public class TestService : IDynamicApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetHello()
        {
            return $"Hello {nameof(Furion)}:{DateTime.Now}";
        }
    }
}
