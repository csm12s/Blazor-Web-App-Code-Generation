// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Base;
using Gardener.Base.Entity;
using Gardener.WoChat.Enums;
using System.ComponentModel;

namespace Gardener.WoChat.Domains
{
    /// <summary>
    /// Im系统会话数据
    /// </summary>
    public class ImSession : GardenerTenantEntityBase<Guid, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
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
        /// 用户签名
        /// </summary>
        /// <remarks>
        /// 通过用户编号组，计算签名，方便从用户编号组进行查重
        /// userIds 正序，逗号拼接，MD5
        /// 目前仅用于私聊，群聊仅在创建时维护了，后续退出或加入时未重新维护
        /// </remarks>
        [DisplayName("UsersSignature")]
        public string UsersSignature { get; set; } = null!;
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
    }
}
