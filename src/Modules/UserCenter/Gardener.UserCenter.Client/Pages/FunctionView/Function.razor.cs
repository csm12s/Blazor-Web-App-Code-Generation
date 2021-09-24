// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using AntDesign.TableModels;
using Gardener.Client.Base;
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
    public partial class Function
    {
        ITable _table;
        FunctionDto[] _datas;
        IEnumerable<FunctionDto> _selectedRows;
        int _total = 0;
        string _name = string.Empty;
        bool _tableIsLoading = false;
        
        [Inject]
        public MessageService messageService { get; set; }
        [Inject]
        public IFunctionService functionService { get; set; }
        [Inject]
        ConfirmService confirmService { get; set; }
        [Inject]
        DrawerService drawerService { get; set; }
        PageRequest pageRequest = new PageRequest();
        /// <summary>
        /// 页面初始化完成
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            
        }
        /// <summary>
        /// 组件渲染后
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                //首次渲染 触发table OnChange
                await InvokeAsync(StateHasChanged);
            }
        }
        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <returns></returns>
        private async Task ReLoadTable()
        {
            _tableIsLoading = true;
            pageRequest = _table?.GetPageRequest() ?? new PageRequest();
            var pagedListResult = await functionService.Search(pageRequest);
            if (pagedListResult != null)
            {
                var pagedList = pagedListResult;
                _datas = pagedList.Items.ToArray();
                _total = pagedList.TotalCount;
            }
            else
            {
                messageService.Error("加载失败");
            }
            _tableIsLoading = false;
        }
        /// <summary>
        /// 刷新页面
        /// </summary>
        /// <returns></returns>
        private async Task OnReLoadTable()
        {
            await ReLoadTable();
        }

        /// <summary>
        /// 查询变化
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        private async Task OnChange(QueryModel<FunctionDto> queryModel)
        {
            await ReLoadTable();
        }
        /// <summary>
        /// 点击删除按钮
        /// </summary>
        /// <param name="id"></param>
        private async Task OnDeleteClick(Guid id)
        {
            if (await confirmService.YesNoDelete() == ConfirmResult.Yes)
            {
                var result = await functionService.Delete(id);
                if (result)
                {
                    await ReLoadTable();
                    messageService.Success("删除成功");
                }
                else
                {
                    messageService.Error("删除失败");
                }
            }

        }
        private bool onDeletesLoading = false;
        /// <summary>
        /// 点击删除选中按钮
        /// </summary>
        private async Task OnDeletesClick()
        {
            if (_selectedRows == null || _selectedRows.Count() == 0)
            {
                messageService.Warn("未选中任何行");
            }
            else
            {
                if (await confirmService.YesNoDelete() == ConfirmResult.Yes)
                {
                    onDeletesLoading = true;
                    await InvokeAsync(StateHasChanged);
                    var result = await functionService.Deletes(_selectedRows.Select(x => x.Id).ToArray());
                    if (result)
                    {
                        await ReLoadTable();
                        messageService.Success("删除成功");
                    }
                    else
                    {
                        messageService.Error($"删除失败");
                    }
                    onDeletesLoading = false;
                    await InvokeAsync(StateHasChanged);
                }
            }
        }
        /// <summary>
        /// 点击锁定按钮
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isLocked"></param>
        private async Task OnChangeIsLocked(FunctionDto model, bool isLocked)
        {
            var result = await functionService.Lock(model.Id, isLocked);
            if (!result)
            {
                model.IsLocked = !isLocked;
                messageService.Error("锁定/解锁失败");
            }
        }

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
