// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.EasyJob.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.EasyJob.Dtos
{
    /// <summary>
    /// 任务详情
    /// </summary>
    [Description("任务详情")]
    public class SysJobDetailDto : BaseDto
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        [DisplayName("编号")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 作业编号
        /// </summary>
        [DisplayName("作业编号")]
        [Required, MaxLength(100)]
        public string JobId { get; set; } = null!;

        /// <summary>
        /// 作业组名称
        /// </summary>
        [DisplayName("作业编号")]
        public string? GroupName { get; set; }
        /// <summary>
        /// 作业处理程序类型
        /// </summary>
        /// <remarks>
        /// 作业处理程序类型，存储的是类型的 FullName
        /// </remarks>
        [DisplayName("作业处理程序类型")]
        public string? JobType { get; set; }
        /// <summary>
        /// 作业处理程序类型所在程序集
        /// </summary>
        /// <remarks>
        /// 作业处理程序类型所在程序集，存储的是程序集 Name
        /// </remarks>
        [DisplayName("作业处理程序类型所在程序集")]
        public string? AssemblyName { get; set; }
        /// <summary>
        /// 描述信息
        /// </summary>
        [DisplayName("描述信息")]
        public string? Description { get; set; }
        /// <summary>
        /// 作业执行方式
        /// </summary>
        /// <remarks>
        /// 作业执行方式，如果设置为 false，那么使用 串行 执行，否则 并行 执行
        /// </remarks>
        [DisplayName("作业执行方式")]
        public bool Concurrent { get; set; } = true;
        /// <summary>
        /// 是否扫描特性
        /// </summary>
        /// <remarks>
        /// IJob 实现类[Trigger] 特性触发器
        /// </remarks>
        [DisplayName("是否扫描特性")]
        public bool IncludeAnnotations { get; set; } = false;
        /// <summary>
        /// 作业信息额外数据
        /// </summary>
        /// <remarks>
        /// 作业信息额外数据，由 Dictionary<string, object> 序列化成字符串存储 
        /// </remarks>
        [DisplayName("作业信息额外数据")]
        public string? Properties { get; set; } = "{}";
        /// <summary>
        /// 作业创建类型
        /// </summary>
        [DisplayName("作业创建类型")]
        public JobCreateType CreateType { get; set; } = JobCreateType.BuiltIn;
        /// <summary>
        /// 脚本代码
        /// </summary>
        [DisplayName("脚本代码")]
        public string? ScriptCode { get; set; }
        /// <summary>
        /// 触发器集合
        /// </summary>
        public IEnumerable<SysJobTriggerDto>? JobTriggers { get; set; }
    }
}
