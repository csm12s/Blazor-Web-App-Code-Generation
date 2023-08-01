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

namespace Gardener.EasyJob.Client.Pages.JobView
{
    /// <summary>
    /// 
    /// </summary>
    public partial class JobTrigger : ListOperateTableBase<SysJobTriggerDto, int, JobTriggerEdit, EasyJobLocalResource>
    {
        protected override void SetOperationDialogSettings(OperationDialogSettings dialogSettings)
        {
            dialogSettings.Width = 1000;
            base.SetOperationDialogSettings(dialogSettings);
        }
    }
}
