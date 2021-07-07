// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign.TableModels;
using AntDesign;
using Gardener.Application.Dtos;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Gardener.Application.Interfaces;

namespace Gardener.Client.Pages.SystemManager.UserView
{
    public partial class User
    {
        ITable _table;
        UserDto[] _users;
        IEnumerable<UserDto> _selectedRows;
        int _pageIndex = 1;
        int _pageSize = 10;
        int _total = 0;
        bool _tableIsLoading = false;

        private Tree<DeptDto> _deptTree;

        private List<DeptDto> depts;

        private string _deptTreeSelectedKey;
        private bool _deptTreeIsLoading = false;

        [Inject]
        MessageService messageService { get; set; }
        [Inject]
        IUserService userService { get; set; }
        [Inject]
        ConfirmService confirmService { get; set; }
        [Inject]
        DrawerService drawerService { get; set; }
        [Inject]
        IDeptService deptService { get; set; }
        /// <summary>
        /// 页面初始化完成
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            _deptTreeIsLoading = true;
            depts = await deptService.GetTree();
            _deptTreeIsLoading = false;
        }
        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <returns></returns>
        private async Task OnClickDeptTree(TreeEventArgs<DeptDto> e)
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
            int? deptId = string.IsNullOrEmpty(_deptTreeSelectedKey) ? null : int.Parse(_deptTreeSelectedKey);
            var pagedListResult = await userService.Search(deptId, _pageIndex, _pageSize);
            if (pagedListResult != null)
            {
                var pagedList = pagedListResult;
                _users = pagedList.Items.ToArray();
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
        private async Task onChange(QueryModel<UserDto> queryModel)
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
                var result = await userService.FakeDelete(id);
                if (result)
                {
                    _users = _users.Remove(_users.FirstOrDefault(x => x.Id == id));
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
        /// <param name="model"></param>
        private async Task OnEditClick(int userId)
        {
            var result = await drawerService.CreateDialogAsync<UserEdit, int, bool>(userId, true, title: "编辑", width: 800);

            if (result)
            {
                await ReLoadTable();
            }
        }
        /// <summary>
        /// 点击添加按钮
        /// </summary>
        private async Task OnAddClick()
        {
            var result = await drawerService.CreateDialogAsync<UserEdit, int, bool>(0, true, title: "添加", width: 800);

            if (result)
            {
                //刷新列表
                _pageIndex = 1;
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
                messageService.Warn("未选中任何行");
            }
            else
            {
                if (await confirmService.YesNoDelete() == ConfirmResult.Yes)
                {
                    var result = await userService.FakeDeletes(_selectedRows.Select(x => x.Id).ToArray());
                    if (result)
                    {
                        _users = _users.Where(x => !_selectedRows.Any(y => y.Id == x.Id)).ToArray();
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
        private async Task OnChangeIsLocked(UserDto model, bool isLocked)
        {
            Task.Run(async () =>
            {
                var result = await userService.Lock(model.Id, isLocked);
                if (!result)
                {
                    model.IsLocked = !isLocked;
                    messageService.Error("锁定失败");
                }
            });
        }

        /// <summary>
        /// 点击分配角色
        /// </summary>
        /// <param name="userId"></param>
        private async Task OnEditUserRoleClick(int userId)
        {
            var result = await drawerService.CreateDialogAsync<UserRoleEdit, int, bool>(userId, true, title: "分配角色", width: 500);

            if (result)
            {
                await ReLoadTable();
            }
        }
        /// <summary>
        /// 点击头像
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task OnAvatarClick(UserDto user)
        {
            int avatarDrawerWidth = 300;
            await drawerService.CreateDialogAsync<UserUploadAvatar, UserUploadAvatarParams, string>(new UserUploadAvatarParams { User = user, SaveDb = true }, true, title: "上传头像", width: avatarDrawerWidth, placement: "left");
        }

    }
}
