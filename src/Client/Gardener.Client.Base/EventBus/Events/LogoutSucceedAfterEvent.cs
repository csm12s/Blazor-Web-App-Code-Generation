// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------


using Gardener.EventBus;

namespace Gardener.Client.Base.EventBus.Events
{
    /// <summary>
    /// 登出成功事件
    /// </summary>
    public class LogoutSucceedAfterEvent : EventBase
    {
        public LogoutSucceedAfterEvent() : base(nameof(LogoutSucceedAfterEvent))
        {
        }
    }
}