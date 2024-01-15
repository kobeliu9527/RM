using Models.Dto;
using Models.NotEntity;
using Models.Services.Base;
using Models.SystemInfo;
using SqlSugar;
using System.Net.Http.Json;

namespace Models.Services.ServerByHttp
{
    /// <summary>
    /// 获取公司数据通过http请求
    /// </summary>
    public class CompanyServerByHttp : ServerByHttpBase<Company>
    {
        /// <summary>
        /// 
        /// </summary>
        public CompanyServerByHttp(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cg"></param>
        public void Update(CompanyGroup cg)
        {
            //执行http请求
            using HttpClient http = httpClientFactory.CreateClient("http");
        }

        public override Task<Result<Company>> SelectByRole(Query<Company> obj)
        {
            throw new NotImplementedException();
        }
    }
}
