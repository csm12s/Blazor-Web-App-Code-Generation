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
        /// <param name="time"></param>
        /// <param name="jobName"></param>
        /// <param name="elapsedTime"></param>
        public SysJobLogElapsedTime(string time, string jobName, long elapsedTime)
        {
            ElapsedTime = elapsedTime;
            JobName = jobName;
            Time = time;
        }

        /// <summary>
        /// 执行耗时
        /// </summary>
        public long ElapsedTime { get; set; }
        /// <summary>
        /// 任务名
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public string Time { get; set; }
    }
}
