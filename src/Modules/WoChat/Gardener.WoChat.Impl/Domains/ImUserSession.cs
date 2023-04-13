// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.WoChat.Domains
{
    /// <summary>
    /// 用户会话列表
    /// </summary>
    public class ImUserSession : GardenerEntityBase<Guid>
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
