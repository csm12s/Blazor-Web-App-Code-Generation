// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;
using System.Reflection;

namespace Gardener.EasyJob.Resources
{
    /// <summary>
    /// EasyJob 本地化资源
    /// </summary>
    public class EasyJobLocalResource : SharedLocalResource
    {
        /// <summary>
        /// 任务编号
        /// </summary>
        public const string JobId= nameof(JobId);
        /// <summary>
        /// 分组名
        /// </summary>
        public const string GroupName = nameof(GroupName);
        /// <summary>
        /// 
        /// </summary>
        public const string JobTypeFullName = nameof(JobTypeFullName);
        /// <summary>
        /// 并行
        /// </summary>
        public const string Concurrent = nameof(Concurrent);
        /// <summary>
        /// 
        /// </summary>
        public const string AssemblyName = nameof(AssemblyName);
        /// <summary>
        /// 
        /// </summary>
        public const string IncludeAnnotations = nameof(IncludeAnnotations);
        /// <summary>
        /// 
        /// </summary>
        public const string JobType = nameof(JobType);
        /// <summary>
        /// 属性
        /// </summary>
        public const string Properties = nameof(Properties);
    }
}
