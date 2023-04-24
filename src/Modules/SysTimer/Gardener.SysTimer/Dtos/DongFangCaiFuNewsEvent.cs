// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.EventBus;

namespace Gardener.SysTimer.Dtos
{
    public class DongFangCaiFuNewsEvent : EventBase
    {
        public DongFangCaiFuNewsEvent() : base(nameof(DongFangCaiFuNewsEvent))
        {
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string? Content { get; set; }
    }
}
