using BootstrapBlazor.Components;
using Models.Dto;
using Models.NotEntity;
using Models.Services.Base;
using Models.SystemInfo;
using SqlSugar;
using System.Collections.Generic;
using System.Security.Claims;

namespace Models.Services.ServerByDb
{
    /// <summary>
    /// 通过数据库获取<see cref="Module"/>
    /// </summary>
    public class ModuleServerByDB : ServerByDbBase<Module>
    {
        /// <summary>
        /// 
        /// </summary>
        public ModuleServerByDB(ISqlSugarClient db) : base(db)
        {
        }
        public async override Task<Result<Module>> SelectByRole(Query<Module> obj)
        {
            Result<Module> res = new();
            if (obj.QueryDto != null && obj.Roles != null)
            {
                var ss = await db.Queryable<Module>()
                            .Includes(x => x.FunctionGroups, y => y.FunctionPages)
                            .FirstAsync(x => x.Name == obj.QueryDto.Name)
                            ;
                ss.FunctionGroups?.ForEach(y => y.FunctionPages?.RemoveAll(fp => fp.Roles?.Intersect(obj.Roles).Count() == 0));
                return res.Ok(ss);
            }
            return res.Ok();



        }

    }
}
