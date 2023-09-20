// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.AntDesignUi.Base.Constants;
using Gardener.Client.Base;
using Gardener.Client.Base.Services;
using Gardener.EasyJob.Dtos;
using Gardener.EasyJob.Dtos.Notification;
using Gardener.EasyJob.Resources;
using Gardener.EasyJob.Services;
using Gardener.EventBus;
using Microsoft.AspNetCore.Components;
using System.Collections.Concurrent;

namespace Gardener.EasyJob.Client.Pages.JobView
{
    /// <summary>
    /// 定时任务控制台输入参数
    /// </summary>
    public class JobLogConsoleInput
    {
        /// <summary>
        /// 定时任务控制台输入参数
        /// </summary>
        /// <param name="jobId"></param>
        public JobLogConsoleInput(string jobId)
        {
            JobId = jobId;
        }

        /// <summary>
        /// 定时任务控制台输入参数
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="triggerId"></param>
        public JobLogConsoleInput(string jobId, string triggerId)
        {
            JobId = jobId;
            TriggerId = triggerId;
        }

        /// <summary>
        /// 作业触发器编号
        /// </summary>
        public string? TriggerId { get; set; }

        /// <summary>
        /// 作业 Id
        /// </summary>
        public string JobId { get; set; }
    }

    /// <summary>
    /// 定时任务日志控制台
    /// </summary>
    public partial class JobLogConsole : OperationDialogBase<JobLogConsoleInput, bool, EasyJobLocalResource>
    {
        /// <summary>
        /// 日志服务
        /// </summary>
        [Inject]
        private ISysJobLogService JobLogService { get; set; } = null!;
        /// <summary>
        /// js操作
        /// </summary>
        [Inject]
        private IJsTool JsTool { get; set; } = null!;

        /// <summary>
        /// 定时任务用户配置
        /// </summary>
        [Inject]
        public ISysJobUserConfigService userConfigService { get; set; } = null!;
        /// <summary>
        /// 消息提示服务
        /// </summary>
        [Inject]
        protected IClientMessageService MessageService { get; set; } = null!;
        /// <summary>
        /// 事件
        /// </summary>
        [Inject]
        public IEventBus EventBus { get; set; } = null!;
        /// <summary>
        /// 日志内容
        /// </summary>
        private string logContent = string.Empty;
        /// <summary>
        /// 分页数
        /// </summary>
        private int logPageTotal = 0;

        private int pageIndex = 1;

        private int pageSize = 20;

        /// <summary>
        /// 用户配置
        /// </summary>
        private SysJobUserConfigDto? easyJobUserConfigDto;

        private bool enableRealTimeMonitor = false;

        private bool enableRealTimeMonitorLoading = false;

        private ConcurrentQueue<SysJobLogDto> sysJobLogs = new ConcurrentQueue<SysJobLogDto>();

        /// <summary>
        /// 通知订阅者
        /// </summary>
        private Subscriber? logNotificationSubscriber;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            //订阅触发器更新
            logNotificationSubscriber = EventBus.Subscribe<EasyJobRunLogNotificationData>(OnReceiveJobRunLog);
            var userConfig = await userConfigService.GetMyConfig();
            if (userConfig != null)
            {
                easyJobUserConfigDto = userConfig;
                enableRealTimeMonitor = userConfig.EnableRealTimeMonitor;
            }
            await ReloadLogData();
            await base.OnInitializedAsync();
        }
        /// <summary>
        /// 收到新日志
        /// </summary>
        /// <param name="notificationData"></param>
        /// <returns></returns>
        private async Task OnReceiveJobRunLog(EasyJobRunLogNotificationData notificationData)
        {
            if (AddToQueue(notificationData.Log))
            {
                ResetContent();
                await base.RefreshPageDom();
                await JsTool.Document.ScrollBarToBottom("log_content_textarea");
            }
        }
        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (logNotificationSubscriber != null)
            {
                EventBus.UnSubscribe(logNotificationSubscriber);
            }
            base.Dispose(disposing);
        }
        /// <summary>
        /// 加载日志
        /// </summary>
        /// <returns></returns>
        private async Task ReloadLogData()
        {
            #region 构建查询参数
            PageRequest request = new PageRequest();
            request.PageSize = pageSize;
            request.PageIndex = pageIndex;
            request.OrderConditions = new List<ListSortDirection>()
            {
                new ListSortDirection ()
                {
                    FieldName=$"{nameof(SysJobLogDto.CreatedTime)}",
                    SortType=Gardener.Enums.ListSortType.Desc
                }
            };
            List<FilterRule> rules = new List<FilterRule>()
            {
                new FilterRule() {
                    Field=$"{nameof(SysJobLogDto.JobId)}",
                    Value=this.Options.JobId,
                    Condition=FilterCondition.And,
                    Operate=FilterOperate.Equal
                }
            };
            if (!string.IsNullOrEmpty(this.Options.TriggerId))
            {
                rules.Add(new FilterRule()
                {
                    Field = $"{nameof(SysJobLogDto.TriggerId)}",
                    Value = this.Options.TriggerId,
                    Condition = FilterCondition.And,
                    Operate = FilterOperate.Equal
                });
            }
            request.FilterGroups = new List<FilterGroup>()
            {
                new FilterGroup ()
                {
                    Rules=rules
                }

            };
            #endregion
            PagedList<SysJobLogDto> result = await JobLogService.Search(request);
            logPageTotal = result.TotalCount;

            if (result.Items.Any())
            {
                bool changed = false;
                foreach (var item in result.Items.OrderBy(x => x.CreatedTime))
                {
                    if (AddToQueue(item))
                    {
                        changed = true;
                    }
                }
                if (changed)
                {
                    ResetContent();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        private bool AddToQueue(SysJobLogDto log)
        {
            if (this.Options.JobId != null && !log.JobId.Equals(this.Options.JobId))
            {
                return false;
            }
            if (this.Options.TriggerId != null && !log.TriggerId.Equals(this.Options.TriggerId))
            {
                return false;
            }
            if (sysJobLogs.Count() >= pageSize)
            {
                sysJobLogs.TryDequeue(out _);
            }
            sysJobLogs.Enqueue(log);

            return true;
        }
        /// <summary>
        /// 重置content
        /// </summary>
        private void ResetContent()
        {
            System.Text.StringBuilder contentSb = new System.Text.StringBuilder();
            int index = 1;
            foreach (var item in sysJobLogs)
            {
                string status = Localizer[nameof(SharedLocalResource.Success)];
                if (!item.Succeeded)
                {
                    status = Localizer[nameof(SharedLocalResource.Fail)];
                }
                contentSb.AppendLine($"[{index++}] {item.CreatedTime.ToString(ClientConstant.DateTimeFormat)} - [{item.JobDetailDescription ?? item.JobId}({item.JobId})] - [{item.JobTriggerDescription ?? item.TriggerId}({item.TriggerId})] - [{item.ElapsedTime} ms] - [{status}]");
                if (!string.IsNullOrEmpty(item.Result))
                {
                    contentSb.AppendLine($"{item.Result}");
                }
                if (!item.Succeeded)
                {
                    contentSb.AppendLine($"{item.ExceptionMessage}");
                    contentSb.AppendLine($"{item.Exception}");
                }
            }
            logContent = contentSb.ToString();
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="args"></param>
        private async Task OnPaginationChange(PaginationEventArgs args)
        {
            (pageIndex, pageSize) = args;
            await ReloadLogData();
            await JsTool.Document.ScrollBarToTop("log_content_textarea");
        }

        /// <summary>
        /// 
        /// </summary>
        private async Task OnEnableRealTimeMonitorChange(bool enable)
        {
            if (easyJobUserConfigDto == null)
            {
                return;
            }
            enableRealTimeMonitorLoading = true;
            easyJobUserConfigDto.EnableRealTimeMonitor = enable;

            SysJobUserConfigDto? result = await userConfigService.SaveMyConfig(easyJobUserConfigDto);
            if (result == null)
            {
                MessageService.Error((enable ? Localizer[nameof(SharedLocalResource.Open)] : Localizer[nameof(SharedLocalResource.Close)]) + Localizer[nameof(SharedLocalResource.Fail)]);
            }
            else
            {
                easyJobUserConfigDto = result;
                enableRealTimeMonitor = enable;
            }
            enableRealTimeMonitorLoading = false;
        }
    }
}
