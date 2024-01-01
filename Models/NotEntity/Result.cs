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
    public class Result<T>:Result
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
        public Result<T> CatchException(Exception ex, int ErrCode = 200)
        {

            Exception = ex;
            ErrorMsg += ex.Message;
            ErrorPosition = ex.StackTrace;
            StatusCode = ErrCode;
            IsSucceeded = false;
            EndTime = DateTime.Now;
            return this;
        }

        public Result<T> End()
        {
            EndTime = DateTime.Now;
            return this;
        }

        public Result<T> End(T? t)
        {
            EndTime = DateTime.Now;
            Data = t;
            return this;
        }
  
        public Result<T> Fail(string err = "")
        {
            this.IsSucceeded = false;
            EndTime= DateTime.Now;
            ErrorMsg += err;
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
        public string ErrorMsg { get; set; } = "";

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

        public Result End()
        {
            EndTime = DateTime.Now;
            return this;
        }
    }
}