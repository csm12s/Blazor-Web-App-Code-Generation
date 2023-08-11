// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Enums;
using Gardener.Base;
using System.ComponentModel;

namespace Gardener.EasyJob.Dtos
{
    /// <summary>
    /// 定时任务用户配置
    /// </summary>
    public class SysJobUserConfigDto : BaseDto<int>
    {
        /// <summary>
        /// 身份唯一编号
        /// </summary>
        [DisplayName("IdentityId")]
        public string IdentityId { get; set; } = null!;
        /// <summary>
        /// 身份类型
        /// </summary>
        [DisplayName("IdentityType")]
        public IdentityType IdentityType { get; set; } = IdentityType.Unknown;
        /// <summary>
        /// 是否启用实时监控
        /// </summary>
        [DisplayName("EnableRealTimeMonitor")]
        public bool EnableRealTimeMonitor { get; set; } = false;
    }
}
