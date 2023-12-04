using MatBlazor;
using Models.NotEntity;
using Models.System;
using SqlSugar;
using System.Net.Http.Json;
using Ufo.Auto.Client.Services.Base;

namespace Ufo.Auto.Client.Services.CompanyServer
{
    /// <summary>
    /// 获取公司数据通过http请求
    /// </summary>
    public class CompanyServerByHttp : ServerByHttpBase<Company>
    {
        private readonly IHttpClientFactory httpClientFactory;

        /// <summary>
        /// 
        /// </summary>
        public CompanyServerByHttp(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
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

   

    }
}
