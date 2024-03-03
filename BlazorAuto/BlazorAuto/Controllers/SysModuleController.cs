using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Dto;
using Models.NotEntity;
using Models.Services.Base;
using Models.SystemInfo;
using System.Linq.Expressions;

namespace BlazorAuto.Controllers
{
    /// <summary>
    /// CRUD模块
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class SysModuleController : ControllerBase, ICrudBaseByDb<Module>
    {
        private readonly ICrudBaseByDb<Module> db1;

        /// <summary>
        /// 
        /// </summary>
        public ISqlSugarClient? db { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db1"></param>
        public SysModuleController(ICrudBaseByDb<Module> db1)
        {
            this.db1 = db1;
            //var cli = HttpContext.User.Claims;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [NonAction]
        public Task<Result<int>> Delete(Module obj)
        {
            var roles = HttpContext.User.Claims;//获取请求者的角色,在表名-角色关系表中,看这个角色时候能否操作这张表
            
            
            throw new NotImplementedException();    
        }
        [HttpPost]
        public async Task<Result<int>> Insert([FromBody] Module obj)
        {
            return await db1.Insert(obj);
            //return await db.Insert(obj);
        }
        [HttpPost]
        public async Task<Result<long>> InsertWithSnowFlakeId(Module obj)
        {
            return await db1.InsertWithSnowFlakeId(obj);
        }
        [NonAction]
        public Task<Result<List<Module>>> SelectAll()
        {
            throw new NotImplementedException();
        }
        [NonAction]
        public Task<Result<List<Module>>> SelectBy(Expression<Func<Module, bool>> func)
        {
            throw new NotImplementedException();
        }
        [NonAction]
        public Task<Result<int>> Update(Module obj)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public async Task<Result<Module>> SelectByRole([FromBody] Query<Module> obj)
        {
            obj.Roles = HttpContext.User.Claims.Select(x => new Role() { Name = x.Value }).ToList();
            var mo = await db1.SelectByRole(obj);
            return mo;
        }

        
    }
}
