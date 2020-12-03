// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using AntDesign.TableModels;
using AntDesign;
using Gardener.Application.Dtos;
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
        MessageService MessageSvr { get; set; }
        [Inject]
        IUserService UserSvr { get; set; }
        [Inject]
        ConfirmService ConfirmSvr { get; set; }
        [Inject]
        public DrawerService DrawerSvr { get; set; }
        [Inject]
        IRoleService RoleSvr { get; set; }
        /// <summary>
        /// 页面初始化完成
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            MessageSvr.Config(new MessageGlobalConfig()
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
            var pagedListResult = await UserSvr.Search(_name, _pageIndex, _pageSize);
            if (pagedListResult.Successed)
            {
                var pagedList = pagedListResult.Data;
                users = pagedList.Items.ToArray();
                _total = pagedList.TotalCount;
            }
            else
            {
                MessageSvr.Error("加载失败");
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
            await ReLoadTable();
        }
        /// <summary>
        /// 点击删除按钮
        /// </summary>
        /// <param name="id"></param>
        private async Task OnDeleteClick(int id)
        {
            if (await ConfirmSvr.YesNoDelete() == ConfirmResult.Yes)
            {
                var result = await UserSvr.FakeDelete(id);
                if (result.Successed)
                {
                    users = users.Remove(users.FirstOrDefault(x => x.Id == id));
                    MessageSvr.Success("删除成功");
                }
                else
                {
                    MessageSvr.Error("删除失败");
                }
               //await InvokeAsync(StateHasChanged);
            }

        }
        /// <summary>
        /// 点击编辑按钮
        /// </summary>
        /// <param name="model"></param>
        private async Task OnEditClick(UserDto model)
        {
            formIsLoading = true;
            drawerVisible = true;
            //Task.Run(() => {
                if (model.UserExtension == null)
                {
                    model.UserExtension = new UserExtensionDto()
                    {
                        UserId = model.Id
                    };
                }
                drawerTitle = "编辑";
                model.Adapt(editModel);
                formIsLoading = false;
            //});
           //await InvokeAsync(StateHasChanged);
        }
        /// <summary>
        /// 点击添加按钮
        /// </summary>
        private async Task OnAddClick()
        {
            new UserDto().Adapt(editModel);
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
                var result = await UserSvr.Insert(editModel);
                formIsLoading = false;
                drawerVisible = false;
                if (result.Successed)
                {
                    MessageSvr.Success("添加成功");
                    _pageIndex = 1;
                    _name = string.Empty;
                    await ReLoadTable();
                }
                else
                {
                    MessageSvr.Error("添加失败");
                }

            }
            else
            {
                editModel.Roles = null;
                //修改
                var result = await UserSvr.Update(editModel);
                formIsLoading = false;
                drawerVisible = false;
                if (result.Successed)
                {
                    await ReLoadTable();
                    MessageSvr.Success("修改成功", 1);
                }
                else
                {
                    MessageSvr.Error("修改失败", 1);
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
            new UserDto().Adapt(editModel);
            drawerVisible = false;
        }
        /// <summary>
        /// 点击删除选中按钮
        /// </summary>
        private async Task OnDeletesClick()
        {
            if (selectedRows == null || selectedRows.Count() == 0)
            {
                MessageSvr.Warn("未选中任何行");
            }
            else
            {
                if (await ConfirmSvr.YesNoDelete() == ConfirmResult.Yes)
                {
                    var result = await UserSvr.FakeDeletes(selectedRows.Select(x => x.Id).ToArray());
                    if (result.Successed)
                    {
                        users = users.Where(x => !selectedRows.Any(y => y.Id == x.Id)).ToArray();
                        MessageSvr.Success("删除成功");
                    }
                    else
                    {
                        MessageSvr.Error($"删除失败");
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
            var result = await UserSvr.Lock(model.Id, isLocked);
            if (!result.Successed)
            {
                model.IsLocked = !isLocked;
                MessageSvr.Error("锁定失败");
            }
        }



        #region 分配角色

        private bool editRoleDrawerVisible;

        private CheckboxOption[] roleOptions=new CheckboxOption[] { };

        private bool editRoleFormIsLoading;
        private UserDto editRoleModel;

        private async Task OnEditRoleDrawerClose()
        {
            editRoleDrawerVisible = false;
        }
        /// <summary>
        /// 点击分配角色
        /// </summary>
        /// <param name="model"></param>
        private async Task OnEditUserRoleClick(UserDto model)
        {
            editRoleFormIsLoading = true;
            editRoleModel = model;
            LoadAllRoles(editRoleModel.Roles);
        }
        /// <summary>
        /// 加载所有角色
        /// </summary>
        /// <param name="roles"></param>
        private async Task LoadAllRoles(ICollection<RoleDto> roles)
        {
            var rolesResult = await RoleSvr.GetEffective();
            if (rolesResult.Successed)
            {
                if (rolesResult.Data == null || rolesResult.Data.Count() == 0)
                {
                    MessageSvr.Error("没有可用角色，请先添加角色");
                    return;
                }
                roleOptions = rolesResult.Data?.Select(x => new CheckboxOption
                {
                    Label = x.Name,
                    Value = x.Id.ToString(),
                    Checked = roles.Any(y => y.Id == x.Id)
                }).ToArray();
                editRoleDrawerVisible = true;
            }
            else
            {
                MessageSvr.Error("角色加载失败");
            }
            editRoleFormIsLoading = false;
            await InvokeAsync(StateHasChanged);
        }
        /// <summary>
        /// 当角色选择有变化时
        /// </summary>
        /// <param name="values"></param>
        private async Task OnEditUserRoleChange(string[] values)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        private async Task OnEditRoleSaveClick()
        {

            string[] selectRoles = roleOptions.Where(x => x.Checked).Select(x => x.Value).ToArray();

            editRoleFormIsLoading = true;
            var result=await UserSvr.Role(editRoleModel.Id, selectRoles?.Select(x=>int.Parse(x)).ToArray());
            
            if (result.Successed)
            {
                editRoleDrawerVisible = false;
                MessageSvr.Success("设置成功");
                editRoleModel.Roles = selectRoles == null ? null : roleOptions.ToList().Where(x => selectRoles.Any(y => y.Equals(x.Value))).Select(x => new RoleDto { Id = int.Parse(x.Value), Name = x.Label }).ToList();
            }
            else {
                MessageSvr.Error("设置失败");
            }
            editRoleFormIsLoading = false;
           //await InvokeAsync(StateHasChanged);
        }
        #region 全选
        private bool indeterminateRole => roleOptions.Count(o => o.Checked) > 0 && roleOptions.Count(o => o.Checked) < roleOptions.Count();

        private bool checkAllRole => roleOptions.All(o => o.Checked);

        private async Task CheckAllRoleChanged()
        {
            bool allChecked = checkAllRole;
            roleOptions.ForEach(o => o.Checked = !allChecked);
        }
        #endregion

        #endregion


    }
}
