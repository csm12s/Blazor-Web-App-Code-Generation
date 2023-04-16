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
    }
}
