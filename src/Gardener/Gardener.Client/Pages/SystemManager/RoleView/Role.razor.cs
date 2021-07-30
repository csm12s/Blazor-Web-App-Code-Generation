// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using AntDesign.TableModels;
using Gardener.Application.Dtos;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Gardener.Application.Interfaces;

namespace Gardener.Client.Pages.SystemManager.RoleView
{
    public partial class Role
    {
        ITable _table;
        RoleDto[] _roles;
        IEnumerable<RoleDto> _selectedRows;

        int _total = 0;
        string _name = string.Empty;
        bool _tableIsLoading = false;
        [Inject]
        public MessageService messageService { get; set; }
        [Inject]
        public IRoleService roleService { get; set; }
        [Inject]
        ConfirmService confirmService { get; set; }
        [Inject]
        DrawerService drawerService { get; set; }
        [Inject]
        IAuthorizeService authorizeService { get; set; }
        PageRequest pageRequest = new PageRequest();
        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <returns></returns>
        private async Task ReLoadTable()
        {
            _tableIsLoading = true;
            var pagedListResult = await roleService.Search(pageRequest);
            if (pagedListResult != null)
            {
                var pagedList = pagedListResult;
                _roles = pagedList.Items.ToArray();
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
        private async Task onChange(QueryModel<RoleDto> queryModel)
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
                var result = await roleService.FakeDelete(id);
                if (result)
                {
                    _roles = _roles.Remove(_roles.FirstOrDefault(x => x.Id == id));
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
        private async Task OnEditClick(int id)
        {
            var result = await drawerService.CreateDialogAsync<RoleEdit, int, bool>(id, true, title: "编辑", width: 500);

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
            var result = await drawerService.CreateDialogAsync<RoleEdit, int, bool>(0, true, title: "添加", width: 500);

            if (result)
            {
                //刷新列表
                pageRequest.PageIndex = 1;
                _name = string.Empty;
                await ReLoadTable();
            }
        }
        /// <summary>
        /// 点击分配资源
        /// </summary>
        /// <returns></returns>
        private async Task OnEditRoleResourceClick(int id)
        {
            var result = await drawerService.CreateDialogAsync<RoleResourceEdit, int, bool>(id, true, title: "分配资源", width: 600);
            Console.WriteLine(result);
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
                    var result = await roleService.FakeDeletes(_selectedRows.Select(x => x.Id).ToArray());
                    if (result)
                    {
                        _roles = _roles.Where(x => !_selectedRows.Any(y => y.Id == x.Id)).ToArray();
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
        private async Task OnChangeIsLocked(RoleDto model, bool isLocked)
        {
            Task.Run(async () =>
            {
                var result = await roleService.Lock(model.Id, isLocked);
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
            var result = await drawerService.CreateDialogAsync<RoleResourceDownload, string, bool>(
                      string.Empty,
                       true,
                       title: "种子数据",
                       width: 1300,
                       placement: "right");
        }

    }
}
