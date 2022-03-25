// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authorization.Dtos;
using Gardener.UserCenter.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Base
{
    /// <summary>
    /// 身份状态管理
    /// </summary>
    public interface IAuthenticationStateManager
    {
        Task<bool> CheckCurrentUserHaveBtnResourceKey(object key);
        Task<UserDto> GetCurrentUser();
        void SetOnMenusLoaded(Action<List<ResourceDto>> action);
        List<ResourceDto> GetCurrentUserEmnus();
        Task ReloadCurrentUserInfos();
        Task Login(TokenOutput token, bool isAutoLogin = true);
        Task Logout();
        Task CleanUserInfo();
        void SetNotifyAuthenticationStateChangedAction(Action c);
        /// <summary>
        /// 获取当前身份的token头，可以添加于自定义的httpclient中验证使用
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<string, string>> GetCurrentTokenHeaders();
        /// <summary>
        /// 刷新token
        /// </summary>
        /// <returns></returns>
        Task RefreshToken();
        /// <summary>
        /// 获取当前token
        /// </summary>
        /// <returns></returns>
        Task<TokenOutput> GetCurrentToken();
    }
}
