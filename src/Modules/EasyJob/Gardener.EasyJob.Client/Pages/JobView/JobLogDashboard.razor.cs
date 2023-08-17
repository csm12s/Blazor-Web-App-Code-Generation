// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

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
    public partial class JobLogDashboard : ReuseTabsPageBase,IDisposable
    {
        [Inject]
        private ILocalizationLocalizer<EasyJobLocalResource> Localizer { get; set; } = null!;
        [Inject]
        public ISysJobDetailService sysJobDetailService { get; set; } = null!;
        [Inject]
        public ISysJobLogService sysJobLogService { get; set; } = null!;
        /// <summary>
        /// 事件
        /// </summary>
        [Inject]
        public IEventBus EventBus { get; set; } = null!;
        /// <summary>
        /// 通知订阅者
        /// </summary>
        private Subscriber? logNotificationSubscriber;
        private IEnumerable<CodeDto>? timeQueryCodes = null;
        private List<SysJobDetailDto>? jobs = null;
        private string? timeQueryDays;
        private string? jobId;

        private SysJobLogCountAll runsNumberCount = new SysJobLogCountAll();
        protected override async Task OnInitializedAsync()
        {
            //订阅触发器更新
            logNotificationSubscriber = EventBus.Subscribe<EasyJobRunLogNotificationData>(OnReceiveJobRunLog);
            jobs = await sysJobDetailService.GetAllUsable();
            timeQueryCodes = CodeUtil.GetCodesFromCache("easy_job_count_query_date");
            timeQueryDays = timeQueryCodes?.FirstOrDefault()?.CodeValue;
            await base.OnInitializedAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private async Task OnTimeQueryTypeSelectedItemChanged(CodeDto value)
        {
            await ReLoadCounts();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private async Task OnJobSelectedItemChanged(SysJobDetailDto value)
        {
            await ReLoadCounts();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task ReLoadCounts()
        {
            int days = timeQueryDays == null ? 1 : int.Parse(timeQueryDays);
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
