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
    /// 创建信息
    /// </summary>
    public interface IModelCreated
    {
        /// <summary>
        /// 创建日期
        /// </summary>
        [DisplayName("CreatedTime")]
        public DateTimeOffset CreatedTime { get; set; }
        /// <summary>
        /// 创建者编号
        /// </summary>
        [DisplayName("CreateBy")]
        public string? CreateBy { get; set; }
        /// <summary>
        /// 创建者身份类型
        /// </summary>
        [DisplayName("CreateIdentityType")]
        public IdentityType? CreateIdentityType { get; set; }
    }
}
