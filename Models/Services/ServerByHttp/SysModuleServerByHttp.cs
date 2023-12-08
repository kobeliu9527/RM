using Models.Dto;
using Models.NotEntity;
using Models.Services.Base;
using Models.System;
using SqlSugar;
using System.Net.Http.Json;

namespace Models.Services.ServerByHttp
{
    /// <summary>
    /// 通过数据库
    /// </summary>
    public class SysModuleServerByHttp : ServerByHttpBase<SysModule>
    {
        /// <summary>
        /// 
        /// </summary>
        public SysModuleServerByHttp(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }



        //public async override Task<Result<SysModule>> SelectNav(Query<SysModule> obj)
        //{
        //    Result<SysModule> res = new();
        //    try
        //    {
        //        using HttpClient http = httpClientFactory.CreateClient("http");
        //        var reshttp = await http.PostAsJsonAsync($"{nameof(SysModule)}/{nameof(SelectNav)}", obj);
        //        if (reshttp.IsSuccessStatusCode)
        //        {
        //            var resread = await reshttp.Content.ReadFromJsonAsync<Result<SysModule>>();
        //            res = resread ?? res.Fail("序列化为Result<SysModule>的时候失败");
        //        }
        //        else
        //        {
        //            return res.Fail("请求失败");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return res.CatchException(e);
        //    }
        //    return res.End();
        //}
    }
}
