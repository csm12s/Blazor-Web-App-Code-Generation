// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.EasyJob.Dtos
{
    /// <summary>
    /// 运行耗时
    /// </summary>
    public class SysJobLogElapsedTime
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        /// <param name="jobName"></param>
        /// <param name="elapsedTime"></param>
        public SysJobLogElapsedTime(string group, string jobName, double elapsedTime)
        {
            ElapsedTime = elapsedTime;
            JobName = jobName;
            Group = group;
        }

        /// <summary>
        /// 执行耗时
        /// </summary>
        public double ElapsedTime { get; set; }
        /// <summary>
        /// 任务名
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; set; }
    }
}
