// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.SysTimer.Dtos;
using Gardener.SysTimer.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.SysTimer.Client.Pages
{
    public partial class SysTimerEdit :EditOperationDialogBase<SysTimerDto, int>
    {

        [Inject]
        private ISysTimerService SysTimerService { get; set; } = null!;

        /// <summary>
        /// 本地方法
        /// </summary>
        private IEnumerable<TaskMethodInfo> localJobs=new List<TaskMethodInfo>();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await base.StartLoading();
            var t1 = base.OnInitializedAsync();
            var t2 = SysTimerService.GetLocalJobs();
            await t2;
            localJobs = await t2;
            await base.StopLoading();
        }
    }
}
