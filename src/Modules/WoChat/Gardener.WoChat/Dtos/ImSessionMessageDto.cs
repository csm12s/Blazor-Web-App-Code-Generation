﻿// -----------------------------------------------------------------------------
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
    /// Im会话消息列表
    /// </summary>
    public class ImSessionMessageDto : BaseDto<Guid>
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
        /// 消息类型
        /// </summary>
        [DisplayName("MessageType")]
        public ImMessageType MessageType { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        [DisplayName("Message")]
        public string? Message { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public UserDto? User { get; set; }
    }
}