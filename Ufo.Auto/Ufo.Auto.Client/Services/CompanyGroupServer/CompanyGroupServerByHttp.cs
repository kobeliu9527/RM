using Models.NotEntity;
using Models.SystemInfo;
using Ufo.Auto.Client.Services.Base;

namespace Ufo.Auto.Client.Services.CompanyGroupServer
{
    /// <summary>
    /// 通过数据库
    /// </summary>
    public class CompanyGroupServerByHttp : ServerByHttpBase<CompanyGroup>
    {
        private readonly IHttpClientFactory httpClientFactory;

        /// <summary>
        /// 
        /// </summary>
        public CompanyGroupServerByHttp(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

    }
    /// <summary>
    /// 通过数据库
    /// </summary>
    public class FunctionPageServerByHttp : ServerByHttpBase<FunctionPage>
    {
        private readonly IHttpClientFactory httpClientFactory;

        /// <summary>
        /// 
        /// </summary>
        public FunctionPageServerByHttp(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

    }
}
