// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Entity;
using Gardener.EasyJob.Dtos;
using Gardener.EasyJob.Resources;
using System.ComponentModel.DataAnnotations;

namespace Gardener.EasyJob.Impl.Domains
{
    /// <summary>
    /// 任务详情
    /// </summary>
    public class SysJobDetail : SysJobDetailDto, IEntityBase
    {
        /// <summary>
        /// 触发器集合
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.JobTriggers), ResourceType = typeof(EasyJobLocalResource))]
        public new IEnumerable<SysJobDetail>? JobTriggers { get; set; }
    }
}
