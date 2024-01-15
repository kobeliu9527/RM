using Models.Dto;
using Models.NotEntity;
using Models.Services.Base;
using Models.SystemInfo;

namespace Models.Services.ServerByHttp
{
    /// <summary>
    /// 通过数据库
    /// </summary>
    public class CompanyGroupServerByHttp : ServerByHttpBase<CompanyGroup>
    {

        /// <summary>
        /// 
        /// </summary>
        public CompanyGroupServerByHttp(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public override Task<Result<CompanyGroup>> SelectByRole(Query<CompanyGroup> obj)
        {
            throw new NotImplementedException();
        }
    }
}
