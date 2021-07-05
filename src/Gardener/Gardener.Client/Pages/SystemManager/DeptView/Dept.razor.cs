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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client.Pages.SystemManager.DeptView
{
    public partial class Dept
    {
        ITable _table;
        List<DeptDto> _dtos;
        IEnumerable<DeptDto> _selectedRows;

        int _pageIndex = 1;
        int _pageSize = 10;
        int _total = 0;
        string _name = string.Empty;
        bool _tableIsLoading = false;
        [Inject]
        public MessageService messageService { get; set; }
        [Inject]
        public IDeptService deptService { get; set; }
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
            var result = await deptService.GetPage(_pageIndex,_pageSize);
            if (result != null)
            {
                _dtos = result.Items.ToList();
                _total = result.TotalCount;
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
        private async Task onChange(QueryModel<DeptDto> queryModel)
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
                var result = await deptService.FakeDelete(id);
                if (result)
                {
                    _dtos.Remove(_dtos.FirstOrDefault(x => x.Id == id));
                    messageService.Success("删除成功");
                }
                else
                {
                    messageService.Error("删除失败");
                }
                //await InvokeAsync(StateHasChanged);
            }

        }
        /// <summary>
        /// 点击编辑按钮
        /// </summary>
        /// <param name="roleDto"></param>
        private async Task OnEditClick(DeptDto model)
        {
            var result = await drawerService.CreateDialogAsync<DeptEdit, int, bool>(model.Id, true, title: "编辑", width: 500);

            if (result)
            {
                //刷新列表
                var newEntity=await deptService.Get(model.Id);

                newEntity.Adapt(model);
                await InvokeAsync(StateHasChanged);

            }
        }
        /// <summary>
        /// 点击添加按钮
        /// </summary>
        private async Task OnAddClick()
        {
            var result = await drawerService.CreateDialogAsync<DeptEdit, int, bool>(0, true, title: "添加", width: 500);

            //if (result)
            //{
            //    //刷新列表
            //    _pageIndex = 1;
            //    _name = string.Empty;
            //    await ReLoadTable();
            //}
        }

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
                    var result = await deptService.FakeDeletes(_selectedRows.Select(x => x.Id).ToArray());
                    if (result)
                    {
                        _dtos = _dtos.Where(x => !_selectedRows.Any(y => y.Id == x.Id)).ToList();
                        messageService.Success("删除成功");
                    }
                    else
                    {
                        messageService.Error($"删除失败");
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
        private async Task OnChangeIsLocked(DeptDto model, bool isLocked)
        {
            Task.Run(async () =>
            {
                var result = await deptService.Lock(model.Id, isLocked);
                if (!result)
                {
                    model.IsLocked = !isLocked;
                    messageService.Error("锁定失败");
                }
            });
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        private async Task OnDownloadClick()
        {
            //var result = await drawerService.CreateDialogAsync<RoleResourceDownload, string, bool>(
            //          string.Empty,
            //           true,
            //           title: "种子数据",
            //           width: 1300,
            //           placement: "right");
        }
    }
}
