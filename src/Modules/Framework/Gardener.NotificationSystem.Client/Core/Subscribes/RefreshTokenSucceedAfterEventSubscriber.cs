﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Client.Base.EventBus.Events;

namespace Gardener.NotificationSystem.Client.Subscribes
{
    /// <summary>
    /// 刷新token后
    /// </summary>
    [TransientService]
    public class RefreshTokenSucceedAfterEventSubscriber : EventSubscriberBase<RefreshTokenSucceedAfterEvent>
    {
        public override Task CallBack(RefreshTokenSucceedAfterEvent e)
        {
            return Task.CompletedTask;
        }
    }
}
