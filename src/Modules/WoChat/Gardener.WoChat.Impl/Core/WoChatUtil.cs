// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.WoChat.Impl.Core
{
    /// <summary>
    /// 公共方法
    /// </summary>
    public class WoChatUtil
    {
        /// <summary>
        /// Im分组前缀
        /// </summary>
        public static readonly string ImGroupNamePrefix = "wo-chat";
        /// <summary>
        /// 获取Im分组名称
        /// </summary>
        /// <param name="imSessionId"></param>
        /// <returns></returns>
        public static string GetImGroupName(Guid imSessionId)
        {
            return ImGroupNamePrefix + imSessionId.ToString();
        }

    }
}
