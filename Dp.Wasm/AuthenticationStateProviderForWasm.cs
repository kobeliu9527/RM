using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Dp.Wasm
{
    public class AuthenticationStateProviderForWasm: AuthenticationStateProvider
    {

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(500);
            ClaimsPrincipal principal = new();//身份证,这个时候还不行,还需要权限身份
            //return new(principal);
            var claims = new List<Claim>();//
            var identity = new ClaimsIdentity(claims, "ufo");//具体什么身份:公民  干部  警察
            principal.AddIdentity(identity);//
            AuthenticationState a = new AuthenticationState(principal);
            NotifyAuthenticationStateChanged(Task.FromResult(a));
            return new(principal);
        }
    }
}
