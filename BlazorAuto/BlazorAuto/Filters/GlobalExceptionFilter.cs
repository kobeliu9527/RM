using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SqlSugar;

namespace BlazorAuto.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ISqlSugarClient db;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        public GlobalExceptionFilter(ISqlSugarClient db)
        {
            this.db = db;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            //context.Result = new StatusCodeResult(500);
            context.Result = new JsonResult($"发生了异常:{context.Exception.Message}")
            {
                StatusCode = 500
            };
            // var ss = db.DbMaintenance.GetDataBaseList();
        }
    }
}
