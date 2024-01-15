using Models.Dto;
using Models.NotEntity;
using Models.Services.Base;
using Models.SystemInfo;
using SqlSugar;

namespace Models.Services.ServerByDb
{
    /// <summary>
    /// 通过数据库
    /// </summary>
    public class CompanyServerByDB : ServerByDbBase<Company>
    {

        /// <summary>
        /// 
        /// </summary>
        public CompanyServerByDB(ISqlSugarClient db) : base(db)
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

        public override Task<Result<Company>> SelectByRole(Query<Company> obj)
        {
            throw new NotImplementedException();
        }
    }
}
