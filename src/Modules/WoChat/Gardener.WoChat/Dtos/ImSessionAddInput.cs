// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Collections.Generic;

namespace Gardener.WoChat.Dtos
{
    /// <summary>
    /// 添加会话输入
    /// </summary>
    public class ImSessionAddInput : ImSessionDto
    {
        /// <summary>
        /// 邀请的用户
        /// </summary>
        public List<int> UserIds { get; set; }=new List<int>();
    }
}
