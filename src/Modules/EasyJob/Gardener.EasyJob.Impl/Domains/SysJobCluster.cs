// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.EasyJob.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.EasyJob.Impl.Domains
{
    /// <summary>
    /// 系统作业集群表
    /// </summary>
    public class SysJobCluster : GardenerEntityBase<int>
    {
        /// <summary>
        /// 作业集群Id
        /// </summary>
        [DisplayName("作业集群编号")]
        [Required, MaxLength(64)]
        public string ClusterId { get; set; } = null!;

        /// <summary>
        /// 描述信息
        /// </summary>
        [DisplayName("描述信息")]
        [MaxLength(128)]
        public string? Description { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DisplayName("状态")]
        public ClusterStatus Status { get; set; }
    }
}
