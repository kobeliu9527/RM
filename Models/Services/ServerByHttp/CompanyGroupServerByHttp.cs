using Models.Dto;
using Models.NotEntity;
using Models.Services.Base;
using Models.System;

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

        public override Task<Result<CompanyGroup>> SelectNav(Query<CompanyGroup> obj)
        {
            throw new NotImplementedException();
        }
    }
}
