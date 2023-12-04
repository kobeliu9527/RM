using Models;
using Models.NotEntity;
using Models.System;
using SqlSugar;
using Ufo.Auto.Client.Services.Base;

namespace Ufo.Auto.Client.Services.CompanyGroupServer
{
    /// <summary>
    /// 通过数据库
    /// </summary>
    public class CompanyGroupServerByDB : ServerByDbBase<CompanyGroup>
    {
        private readonly ISqlSugarClient db;
        /// <summary>
        /// 
        /// </summary>
        public CompanyGroupServerByDB(ISqlSugarClient db):base(db) 
        {
            this.db = db;
        }
    }
    /// <summary>
    /// 通过数据库
    /// </summary>
    public class FunctionPageServerByDB : ServerByDbBase<FunctionPage>
    {
        private readonly ISqlSugarClient db;
        /// <summary>
        /// 
        /// </summary>
        public FunctionPageServerByDB(ISqlSugarClient db) : base(db)
        {
            this.db = db;
        }
    }
}
