﻿using Models.Dto;
using Models.NotEntity;
using SqlSugar;
using System.Linq.Expressions;

namespace Models.Services.Base
{
    /// <summary>
    /// 增删改查接口 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICrudBase<T> where T : class, new()
    {
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<Result<int>> Delete(T obj);

        /// <summary>
        /// 新曾一条数据
        /// </summary>
        /// <param name="obj"></param>
        public Task<Result<int>> Insert(T obj);
        /// <summary>
        /// 新曾一条数据:返回雪花Id
        /// </summary>
        /// <param name="obj"></param>
        public Task<Result<long>> InsertWithSnowFlakeId(T obj);
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        Task<Result<List<T>>> SelectAll();
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<Result<List<T>>> SelectBy(Expression<Func<T, bool>> func);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<Result<int>> Update(T obj);
        /// <summary>
        /// 导航查询:根据角色过滤掉
        /// </summary>
        /// <param name="obj">携带的用户角色</param>
        /// <returns></returns>
        Task<Result<T>> SelectByRole(Query<T> obj);
    }
    /// <summary>
    /// 直连数据库增删改查接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICrudBaseByDb<T>  : ICrudBase<T> where T : class, new()
    {
        /// <summary>
        /// 数据库实例
        /// </summary>
         ISqlSugarClient db { get; set; }
    }
    /// <summary>
    /// http请求增删改查接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICrudBaseByHttp<T> : ICrudBase<T> where T : class, new()
    {
        IHttpClientFactory http { get; set; }
    }
}
