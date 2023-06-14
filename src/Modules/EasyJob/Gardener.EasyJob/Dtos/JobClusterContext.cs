// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.EasyJob.Dtos
{
    /// <summary>
    /// 作业集群服务上下文
    /// </summary>
    public sealed class JobClusterContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="clusterId">作业集群 Id</param>
        public JobClusterContext(string clusterId)
        {
            ClusterId = clusterId;
        }

        /// <summary>
        /// 作业集群 Id
        /// </summary>
        public string ClusterId { get; }
    }
}
