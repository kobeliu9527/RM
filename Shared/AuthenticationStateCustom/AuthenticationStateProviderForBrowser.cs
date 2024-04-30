using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Models.Dto;
using Models.NotEntity;
using Models.SystemInfo;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Shared.AuthenticationStateCustom
{
    /// <summary>
    /// 通过获取浏览器中token值
    /// </summary>
    public class AuthenticationStateProviderForBrowser : AuthenticationStateProvider, IAuthService
    {
        private readonly ILocalStorageService local;
        private readonly IHttpClientFactory httpClientFactory;

        public AuthenticationStateProviderForBrowser(ILocalStorageService local, IHttpClientFactory httpClientFactory)
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
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// 将获取到的token值写入到浏览器的cokie中
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task SetTokenAsync(string token)
        {
            try
            {
                await local.SetItemAsStringAsync("token", token);

            }
            catch (Exception)
            {
                await Task.CompletedTask;
            }
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            System.Console.WriteLine(DateTime.Now+":获取身份信息");
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
            catch (Exception)
            {
                return new(principal);
            }
            AuthenticationState a = new AuthenticationState(principal);
            NotifyAuthenticationStateChanged(Task.FromResult(a));
            return new(principal);
        }

        public async Task<Result<string>> SignInAsync(UserDto user)
        {
            using HttpClient http = httpClientFactory.CreateClient();
            Result<string> result=new Result<string>();
            try
            {
                var res = await http.PostAsJsonAsync(user.Url + "auth/login", user);
                if (res.IsSuccessStatusCode)
                {
                    var token = await res.Content.ReadFromJsonAsync<Result<string>>();
                    if (token != null && token.IsSucceeded)
                    {
                        await SetTokenAsync(token.Data!);
                        return result.Ok("登录成功");
                    }
                    else
                    {
                        return result.Fail("登录失败,没有获取到token");
                    }
                }
            }
            catch (Exception e)
            {
                return result.HasException(e);
            }
            return result.Fail("登录失败,没有获取到token");
        }

        public async Task<Result<string>> SignOutAsync()
        {
            Result<string> result = new Result<string>();
            try
            {
                await local.SetItemAsStringAsync("token", "");
                return result.Ok("退出登录成功");
            }
            catch (Exception e)
            {
                return result.HasException(e);
            }
        }
        public async Task<Result<User>> Register(UserDto user)
        {
            using HttpClient http = httpClientFactory.CreateClient();
            var res = await http.PostAsJsonAsync(user.Url + "auth/Register", user);
            if (res.IsSuccessStatusCode)
            {
                var token = await res.Content.ReadFromJsonAsync<Result<User>>();
                if (token != null && token.IsSucceeded)
                {
                    return token;
                }
                else
                {
                    return new Result<User>() { Data = null, IsSucceeded = false };
                }
            }
            return new Result<User>() { Data = null, IsSucceeded = false };
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
        Task<Result<User>> Register(UserDto user);
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
