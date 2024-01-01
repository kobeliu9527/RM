using Models.CustomConverter;
using Models.Dto;
using Models.NotEntity;
using Models.System;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Text.Json;

namespace Models.Services.Base
{
    /// <summary>
    /// 公司的crud by http
    /// </summary>
    public class ServerByHttpBase<T> : ICrudBase<T> where T : class, new()
    {
        protected readonly IHttpClientFactory httpClientFactory;//ControName
        protected readonly string ControName;//ControName

        public ServerByHttpBase(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<Result<int>> Insert(T obj)
        {
            Result<int> res = new();
            try
            {
                var ControName = obj.GetType().Name;
                using HttpClient http = httpClientFactory.CreateClient("http");
                var reshttp = await http.PostAsJsonAsync($"{ControName}/{nameof(Insert)}", obj);
                if (reshttp.IsSuccessStatusCode)
                {

                   //JsonSerializerOptions op=new JsonSerializerOptions();
                   // op.Converters.Add(new JsonTextConverter("yyyy-MM-dd HH:mm:ss"));

                    var resread = await reshttp.Content.ReadFromJsonAsync<Result<int>>();
                    //var resread = await reshttp.Content.ReadFromJsonAsync<Result<int>>(op);
                    res = resread ?? res.Fail("将返回值序列化的时候失败");
                }
                else
                {
                    return res.Fail("请求失败");
                }
            }
            catch (Exception e)
            {
                return res.CatchException(e);
            }
            return res.End();
            //using HttpClient http = httpClientFactory.CreateClient("http");

            ////执行http请求
            //var res = await http.PostAsJsonAsync("api/Company/Insert", new Company());
            //if (res.IsSuccessStatusCode)
            //{
            //    var res2 = await res.Content.ReadFromJsonAsync<Result<int>>();
            //}
            //return await Task.FromResult(new Result<int>() { Data = 22 });
        }
        public Task<Result<long>> InsertWithSnowFlakeId(T obj)
        {
            throw new NotImplementedException();
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

        public async virtual Task<Result<T>> SelectByRole(Query<T> obj)
        {
            Result<T> res = new();
            try
            {
                var ControName = obj.QueryDto.GetType().Name;
                using HttpClient http = httpClientFactory.CreateClient("http");
                var reshttp = await http.PostAsJsonAsync($"{ControName}/{nameof(SelectByRole)}", obj);
                if (reshttp.IsSuccessStatusCode)
                {
                    var resread = await reshttp.Content.ReadFromJsonAsync<Result<T>>();
                    res = resread ?? res.Fail("序列化为Result<SysModule>的时候失败");
                }
                else
                {
                    return res.Fail("请求失败");
                }
            }
            catch (Exception e)
            {
                return res.CatchException(e);
            }
            return res.End();
        }


    }
}
