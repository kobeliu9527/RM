using Models.Dto;
using Models.NotEntity;
using Models.System;
using SqlSugar;
using System.Linq.Expressions;

namespace Models.Services.Base
{
    /// <summary>
    /// base crud by db
    /// </summary>
    public abstract class ServerByDbBase<T> : ICrudBase<T> where T : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly ISqlSugarClient db;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        public ServerByDbBase(ISqlSugarClient db)
        {
            this.db = db;
        }

        public async virtual Task<Result<int>> Insert(T obj)
        {
            var result = new Result<int>();
            try
            {
                result.Data = await db.Insertable(obj).ExecuteCommandAsync();
            }
            catch (Exception ex)
            {
                return result.CatchException(ex);
            }
            return result;
        }
        public async virtual Task<Result<int>> Delete(T obj)
        {
            var result = new Result<int>();
            try
            {
                result.Data = await db.Deleteable(obj).ExecuteCommandAsync();
            }
            catch (Exception ex)
            {
                return result.CatchException(ex);
            }
            return result;
        }
        public async virtual Task<Result<int>> Update(T obj)
        {
            var result = new Result<int>();
            try
            {
                result.Data = await db.Updateable(obj).ExecuteCommandAsync();
            }
            catch (Exception ex)
            {
                return result.CatchException(ex);
            }
            return result;
        }
        public async virtual Task<Result<List<T>>> SelectAll()
        {
            var result = new Result<List<T>>();
            try
            {
                result.Data = await db.Queryable<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                return result.CatchException(ex);
            }
            return result;
        }
        public async virtual Task<Result<List<T>>> SelectBy(Expression<Func<T, bool>> func)
        {
            var result = new Result<List<T>>();
            try
            {
                result.Data = await db.Queryable<T>().Where(func).ToListAsync();
            }
            catch (Exception ex)
            {
                return result.CatchException(ex);
            }
            return result;
        }

        public abstract Task<Result<T>> SelectNav(Query<T> obj);
    }

}
