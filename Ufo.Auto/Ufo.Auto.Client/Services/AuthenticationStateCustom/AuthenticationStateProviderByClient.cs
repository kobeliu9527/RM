using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Ufo.Auto.Client.Services.AuthenticationStateCustom
{
    public class AuthenticationStateProviderByClient: AuthenticationStateProvider
    {
        private readonly ILocalStorageService local;

        public AuthenticationStateProviderByClient(ILocalStorageService local)
        {
            this.local = local;
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsPrincipal principal = new();
            try
            {
                var jwt = await local.GetItemAsStringAsync("token");

                if (!string.IsNullOrEmpty(jwt))
                {
                    JwtSecurityTokenHandler handler = new();
                    var jwttoken = handler.ReadJwtToken(jwt);
                    var claims = jwttoken.Claims;
                    
                    var identity = new ClaimsIdentity(claims, "ufo");
                    principal.AddIdentity(identity);
                }
                else
                {
                    await local.SetItemAsStringAsync("token", 
"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4xIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc2lkIjoiYWRtaW4yIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbImxlYWRlciIsImd1c3QiXSwiZXhwIjoxNzAxNzg0NTc3fQ.rQLVupOVhTq42LE5DKEfa5IedWcc1YinNKRj6UvAFrA");
                }
            }
            catch (Exception ex)
            {

        
               return new(principal);
            }
            AuthenticationState a=new AuthenticationState(principal);
            NotifyAuthenticationStateChanged(Task.FromResult(a));
            return new(principal);
        }
    }
}
