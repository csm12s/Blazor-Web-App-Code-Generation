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
using System.Threading.Tasks;

namespace Gardener.SysTimer.Client.Pages
{
    public partial class SysTimerConfig: TableBase<SysTimerDto, int, SysTimerEdit>
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
            switch (model.TimerStatus)
            {
                case TimerStatus.Running:
                    var resultStop = await confirmService.YesNo("执行", "确认要停止运行吗？");
                    if (resultStop == ConfirmResult.Yes)
                    {
                        _systimerService.Stop(new StopJobInput { JobName = model.JobName });
                    }
                    break;
                case TimerStatus.Stopped:
                    var resultStart = await confirmService.YesNo("执行", "确认要开始运行吗？");
                    if (resultStart == ConfirmResult.Yes)
                    {
                        _systimerService.Start(model);
                    }
                    break;
                default:
                    var result = await confirmService.YesNo("执行", "任务处于非正常状态，尝试重新运行？");
                    if (result == ConfirmResult.Yes)
                    {
                        _systimerService.Start(model);
                    }
                    break;
            }
            await ReLoadTable(false);
        }

        public readonly static TableFilter<ExecuteType>[] FunctionRequestTypeFilters = EnumHelper.EnumToList<ExecuteType>().Select(x => { return new TableFilter<ExecuteType>() { Text = x.ToString(), Value = x }; }).ToArray();
        public readonly static TableFilter<TimerStatus>[] FunctionTimerStatusFilters = EnumHelper.EnumToList<TimerStatus>().Select(x => { return new TableFilter<TimerStatus>() { Text = x.ToString(), Value = x }; }).ToArray();
    }
}
