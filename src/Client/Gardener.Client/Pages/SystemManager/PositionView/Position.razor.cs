// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using AntDesign.TableModels;
using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Gardener.Client.Core;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client.Pages.SystemManager.PositionView
{
    public partial class Position
    {
        ITable _table;
        PositionDto[] _positions;
        IEnumerable<PositionDto> _selectedRows;

        int _total = 0;
        bool _tableIsLoading = false;
        [Inject]
        public MessageService messageService { get; set; }
        [Inject]
        public IPositionService positionService { get; set; }
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
        /// 查询变化
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        private async Task OnChange(QueryModel<PositionDto> queryModel)
        {
            await ReLoadTable();
        }
        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <returns></returns>
        private async Task ReLoadTable()
        {
            _tableIsLoading = true;
            pageRequest = _table?.GetPageRequest() ?? new PageRequest();
            var pagedListResult = await positionService.Search(pageRequest);
            if (pagedListResult != null)
            {
                var pagedList = pagedListResult;
                _positions = pagedList.Items.ToArray();
                _total = pagedList.TotalCount;
            }
            else
            {
                _ = messageService.Error("加载失败");
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
        /// 点击删除按钮
        /// </summary>
        /// <param name="id"></param>
        private async Task OnDeleteClick(int id)
        {
            if (await confirmService.YesNoDelete() == ConfirmResult.Yes)
            {
                var result = await positionService.FakeDelete(id);
                if (result)
                {
                    _positions = _positions.Remove(_positions.FirstOrDefault(x => x.Id == id));
                    _ = messageService.Success("删除成功");
                }
                else
                {
                    _ = messageService.Error("删除失败");
                }
                //await InvokeAsync(StateHasChanged);
            }
        }
        /// <summary>
        /// 点击编辑按钮
        /// </summary>
        /// <param name="roleDto"></param>
        private async Task OnEditClick(int id)
        {
            var result = await drawerService.CreateDialogAsync<PositionEdit, int, bool>(id, true, title: "编辑", width: 500);

            if (result)
            {
                //刷新列表
                await ReLoadTable();
            }
        }
        /// <summary>
        /// 点击添加按钮
        /// </summary>
        private async Task OnAddClick()
        {
            var result = await drawerService.CreateDialogAsync<PositionEdit, int, bool>(0, true, title: "添加", width: 500);

            if (result)
            {
                //刷新列表
                pageRequest.PageIndex = 1;
                await ReLoadTable();
            }
        }
        /// <summary>
        /// 点击删除选中按钮
        /// </summary>
        private async Task OnDeletesClick()
        {
            if (_selectedRows == null || _selectedRows.Count() == 0)
            {
                _ = messageService.Warn("未选中任何行");
            }
            else
            {
                if (await confirmService.YesNoDelete() == ConfirmResult.Yes)
                {
                    var result = await positionService.FakeDeletes(_selectedRows.Select(x => x.Id).ToArray());
                    if (result)
                    {
                        _positions = _positions.Where(x => !_selectedRows.Any(y => y.Id == x.Id)).ToArray();
                        _ = messageService.Success("删除成功");
                    }
                    else
                    {
                        _ = messageService.Error($"删除失败");
                    }
                    //await InvokeAsync(StateHasChanged);
                }
            }
        }
        /// <summary>
        /// 点击锁定按钮
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isLocked"></param>
        private async Task OnChangeIsLocked(PositionDto model, bool isLocked)
        {
            var result = await positionService.Lock(model.Id, isLocked);
            if (!result)
            {
                model.IsLocked = !isLocked;
                messageService.Error("锁定/解锁失败");
            }
        }
    }
}
