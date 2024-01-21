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
    public class FunctionPageServerByDB : ServerByDbBase<FunctionPage>
    {
        /// <summary>
        /// 
        /// </summary>
        public FunctionPageServerByDB(ISqlSugarClient db) : base(db)
        {
        }

        public override Task<Result<FunctionPage>> SelectByRole(Query<FunctionPage> obj)
        {
            throw new NotImplementedException();
        }
    }
}
