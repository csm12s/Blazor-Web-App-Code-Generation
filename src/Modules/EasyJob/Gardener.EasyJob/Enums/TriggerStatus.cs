﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Attributes;
using System.ComponentModel;

namespace Gardener.EasyJob.Enums
{

    /// <summary>
    /// 作业触发器状态
    /// </summary>
    public enum TriggerStatus : uint
    {
        /// <summary>
        /// 积压
        /// </summary>
        /// <remarks>起始时间大于当前时间</remarks>
        [TagColor("warning")]
        [TagIcon("exclamation-circle")]
        Backlog = 0,

        /// <summary>
        /// 就绪
        /// </summary>
        [TagColor("success")]
        [TagIcon("check-circle","outline")]
        Ready = 1,

        /// <summary>
        /// 正在运行
        /// </summary>
        [TagColor("processing")]
        [TagIcon("sync", "outline",true)]
        Running = 2,

        /// <summary>
        /// 暂停
        /// </summary>
        [TagColor("default")]
        [TagIcon("clock-circle")]
        Pause = 3,

        /// <summary>
        /// 阻塞
        /// </summary>
        /// <remarks>本该执行但是没有执行</remarks>
        [TagColor("warning")]
        [TagIcon("exclamation-circle")]
        Blocked = 4,

        /// <summary>
        /// 由失败进入就绪
        /// </summary>
        /// <remarks>运行错误当并未超出最大错误数，进入下一轮就绪</remarks>
        [TagColor("warning")]
        [TagIcon("exclamation-circle")]
        ErrorToReady = 5,

        /// <summary>
        /// 归档
        /// </summary>
        /// <remarks>结束时间小于当前时间</remarks>
        [TagColor("default")]
        Archived = 6,

        /// <summary>
        /// 崩溃
        /// </summary>
        /// <remarks>错误次数超出了最大错误数</remarks>
        [TagColor("error")]
        [TagIcon("close-circle")]
        Panic = 7,

        /// <summary>
        /// 超限
        /// </summary>
        /// <remarks>运行次数超出了最大限制</remarks>
        [TagColor("warning")]
        [TagIcon("exclamation-circle")]
        Overrun = 8,

        /// <summary>
        /// 无触发时间
        /// </summary>
        /// <remarks>下一次执行时间为 null </remarks>
        [TagColor("warning")]
        [TagIcon("exclamation-circle")]
        Unoccupied = 9,

        /// <summary>
        /// 未启动
        /// </summary>
        [TagColor("warning")]
        [TagIcon("exclamation-circle")]
        NotStart = 10,

        /// <summary>
        /// 未知作业触发器
        /// </summary>
        /// <remarks>作业触发器运行时类型为 null</remarks>
        [Description("UnknownTriggerType")]
        [TagColor("error")]
        [TagIcon("close-circle")]
        Unknown = 11,

        /// <summary>
        /// 未知作业处理程序
        /// </summary>
        /// <remarks>作业处理程序类型运行时类型为 null</remarks>
        [Description("UnknownJobAssembly")]
        [TagColor("error")]
        [TagIcon("close-circle")]
        Unhandled = 12
    }
}
