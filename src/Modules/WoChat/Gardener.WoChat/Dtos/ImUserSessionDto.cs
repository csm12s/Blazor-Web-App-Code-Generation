// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using System.ComponentModel;

namespace Gardener.WoChat.Dtos
{
    /// <summary>
    /// 用户会话列表
    /// </summary>
    public class ImUserSessionDto : BaseDto<Guid>
    {
        /// <summary>
        /// 会话编号
        /// </summary>
        [DisplayName("ImSessionId")]
        public Guid ImSessionId { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        [DisplayName("UserId")]
        public int UserId { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        [DisplayName("IsActive")]
        public bool IsActive { get; set; }
    }
}
