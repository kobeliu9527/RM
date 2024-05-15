using EntityStore;
using EntityStore.SystemTable;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SqlSugar;
using System.Reflection;

namespace Dp.Server.Server.Controllers
{
    /// <summary>
    /// 数据库操作
    /// </summary>
    [Route("api/[controller]/[action]")]
    //[Authorize(Roles ="SupAdmin")]
    public class DbMangerController : Controller
    {
        private readonly ISqlSugarClient db;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        public DbMangerController(ISqlSugarClient db)
        {
            this.db = db;
        }
        /// <summary>
        /// 初始化数据库
        /// </summary>
        [HttpPost]
        public Result<IEnumerable<string>> Init()
        {
            Result<IEnumerable<string>> res = new();
            try
            {
                var types1 = Assembly.Load("EntityStore").GetTypes();
                var types2 = Assembly.Load("Dp.Server.Server").GetTypes();
                var types3 = Assembly.Load("Dp.Wasm").GetTypes();
                var types4 = Assembly.Load("SharedPage").GetTypes();
                var types = types1.Concat(types2).Concat(types3).Concat(types4);

                var entityTypes = types.Where(type => type.IsClass && !type.IsAbstract && typeof(EntityBase).IsAssignableFrom(type)).ToArray();
                db.DbMaintenance.CreateDatabase();
                db.CodeFirst.InitTables(
                entityTypes
                    );
                IEnumerable<string> tabs = db.DbMaintenance.GetTableInfoList().Select(x => x.Name + ":" + x.Description);
                res.Data = tabs;
            }
            catch (Exception e)
            {
                return res.HasException(e);
            }
            return res;
        }
    }
}

