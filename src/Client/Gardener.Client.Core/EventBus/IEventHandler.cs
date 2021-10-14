// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Threading.Tasks;

namespace Gardener.Client.Core.EventBus
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public interface IEventHandler<TEvent> where TEvent : EventBase
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="e"></param>
        Task Handler(TEvent e);
    }
}
