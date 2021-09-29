// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;

namespace Gardener.EventBus
{
    /// <summary>
    /// 时间基本信息
    /// </summary>
    public class EventBaseInfo
    {
        /// <summary>
        /// 时间基本信息
        /// </summary>
        public EventBaseInfo()
        {
            SendTime = DateTimeOffset.UtcNow;
        }

        public DateTimeOffset SendTime { get; }
    }
}
