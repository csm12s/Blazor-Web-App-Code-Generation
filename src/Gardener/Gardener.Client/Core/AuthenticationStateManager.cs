// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Client.Services;
using Gardener.Application.Dtos;
using System;
using System.Threading.Tasks;
using System.Threading;
using Gardener.Client.Constants;

namespace Gardener.Client
{
    /// <summary>
    /// 验证状态管理
    /// </summary>
    public interface IAuthenticationStateManager
    {
        Task FromLocalResetToken();
        UserDto GetCurrentUser();
        Task<UserDto> RefreshUser();
        Task Login(LoginOutput loginOutput);
        Task Logout();
        Task RemoveIdentity();
        Task SetCurrentUser(UserDto currentUser);
        void SetNotifyAuthenticationStateChangedAction(Action c);
        Task SetToken(string token);
    }
    public class AuthenticationStateManager : IAuthenticationStateManager
    {
        private readonly JsTool jsTool;
        private readonly HttpClientManager httpClientManager;
        private readonly IAuthorizeService authorizeService;
        private readonly ILogger logger;

        private int RefreshTokenErrorCount = 0;
        private UserDto currentUser;
        private LoginOutput loginOutput;
        /// <summary>
        /// 定时器 用来刷新token
        /// </summary>
        private Timer timer;
        public AuthenticationStateManager(JsTool jsTool, HttpClientManager httpClientManager, IAuthorizeService authorizeService, ILogger logger)
        {
            this.jsTool = jsTool;
            this.httpClientManager = httpClientManager;
            timer = new Timer(TimerCallback, true, 10000, SystemConstant.RefreshTokenInterval * 60);
            this.authorizeService = authorizeService;
            this.logger = logger;
        }
        /// <summary>
        /// 定时执行方法
        /// </summary>
        /// <param name="state"></param>
        private async void TimerCallback(object state)
        {
            //未登录
            if (loginOutput == null) return;
            //已经过期了
            if (loginOutput.AccessTokenExpiresIn < DateTimeOffset.Now.ToUnixTimeSeconds()) { await Logout();return;}
            //时间还很充裕
            if (loginOutput.AccessTokenExpiresIn - DateTimeOffset.Now.ToUnixTimeSeconds() > SystemConstant.RefreshTokenTimeThreshold) return;
            
            //拿到新的token
            var tokenResult=await authorizeService.RefreshToken();
            if (tokenResult.Successed)
            {
                RefreshTokenErrorCount = 0;
                loginOutput.AccessToken = tokenResult.Data.AccessToken;
                loginOutput.AccessTokenExpiresIn = tokenResult.Data.AccessTokenExpiresIn;
                //token 设置
                await SetToken(tokenResult.Data.AccessToken);
                await logger.Debug($"token refresh successed {DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss")}");
            }
            else 
            {
                RefreshTokenErrorCount++;
                if (RefreshTokenErrorCount > SystemConstant.RefreshTokenErrorCountMax)
                {
                    //失败时退出登录
                    await Logout();
                }
            }
        }
        /// <summary>
        /// 标记授权
        /// </summary>
        /// <param name="loginOutput"></param>
        /// <returns></returns>
        public async Task Login(LoginOutput loginOutput)
        {
            this.loginOutput = loginOutput;
            await SetToken(loginOutput.AccessToken);
            notifyAuthenticationStateChangedAction();
        }
        /// <summary>
        /// 标记注销
        /// </summary>
        public async Task Logout()
        {
            await RemoveIdentity();
            notifyAuthenticationStateChangedAction();
        }

        Action notifyAuthenticationStateChangedAction = () => { };

        public void SetNotifyAuthenticationStateChangedAction(Action c)
        {
            notifyAuthenticationStateChangedAction = c;
        }
        
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        public UserDto GetCurrentUser()
        {
            return currentUser;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        public async Task SetCurrentUser(UserDto currentUser)
        {
            this.currentUser = currentUser;
        }
        /// <summary>
        /// 检测本地token,并使用
        /// </summary>
        /// <param name="loginOutput"></param>
        /// <returns></returns>
        public async Task FromLocalResetToken()
        {
            //本地是否有token
            string token = await jsTool.SessionStorage.GetAsync<string>("AccessToken");
            if (!string.IsNullOrEmpty(token))
            {
                SetHttpClientAuthorization(token);
            }
        }

        /// <summary>
        /// token 设置到浏览器缓存 和 httpclient 头部
        /// </summary>
        /// <param name="loginOutput"></param>
        /// <returns></returns>
        public async Task SetToken(string token)
        {
            await jsTool.SessionStorage.SetAsync("AccessToken", token);
            SetHttpClientAuthorization(token);
        }
        /// <summary>
        ///  移除浏览器token缓存
        ///  移除httpclient 头部token
        ///  本地currentUser设置为null
        /// </summary>
        public async Task RemoveIdentity()
        {
            await jsTool.SessionStorage.RemoveAsync("AccessToken");
            SetHttpClientAuthorization("");
            currentUser = null;
        }
        #region private
        /// <summary>
        /// 给httpclient设置验证token
        /// </summary>
        /// <param name="token"></param>
        private void SetHttpClientAuthorization(string token)
        {
            //httpClient.Authenticator = new JwtAuthenticator(token);
            httpClientManager.SetClientAuthorization(token);
        }
        /// <summary>
        /// 刷新用户
        /// </summary>
        /// <returns></returns>
        public async Task<UserDto> RefreshUser()
        {
            if (currentUser != null)
                return currentUser;
            //从浏览器重新加载token
            await FromLocalResetToken();
            //请求
            var userResult = await authorizeService.GetCurrentUser();
            if (userResult.Successed)
            {
                await SetCurrentUser(userResult.Data);
                return userResult.Data;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
