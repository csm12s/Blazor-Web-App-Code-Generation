// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authorization.Dtos;
using Gardener.SystemManager.Dtos;
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
        /// <summary>
        /// 判断当前用户是否有该按钮资源权限
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> CheckCurrentUserHaveBtnResourceKey(object key);
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        Task<UserDto> GetCurrentUser();
        /// <summary>
        /// 设置一个身份验证刷新成功的回调
        /// </summary>
        /// <param name="action"></param>
        void SetOnAuthenticationRefreshSuccessed(Action<UserDto, bool, List<ResourceDto>, List<string>> action);
        /// <summary>
        /// 获取当前用户的菜单
        /// </summary>
        /// <returns></returns>
        List<ResourceDto> GetCurrentUserMenus();
        /// <summary>
        /// 刷新当前用户信息
        /// </summary>
        /// <returns></returns>
        Task ReloadCurrentUserInfos();
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="token"></param>
        /// <param name="isAutoLogin"></param>
        /// <returns></returns>
        Task Login(TokenOutput token, bool isAutoLogin = true);
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        Task Logout();
        /// <summary>
        /// 清理本地当前登录用户信息
        /// </summary>
        /// <returns></returns>
        Task CleanUserInfo();
        /// <summary>
        /// 设置一个身份验证状态变化的回调
        /// </summary>
        /// <param name="c"></param>
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
