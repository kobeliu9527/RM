using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Models.Dto;
using Models.NotEntity;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Shared.AuthenticationStateCustom
{
    public class AuthenticationStateProviderByClient : AuthenticationStateProvider, IAuthService
    {
        private readonly ILocalStorageService local;
        private readonly IHttpClientFactory httpClientFactory;

        public AuthenticationStateProviderByClient(ILocalStorageService local, IHttpClientFactory httpClientFactory)
        {
            this.local = local;
            this.httpClientFactory = httpClientFactory;
        }
        /// <summary>
        /// 从浏览器中获取tokens
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetTokenAsync()
        {
            try
            {
                return await local.GetItemAsStringAsync("token");

            }
            catch (Exception ex)
            {
                return "";
            }
        }
        private async Task SetTokenAsync(string token)
        {
            try
            {
                await local.SetItemAsStringAsync("token", token);

            }
            catch (Exception ex)
            {
                await Task.CompletedTask;
            }
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsPrincipal principal = new();
            try
            {
                var jwt = await GetTokenAsync();

                if (!string.IsNullOrEmpty(jwt))
                {
                    JwtSecurityTokenHandler handler = new();
                    var jwttoken = handler.ReadJwtToken(jwt);
                    var claims = jwttoken.Claims;
                    var identity = new ClaimsIdentity(claims, "ufo");
                    principal.AddIdentity(identity);
                }
            }
            catch (Exception ex)
            {
                return new(principal);
            }
            AuthenticationState a = new AuthenticationState(principal);
            NotifyAuthenticationStateChanged(Task.FromResult(a));
            return new(principal);
        }

        public async Task<Result<string>> SignInAsync(UserDto user)
        {
            //
            using HttpClient http = httpClientFactory.CreateClient("http");
            var res = await http.PostAsJsonAsync("auth/login", user);
            if (res.IsSuccessStatusCode)
            {
                var token = await res.Content.ReadFromJsonAsync<Result<string>>();
                if (token != null && token.IsSucceeded)
                {
                    await SetTokenAsync(token.Data!);
                    return new Result<string>() { Data = "登录成功" };
                }
                else
                {
                    return new Result<string>() { Data = "登录失败", IsSucceeded = false };
                }
            }
            return new Result<string>() { Data = "登录失败", IsSucceeded = false };
        }

        public async Task<Result<string>> SignOutAsync()
        {
            try
            {
                await local.SetItemAsStringAsync("token", "");
                return new Result<string>() { Data = "退出登录成功", IsSucceeded = true };
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<Result<string>> Register(UserDto user)
        {
            using HttpClient http = httpClientFactory.CreateClient("http");
            var res = await http.PostAsJsonAsync("auth/Register", user);
            if (res.IsSuccessStatusCode)
            {
                var token = await res.Content.ReadFromJsonAsync<Result<string>>();
                if (token != null && token.IsSucceeded)
                {
                    await SetTokenAsync(token.Data!);
                    return new Result<string>() { Data = "注册成功" };
                }
                else
                {
                    return new Result<string>() { Data = "登录失败", IsSucceeded = false };
                }
            }
            return new Result<string>() { Data = "登录失败", IsSucceeded = false };
        }

        public async Task<Result<string>> WrittenOff(UserDto user)
        {
            using HttpClient http = httpClientFactory.CreateClient("http");
            var res = await http.PostAsJsonAsync("auth/WrittenOff", user);
            if (res.IsSuccessStatusCode)
            {
                var token = await res.Content.ReadFromJsonAsync<Result<string>>();
                if (token != null && token.IsSucceeded)
                {
                    await SetTokenAsync(token.Data!);
                    return new Result<string>() { Data = "注销成功" };
                }
                else
                {
                    return new Result<string>() { Data = "注销失败", IsSucceeded = false };
                }
            }
            return new Result<string>() { Data = "注销失败", IsSucceeded = false };
        }
    }

    /// <summary>
    /// 登录认证接口
    /// </summary>
    public interface IAuthService
    {
        
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<Result<string>> Register(UserDto user);
        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<Result<string>> WrittenOff(UserDto user);
        /// <summary>
        /// 登录:
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<Result<string>> SignInAsync(UserDto user);
        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        Task<Result<string>> SignOutAsync();
    }
}
