// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.AntDesignUi.Base;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Common;
using Gardener.EasyJob.Dtos;
using Gardener.EasyJob.Dtos.Notification;
using Gardener.EasyJob.Resources;
using Gardener.EasyJob.Services;
using Gardener.EventBus;
using Mapster;
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
        public IEasyJobUserConfigService userConfigService { get; set; } = null!;
        /// <summary>
        /// 触发器更新通知订阅者
        /// </summary>
        private Subscriber? triggerUpdateNotificationSubscriber;
        /// <summary>
        /// 用户配置
        /// </summary>
        private EasyJobUserConfigDto? easyJobUserConfigDto;

        private bool enableRealTimeMonitor = false;
        private bool enableRealTimeMonitorLoading = false;
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
            if (oldTrigger == null || (oldTrigger.UpdatedTime!=null && trigger.UpdatedTime!=null && trigger.UpdatedTime< oldTrigger.UpdatedTime))
            {
                return Task.CompletedTask;
            }
            //赋值
            trigger.Adapt(oldTrigger);
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
            if (await ConfirmService.YesNo(Localizer[EasyJobLocalResource.Pause]) == ConfirmResult.Yes)
            {
                bool result = await sysJobTriggerService.Pause(id);
                if (result)
                {
                    //成功
                    MessageService.Success(Localizer.Combination(EasyJobLocalResource.Pause, EasyJobLocalResource.Success));
                    await base.ReLoadTable();
                }
                else
                {
                    //失败
                    MessageService.Error(Localizer.Combination(EasyJobLocalResource.Pause, EasyJobLocalResource.Fail));
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
            if (await ConfirmService.YesNo(Localizer[EasyJobLocalResource.Start]) == ConfirmResult.Yes)
            {
                bool result = await sysJobTriggerService.Start(id);
                if (result)
                {
                    //成功
                    MessageService.Success(Localizer.Combination(EasyJobLocalResource.Start, EasyJobLocalResource.Success));
                    await base.ReLoadTable();
                }
                else
                {
                    //失败
                    MessageService.Error(Localizer.Combination(EasyJobLocalResource.Start, EasyJobLocalResource.Fail));
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

            EasyJobUserConfigDto? result = await userConfigService.SaveMyConfig(easyJobUserConfigDto);
            if (result == null)
            {
                MessageService.Error((enable ? Localizer[EasyJobLocalResource.Open] : Localizer[EasyJobLocalResource.Close]) + Localizer[EasyJobLocalResource.Fail]);
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
