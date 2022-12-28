// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Resources
{
    /// <summary>
    /// 用户中心资源
    /// </summary>
    public class UserCenterResource : SharedLocalResource
    {
        /// <summary>
        /// 点击修改头像
        /// </summary>
        public const string ClickUpdateAvatar = nameof(ClickUpdateAvatar);
        /// <summary>
        /// 用户编号
        /// </summary>
        public const string UserId = nameof(UserId);
    }
}
