// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using AntDesign.TableModels;
using AntDesign;
using Gardener.Core.Dtos;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Gardener.Client.Services;
using Mapster;

namespace Gardener.Client.Pages.UserCenter
{
    public partial class User
    {
        ITable table;
        UserDto[] users;
        IEnumerable<UserDto> selectedRows;

        int _pageIndex = 1;
        int _pageSize = 10;
        int _total = 0;
        string _name = string.Empty;
        bool drawerVisible = false;
        string drawerTitle = String.Empty;
        bool formIsLoading = false;
        bool tableIsLoading = false;
        UserDto editModel = new UserDto();
        [Inject]
        MessageService MessaheSvr { get; set; }
        [Inject]
        IUserService UserService { get; set; }
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
            var pagedListResult = await UserService.Search(_name, _pageIndex, _pageSize);
            if (pagedListResult.Successed)
            {
                var pagedList = pagedListResult.Data;
                users = pagedList.Items.ToArray();
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
        private async Task onChange(QueryModel<UserDto> queryModel)
        {
            tableIsLoading = true;
            var pagedListResult = await UserService.Search(_name, _pageIndex, _pageSize);
            if (pagedListResult.Successed)
            {
                var pagedList = pagedListResult.Data;
                users = pagedList.Items.ToArray();
                _total = pagedList.TotalCount;
            }
            else
            {
                MessaheSvr.Error("加载失败1");
            }
            tableIsLoading = false;
        }
        /// <summary>
        /// 点击删除按钮
        /// </summary>
        /// <param name="id"></param>
        private async void OnDeleteClick(int id)
        {
            if (await ConfirmSvr.YesNoDelete() == ConfirmResult.Yes)
            {
                var result = await UserService.FakeDelete(id);
                if (result.Successed)
                {
                    users = users.Remove(users.FirstOrDefault(x => x.Id == id));
                    MessaheSvr.Success("删除成功");
                }
                else
                {
                    MessaheSvr.Error("删除失败");
                }
                await InvokeAsync(StateHasChanged);
            }

        }
        /// <summary>
        /// 点击编辑按钮
        /// </summary>
        /// <param name="model"></param>
        private async void OnEditClick(UserDto model)
        {
            if (model.UserExtension == null)
            {
                model.UserExtension = new UserExtensionDto()
                {
                    UserId = model.Id
                };
            }
            drawerTitle = "编辑";
            model.Adapt(editModel);
            drawerVisible = true;
            await InvokeAsync(StateHasChanged);
        }
        /// <summary>
        /// 点击添加按钮
        /// </summary>
        private async void OnAddClick()
        {
            new UserDto().Adapt(editModel);
            drawerTitle = "添加";
            drawerVisible = true;
            await InvokeAsync(StateHasChanged);
        }
        /// <summary>
        /// 抽屉关闭时
        /// </summary>
        private void OnDrawerClose()
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
                var result = await UserService.Insert(editModel);
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
                    MessaheSvr.Error("添加失败");
                }

            }
            else
            {
                editModel.Roles = null;
                editModel.UserRoles = null;
                //修改
                var result = await UserService.Update(editModel);
                formIsLoading = false;
                drawerVisible = false;
                if (result.Successed)
                {
                    await ReLoadTable();
                    MessaheSvr.Success("修改成功", 1);
                }
                else
                {
                    MessaheSvr.Error("修改失败", 1);
                }

            }

        }
        /// <summary>
        /// 表单失败时
        /// </summary>
        /// <param name="editContext"></param>
        private void OnFormFinishFailed(EditContext editContext)
        {
            //drawerVisible = false;
        }
        /// <summary>
        /// 表单取消
        /// </summary>
        private void OnFormCancel()
        {
            new UserDto().Adapt(editModel);
            drawerVisible = false;
        }
        /// <summary>
        /// 点击删除选中按钮
        /// </summary>
        private async void OnDeletesClick()
        {
            if (selectedRows == null || selectedRows.Count() == 0)
            {
                MessaheSvr.Warn("未选中任何行");
            }
            else
            {
                if (await ConfirmSvr.YesNoDelete() == ConfirmResult.Yes)
                {
                    var result = await UserService.FakeDeletes(selectedRows.Select(x => x.Id).ToArray());
                    if (result.Successed)
                    {
                        users = users.Where(x => !selectedRows.Any(y => y.Id == x.Id)).ToArray();
                        MessaheSvr.Success("删除成功");
                    }
                    else
                    {
                        MessaheSvr.Error($"删除失败");
                    }
                    await InvokeAsync(StateHasChanged);
                }
            }
        }
        /// <summary>
        /// 点击锁定按钮
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isLocked"></param>
        private async void OnChangeIsLocked(UserDto model, bool isLocked)
        {
            var result = await UserService.Lock(model.Id, isLocked);
            if (!result.Successed)
            {
                model.IsLocked = !isLocked;
                MessaheSvr.Error("锁定失败");
            }
        }
    }
}
