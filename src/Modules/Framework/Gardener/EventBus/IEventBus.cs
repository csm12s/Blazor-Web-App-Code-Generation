// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;

namespace Gardener.EventBus
{ 
    /// <summary>
    /// 事件服务
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// 发布
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="e"></param>
        /// <param name="cancellationToken"></param>
        Task Publish<TEvent>(TEvent e, CancellationToken? cancellationToken=null) where TEvent : EventBase;
    }
}
