
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.Dto;
using Models.NotEntity;
using Models.Services.Base;
using Models.SystemInfo;
using Shared.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace BlazorAuto.Controllers
{
    /// <summary>
    /// 登录注册相关
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class AuthController : Controller//, //ICrudBase<User>
    {
        private readonly Appsettings appsettings;
        private readonly ISqlSugarClient db;

        /// <summary>
        /// 登录注册相关
        /// </summary>
        /// <param name="options"></param>
        /// <param name="db"></param>
        public AuthController(IOptionsSnapshot<Appsettings> options, ISqlSugarClient db)
        {
            appsettings = options.Value;
            this.db = db;
        }
        /// <summary>
        /// 注册  [Authorize(Roles ="hr,admin")]
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result<User>> Register([FromBody] UserDto user)
        {
            Result<User> result = new Result<User>();
            try
            {
                var has = await db.Queryable<User>().SingleAsync(x => x.SysName == user.SysName);
                if (has != null)
                {
                    return result.Fail("用户名重复");
                }
                else
                {
                    User newUser = new User()
                    {
                        RealName = user.RealName,
                        SysName = user.SysName,
                        PassWord = user.PassWord.ToHashPassword(),
                        Roles = user.Roles.Select(x => new Role() { Id = x.Id, Name = x.Name }).ToList()
                    };
                    List<User> list = new List<User>() { newUser };
                    var obj = await db.InsertNav(list).Include(x => x.Roles).ExecuteReturnEntityAsync();
                    obj.PassWord = "";
                    return result.Ok(obj, "注册成功");
                }
            }
            catch (Exception e)
            {
                return result.HasException(e);
            }
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userdto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result<string>> Login([FromBody] UserDto userdto)
        {
            Result<string> res = new Result<string>();
            userdto.PassWord=userdto.PassWord.ToHashPassword();
            var user = await db.Queryable<User>()
                            .Includes(x => x.Roles)
                            .FirstAsync(x => x.SysName == userdto.SysName && x.PassWord == userdto.PassWord)
                            ;
            if (user != null)
            {
                var j = GetJwt(user, appsettings.JwtOption, userdto.ModuleName, userdto.Url);
                return res.Ok(j.Data);
            }
            else
            {
                return res.Fail();
            }
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result<string>> UpdatePassWord([FromBody] UserDto userDto)
        {
            Result<string> res = new Result<string>();
            var u = await db.Queryable<User>()
                            .Includes(x => x.Roles)
                            .FirstAsync(x => x.SysName == userDto.SysName && x.PassWord == userDto.PassWord)
                            ;
            if (u != null)
            {
                u.PassWord = userDto.NewPassWord;
                await db.Updateable(u).ExecuteCommandAsync();
                var j = GetJwt(u, appsettings.JwtOption, userDto.ModuleName);
                return res.Ok(j.Data);
            }
            else
            {
                return res.Fail("原始用户名或者密码错误");
            }
        }
        /// <summary>
        /// 获取当前角色在一个系统下的所有页面
        /// </summary>
        [HttpGet]
        //[Authorize]
        public async void GetFuncPageByModule(string ModuleNmae)
        {
            var ip = HttpContext.Connection.RemoteIpAddress;
            var user = HttpContext.User.Identity?.Name;
            var cliams = HttpContext.User.Claims.ToList().FindAll(x => x.Type == ClaimTypes.Role).Select(x => new Role() { Name = x.Value }).ToList();

            var ss = await db.Queryable<Module>().Includes(x => x.FunctionGroups, y => y.FunctionPages).Where(x => x.Name == "").ToListAsync();

            ss.ForEach(x => x.FunctionGroups?
            .ForEach(y => y.FunctionPages?
            .RemoveAll(fp =>
            fp.Roles?.Intersect(cliams).Count() == 0
            )));

        }
        [NonAction]
        public Result<string> GetJwt(User user, JwtOption op, string modName,string url="")
        {
            Result<string> res = new Result<string>();
            try
            {
                if (user.Roles != null)
                {
                    var cliaims = new List<Claim>();
                    cliaims.Add(new Claim(ClaimTypes.Name, user.SysName));
                    cliaims.Add(new Claim(ClaimTypes.Sid, user.RealName));
                    cliaims.Add(new Claim("moduleName", modName));
                    cliaims.Add(new Claim(ClaimTypes.Uri, url));
                    foreach (var item in user.Roles)
                    {
                        cliaims.Add(new Claim(ClaimTypes.Role, item.Name));
                    }
                    var credential = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appsettings.JwtOption.SecurityKey)), SecurityAlgorithms.HmacSha256);
                    JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(expires: DateTime.Now
                        .AddYears(op.ExpiresYear)
                        .AddMonths(op.ExpiresMouth)
                        .AddDays(op.ExpiresDay)
                        .AddHours(op.ExpiresHour)
                        .AddMinutes(op.ExpiresMinutes)
                        , claims: cliaims, signingCredentials: credential);
                    JwtSecurityTokenHandler hander = new();
                    var token = hander.WriteToken(jwtSecurityToken);
                    res.Data = token;
                }
            }
            catch (Exception e)
            {
                return res.HasException(e);
            }
            return res.Ok();
        }
        [Authorize]
        [HttpPost]
        public IResult TestAuth()
        {
            var ss = HttpContext.User.Claims.ToList();
            return Results.Ok("ok");
        }
        [NonAction]
        public Task<Result<int>> Delete(User obj)
        {
            throw new NotImplementedException();
        }
        [NonAction]
        public Task<Result<int>> Insert(User obj)
        {
            throw new NotImplementedException();
        }
        [NonAction]
        public Task<Result<long>> InsertWithSnowFlakeId(User obj)
        {
            throw new NotImplementedException();
        }
        [NonAction]
        public Task<Result<List<User>>> SelectAll()
        {
            throw new NotImplementedException();
        }
        [NonAction]
        public Task<Result<List<User>>> SelectBy(Expression<Func<User, bool>> func)
        {
            throw new NotImplementedException();
        }
        [NonAction]
        public Task<Result<int>> Update(User obj)
        {
            throw new NotImplementedException();
        }
        [NonAction]
        public Task<Result<User>> SelectByRole(Query<User> obj)
        {
            throw new NotImplementedException();
        }
    }
}
