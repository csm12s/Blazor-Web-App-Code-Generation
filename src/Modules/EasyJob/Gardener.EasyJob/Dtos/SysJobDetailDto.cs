// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.EasyJob.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.EasyJob.Dtos
{
    /// <summary>
    /// 任务详情
    /// </summary>
    [Description("任务详情")]
    public class SysJobDetailDto : BaseDto<int>
    {
        /// <summary>
        /// 作业编号
        /// </summary>
        [DisplayName("JobId")]
        [Required, MaxLength(100)]
        public string JobId { get; set; } = null!;

        /// <summary>
        /// 作业组名称
        /// </summary>
        [DisplayName("GroupName")]
        public string? GroupName { get; set; }
        /// <summary>
        /// 作业处理程序类型
        /// </summary>
        /// <remarks>
        /// 作业处理程序类型，存储的是类型的 FullName
        /// </remarks>
        [DisplayName("JobTypeFullName")]
        public string? JobType { get; set; }
        /// <summary>
        /// 作业处理程序类型所在程序集
        /// </summary>
        /// <remarks>
        /// 作业处理程序类型所在程序集，存储的是程序集 Name
        /// </remarks>
        [DisplayName("AssemblyName")]
        public string? AssemblyName { get; set; }
        /// <summary>
        /// 描述信息
        /// </summary>
        [DisplayName("Description")]
        public string? Description { get; set; }
        /// <summary>
        /// 并行处理
        /// </summary>
        /// <remarks>
        /// 作业执行方式，
        /// 设置为 false 串行 执行
        /// 设置为 true  并行 执行
        /// </remarks>
        [DisplayName("Concurrent")]
        public bool Concurrent { get; set; } = true;
        /// <summary>
        /// 是否扫描特性
        /// </summary>
        /// <remarks>
        /// IJob 实现类[Trigger] 特性触发器
        /// </remarks>
        [DisplayName("IncludeAnnotations")]
        public bool IncludeAnnotations { get; set; } = false;
        /// <summary>
        /// 作业信息额外数据
        /// </summary>
        /// <remarks>
        /// 作业信息额外数据，由 Dictionary{string,object} 序列化成字符串存储 
        /// </remarks>
        [DisplayName("Properties")]
        public string? Properties { get; set; } = "{\"key\":\"value\"}";
        /// <summary>
        /// 作业创建类型
        /// </summary>
        [DisplayName("JobType")]
        public JobCreateType CreateType { get; set; } = JobCreateType.BuiltIn;
        /// <summary>
        /// 脚本代码
        /// </summary>
        [DisplayName("ScriptCode")]
        public string? ScriptCode { get; set; }
        /// <summary>
        /// 触发器集合
        /// </summary>
        public IEnumerable<SysJobTriggerDto>? JobTriggers { get; set; }
    }
}
