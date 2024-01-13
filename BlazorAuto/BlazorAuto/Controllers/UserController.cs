using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using Models.Dto.SVG;
using Models.NotEntity;
using Models.Services.Base;
using Models.System;

namespace BlazorAuto.Controllers
{
    /// <summary>
    /// 人员管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICrudBase<User> db;
        private readonly ICrudBase<WorkFlow> db2;

        /// <summary>
        /// 
        /// </summary>
        public UserController(ICrudBase<User> db, ICrudBase<WorkFlow> db2)
        {
            this.db = db;
            this.db2 = db2;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result<int>> Insert(UserDto obj)
        {
           // var roles = HttpContext.User?.Claims;
            //验证权限 能否执行
            var tabName = obj.GetType().Name;

            //可以操作这张表的角色集合
            //List<Role>? roles = db.Queryable<TableRole>().Includes(x => x.InsertRole).Where(x => x.TableName == tabName).First().InsertRole;
            //当前用户的角色集合根上面的的集合是否有交集,有才可以
            return await db.Insert(obj.Adapt<User>());
        }
    }
}
