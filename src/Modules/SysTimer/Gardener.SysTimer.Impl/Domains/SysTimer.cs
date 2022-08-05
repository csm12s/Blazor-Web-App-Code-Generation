﻿using Furion.TaskScheduler;
using Gardener.Base;
using Gardener.Enums;
using Gardener.SysTimer.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.SysTimer.Domains
{
    /// <summary>
    /// 定时任务
    /// </summary>
    [Comment("定时任务表")][Table("SysTimer")]
    public class SysTimerEntity : GardenerEntityBase, IEntityTypeConfiguration<SysTimerEntity>
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        /// <example>dilon</example>
        [Comment("任务名称")]
        [Required, MaxLength(20)]
        public string JobName { get; set; }

        /// <summary>
        /// 只执行一次
        /// </summary>
        [Comment("只执行一次")]
        public bool DoOnce { get; set; } = false;

        /// <summary>
        /// 立即执行（默认等待启动）
        /// </summary>
        [Comment("立即执行")]
        public bool StartNow { get; set; } = false;

        /// <summary>
        /// 执行模式(并行、列队)
        /// </summary>
        [Comment("执行模式")]
        public ExecutMode ExecutMode { get; set; }

        /// <summary>
        /// 执行间隔时间（单位秒）
        /// </summary>
        /// <example>5</example>
        [Comment("间隔时间")]
        public int? Interval { get; set; } = 5;

        /// <summary>
        /// Cron表达式
        /// </summary>
        /// <example></example>
        [Comment("Cron表达式")]
        [MaxLength(20)]
        public string Cron { get; set; }

        /// <summary>
        /// 定时器类型
        /// </summary>
        [Comment("定时器类型")]
        public SpareTimeTypes TimerType { get; set; } = SpareTimeTypes.Interval;

        /// <summary>
        /// 请求url
        /// </summary>
        [Comment("请求url")]
        [MaxLength(200)]
        public string RequestUrl { get; set; }

        /// <summary>
        /// 本地方法
        /// </summary>
        [Comment("本地方法")]
        [MaxLength(200)]
        public string LocalMethod { get; set; }

        /// <summary>
        /// 请求参数（Post，Put请求用）
        /// </summary>
        [Comment("请求参数")]
        public string RequestParameters { get; set; }

        /// <summary>
        /// Headers(可以包含如：Authorization授权认证)
        /// 格式：{"Authorization":"userpassword.."}
        /// </summary>
        [Comment("Headers")]
        public string Headers { get; set; }

        /// <summary>
        /// 执行类型
        /// </summary>
        /// <example>2</example>
        [Comment("执行类型")]
        public ExecuteType ExecuteType { get; set; }

        /// <summary>
        /// HTTP请求方式
        /// </summary>
        /// <example>2</example>
        [Comment("HTTP请求方式")]
        public HttpMethod HttpMethod { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Comment("备注")]
        [MaxLength(100)]
        public string Remark { get; set; }

        /// <summary>
        /// 配置Entity
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<SysTimerEntity> builder)
        {
            builder.HasIndex(x => x.JobName).IsUnique();
        }
    }
}