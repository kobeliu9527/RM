using System.Text.Json.Serialization;

namespace EntityStore
{
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
