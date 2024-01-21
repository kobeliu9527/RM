using Microsoft.AspNetCore.Mvc;
using Models.SystemInfo;
using SqlSugar;

namespace Ufo.Auto.Controllers
{
    /// <summary>
    /// 数据库操作
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class DbMangerController : Controller
    {
        private readonly ISqlSugarClient db;

        public DbMangerController(ISqlSugarClient db)
        {
            this.db = db;
        }
        [HttpPost]
        public void Init()
        {
            db.DbMaintenance.CreateDatabase();
            db.CodeFirst.InitTables(typeof(FunctionPage));
        }
    }
}
