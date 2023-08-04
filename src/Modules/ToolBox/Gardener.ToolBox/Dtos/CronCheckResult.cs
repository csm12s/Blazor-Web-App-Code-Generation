// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.ToolBox.Dtos
{
    /// <summary>
    /// Cron检验结果
    /// </summary>
    public class CronCheckResult
    {
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid { get; set; }
        /// <summary>
        /// 运行时间
        /// </summary>
        public List<DateTimeOffset>? RunTimes { get; set; }
    }
}
