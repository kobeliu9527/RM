using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.NotEntity;
using Models.SystemInfo;
using SqlSugar;

namespace AutoServer.Controllers
{
    /// <summary>
    /// 数据库操作
    /// </summary>
    [Route("api/[controller]/[action]")]
    //[Authorize(Roles ="SupAdmin")]
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
        public Result<IEnumerable<string>> Init()
        {
            Result<IEnumerable<string>> res = new();
            try
            {
                db.DbMaintenance.CreateDatabase();
                db.CodeFirst.InitTables(
                    typeof(CompanyGroup),
                    typeof(Company),
                    typeof(Factory),
                    typeof(Module),
                    typeof(FunctionGroup),
                    typeof(FunctionPage),
                    typeof(User),
                    typeof(Role),
                    typeof(MutLanguage),
                    typeof(RelationFunctionAndFunctionGroup),
                    typeof(RoleFunction),
                    typeof(UserRole)

                    );
                IEnumerable<string> tabs = db.DbMaintenance.GetTableInfoList().Select(x => x.Name + ":" + x.Description);
                res.Data = tabs;
            }
            catch (Exception e)
            {
                return res.CatchException(e);
            }
            return res;
        }
    }
}

