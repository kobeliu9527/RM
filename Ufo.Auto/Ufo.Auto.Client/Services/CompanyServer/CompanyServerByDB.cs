using Models.NotEntity;
using Models.System;
using SqlSugar;
using Ufo.Auto.Client.Services.Base;

namespace Ufo.Auto.Client.Services.CompanyServer
{
    /// <summary>
    /// 通过数据库
    /// </summary>
    public class CompanyServerByDB : ServerByDbBase<Company>
    {

        /// <summary>
        /// 
        /// </summary>
        public CompanyServerByDB(ISqlSugarClient db):base(db) 
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cg"></param>
        public void Update(CompanyGroup cg)
        {
            db.Updateable(cg);
        }

   
    }
}
