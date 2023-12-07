using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.NotEntity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AutoServer.Controllers
{
    [Route("api/[controller]/[action]")]
    public class LoginController : Controller
    {
        private readonly Appsettings appsettings;

        public LoginController(IOptionsSnapshot<Appsettings> options)
        {
            this.appsettings = options.Value;
        }
        [HttpPost]
        public async Task<Result<string>> GetJwt()
        {
            Result<string> res = new Result<string>();
            var ss = HttpContext.User.Claims.ToList();
            var cliaims = new List<Claim>() {
                new Claim(ClaimTypes.Name,"Name"),
                new Claim(ClaimTypes.Sid,"Sid"),
                new Claim(ClaimTypes.Role,"leader"),
                new Claim(ClaimTypes.Role,"gust")
            };
            var credential = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appsettings.JwtOption.SecurityKey)), SecurityAlgorithms.HmacSha256);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(expires: DateTime.Now.AddDays(1), claims: cliaims, signingCredentials: credential);
            JwtSecurityTokenHandler hander = new JwtSecurityTokenHandler();
            var token = hander.WriteToken(jwtSecurityToken);
            await Task.Delay(100);
            res.Data = token;
            return res.End();
        }
        [Authorize]
        [HttpPost]
        public IResult TestAuth()
        {
            var ss = HttpContext.User.Claims.ToList();
            return Results.Ok("ok");
        }
    }
}
