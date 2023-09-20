// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.EasyJob.Resources;
using System.ComponentModel.DataAnnotations;

namespace Gardener.EasyJob.Dtos
{
    /// <summary>
    /// 运行耗时
    /// </summary>
    [Display(Name = nameof(EasyJobLocalResource.SysJobLogElapsedTime), ResourceType = typeof(EasyJobLocalResource))]
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
        [Display(Name = nameof(EasyJobLocalResource.ElapsedTime), ResourceType = typeof(EasyJobLocalResource))] 
        public long ElapsedTime { get; set; }

        /// <summary>
        /// 任务名
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.JobName), ResourceType = typeof(EasyJobLocalResource))] 
        public string JobName { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.Time), ResourceType = typeof(EasyJobLocalResource))] 
        public string Time { get; set; }
    }
}
