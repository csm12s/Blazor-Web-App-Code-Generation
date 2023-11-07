// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.UserCenter.Dtos;
using Gardener.WoChat.Enums;
using Gardener.WoChat.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.WoChat.Dtos
{
    /// <summary>
    /// Im会话消息列表
    /// </summary>
    [Display(Name = nameof(WoChatResource.ImSessionMessage), ResourceType = typeof(WoChatResource))]
    public class ImSessionMessageDto : TenantBaseDto<Guid>
    {
        /// <summary>
        /// 会话编号
        /// </summary>
        [Display(Name = nameof(WoChatResource.ImSessionId), ResourceType = typeof(WoChatResource))]
        public Guid ImSessionId { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        [Display(Name = nameof(WoChatResource.UserId), ResourceType = typeof(WoChatResource))]
        public int UserId { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        [Display(Name = nameof(WoChatResource.MessageType), ResourceType = typeof(WoChatResource))]
        public ImMessageType MessageType { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        [Display(Name = nameof(WoChatResource.Message), ResourceType = typeof(WoChatResource))]
        public string? Message { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        [NotMapped]
        public UserDto? User { get; set; }
    }
}
