// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign.Charts;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.EasyJob.Dtos;
using Gardener.EasyJob.Dtos.Notification;
using Gardener.EasyJob.Resources;
using Gardener.EasyJob.Services;
using Gardener.EventBus;
using Gardener.LocalizationLocalizer;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Utils;
using Microsoft.AspNetCore.Components;

namespace Gardener.EasyJob.Client.Pages.JobView
{
    public partial class JobLogDashboard : ReuseTabsPageBase, IDisposable
    {
        [Inject]
        private ILocalizationLocalizer<EasyJobLocalResource> Localizer { get; set; } = null!;
        [Inject]
        public ISysJobDetailService sysJobDetailService { get; set; } = null!;
        [Inject]
        public ISysJobLogService sysJobLogService { get; set; } = null!;
        [Inject]
        public IEventBus EventBus { get; set; } = null!;
        /// <summary>
        /// 通知订阅者
        /// </summary>
        private Subscriber? logNotificationSubscriber;
        private IEnumerable<CodeDto>? timeQueryCodes = null;
        private List<SysJobDetailDto>? jobs = null;
        /// <summary>
        /// 时间查询参数
        /// </summary>
        private string? timeQueryDays;
        /// <summary>
        /// 时间查询参数-天数
        /// </summary>
        private int? days
        {
            get
            {
                int.TryParse(timeQueryDays, out int temp);
                return temp;
            }
        }
        /// <summary>
        /// 任务编号参数
        /// </summary>
        private string? jobId;

        #region 耗时图
        /// <summary>
        /// 耗时图引用
        /// </summary>
        public IChartComponent? logElapsedTimeChart;
        /// <summary>
        /// 耗时图配置
        /// </summary>
        private LineConfig logElapsedTimeChartConfig = new LineConfig
        {
            YAxis = new ValueAxis()
            {
                Label = new BaseAxisLabel()
                {
                    Visible = true
                },
                Title = new BaseAxisTitle()
                {
                    Text = Lo.GetValue<EasyJobLocalResource>("ElapsedTime") + " (ms)",
                    Visible = true
                }
            },
            Padding = "auto",
            XField = "time",
            YField = "elapsedTime",
            XAxis = new ValueCatTimeAxis()
            {
                //Type = "time"
            },
            SeriesField = "jobName",
            Label = new Label()
            {
                Visible = true
            }
        };

        /// <summary>
        /// 耗时图表首次加载
        /// </summary>
        /// <param name="chart"></param>
        /// <returns></returns>
        private async Task OnElapsedTimeChartFirstRender(IChartComponent chart)
        {
            logElapsedTimeChart = chart;
            await ReLoadLogElapsedTimes();
        }

        /// <summary>
        /// 加载执行耗时数据
        /// </summary>
        /// <param name="reset">重置图表（坐标如果修改后需要重置）</param>
        /// <returns></returns>
        private async Task ReLoadLogElapsedTimes(bool reset = false)
        {
            if (logElapsedTimeChart == null)
            {
                return;
            }
            var logElapsedTimes = await sysJobLogService.GetAvgElapsedTime(jobId, days);
            if (logElapsedTimes != null)
            {
                foreach (var item in logElapsedTimes)
                {
                    var job = jobs?.FirstOrDefault(x => x.JobId.Equals(item.JobName));
                    if (job != null && !string.IsNullOrWhiteSpace(job.Description))
                    {
                        item.JobName = $"{job.Description}({job.JobId})";
                    }
                }
            }
            if (reset)
            {
                logElapsedTimeChart?.UpdateChart(config: logElapsedTimeChartConfig, data: logElapsedTimes);
            }
            else
            {
                logElapsedTimeChart?.ChangeData(logElapsedTimes);
            }
        }
        #endregion

        #region 任务执行次数
        /// <summary>
        /// 任务执行次数图引用
        /// </summary>
        public IChartComponent? jobRunCountChart;
        /// <summary>
        /// 任务执行次数配置
        /// </summary>
        private LineConfig jobRunCountChartConfig = new LineConfig
        {
            YAxis = new ValueAxis()
            {
                Label = new BaseAxisLabel()
                {
                    Visible = true
                },
                Title = new BaseAxisTitle()
                {
                    Text = Lo.GetValue<EasyJobLocalResource>(EasyJobLocalResource.NumberOfRuns),
                    Visible = true
                }
            },
            Padding = "auto",
            XField = "time",
            YField = "total",
            XAxis = new ValueCatTimeAxis()
            {
                //Type = "time"
            },
            SeriesField = "jobName",
            Label = new Label()
            {
                Visible = true
            }
        };

        /// <summary>
        /// 任务执行次数图表首次加载
        /// </summary>
        /// <param name="chart"></param>
        /// <returns></returns>
        private async Task OnJobRunCountChartFirstRender(IChartComponent chart)
        {
            jobRunCountChart = chart;
            await ReLoadJobRunCount();
        }
        /// <summary>
        /// 加载任务执行次数数据
        /// </summary>
        /// <param name="reset">重置图表（坐标如果修改后需要重置）</param>
        /// <returns></returns>
        private async Task ReLoadJobRunCount(bool reset = false)
        {
            if (jobRunCountChart == null)
            {
                return;
            }
            var datas = await sysJobLogService.GetCount(jobId, days);
            if (datas != null)
            {
                foreach (var item in datas)
                {
                    var job = jobs?.FirstOrDefault(x => x.JobId.Equals(item.JobName));
                    if (job != null && !string.IsNullOrWhiteSpace(job.Description))
                    {
                        item.JobName = $"{job.Description}({job.JobId})";
                    }
                }
            }
            if (reset)
            {
                jobRunCountChart?.UpdateChart(config: jobRunCountChartConfig, data: datas);
            }
            else
            {
                jobRunCountChart?.ChangeData(datas);
            }
        }
        #endregion

        /// <summary>
        /// 总体运行次数统计
        /// </summary>
        private SysJobLogAllCount runsNumberCount = new SysJobLogAllCount();
        protected override void OnInitialized()
        {
            logNotificationSubscriber = EventBus.Subscribe<EasyJobRunLogNotificationData>(OnReceiveJobRunLog);
            timeQueryCodes = CodeUtil.GetCodesFromCache("easy_job_count_query_date");
            timeQueryDays = timeQueryCodes?.FirstOrDefault()?.CodeValue;
            base.OnInitialized();
        }

        protected override async Task OnInitializedAsync()
        {
            //订阅触发器更新
            jobs = await sysJobDetailService.GetAllUsable();
            await base.OnInitializedAsync();
        }
        /// <summary>
        /// 时间选中
        /// </summary>
        /// <param name="value"></param>
        private async Task OnTimeQueryTypeSelectedItemChanged(CodeDto value)
        {
            Task[] tasks = new Task[]
             {
                ReLoadAllCount(),
                ReLoadLogElapsedTimes(true),
                ReLoadJobRunCount(true)
             };
            await Task.WhenAll(tasks);
            await base.RefreshPageDom();
        }
        /// <summary>
        /// job 选中
        /// </summary>
        /// <param name="value"></param>
        private async Task OnJobSelectedItemChanged(SysJobDetailDto value)
        {
            Task[] tasks = new Task[]
            {
                ReLoadAllCount(),
                ReLoadLogElapsedTimes(),
                ReLoadJobRunCount()
            };
            await Task.WhenAll(tasks);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task ReLoadAllCount()
        {
            runsNumberCount = await sysJobLogService.GetAllCount(jobId, days);
        }
        /// <summary>
        /// 收到新日志
        /// </summary>
        /// <param name="notificationData"></param>
        /// <returns></returns>
        private Task OnReceiveJobRunLog(EasyJobRunLogNotificationData notificationData)
        {
            if (!string.IsNullOrEmpty(jobId) && !notificationData.Log.JobId.Equals(jobId))
            {
                return Task.CompletedTask;
            }
            if (notificationData.Log.Succeeded)
            {
                runsNumberCount.Succeed++;
            }
            else
            {
                runsNumberCount.Fail++;
            }
            runsNumberCount.Total++;

            return base.RefreshPageDom();
        }
        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="disposing"></param>
        public void Dispose()
        {
            if (logNotificationSubscriber != null)
            {
                EventBus.UnSubscribe(logNotificationSubscriber);
            }
        }
    }
}
