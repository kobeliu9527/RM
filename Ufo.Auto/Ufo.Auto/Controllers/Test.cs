using Microsoft.AspNetCore.Mvc;
using Models.NotEntity;
using Models.SystemInfo;
using SqlSugar;

namespace Ufo.Auto.Controllers
{
    [Route("api/[controller]/[action]")]
    public class Test : ControllerBase
    {
        private readonly ISqlSugarClient db;
        private readonly ILogger<Test> logger;

        public Test(ISqlSugarClient db,ILogger<Test> logger)
        {
            this.db = db;
            this.logger = logger;
        }
        [HttpPost]
        public async Task<Result<int>> Insert222(Company obj)
        {
            //http://localhost:5067/api/Company/Insert
            throw new NotImplementedException();
        }
        [HttpPost("asdfasdfsaf")]
        public  int Delete222qw([FromBody]Company obj)
        {
            return 111;
        }
        [HttpGet,HttpPost]
        public string ReturnError() 
        {
           // logger.LogInformation("fasdf","111112222");
            logger.LogInformation("{pp}:asdfas","11");

            return "1";
        }
        [HttpGet("ReturnError6")]
        public string ReturnError2()
        {
            // logger.LogInformation("fasdf","111112222");
            logger.LogInformation("{pp}:asdfas", "22");
            return "1";
        }
        /// <summary>
        /// asdfas
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetInfo(User user)
        {
            throw new Exception("adfasf");
            List<Role> roles = user.Roles!;
            var ss = db.Queryable<SysModule>()
                    .Includes(x => x.FunctionGroups, y => y.FunctionPages)
                    .ToList();
            ss.ForEach(x => x.FunctionGroups?
            .ForEach(y => y.FunctionPages?
            .RemoveAll(fp =>
            fp.Roles?.Intersect(roles).Count() == 0
            )));
            return "11";
        }
    }
}
