﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Enums;
using Gardener.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.EasyJob.Impl.Domains
{
    /// <summary>
    /// 定时任务用户配置
    /// </summary>
    public class SysJobUserConfig : GardenerEntityBase<int>
    {
        /// <summary>
        /// 身份唯一编号
        /// </summary>
        [DisplayName("身份唯一编号")]
        [Required, MaxLength(100)]
        public string IdentityId { get; set; } = null!;
        /// <summary>
        /// 身份类型
        /// </summary>
        [DisplayName("身份类型")]
        [Required]
        public IdentityType IdentityType { get; set; }
        /// <summary>
        /// 是否启用实时监控
        /// </summary>
        [DisplayName("是否启用实时监控")]
        public bool EnableRealTimeMonitor { get; set; }=false;
    }
}