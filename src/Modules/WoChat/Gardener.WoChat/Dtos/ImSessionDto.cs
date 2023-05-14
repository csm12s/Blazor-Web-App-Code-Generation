// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.UserCenter.Dtos;
using Gardener.WoChat.Enums;
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
        /// 所有用户是否都激活
        /// </summary>
        /// <remarks>
        /// 在收到消息后，用户会话未激活的会被动置为激活 
        /// </remarks>
        [DisplayName("AllUserIsActive")]
        public bool AllUserIsActive { get; set; } = false;

        /// <summary>
        /// 是否禁言
        /// </summary>
        /// <remarks>
        /// 禁言后只有创建者能够发送消息
        /// </remarks>
        [DisplayName("DisableSendMessage")]
        public bool DisableSendMessage { get; set; } = false;

        /// <summary>
        /// 用户列表-输出展示
        /// </summary>
        public IEnumerable<UserDto> Users { get; set; }=new List<UserDto>();

        /// <summary>
        /// 当前用户的未读消息数
        /// </summary>
        [DisplayName("UnreadMessageCount")]
        public int UnreadMessageCount { get; set; }

        /// <summary>
        /// 当前用户是否能发送消息
        /// </summary>
        /// <remarks>
        /// 当前用户是否能发送消息
        /// </remarks>
        [DisplayName("CurrentUserCanSendMessage")]
        public bool CurrentUserCanSendMessage { get; set; } = false;
    }
}
