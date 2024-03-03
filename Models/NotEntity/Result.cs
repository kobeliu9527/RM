using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.NotEntity
{

    /// <summary>
    /// 结果类
    /// </summary>
    /// <typeparam name="T">返回的数据</typeparam>
    public class Result<T> : Result
    {

        /// <summary>
        /// 数据
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// 捕获到错误
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="ErrCode"></param>
        /// <returns></returns>
        public Result<T> HasException(Exception ex, int ErrCode = 200)
        {
            Exception = ex;
            ReturnMsg += ex.Message;
            ErrorPosition = ex.StackTrace;
            StatusCode = ErrCode;
            IsSucceeded = false;
            EndTime = DateTime.Now;
            return this;
        }

        public Result<T> Ok()
        {
            EndTime = DateTime.Now;
            return this;
        }

        public Result<T> Ok(T? data, string msg = "")
        {
            EndTime = DateTime.Now;
            ReturnMsg += msg;
            Data = data;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="err"></param>
        /// <returns></returns>
        public new Result<T> Fail(string err = "")
        {
            IsSucceeded = false;
            EndTime = DateTime.Now;
            ReturnMsg += err;
            return this;//fail
        }


    }
    /// <summary>
    /// 结果类
    /// </summary>
    public class Result
    {
        /// <summary>
        /// 执行成功
        /// </summary>
        public bool IsSucceeded { get; set; } = true;

        /// <summary>
        /// 状态码:通常负数表示异常,正数表示正常,0表示没有异常或者没有结果
        /// </summary>
        public int StatusCode { get; set; } = 0;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ReturnMsg { get; set; } = "";

        /// <summary>
        /// 错误代码位置
        /// </summary>
        public string? ErrorPosition { get; set; }


        /// <summary>
        /// 附加数据
        /// </summary>
        public object? Extras { get; set; }

        /// <summary>
        /// 结果开始时间
        /// </summary>
        public DateTime StartTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 结果开始时间
        /// </summary>
        public DateTime EndTime { get; set; }

        ///// <summary>
        ///// 异常
        ///// </summary>
        [JsonIgnore]
        public Exception? Exception { get; set; }
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="err"></param>
        /// <returns></returns>
        public Result Fail(string err = "失败")
        {
            IsSucceeded = false;
            EndTime = DateTime.Now;
            ReturnMsg += err;
            return this;//fail
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public Result HasException(Exception e)
        {
            IsSucceeded = false;
            EndTime = DateTime.Now;
            Exception = e;
            ErrorPosition = e.StackTrace;
            ReturnMsg += e.Message;
            return this;
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public Result Ok(string msg = "成功")
        {
            EndTime = DateTime.Now;
            ReturnMsg += msg;
            return this;//fail
        }
    }
}