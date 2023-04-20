// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;
using Gardener.UserCenter.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.WoChat.Resources
{
    /// <summary>
    /// 资源
    /// </summary>
    public class WoChatResource : SharedLocalResource
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public const string UserInfo = nameof(UserInfo);
        /// <summary>
        /// 发送消息
        /// </summary>
        public const string SendMessage = nameof(SendMessage);
        /// <summary>
        /// 开启会话
        /// </summary>
        public const string OpenSession = nameof(OpenSession);
        /// <summary>
        /// 发送图片
        /// </summary>
        public const string SendImage = nameof(SendImage);
        /// <summary>
        /// 输入群组名称
        /// </summary>
        public const string InputGroupName = nameof(InputGroupName);
        /// <summary>
        /// 发起群组聊天
        /// </summary>
        public const string InitiateGroupChat = nameof(InitiateGroupChat);
        /// <summary>
        /// 请输入内容
        /// </summary>
        public const string PleaseInputContent = nameof(PleaseInputContent);
        /// <summary>
        /// 删除会话
        /// </summary>
        public const string DeleteSession = nameof(DeleteSession);
        /// <summary>
        /// 移除群聊
        /// </summary>
        public const string RemoveGroupChat = nameof(RemoveGroupChat);
        /// <summary>
        /// 退出群聊
        /// </summary>
        public const string QuitGroupChat = nameof(QuitGroupChat);
        /// <summary>
        /// 开启会话消息发送权限
        /// </summary>
        public const string EnableSessionSendMessage = nameof(EnableSessionSendMessage);
        /// <summary>
        /// 关闭会话消息发送权限
        /// </summary>
        public const string DisableSessionSendMessage = nameof(DisableSessionSendMessage);
        /// <summary>
        /// 禁止发送消息
        /// </summary>
        public const string DisableSendMessage = nameof(DisableSendMessage);

    }
}
