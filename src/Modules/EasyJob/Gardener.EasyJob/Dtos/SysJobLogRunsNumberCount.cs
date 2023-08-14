// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.EasyJob.Dtos
{
    /// <summary>
    /// 运行次数统计
    /// </summary>
    public class SysJobLogRunsNumberCount
    {
        /// <summary>
        /// 总数
        /// </summary>
        public long Total { get; set; }
        /// <summary>
        /// 成功的次数
        /// </summary>
        public long Succeed { get; set; }
        /// <summary>
        /// 失败的次数
        /// </summary>
        public long Fail { get; set; }
    }
}
