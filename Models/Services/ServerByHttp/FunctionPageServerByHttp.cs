﻿using Models.Dto;
using Models.NotEntity;
using Models.Services.Base;
using Models.System;

namespace Models.Services.ServerByHttp
{
    /// <summary>
    /// 通过数据库
    /// </summary>
    public class FunctionPageServerByHttp : ServerByHttpBase<FunctionPage>
    {

        /// <summary>
        /// 
        /// </summary>
        public FunctionPageServerByHttp(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public override Task<Result<FunctionPage>> SelectNav(Query<FunctionPage> obj)
        {
            throw new NotImplementedException();
        }
    }
}