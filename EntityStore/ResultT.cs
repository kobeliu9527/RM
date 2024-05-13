namespace EntityStore
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
}
