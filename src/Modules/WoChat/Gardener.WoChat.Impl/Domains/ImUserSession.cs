// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Base;
using Gardener.Base.Entity;
using System.ComponentModel;

namespace Gardener.WoChat.Domains
{
    /// <summary>
    /// 用户会话列表
    /// </summary>
    public class ImUserSession : GardenerTenantEntityBase<Guid, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
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
        /// <remarks>
        /// 未激活的用户，在会话列表中不会出现该会话，在收到消息后，会被动置为激活 
        /// </remarks>
        [DisplayName("IsActive")]
        public bool IsActive { get; set; }
    }
}
