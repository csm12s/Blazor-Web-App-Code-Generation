// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.UserCenter.Dtos;
using Gardener.WoChat.Enums;
using Gardener.WoChat.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.WoChat.Dtos
{
    /// <summary>
    /// Im系统会话数据
    /// </summary>
    [Display(Name = nameof(WoChatResource.ImSession), ResourceType = typeof(WoChatResource))]
    public class ImSessionDto : TenantBaseDto<Guid>
    {
        /// <summary>
        /// 会话类型
        /// </summary>
        [Display(Name = nameof(WoChatResource.SessionType), ResourceType = typeof(WoChatResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public ImSessionType SessionType { get; set; }

        /// <summary>
        /// 会话名称
        /// </summary>
        [Display(Name = nameof(WoChatResource.SessionName), ResourceType = typeof(WoChatResource))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? SessionName { get; set; }

        /// <summary>
        /// 公告
        /// </summary>
        [Display(Name = nameof(WoChatResource.Announcement), ResourceType = typeof(WoChatResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Announcement { get; set; }

        /// <summary>
        /// 最后消息时间
        /// </summary>
        [Display(Name = nameof(WoChatResource.LastMessageTime), ResourceType = typeof(WoChatResource))]
        public DateTimeOffset LastMessageTime { get; set; }

        /// <summary>
        /// 所有用户是否都激活
        /// </summary>
        /// <remarks>
        /// 在收到消息后，用户会话未激活的会被动置为激活 
        /// </remarks>
        [Display(Name = nameof(WoChatResource.AllUserIsActive), ResourceType = typeof(WoChatResource))]
        public bool AllUserIsActive { get; set; } = false;

        /// <summary>
        /// 是否禁言
        /// </summary>
        /// <remarks>
        /// 禁言后只有创建者能够发送消息
        /// </remarks>
        [Display(Name = nameof(WoChatResource.DisableSendMessage), ResourceType = typeof(WoChatResource))]
        public bool DisableSendMessage { get; set; } = false;

        /// <summary>
        /// 用户列表-输出展示
        /// </summary>
        [NotMapped]
        public IEnumerable<UserDto> Users { get; set; }=new List<UserDto>();

        /// <summary>
        /// 当前用户的未读消息数
        /// </summary>
        [Display(Name = nameof(WoChatResource.UnreadMessageCount), ResourceType = typeof(WoChatResource))]
        public int UnreadMessageCount { get; set; }

        /// <summary>
        /// 当前用户是否能发送消息
        /// </summary>
        /// <remarks>
        /// 当前用户是否能发送消息
        /// </remarks>
        [Display(Name = nameof(WoChatResource.CurrentUserCanSendMessage), ResourceType = typeof(WoChatResource))]
        public bool CurrentUserCanSendMessage { get; set; } = false;
    }
}
