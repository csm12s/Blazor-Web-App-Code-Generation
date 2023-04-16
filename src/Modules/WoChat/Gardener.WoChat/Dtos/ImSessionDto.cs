// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.NotificationSystem.Enums;
using Gardener.UserCenter.Dtos;
using Gardener.WoChat.Enums;
using System;
using System.ComponentModel;

namespace Gardener.WoChat.Dtos
{
    /// <summary>
    /// Im系统会话数据
    /// </summary>
    public class ImSessionDto : BaseDto<Guid>
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
        /// <summary>
        /// 用户列表-输出展示
        /// </summary>
        public IEnumerable<UserDto> Users { get; set; }=new List<UserDto>();
    }
}
