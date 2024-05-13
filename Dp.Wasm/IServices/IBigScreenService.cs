using EntityStore;
using Microsoft.AspNetCore.Components;
using SharedPage;
using SharedPage.Model;
using System.Diagnostics.CodeAnalysis;

namespace Dp.Wasm.IServices
{
    public interface IBigScreenService
    {
        /// <summary>
        /// 
        /// </summary>
        Task<Result<BigScreen>> GetBigScreen(long id);
        Task<Result<List<BigScreen>>> GetBigScreens();
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
    public class BigScreenByDb //: IBigScreenService
    {
        public Task<Result<BigScreen>> GetBigScreen(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<BigScreen>>> GetBigScreens()
        {
            throw new NotImplementedException();
        }

        public Task<Result> Save(BigScreen bs)
        {
            throw new NotImplementedException();
        }
    }
}
