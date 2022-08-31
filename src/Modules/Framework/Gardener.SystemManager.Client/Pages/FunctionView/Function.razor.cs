// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base.Components;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Services;
using Microsoft.AspNetCore.Components;

namespace Gardener.SystemManager.Client.Pages.FunctionView
{
    public partial class Function : ListTableBase<FunctionDto, Guid, FunctionEdit>
    {
        [Inject]
        public IFunctionService functionService { get; set; }

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
                messageService.Error((enableAudit ? "启用" : "禁用") + "失败");
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
            await OpenOperationDialogAsync<FunctionImport, int, bool>(localizer["导入"], 0, async r =>
            {
                await ReLoadTable();

            }, setting);
        }

    }
}
