// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.EventBus;
using System.Threading.Tasks;

namespace Gardener.Client.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public abstract class EventSubscriberBase<TEvent> : IEventSubscriber where TEvent : EventBase
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="e"></param>
        public abstract Task CallBack(TEvent e);

        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public Task CallBack(object e)
        {
            //类型不同，无法转换，返回
            if (!e.GetType().Equals(typeof(TEvent)))
            {
                return Task.CompletedTask;
            }
            return CallBack((TEvent)e);
        }
    }


}
