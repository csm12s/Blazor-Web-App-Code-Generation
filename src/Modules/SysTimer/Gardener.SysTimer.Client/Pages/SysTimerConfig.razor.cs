// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Common;
using Gardener.SysTimer.Dtos;
using Gardener.SysTimer.Enums;
using Gardener.SysTimer.Services;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gardener.SysTimer.Client.Pages
{
    public partial class SysTimerConfig : ListOperateTableBase<SysTimerDto, int, SysTimerEdit>
    {
        [Inject]
        private ISysTimerService SystimerService { get; set; } = null!;

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected async Task OnStartExecute(SysTimerDto model)
        {
            string title = TimerStatus.Running.Equals(model.TimerStatus) ? Localizer[SharedLocalResource.Close] : Localizer[SharedLocalResource.Open];
            var resultStop = await ConfirmService.YesNo(Localizer[title], Localizer[SharedLocalResource.OperateConfirmMessage]);
            if (resultStop == ConfirmResult.Yes)
            {
                switch (model.TimerStatus)
                {
                    case TimerStatus.Running:
                        await SystimerService.Stop(model.JobName);
                        break;
                    case TimerStatus.Stopped:
                        await SystimerService.Start(model.JobName);
                        break;
                    default:
                        await SystimerService.Start(model.JobName);
                        break;
                }
                Thread.Sleep(1000);
                await ReLoadTable(false);
            }
            
        }

        public static readonly TableFilter<ExecuteType>[] FunctionRequestTypeFilters = EnumHelper.EnumToList<ExecuteType>().Select(x => { return new TableFilter<ExecuteType>() { Text = x.ToString(), Value = x }; }).ToArray();
        public static readonly TableFilter<TimerStatus>[] FunctionTimerStatusFilters = EnumHelper.EnumToList<TimerStatus>().Select(x => { return new TableFilter<TimerStatus>() { Text = x.ToString(), Value = x }; }).ToArray();
    }
}
