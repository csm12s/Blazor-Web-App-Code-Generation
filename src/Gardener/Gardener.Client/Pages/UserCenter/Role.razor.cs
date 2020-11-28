// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using AntDesign;
using AntDesign.TableModels;
using Gardener.Client.Services;
using Gardener.Application.Dtos;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client.Pages.UserCenter
{
    public partial class Role
    {
        ITable table;
        RoleDto[] roles;
        IEnumerable<RoleDto> selectedRows;

        int _pageIndex = 1;
        int _pageSize = 10;
        int _total = 0;
        string _name = string.Empty;
        bool drawerVisible = false;
        string drawerTitle = String.Empty;
        bool formIsLoading = false;
        bool tableIsLoading = false;
        RoleDto editModel = new RoleDto();
        [Inject]
        public MessageService MessaheSvr { get; set; }
        [Inject]
        public IRoleService RoleService { get; set; }
        [Inject]
        ConfirmService ConfirmSvr { get; set; }
        /// <summary>
        /// 页面初始化完成
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            MessaheSvr.Config(new MessageGlobalConfig()
            {
                Top = 24,
                Duration = 1,
                MaxCount = 3,
                Rtl = true,
            });
        }
        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <returns></returns>
        private async Task ReLoadTable()
        {
            tableIsLoading = true;
            var pagedListResult = await RoleService.Search(_name, _pageIndex, _pageSize);
            if (pagedListResult.Successed)
            {
                var pagedList = pagedListResult.Data;
                roles = pagedList.Items.ToArray();
                _total = pagedList.TotalCount;
            }
            else
            {
                MessaheSvr.Error("加载失败");
            }
            tableIsLoading = false;
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
            tableIsLoading = true;
            var pagedListResult = await RoleService.Search(_name, _pageIndex, _pageSize);
            if (pagedListResult.Successed)
            {
                var pagedList = pagedListResult.Data;
                roles = pagedList.Items.ToArray();
                _total = pagedList.TotalCount;
            }
            else
            {
                MessaheSvr.Error("加载失败");
            }
            tableIsLoading = false;
        }
        /// <summary>
        /// 点击删除按钮
        /// </summary>
        /// <param name="id"></param>
        private async Task OnDeleteClick(int id)
        {
            if (await ConfirmSvr.YesNoDelete() == ConfirmResult.Yes)
            {
                var result = await RoleService.FakeDelete(id);
                if (result.Successed)
                {
                    roles = roles.Remove(roles.FirstOrDefault(x => x.Id == id));
                    MessaheSvr.Success("删除成功");
                }
                else
                {
                    MessaheSvr.Error("删除失败");
                }
                //await InvokeAsync(StateHasChanged);
            }

        }
        /// <summary>
        /// 点击编辑按钮
        /// </summary>
        /// <param name="roleDto"></param>
        private async Task OnEditClick(RoleDto roleDto)
        {
            drawerTitle = "编辑";
            roleDto.Adapt(editModel);
            drawerVisible = true;
            //await InvokeAsync(StateHasChanged);
        }
        /// <summary>
        /// 点击添加按钮
        /// </summary>
        private async Task OnAddClick()
        {
            new RoleDto().Adapt(editModel);
            drawerTitle = "添加";
            drawerVisible = true;
            //await InvokeAsync(StateHasChanged);
        }
        /// <summary>
        /// 抽屉关闭时
        /// </summary>
        private async Task OnDrawerClose()
        {
            drawerVisible = false;
        }
        /// <summary>
        /// 表单完成时
        /// </summary>
        /// <param name="editContext"></param>
        /// <returns></returns>
        private async Task OnFormFinish(EditContext editContext)
        {
            formIsLoading = true;
            //开始请求
            if (editModel.Id == 0)
            {
                //添加
                var result = await RoleService.Insert(editModel);
                formIsLoading = false;
                drawerVisible = false;
                if (result.Successed)
                {
                    MessaheSvr.Success("添加成功");
                    _pageIndex = 1;
                    _name = string.Empty;
                    await ReLoadTable();
                }
                else
                {
                    MessaheSvr.Success("添加失败");
                }

            }
            else
            {
                //修改
                var result = await RoleService.Update(editModel);
                formIsLoading = false;
                drawerVisible = false;
                if (result.Successed)
                {
                    await ReLoadTable();
                    MessaheSvr.Success("修改成功", 1);
                }
                else
                {
                    MessaheSvr.Success("修改失败", 1);
                }

            }

        }
        /// <summary>
        /// 表单失败时
        /// </summary>
        /// <param name="editContext"></param>
        private async Task OnFormFinishFailed(EditContext editContext)
        {
            //drawerVisible = false;
        }
        /// <summary>
        /// 表单取消
        /// </summary>
        private async Task OnFormCancel()
        {
            new RoleDto().Adapt(editModel);
            drawerVisible = false;
        }
        /// <summary>
        /// 点击删除选中按钮
        /// </summary>
        private async Task OnDeletesClick()
        {
            if (selectedRows == null || selectedRows.Count() == 0)
            {
                MessaheSvr.Warn("未选中任何行");
            }
            else
            {
                if (await ConfirmSvr.YesNoDelete() == ConfirmResult.Yes)
                {
                    var result = await RoleService.FakeDeletes(selectedRows.Select(x => x.Id).ToArray());
                    if (result.Successed)
                    {
                        roles = roles.Where(x => !selectedRows.Any(y => y.Id == x.Id)).ToArray();
                        MessaheSvr.Success("删除成功");
                    }
                    else
                    {
                        MessaheSvr.Error($"删除失败");
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
            ChangeIsLocked(model, isLocked);
        }
        private async Task ChangeIsLocked(RoleDto model, bool isLocked)
        {
            var result = await RoleService.Lock(model.Id, isLocked);
            if (!result.Successed)
            {
                model.IsLocked = !isLocked;
                MessaheSvr.Error("锁定失败");
            }
        }
    }
}
