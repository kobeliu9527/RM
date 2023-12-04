using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ufo.Auto.Controllers
{
    [Route("api/[controller]/[action]")]
    public class LoginController : Controller
    {
        public const string key = "asfsafasgsdagsafasfasfasfsagasdf";
        public LoginController()
        {

        }
        [HttpPost]
        public IResult GetJwt()
        {
            var ss = HttpContext.User.Claims.ToList();
            var cliaims = new List<Claim>() {
                new Claim(ClaimTypes.Name,"admin1"),
                new Claim(ClaimTypes.Sid,"admin2"),
                new Claim(ClaimTypes.Role,"leader"),
                new Claim(ClaimTypes.Role,"gust")
            };
            var credential = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(expires: DateTime.Now.AddDays(1), claims: cliaims, signingCredentials: credential);
            JwtSecurityTokenHandler hander = new JwtSecurityTokenHandler();
            var token = hander.WriteToken(jwtSecurityToken);
            return Results.Ok(token);
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
