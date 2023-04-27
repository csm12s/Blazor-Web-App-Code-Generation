// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authorization.Dtos;
using Gardener.EventBus;
using Gardener.SystemManager.Dtos;
using Gardener.UserCenter.Dtos;
using System.Collections.Generic;

namespace Gardener.Client.Base.EventBus.Events
{
    /// <summary>
    /// 重载当前用户事件
    /// </summary>
    public class ReloadCurrentUserEvent : EventBase
    {
        /// <summary>
        /// 登录token
        /// </summary>
        public TokenOutput Token { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserDto CurrentUser { get; set; }
        /// <summary>
        /// 是否是超管
        /// </summary>
        public bool CurrentUserIsSuperAdmin { get; set; }=false;
        /// <summary>
        /// 界面资源
        /// </summary>
        public List<string>? UiResourceKeys { get; set; }
        /// <summary>
        /// 菜单资源
        /// </summary>
        public List<ResourceDto>? MenuResources { get; set; }
        /// <summary>
        /// 重载当前用户事件
        /// </summary>
        /// <param name="token"></param>
        public ReloadCurrentUserEvent(TokenOutput token, UserDto currentUser) : base(nameof(ReloadCurrentUserEvent))
        {
            Token = token;
            CurrentUser = currentUser;
        }
    }
}
