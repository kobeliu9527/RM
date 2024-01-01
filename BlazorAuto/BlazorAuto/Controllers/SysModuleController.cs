using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Dto;
using Models.NotEntity;
using Models.Services.Base;
using Models.System;
using System.Linq.Expressions;

namespace BlazorAuto.Controllers
{
    /// <summary>
    /// CRUD模块
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class SysModuleController : ControllerBase, ICrudBase<SysModule>
    {
        private readonly ICrudBase<SysModule> db;

        public SysModuleController(ICrudBase<SysModule> db)
        {
            this.db = db;
        }
        [NonAction]
        public Task<Result<int>> Delete(SysModule obj)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public async Task<Result<int>> Insert([FromBody] SysModule obj)
        {
            return await db.Insert(obj);
        }
        [HttpPost]
        public async Task<Result<long>> InsertWithSnowFlakeId(SysModule obj)
        {
            return await db.InsertWithSnowFlakeId(obj);
        }
        [NonAction]
        public Task<Result<List<SysModule>>> SelectAll()
        {
            throw new NotImplementedException();
        }
        [NonAction]
        public Task<Result<List<SysModule>>> SelectBy(Expression<Func<SysModule, bool>> func)
        {
            throw new NotImplementedException();
        }
        [NonAction]
        public Task<Result<int>> Update(SysModule obj)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public async Task<Result<SysModule>> SelectByRole([FromBody] Query<SysModule> obj)
        {
            obj.Roles = HttpContext.User.Claims.Select(x => new Role() { Name = x.Value }).ToList();
            var mo = await db.SelectByRole(obj);
            return mo;
        }


    }
}
