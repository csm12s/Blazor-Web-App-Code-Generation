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
using Gardener.EasyJob.Resources;
using Gardener.EasyJob.Services;
using Microsoft.AspNetCore.Components;

namespace Gardener.EasyJob.Client.Pages.JobView
{
    /// <summary>
    /// 
    /// </summary>
    public partial class JobTrigger : ListOperateTableBase<SysJobTriggerDto, int, JobTriggerEdit, EasyJobLocalResource>
    {
        [Inject]
        public ISysJobTriggerService sysJobTriggerService { get; set; } = null!;
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
    }
}
