using Microsoft.AspNetCore.Components.Authorization;
using Models.Dto;
using Models.NotEntity;
using Models.SystemInfo;
using Shared.AuthenticationStateCustom;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace RMApp.Services
{
    public class AuthenticationStateProviderByClientForMaui : AuthenticationStateProvider, IAuthService
    {
        private readonly IHttpClientFactory factory;
        private readonly ISqlSugarClient db;

        public AuthenticationStateProviderByClientForMaui(IHttpClientFactory factory, ISqlSugarClient db)
        {
            this.factory = factory;
            this.db = db;
        }
        public Task<Result<User>> Register(UserDto user)
        {
            throw new NotImplementedException();
        }

        public Task<Result<string>> WrittenOff(UserDto user)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<string>> SignInAsync(UserDto user)
        {
            Result<string> res = new Result<string>();
            await Task.Delay(100);
            MauiProgram.GlobalInfo.User = new User() {
                RealName = user.RealName,
                Roles = new List<Role>() };
            return res.Ok();
        }

        public async Task<Result<string>> SignOutAsync()
        {
            Result<string> res = new Result<string>();
            await Task.Delay(100);
            MauiProgram.GlobalInfo.User = null;
            return res.Ok();
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsPrincipal principal = new();
            try
            {
                if (MauiProgram.GlobalInfo.User != null)
                {
                    var claims = MauiProgram.GlobalInfo.User.Roles?.Select(x => new Claim(ClaimTypes.Role, x.Name)).ToList();
                    if (claims != null)
                    {
                        claims.Add(new Claim(ClaimTypes.Name, MauiProgram.GlobalInfo.User.SysName));
                        claims.Add(new Claim(ClaimTypes.Sid, MauiProgram.GlobalInfo.User.RealName));
                        claims.Add(new Claim("moduleName", MauiProgram.GlobalInfo.ModelName));
                        claims.Add(new Claim(ClaimTypes.Uri, MauiProgram.GlobalInfo.Url));
                        var identity = new ClaimsIdentity(claims, "ufo");
                        principal.AddIdentity(identity);
                    }
                }
            }
            catch (Exception)
            {
                return new(principal);
            }
            AuthenticationState status = new AuthenticationState(principal);
            NotifyAuthenticationStateChanged(Task.FromResult(status));
            return new(principal);
        }
    }
}
