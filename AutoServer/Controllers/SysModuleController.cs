using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Dto;
using Models.NotEntity;
using Models.Services.Base;
using Models.SystemInfo;
using System.Linq.Expressions;

namespace AutoServer.Controllers
{
    /// <summary>
    /// CRUD模块
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class SysModuleController : ControllerBase, ICrudBase<Module>
    {
        private readonly ICrudBase<Module> db;

        public SysModuleController(ICrudBase<Module> db)
        {
            this.db = db;
        }
        [NonAction]
        public Task<Result<int>> Delete(Module obj)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public async Task<Result<int>> Insert([FromBody]Module obj)
        {
            return await db.Insert(obj);
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
            var mo = await db.SelectByRole(obj);
            return mo;
        }

    }
}
