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
        /// 定时执行方式
        /// 0：间隔
        /// 1：Cron
        /// </summary>
        private int executeType = 0;
        /// <summary>
        /// 触发器-执行间隔
        /// </summary>
        private long? triggerInterval
        {
            get
            {

                if (string.IsNullOrEmpty(_editModel.Args) || GetExecuteType() != 0)
                {
                    return null;
                }
                return long.Parse(_editModel.Args.TrimStart('[').TrimEnd(']'));
            }
            set
            {
                if(value!= null)
                {
                    _editModel.Args = $"[{value}]";
                }
            }
        }
        /// <summary>
        /// 触发器-执行Cron
        /// </summary>
        private string? triggerCron
        {
            get
            {

                if (string.IsNullOrEmpty(_editModel.Args) || GetExecuteType()!=1)
                {
                    return null;
                }
                return _editModel.Args.TrimStart('[').TrimEnd(']').Split(",")[0];
            }
            set
            {
                if (value != null)
                {
                    _editModel.Args = $"[{value},{cronType}]";
                }

            }
        }
        /// <summary>
        /// 触发器-Cron类型
        /// </summary>
        private int cronType
        {
            get
            {

                if (string.IsNullOrEmpty(_editModel.Args) || GetExecuteType() != 1)
                {
                    return 0;
                }
                return int.Parse(_editModel.Args.TrimStart('[').TrimEnd(']').Split(",")[1]);
            }
            set
            {
                if (!string.IsNullOrEmpty(triggerCron))
                {
                    _editModel.Args = $"[{triggerCron},{value}]";
                }
            }
        }
        /// <summary>
        /// 定时执行方式-触发器实现类名称
        /// </summary>
        Dictionary<int, string> executeTypeMap = new Dictionary<int, string>()
        {
            {0,"Furion.Schedule.PeriodTrigger" },
            {1,"Furion.Schedule.CronTrigger" },
        };
        /// <summary>
        /// 触发器-Cron类型字典
        /// </summary>
        Dictionary<int, string> cronFormatTypeMap = new Dictionary<int, string>()
        {
            {0,"Default" },
            {1,"WithYears" },
            {2,"WithSeconds" },
            {3,"WithSecondsAndYears" },
        };
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int GetExecuteType()
        {
            foreach (var item in executeTypeMap)
            {
                if (item.Value.Equals(_editModel.TriggerType))
                {
                    return item.Key;
                }
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            //订阅触发器更新
            triggerUpdateNotificationSubscriber = EventBus.Subscribe<EasyJobTriggerUpdateNotificationData>(OnEasyJobTriggerUpdate);
            await base.OnInitializedAsync();
            executeType = GetExecuteType();
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
            if (_editModel == null || _editModel.Id != trigger.Id || (_editModel.UpdatedTime != null && trigger.UpdatedTime != null && trigger.UpdatedTime < _editModel.UpdatedTime))
            {
                return Task.CompletedTask;
            }
            //赋值
            _editModel.Status = trigger.Status;
            _editModel.Result = trigger.Result;
            _editModel.LastRunTime = trigger.LastRunTime;
            _editModel.NextRunTime = trigger.NextRunTime;
            _editModel.NumberOfErrors = trigger.NumberOfErrors;
            _editModel.NumberOfRuns = trigger.NumberOfRuns;
            _editModel.ElapsedTime = trigger.ElapsedTime;
            return base.RefreshPageDom();
        }
        /// <summary>
        /// 
        /// </summary>
        private Task OnChangeExecuteType(int i)
        {
            _editModel.TriggerType = executeTypeMap.GetValueOrDefault(executeType);
            _editModel.Args = null;
            return Task.CompletedTask;
        }
    }
}
