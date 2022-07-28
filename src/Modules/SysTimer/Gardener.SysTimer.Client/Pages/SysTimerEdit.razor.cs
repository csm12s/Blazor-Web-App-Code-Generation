// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.SysTimer.Client.Services;
using Gardener.SysTimer.Dtos;
using Gardener.SysTimer.Services;
using Gardener.UserCenter.Dtos;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.SysTimer.Client.Pages
{
    public partial class SysTimerEdit :EditDrawerBase<SysTimerDto, int>
    {

        [Inject]
        private ISysTimerService sysTimerService { get; set; }

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
            localJobs=await sysTimerService.GetLocalJobs();
            await base.OnInitializedAsync();
        }
    }
}
