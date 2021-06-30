// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using AntDesign.TableModels;
using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gardener.Client.Pages.SystemManager
{
    public partial class Function
    {
        ITable _table;
        FunctionDto[] _datas;
        IEnumerable<FunctionDto> _selectedRows;
        FunctionSearchInput searchInput = new FunctionSearchInput();
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

        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <returns></returns>
        private async Task ReLoadTable()
        {
            _tableIsLoading = true;
            var pagedListResult = await functionService.Search(searchInput);
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
            searchInput.OrderConditions = queryModel.
                SortModel.
                Select(x => x.Adapt<SearchSort>()).ToArray();
            if (searchInput.OrderConditions.Length == 0)
            {
                searchInput.OrderConditions = new[] {
                    new SearchSort()
                    {
                        FieldName=nameof(FunctionDto.CreatedTime),
                        SortType=SearchSortType.Desc
                    }
                };
            }
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
            Task.Run(async () =>
            {
                var result = await functionService.Lock(model.Id, isLocked);
                if (!result)
                {
                    model.IsLocked = !isLocked;
                    messageService.Error("锁定失败");
                }
            });
        }

        // <summary>
        /// 点击启用审计按钮
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isLocked"></param>
        private async Task OnChangeEnableAudit(FunctionDto model, bool enableAudit)
        {
            Task.Run(async () =>
            {
                var result = await functionService.EnableAudit(model.Id, enableAudit);
                if (!result)
                {
                    model.EnableAudit = !enableAudit;
                    messageService.Error((enableAudit?"启用":"禁用")+"失败");
                }
            });
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
                searchInput.PageIndex = 1;
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
    }
}
