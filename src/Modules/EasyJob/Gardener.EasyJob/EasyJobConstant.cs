﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.EasyJob
{
    /// <summary>
    /// 常量定义
    /// </summary>
    public class EasyJobConstant
    {
        /// <summary>
        /// 定时任务通知组
        /// </summary>
        public static readonly string EasyJobNotificationGroupName = "EasyJobNotificationGroup";
        /// <summary>
        /// 间隔触发器类型
        /// </summary>
        public readonly static string TriggerTypeInterval = "Furion.Schedule.PeriodTrigger";
        /// <summary>
        /// 间隔触发器类型
        /// </summary>
        public readonly static string TriggerTypeCron = "Furion.Schedule.CronTrigger";
    }
}
