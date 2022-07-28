// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.Base;
using Gardener.Client.Base.Components;
using Gardener.Common;
using Gardener.SysTimer.Dtos;
using Gardener.SysTimer.Enums;
using Gardener.SysTimer.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gardener.SysTimer.Client.Pages
{
    public partial class SysTimerConfig : TableBase<SysTimerDto, int, SysTimerEdit>
    {
        [Inject]
        private ISysTimerService _systimerService { get; set; }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected async Task OnStartExecute(SysTimerDto model)
        {
            string title = TimerStatus.Running.Equals(model.TimerStatus) ? "关闭" : "开启";
            var resultStop = await confirmService.YesNo(localizer[title], localizer["确认要执行该操作吗？"]);
            if (resultStop == ConfirmResult.Yes)
            {
                switch (model.TimerStatus)
                {
                    case TimerStatus.Running:
                        await _systimerService.Stop(new StopJobInput { JobName = model.JobName });
                        break;
                    case TimerStatus.Stopped:
                        await _systimerService.Start(model);
                        break;
                    default:
                        await _systimerService.Start(model);
                        break;
                }
                Thread.Sleep(1000);
                await ReLoadTable(false);
            }
            
        }

        public readonly static TableFilter<ExecuteType>[] FunctionRequestTypeFilters = EnumHelper.EnumToList<ExecuteType>().Select(x => { return new TableFilter<ExecuteType>() { Text = x.ToString(), Value = x }; }).ToArray();
        public readonly static TableFilter<TimerStatus>[] FunctionTimerStatusFilters = EnumHelper.EnumToList<TimerStatus>().Select(x => { return new TableFilter<TimerStatus>() { Text = x.ToString(), Value = x }; }).ToArray();
    }
}
