using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.System;
using SqlSugar;

namespace AutoServer.Controllers
{
    /// <summary>
    /// 数据库操作
    /// </summary>
    [Route("api/[controller]/[action]")]
    [Authorize(Roles ="SupAdmin")]
    public class DbMangerController : Controller
    {
        private readonly ISqlSugarClient db;

        public DbMangerController(ISqlSugarClient db)
        {
            this.db = db;
        }
        /// <summary>
        /// 初始化数据库
        /// </summary>
        [HttpPost]
        public void Init()
        {
            db.DbMaintenance.CreateDatabase();
            db.CodeFirst.InitTables(typeof(FunctionPage),typeof(UploadFileInfo));
        }
    }
}
