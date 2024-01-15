using Models.Dto;
using Models.NotEntity;
using Models.Services.Base;
using Models.SystemInfo;
using SqlSugar;

namespace Models.Services.ServerByDb
{
    /// <summary>
    /// 通过数据库,Controller或者CS客户端,直接用这个操作数据库;前后分离
    /// </summary>
    public class UserServerByDB : ServerByDbBase<User>
    {
        /// <summary>
        /// 
        /// </summary>
        public UserServerByDB(ISqlSugarClient db) : base(db)
        {
        }

        public override Task<Result<User>> SelectByRole(Query<User> obj)
        {
            throw new NotImplementedException();
        }
    }
}
