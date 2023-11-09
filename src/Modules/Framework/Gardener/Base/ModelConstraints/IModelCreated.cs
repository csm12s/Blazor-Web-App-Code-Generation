// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Enums;
using Gardener.Base.Resources;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = nameof(SharedLocalResource.CreatedTime), ResourceType = typeof(SharedLocalResource))]
        public DateTimeOffset CreatedTime { get; set; }
        /// <summary>
        /// 创建者编号
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.CreateBy), ResourceType = typeof(SharedLocalResource))]
        public string? CreateBy { get; set; }
        /// <summary>
        /// 创建者身份类型
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.CreateIdentityType), ResourceType = typeof(SharedLocalResource))]
        public IdentityType? CreateIdentityType { get; set; }
    }
}
