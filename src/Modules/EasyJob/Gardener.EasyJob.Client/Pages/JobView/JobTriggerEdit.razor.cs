// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.EasyJob.Dtos;
using Gardener.EasyJob.Dtos.Notification;
using Gardener.EasyJob.Resources;
using Gardener.EventBus;
using Mapster;
using Microsoft.AspNetCore.Components;

namespace Gardener.EasyJob.Client.Pages.JobView
{
    /// <summary>
    /// 
    /// </summary>
    public partial class JobTriggerEdit : EditOperationDialogBase<SysJobTriggerDto, int, EasyJobLocalResource>
    {
        /// <summary>
        /// 事件
        /// </summary>
        [Inject]
        public IEventBus EventBus { get; set; } = null!;
        /// <summary>
        /// 触发器更新通知订阅者
        /// </summary>
        private Subscriber? triggerUpdateNotificationSubscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Task OnInitializedAsync()
        {
            //订阅触发器更新
            triggerUpdateNotificationSubscriber = EventBus.Subscribe<EasyJobTriggerUpdateNotificationData>(OnEasyJobTriggerUpdate);
            return base.OnInitializedAsync();
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
            if (_editModel == null || _editModel.Id!=trigger.Id)
            {
                return Task.CompletedTask;
            }
            //赋值
            trigger.Adapt(_editModel);
            return base.RefreshPageDom();
        }
    }
}
