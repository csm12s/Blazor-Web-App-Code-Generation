// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.Base;
using Gardener.EasyJob.Dtos;
using Gardener.EasyJob.Dtos.Notification;
using Gardener.EasyJob.Resources;
using Gardener.EasyJob.Services;
using Gardener.EventBus;
using Microsoft.AspNetCore.Components;

namespace Gardener.EasyJob.Client.Pages.JobView
{
    /// <summary>
    /// 
    /// </summary>
    public partial class JobTrigger : ListOperateTableBase<SysJobTriggerDto, int, JobTriggerEdit, EasyJobLocalResource>, IDisposable
    {
        /// <summary>
        /// 事件
        /// </summary>
        [Inject]
        public IEventBus EventBus { get; set; } = null!;
        /// <summary>
        /// 触发器api
        /// </summary>
        [Inject]
        public ISysJobTriggerService sysJobTriggerService { get; set; } = null!;
        /// <summary>
        /// 定时任务用户配置
        /// </summary>
        [Inject]
        public ISysJobUserConfigService userConfigService { get; set; } = null!;
        /// <summary>
        /// 触发器更新通知订阅者
        /// </summary>
        private Subscriber? triggerUpdateNotificationSubscriber;
        /// <summary>
        /// 用户配置
        /// </summary>
        private SysJobUserConfigDto? easyJobUserConfigDto;

        private bool enableRealTimeMonitor = false;
        private bool enableRealTimeMonitorLoading = false;
        private bool openOperationDialog = false;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            //订阅触发器更新
            triggerUpdateNotificationSubscriber = EventBus.Subscribe<EasyJobTriggerUpdateNotificationData>(OnEasyJobTriggerUpdate);
            var userConfig = await userConfigService.GetMyConfig();
            if (userConfig != null)
            {
                easyJobUserConfigDto = userConfig;
                enableRealTimeMonitor = userConfig.EnableRealTimeMonitor;
            }
            await base.OnInitializedAsync();
        }
        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (triggerUpdateNotificationSubscriber != null)
            {
                EventBus.UnSubscribe(triggerUpdateNotificationSubscriber);
            }
            base.Dispose(disposing);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trigger"></param>
        /// <returns></returns>
        private Task OnEasyJobTriggerUpdate(EasyJobTriggerUpdateNotificationData triggerNotificationData)
        {
           
            SysJobTriggerDto trigger = triggerNotificationData.Trigger;
            if (_datas == null)
            {
                return Task.CompletedTask;
            }
            //更新
            SysJobTriggerDto? oldTrigger = _datas.FirstOrDefault(x => x.Id == trigger.Id);
            if (oldTrigger == null || (oldTrigger.UpdatedTime != null && trigger.UpdatedTime != null && trigger.UpdatedTime < oldTrigger.UpdatedTime))
            {
                return Task.CompletedTask;
            }
            //赋值
            oldTrigger.Status = trigger.Status;
            oldTrigger.Result = trigger.Result;
            oldTrigger.LastRunTime = trigger.LastRunTime;
            oldTrigger.NextRunTime = trigger.NextRunTime;
            oldTrigger.NumberOfErrors = trigger.NumberOfErrors;
            oldTrigger.NumberOfRuns = trigger.NumberOfRuns;
            oldTrigger.ElapsedTime = trigger.ElapsedTime;
            //打开弹框时不刷新
            if (openOperationDialog)
            {
                return Task.CompletedTask;
            }
            return base.RefreshPageDom();
        }
        /// <summary>
        /// 配置弹框
        /// </summary>
        /// <param name="dialogSettings"></param>
        protected override void SetOperationDialogSettings(OperationDialogSettings dialogSettings)
        {
            dialogSettings.Width = 1000;
            base.SetOperationDialogSettings(dialogSettings);
        }
        /// <summary>
        /// 暂停
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task OnClickPause(int id)
        {
            if (await ConfirmService.YesNo(EasyJobLocalResource.Pause) == ConfirmResult.Yes)
            {
                bool result = await sysJobTriggerService.Pause(id);
                if (result)
                {
                    //成功
                    MessageService.Success(Localizer.Combination(nameof(EasyJobLocalResource.Pause), nameof(SharedLocalResource.Success)));
                    await base.ReLoadTable();
                }
                else
                {
                    //失败
                    MessageService.Error(Localizer.Combination(nameof(EasyJobLocalResource.Pause), nameof(SharedLocalResource.Fail)));
                }
            }
        }
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task OnClickStart(int id)
        {
            if (await ConfirmService.YesNo(EasyJobLocalResource.Start) == ConfirmResult.Yes)
            {
                bool result = await sysJobTriggerService.Start(id);
                if (result)
                {
                    //成功
                    MessageService.Success(Localizer.Combination(nameof(EasyJobLocalResource.Start), nameof(SharedLocalResource.Success)));
                    await base.ReLoadTable();
                }
                else
                {
                    //失败
                    MessageService.Error(Localizer.Combination(nameof(EasyJobLocalResource.Start), nameof(SharedLocalResource.Fail)));
                }
            }
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
        protected override Task<bool> OnClickAddRunBefore(OperationDialogInput<int> input)
        {
            openOperationDialog = true;
            return base.OnClickAddRunBefore(input);
        }
        protected override Task<bool> OnClickDetailRunBefore(OperationDialogInput<int> input)
        {
            openOperationDialog = true;
            return base.OnClickDetailRunBefore(input);
        }

        protected override Task<bool> OnClickEditRunBefore(OperationDialogInput<int> input)
        {
            openOperationDialog = true;
            return base.OnClickEditRunBefore(input);
        }

        protected override Task<bool> OnAddDialogCloseAfter(OperationDialogInput<int> input, OperationDialogOutput<int>? output)
        {
            openOperationDialog = false;
            return base.OnAddDialogCloseAfter(input, output);
        }
        protected override Task<bool> OnEditDialogCloseAfter(OperationDialogInput<int> input, OperationDialogOutput<int>? output)
        {
            openOperationDialog = false;
            return base.OnAddDialogCloseAfter(input, output);
        }
        protected override Task<bool> OnDetailDialogCloseAfter(OperationDialogInput<int> input, OperationDialogOutput<int>? output)
        {
            openOperationDialog = false;
            return base.OnAddDialogCloseAfter(input, output);
        }
        /// <summary>
        /// 打开日志控制台
        /// </summary>
        /// <param name="jobId"></param>
        private Task OpenJobLogConsole(SysJobTriggerDto detail)
        {
            OperationDialogSettings operationDialogSettings = base.GetOperationDialogSettings();

            operationDialogSettings.ModalMaximizable = true;
            openOperationDialog = true;
            return base.OpenOperationDialogAsync<JobLogConsole, JobLogConsoleInput, bool>(detail.Description ?? (detail.JobId + " " + detail.TriggerId), new JobLogConsoleInput(detail.JobId, detail.TriggerId),
                operationDialogSettings: operationDialogSettings,
                onClose: b =>
                {
                    openOperationDialog = false;
                    return Task.CompletedTask;
                });
        }
    }
}
