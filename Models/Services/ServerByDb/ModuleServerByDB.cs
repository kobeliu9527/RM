using BootstrapBlazor.Components;
using Models.Dto;
using Models.NotEntity;
using Models.Services.Base;
using Models.System;
using SqlSugar;
using System.Collections.Generic;
using System.Security.Claims;

namespace Models.Services.ServerByDb
{
    /// <summary>
    /// 通过数据库获取<see cref="SysModule"/>
    /// </summary>
    public class ModuleServerByDB : ServerByDbBase<SysModule>
    {
        /// <summary>
        /// 
        /// </summary>
        public ModuleServerByDB(ISqlSugarClient db) : base(db)
        {
        }
        public async override Task<Result<SysModule>> SelectByRole(Query<SysModule> obj)
        {
            Result<SysModule> res = new();
            if (obj.QueryDto != null && obj.Roles != null)
            {
                var ss = await db.Queryable<SysModule>()
                            .Includes(x => x.FunctionGroups, y => y.FunctionPages)
                            .FirstAsync(x => x.Name == obj.QueryDto.Name)
                            ;
                ss.FunctionGroups?.ForEach(y => y.FunctionPages?.RemoveAll(fp => fp.Roles?.Intersect(obj.Roles).Count() == 0));
                return res.End(ss);
            }
            return res.End();



        }

    }
}
