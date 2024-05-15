using Dp.Wasm.IServices;
using EntityStore;
using Microsoft.AspNetCore.Mvc;
using NetTaste;
using SharedPage.Model;
using SqlSugar;
using System.Collections.Generic;

namespace Dp.Server.Server.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class BigScreenController : ControllerBase, IBigScreenService
    {
        private readonly ISqlSugarClient db;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        public BigScreenController(ISqlSugarClient db)
        {
            this.db = db;
        }
        /// <summary>
        /// 返回指定的大屏数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet,HttpPost]
        public async Task<Result<BigScreen>> GetBigScreen([FromBody]long id)
        {
            Result<BigScreen> res = new Result<BigScreen>();
            try
            {
                //应该这里拿到这个用户的角色集合
                // var username = User?.Identity?.Name;
                // var claims = User?.Claims?.ToList();
                var obj = await db.Queryable<BigScreen>().SingleAsync(x => x.Id == id);
                return res.SetResult(obj);
            }
            catch (Exception ex)
            {
                return res.HasException(ex);
            }
        }
        /// <summary>
        /// 返回所有的大屏
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet, HttpPost]
        public async Task<Result<List<BigScreen>>> GetBigScreens()
        {
            Result<List<BigScreen>> res = new();
            try
            {
                //应该这里拿到这个用户的角色集合
                // var username = User?.Identity?.Name;
                // var claims = User?.Claims?.ToList();
                var obj = await db.Queryable<BigScreen>().ToListAsync();
                return res.SetResult(obj);
            }
            catch (Exception ex)
            {
                return res.HasException(ex);
            }
        }
        /// <inheritdoc/>
        [HttpPost]
        public async Task<Result> Save([FromBody]BigScreen bs)
        {
            Result res = new();
            try
            {
                //应该这里拿到这个用户的角色集合
                //  var username = User?.Identity?.Name;
                //  var claims = User?.Claims?.ToList();
                var obj = await db.Updateable(bs).ExecuteCommandAsync();
                return res.Ok();
            }
            catch (Exception ex)
            {
                return res.HasException(ex);
            }
        }
        /// <summary>
        /// 新增大屏
        /// </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        public async Task<Result<long>> Insert([FromBody] BigScreen bs)
        {
            Result<long> res = new();
            try
            {
                //应该这里拿到这个用户的角色集合
                //  var username = User?.Identity?.Name;
                //  var claims = User?.Claims?.ToList();
                var obj = await db.Insertable(bs).ExecuteReturnSnowflakeIdAsync();
                return res.SetResult(obj);
            }
            catch (Exception ex)
            {
                return res.HasException(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        public async Task<Result> Delete([FromBody]long id)
        {
            Result res = new();
            try
            {
                //应该这里拿到这个用户的角色集合
                //  var username = User?.Identity?.Name;
                //  var claims = User?.Claims?.ToList();
                var obj = await db.Deleteable<BigScreen>().Where(x => x.Id == id).ExecuteCommandAsync();
                return res.Ok();
            }
            catch (Exception ex)
            {
                return res.HasException(ex);
            }
        }
    }
}
