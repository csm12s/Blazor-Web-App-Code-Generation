// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Services;
using Microsoft.AspNetCore.Components;

namespace Gardener.SystemManager.Client.Pages.FunctionView
{
    public partial class Function : ListOperateTableBase<FunctionDto, Guid, FunctionEdit>
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
                #pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                messageService.Error((enableAudit ? SharedLocalResource.Enable : SharedLocalResource.Disabled) + SharedLocalResource.Fail);
                #pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
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
            await OpenOperationDialogAsync<FunctionImport, int, bool>(localizer[SharedLocalResource.Import], 0, async r =>
            {
                await ReLoadTable();

            }, setting);
        }

    }
}
