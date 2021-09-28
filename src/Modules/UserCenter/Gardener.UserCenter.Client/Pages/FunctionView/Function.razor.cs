// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using AntDesign.TableModels;
using Gardener.Client.Base;
using Gardener.Client.Base.Components;
using Gardener.EntityFramwork.Dto;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.FunctionView
{
    public partial class Function : TableBase<FunctionDto, Guid>
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
        /// 点击添加按钮
        /// </summary>
        private async Task OnAddClick()
        {
            var result = await drawerService.CreateDialogAsync<FunctionEdit, Guid, bool>(Guid.Empty, true, title: "添加", width: 500);

            if (result)
            {
                //刷新列表
                pageRequest.PageIndex = 1;
                await ReLoadTable();
            }
        }
        /// <summary>
        /// 点击编辑按钮
        /// </summary>
        /// <param name="model"></param>
        private async Task OnEditClick(Guid id)
        {
            var result = await drawerService.CreateDialogAsync<FunctionEdit, Guid, bool>(id, true, title: "编辑", width: 500);

            if (result)
            {
                await ReLoadTable();
            }
        }

        /// <summary>
        /// 点击导入按钮
        /// </summary>
        /// <returns></returns>
        private async Task OnImportClick()
        {
            var result = await drawerService.CreateDialogAsync<FunctionImport, int, bool>(0, true, title: "导入", width: 1000);
            await ReLoadTable();
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        private async Task OnDownloadClick()
        {
            var result = await drawerService.CreateDialogAsync<FunctionDownload, string, bool>(
                      string.Empty,
                       true,
                       title: "种子数据",
                       width: 1300,
                       placement: "right");
        }
    }
}
