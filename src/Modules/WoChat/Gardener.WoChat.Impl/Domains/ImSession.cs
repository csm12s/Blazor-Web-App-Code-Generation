// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.NotificationSystem.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.WoChat.Domains
{
    /// <summary>
    /// Im系统会话数据
    /// </summary>
    public class ImSession : GardenerEntityBase<Guid>
    {
        /// <summary>
        /// 会话类型
        /// </summary>
        [DisplayName("SessionType")]
        public ImSessionType SessionType { get; set; }

        /// <summary>
        /// 会话名称
        /// </summary>
        [DisplayName("SessionName")]
        public string? SessionName { get; set; }

        /// <summary>
        /// 公告
        /// </summary>
        [DisplayName("Announcement")]
        public string? Announcement { get; set; }

        /// <summary>
        /// 最后消息时间
        /// </summary>
        [DisplayName("LastMessageTime")]
        public DateTimeOffset LastMessageTime { get; set; }
    }
}
