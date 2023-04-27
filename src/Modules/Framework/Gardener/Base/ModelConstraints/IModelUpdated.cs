// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Enums;
using System;
using System.ComponentModel;

namespace Gardener.Base
{
    /// <summary>
    /// 更新信息
    /// </summary>
    public interface IModelUpdated
    {
        /// <summary>
        /// 修改日期
        /// </summary>
        [DisplayName("UpdatedTime")]
        public DateTimeOffset? UpdatedTime { get; set; }
        /// <summary>
        /// 修改者编号
        /// </summary>
        [DisplayName("UpdateBy")]
        public string? UpdateBy { get; set; }
        /// <summary>
        /// 修改者身份类型
        /// </summary>
        [DisplayName("UpdateIdentityType")]
        public IdentityType? UpdateIdentityType { get; set; }
    }
}
