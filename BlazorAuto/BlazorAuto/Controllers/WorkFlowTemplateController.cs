using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAuto.Controllers
{
    /// <summary>
    /// 工作流
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WorkFlowTemplateController : ControllerBase
    {
        private readonly ISqlSugarClient db;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        public WorkFlowTemplateController(ISqlSugarClient db)
        {
            this.db = db;
        }
    }
}
