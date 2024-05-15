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
        /// <summary>
        /// 根据结果返回
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Result<T> SetResult(T obj)
        {
            if (obj != null)
            {
                this.Data = obj;
                this.IsSucceeded = true;
            }
            else
            {
                this.Data = default;
                this.IsSucceeded = false;
            }
            return this;
        }
    }
}
