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
    public class ServerByDbBase<T> : ICrudBaseByDb<T> where T : class, new()
    {
        public ISqlSugarClient db { get; set; }
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
            //验证权限 能否执行
          //  var tabName = obj.GetType().Name;
            //可以操作这张表的角色集合
          //  List<Role>? roles = db.Queryable<TableRole>().Includes(x => x.InsertRole).Where(x => x.TableName == tabName).First().InsertRole;
            //当前用户的角色集合根上面的的集合是否有交集,有才可以
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
        public async Task<Result<long>> InsertWithSnowFlakeId(T obj)
        {
            var result = new Result<long>();
            try
            {
                result.Data = await db.Insertable(obj).ExecuteReturnSnowflakeIdAsync();
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

        public virtual Task<Result<T>> SelectByRole(Query<T> obj)
        {
            throw new NotImplementedException();
        }


    }

}
