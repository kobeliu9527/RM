﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Dto.SVG;
using Models.NotEntity;
using Models.SystemInfo;
using SqlSugar;

namespace BlazorAuto.Controllers
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
                    typeof(ModuleAndFunctionGroup), 
                    typeof(RoleFunction),
                    typeof(UserRole),
                    typeof(FwHistory),
                    //typeof(Production),
                    //typeof(WorkFlow),
                    typeof(NodeDto),
                    typeof(LinkDto), 
                    //typeof(Product),
                    typeof(Part),
                    //typeof(Mo), 
                    typeof(WorkFlowTemplate)
                    //typeof(Lot)
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

