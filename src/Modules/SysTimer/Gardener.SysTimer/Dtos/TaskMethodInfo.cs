// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.SysTimer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.SysTimer.Dtos
{
    /// <summary>
    /// 任务方法信息
    /// </summary>
    public class TaskMethodInfo
    {
        /// <summary>
        /// 方法名
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// 方法所属类的Type对象
        /// </summary>
        public Type DeclaringType { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// 只执行一次
        /// </summary>
        public bool DoOnce { get; set; } = false;

        /// <summary>
        /// 立即执行（默认等待启动）
        /// </summary>
        public bool StartNow { get; set; } = false;

        /// <summary>
        /// 执行类型(并行、列队)
        /// </summary>
        public ExecutType ExecuteType { get; set; }

        /// <summary>
        /// 执行间隔时间（单位秒）
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        public string Cron { get; set; }

        /// <summary>
        /// 定时器类型
        /// </summary>
        public TimerTypes TimerType { get; set; }

        /// <summary>
        /// 请求url
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// 请求类型
        /// </summary>
        /// <example>2</example>
        public RequestType RequestType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
