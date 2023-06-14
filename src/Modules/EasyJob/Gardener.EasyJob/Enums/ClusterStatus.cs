// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.EasyJob.Enums
{
    /// <summary>
    /// 作业集群状态
    /// </summary>
    public enum ClusterStatus : uint
    {
        /// <summary>
        /// 宕机
        /// </summary>
        Crashed = 0,

        /// <summary>
        /// 工作中
        /// </summary>
        Working = 1,

        /// <summary>
        /// 等待被唤醒
        /// </summary>
        Waiting = 2
    }
}
