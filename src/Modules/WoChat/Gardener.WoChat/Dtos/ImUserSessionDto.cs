// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.WoChat.Resources;
using System.ComponentModel.DataAnnotations;

namespace Gardener.WoChat.Dtos
{
    /// <summary>
    /// 用户会话列表
    /// </summary>
    [Display(Name = nameof(WoChatResource.ImUserSession), ResourceType = typeof(WoChatResource))]
    public class ImUserSessionDto : TenantBaseDto<Guid>
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
        /// 是否激活
        /// </summary>
        [Display(Name = nameof(WoChatResource.IsActive), ResourceType = typeof(WoChatResource))]
        public bool IsActive { get; set; }
    }
}
