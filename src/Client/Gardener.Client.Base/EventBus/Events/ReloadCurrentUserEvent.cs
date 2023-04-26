﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authorization.Dtos;
using Gardener.EventBus;

namespace Gardener.Client.Base.EventBus.Events
{
    /// <summary>
    /// 重载当前用户事件-重载进程将等待所有事件处理完成后继续
    /// </summary>
    public class ReloadCurrentUserEvent : EventBase
    {
        /// <summary>
        /// 登录token
        /// </summary>
        public TokenOutput Token { get; set; }
        /// <summary>
        /// 重载当前用户事件-重载进程将等待所有事件处理完成后继续
        /// </summary>
        /// <param name="token"></param>
        public ReloadCurrentUserEvent(TokenOutput token) : base(nameof(ReloadCurrentUserEvent))
        {
            Token = token;
        }
    }
}
