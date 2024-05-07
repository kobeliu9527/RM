using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Dp.Wasm
{
    public class AuthenticationStateProviderForWasm: AuthenticationStateProvider
    {

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(500);
            ClaimsPrincipal principal = new();
            var claims = new List<Claim>();
            var identity = new ClaimsIdentity(claims, "ufo");
            principal.AddIdentity(identity);
            AuthenticationState a = new AuthenticationState(principal);
            NotifyAuthenticationStateChanged(Task.FromResult(a));
            return new(principal);
        }
    }
}
