using Models.NotEntity;
using Models.System;
using System.Linq.Expressions;
using System.Net.Http.Json;

namespace Ufo.Auto.Client.Services.Base
{
    /// <summary>
    /// 公司的crud by http
    /// </summary>
    public abstract class ServerByHttpBase<T> : ICrudBase<T> where T : class, new()
    {
        protected readonly IHttpClientFactory httpClientFactory;

        public ServerByHttpBase(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<Result<int>> Insert(T obj)
        {
            using HttpClient http = httpClientFactory.CreateClient("http");

            //执行http请求
            var res = await http.PostAsJsonAsync("api/Company/Insert", new Company());
            if (res.IsSuccessStatusCode)
            {
                var res2 = await res.Content.ReadFromJsonAsync<Result<int>>();
            }
            return await Task.FromResult(new Result<int>() { Data = 22 });
        }

        public Task<Result<int>> Delete(T obj)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<T>>> SelectAll()
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<T>>> SelectBy(Expression<Func<T, bool>> func)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
