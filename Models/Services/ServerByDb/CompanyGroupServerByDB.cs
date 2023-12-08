using Models.Dto;
using Models.NotEntity;
using Models.Services.Base;
using Models.System;
using SqlSugar;

namespace Models.Services.ServerByDb
{
    /// <summary>
    /// 通过数据库
    /// </summary>
    public class CompanyGroupServerByDB : ServerByDbBase<CompanyGroup>
    {
        /// <summary>
        /// 
        /// </summary>
        public CompanyGroupServerByDB(ISqlSugarClient db) : base(db)
        {
        }

        public override Task<Result<CompanyGroup>> SelectNav(Query<CompanyGroup> obj)
        {
            throw new NotImplementedException();
        }
    }
}
