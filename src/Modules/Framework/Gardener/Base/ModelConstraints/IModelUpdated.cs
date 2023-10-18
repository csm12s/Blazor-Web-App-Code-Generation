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
    /// 更新信息
    /// </summary>
    public interface IModelUpdated
    {
        /// <summary>
        /// 修改日期
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.UpdatedTime), ResourceType = typeof(SharedLocalResource))]
        public DateTimeOffset? UpdatedTime { get; set; }
        /// <summary>
        /// 修改者编号
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.UpdateBy), ResourceType = typeof(SharedLocalResource))]
        public string? UpdateBy { get; set; }
        /// <summary>
        /// 修改者身份类型
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.UpdateIdentityType), ResourceType = typeof(SharedLocalResource))]
        public IdentityType? UpdateIdentityType { get; set; }
    }
}
