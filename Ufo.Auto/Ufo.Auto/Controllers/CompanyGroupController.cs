using Microsoft.AspNetCore.Mvc;
using Models.NotEntity;
using Models.System;
using System.Linq.Expressions;
using Ufo.Auto.Client.Services.Base;


namespace Ufo.Auto.Controllers
{
    /// <summary>
    /// CRUD集团公司
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class CompanyGroupController : ControllerBase, ICrudBase<CompanyGroup>
    {
        private readonly ICrudBase<CompanyGroup> companyGroupServer;

        /// <summary>
        /// 
        /// </summary>
        public CompanyGroupController(ICrudBase<CompanyGroup> CompanyGroupServer)
        {
            companyGroupServer = CompanyGroupServer;
        }

        [HttpPost]
        public async Task<Result<int>> Insert(CompanyGroup obj)
        {
            return await companyGroupServer.Insert(obj);
        }
        [HttpPost]
        public Task<Result<int>> Delete(CompanyGroup obj)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public Task<Result<List<CompanyGroup>>> SelectAll()
        {
            throw new NotImplementedException();
        }
        [NonAction]
        public Task<Result<List<CompanyGroup>>> SelectBy(Expression<Func<CompanyGroup, bool>> func)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public Task<Result<int>> Update(CompanyGroup obj)
        {
            throw new NotImplementedException();
        }
    }
}
