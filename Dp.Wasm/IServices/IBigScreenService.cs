using BootstrapBlazor.Components;
using EntityStore;
using Microsoft.AspNetCore.Components;
using NetTaste;
using SharedPage;
using SharedPage.Model;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;

namespace Dp.Wasm.IServices
{
    public interface IBigScreenService
    {
        /// <summary>
        /// 
        /// </summary>
        Task<Result<BigScreen>> GetBigScreen(long id);
        /// <summary>
        /// 获取当前用户能够查看的所有的屏幕
        /// </summary>
        /// <returns></returns>
        Task<Result<List<BigScreen>>> GetBigScreens();
        /// <summary>
        /// 保存(更新)一个对象,所以必须要有id
        /// </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        Task<Result> Save(BigScreen bs);
        Task<Result<long>> Insert(BigScreen bs);
        Task<Result> Delete(long id);
    }
    /// <summary>
    /// 通过
    /// </summary>
    public class BigScreenByLocal : IBigScreenService
    {
        public JsInterOp JsOp { get; set; }
        public BigScreenByLocal(JsInterOp JsOp)
        {
            this.JsOp = JsOp;
        }
        /// <inheritdoc/>
        public async Task<Result<BigScreen>> GetBigScreen(long id)
        {
            var res = new Result<BigScreen>();
            var obj = await JsOp.getItem<BigScreen>(id.ToString());
            if (obj != null)
            {
                return res.Ok(obj);
            }
            else
            {
                return res.Fail("获取失败");
            }

        }

        public async Task<Result<List<BigScreen>>> GetBigScreens()
        {
            var res = new Result<List<BigScreen>>();
            var obj = await JsOp.getItems<BigScreen>();
            if (obj != null && obj.Count > 0)
            {
                return res.Ok(obj);
            }
            else
            {
                return res.Fail("获取失败");
            }
        }

        public async Task<Result> Save(BigScreen bs)
        {
            //  Result result = new Result();

            var result = await JsOp.setItem(bs.Id.ToString(), bs);
            return result;
        }
        /// <summary>
        /// 由于不是数据库,所以要先定义主键
        /// </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        public async Task<Result<long>> Insert(BigScreen bs)
        {
            long id = 0;
            var has_ids1 = await JsOp.getKeys();
            var longId = has_ids1.Select(id =>
                                                 {
                                                     if (long.TryParse(id, out long result))
                                                     {
                                                         return result;
                                                     }
                                                     else
                                                     {
                                                         return (long?)null;
                                                     }
                                                 })
                                                 .Where(id => id.HasValue)
                                                 .Max();

            if (longId.HasValue)
            {
                id = longId.Value + 1;
            }
            else
            {
                id = 1;
            }
            Result<long> res = new();
            bs.Id = id;
            var result = await JsOp.setItem(id.ToString(), bs);
            res.IsSucceeded = result.IsSucceeded;
            res.Data = bs.Id;
            return res;
        }

        public async Task<Result> Delete(long bs)
        {
            var result = await JsOp.removeItem(bs.ToString());
            return result;
        }
    }
    /// <summary>
    /// 通过http请求获取数据
    /// </summary>
    public class BigScreenByHttp : IBigScreenService
    {
        private readonly IHttpClientFactory httpClientFactory;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public BigScreenByHttp(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        /// <inheritdoc/>
        public async Task<Result<BigScreen>> GetBigScreen(long id)
        {
            try
            {
                using HttpClient http = httpClientFactory.CreateClient("http");
                var response = await http.PostAsJsonAsync<long>($"api/{nameof(BigScreen)}/{nameof(GetBigScreen)}", id);
                response.EnsureSuccessStatusCode();
                var ss = await response.Content.ReadFromJsonAsync<Result<BigScreen>>();
                return ss ?? new Result<BigScreen>().Fail();
            }
            catch (Exception ex)
            {
                return new Result<BigScreen>().HasException(ex);
            }

        }
        /// <inheritdoc/>
        public async Task<Result<List<BigScreen>>> GetBigScreens()
        {
            try
            {
                using HttpClient http = httpClientFactory.CreateClient("http");
                var response = await http.PostAsJsonAsync<int>($"api/{nameof(BigScreen)}/{nameof(GetBigScreens)}", 0);
                response.EnsureSuccessStatusCode();
                var ss = await response.Content.ReadFromJsonAsync<Result<List<BigScreen>>>();
                return ss ?? new Result<List<BigScreen>>().Fail();
            }
            catch (Exception ex)
            {
                return new Result<List<BigScreen>>().HasException(ex);
            }


        }

        public async Task<Result> Save(BigScreen bs)
        {
            try
            {
                using HttpClient http = httpClientFactory.CreateClient("http");
                var response = await http.PostAsJsonAsync<BigScreen>($"api/{nameof(BigScreen)}/{nameof(Save)}", bs);
                response.EnsureSuccessStatusCode();
                var res = await response.Content.ReadFromJsonAsync<Result>();
                return res ?? new Result().Fail();
            }
            catch (Exception ex)
            {
                return new Result().HasException(ex);
            }
        }
        /// <inheritdoc/>
        public async Task<Result<long>> Insert(BigScreen bs)
        {
            try
            {
                using HttpClient http = httpClientFactory.CreateClient("http");
                var response = await http.PostAsJsonAsync<BigScreen>($"api/{nameof(BigScreen)}/{nameof(Insert)}", bs);
                response.EnsureSuccessStatusCode();
                var res = await response.Content.ReadFromJsonAsync<Result<long>>();
                return res ?? new Result<long>().Fail();
            }
            catch (Exception ex)
            {
                return new Result<long>().HasException(ex);
            }
        }
        /// <inheritdoc/>

        public async Task<Result> Delete(long id)
        {
            try
            {
                using HttpClient http = httpClientFactory.CreateClient("http");
                var response = await http.PostAsJsonAsync<long>($"api/{nameof(BigScreen)}/{nameof(Delete)}", id);
                response.EnsureSuccessStatusCode();
                var res = await response.Content.ReadFromJsonAsync<Result>();
                return res ?? new Result().Fail();
            }
            catch (Exception ex)
            {
                return new Result().HasException(ex);
            }
        }
    }
}
