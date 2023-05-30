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
        /// 判断当前用户是否有该资源权限
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool CheckCurrentUserHaveResource(object key);
        /// <summary>
        /// 判断当前用户是否有该资源权限
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> CheckCurrentUserHaveResourceAsync(object key);
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        Task<UserDto?> GetCurrentUser();
        /// <summary>
        /// 当前用户是否是超级管理员
        /// </summary>
        /// <returns></returns>
        public bool CurrentUserIsSuperAdmin();
        /// <summary>
        /// 获取当前用户的菜单
        /// </summary>
        /// <returns></returns>
        List<ResourceDto>? GetCurrentUserMenus();
        /// <summary>
        /// 重新加载用户相关信息
        /// </summary>
        /// <returns></returns>
        Task<(UserDto?, bool?, List<ResourceDto>?, List<string>?)> ReloadCurrentUserInfos();
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
        Task<Dictionary<string, string>?> GetCurrentTokenHeaders();
        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="force">强制刷新</param>
        /// <returns></returns>
        Task<bool> RefreshToken(bool force=false);
        /// <summary>
        /// 获取当前token
        /// </summary>
        /// <returns></returns>
        Task<TokenOutput?> GetCurrentToken();
        /// <summary>
        /// 测试token是否可用
        /// </summary>
        /// <param name="flag">标记</param>
        /// <returns></returns>
        /// <remarks>
        /// <para>服务端不执行任何内容，token无效将返回响应401；</para>
        /// <para>在特殊位置，不通过apicaller调用接口，无法实现token的被动刷新，就需要调用该方法去触发一下；</para>
        /// <para>当然通过apicaller调用的其他需校验token的接口也可以达到该效果；</para>
        /// </remarks>
        Task<bool> TestToken(string? flag = null);

        /// <summary>
        /// 是否是租户
        /// </summary>
        /// <remarks>
        /// <para>如果用户租户编号不为null或空认为是租户</para>
        /// </remarks>
        /// <returns></returns>
        bool CurrentUserIsTenant();

        /// <summary>
        /// 是否是租户管理员
        /// </summary>
        /// <remarks>
        /// 是否分配资源 <see cref="Authorization.Constants.ResourceKeys.SystemTenantAdministratorKey"/>
        /// </remarks>
        /// <returns></returns>
        bool CurrentUserIsTenantAdministrator();
    }
}
