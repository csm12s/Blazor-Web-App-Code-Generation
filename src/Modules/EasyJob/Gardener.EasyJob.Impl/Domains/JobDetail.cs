// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;

namespace Gardener.EasyJob.Impl.Domains
{
    public class JobDetail : GardenerEntityBase<String>
    {
        /// <summary>
        /// 作业 Id
        /// </summary>
        public string JobId { get; set; } = null!;

        /// <summary>
        /// 作业组名称
        /// </summary>
        public string GroupName { get; set; } = null!;
        /// <summary>
        /// 作业处理程序类型
        /// </summary>
        /// <remarks>
        /// 作业处理程序类型，存储的是类型的 FullName
        /// </remarks>
        public string JobType { get; set; } = null!;
        /// <summary>
        /// 作业处理程序类型所在程序集
        /// </summary>
        /// <remarks>
        /// 作业处理程序类型所在程序集，存储的是程序集 Name
        /// </remarks>
        public string AssemblyName { get; set; } = null!;
        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description { get; set; } = null!;
        /// <summary>
        /// 作业执行方式
        /// </summary>
        /// <remarks>
        /// 作业执行方式，如果设置为 false，那么使用 串行 执行，否则 并行 执行
        /// </remarks>
        public bool Concurrent { get; set; }
        /// <summary>
        /// 是否扫描特性
        /// </summary>
        /// <remarks>
        /// IJob 实现类[Trigger] 特性触发器
        /// </remarks>
        public bool IncludeAnnotations { get; set; }
        /// <summary>
        /// 作业信息额外数据
        /// </summary>
        /// <remarks>
        /// 作业信息额外数据，由 Dictionary<string, object> 序列化成字符串存储 
        /// </remarks>
        public string? Properties { get; set; }
    }
}
