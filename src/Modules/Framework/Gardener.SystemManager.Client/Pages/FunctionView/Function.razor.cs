// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Resources;
using Gardener.SystemManager.Services;
using Microsoft.AspNetCore.Components;

namespace Gardener.SystemManager.Client.Pages.FunctionView
{
    public partial class Function : ListOperateTableBase<FunctionDto, Guid, FunctionEdit, SystemManagerResource>
    {
        [Inject]
        public IFunctionService functionService { get; set; } = null!;

        // <summary>
        /// 点击启用审计按钮
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isLocked"></param>
        private async Task OnChangeEnableAudit(FunctionDto model, bool enableAudit)
        {
            var result = await functionService.EnableAudit(model.Id, enableAudit);
            if (!result)
            {
                model.EnableAudit = !enableAudit;
                MessageService.Error((enableAudit ? nameof(SharedLocalResource.Enable) : nameof(SharedLocalResource.Disabled)) + nameof(SharedLocalResource.Fail));
            }
        }

        /// <summary>
        /// 点击导入按钮
        /// </summary>
        /// <returns></returns>
        private async Task OnImportClick()
        {
            var setting = base.GetOperationDialogSettings();
            setting.Width = 1000;
            await OpenOperationDialogAsync<FunctionImport, int, bool>(SharedLocalResource.Import, 0, async r =>
            {
                await ReLoadTable();

            }, setting);
        }

    }
}
