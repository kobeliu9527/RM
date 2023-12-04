using Microsoft.AspNetCore.Mvc;
using Models.NotEntity;
using Models.System;
using NSwag.Annotations;
using System.Linq.Expressions;
using Ufo.Auto.Client.Services.Base;

namespace Ufo.Auto.Controllers
{
    /// <summary>
    /// CRUD公司
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class CompanyController : ControllerBase, ICrudBase<Company>
    {

        private readonly ICrudBase<Company> companyServer;

        /// <summary>
        /// 
        /// </summary>
        public CompanyController(ICrudBase<Company> companyServer)
        {
            this.companyServer = companyServer;
        }
        [HttpPost]
        public async Task<Result<int>> Insert(Company obj)
        {
            //http://localhost:5067/api/Company/Insert
            return await companyServer.Insert(obj);
        }
        [HttpPost]
        public Task<Result<int>> Delete(Company obj)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public Task<Result<List<Company>>> SelectAll()
        {
            throw new NotImplementedException();
        }
        [NonAction]
        public Task<Result<List<Company>>> SelectBy(Expression<Func<Company, bool>> func)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public Task<Result<int>> Update(Company obj)
        {
            throw new NotImplementedException();
        }
    }
}
