// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using AntDesign;
using AntDesign.TableModels;
using Gardener.Client.Services;
using Gardener.Core.Dtos;
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
        /// <summary>
        /// 页面初始化完成
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {

            tableIsLoading = true;
            MessaheSvr.Config(new MessageGlobalConfig()
            {
                Top = 24,
                Duration = 1,
                MaxCount = 3,
                Rtl = true,
            });

            var pagedListResult = await RoleService.Search(_name, _pageIndex, _pageSize);
            _total = pagedListResult.TotalCount;
            roles = pagedListResult.Items.ToArray();
            tableIsLoading = false;
        }
        /// <summary>
        /// 查询变化
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        async Task onChange(QueryModel<RoleDto> queryModel)
        {
            var pagedListResult = await RoleService.Search(_name, _pageIndex, _pageSize);
            roles = pagedListResult.Items.ToArray();
            _total = pagedListResult.TotalCount;
        }
        /// <summary>
        /// 点击删除按钮
        /// </summary>
        /// <param name="id"></param>
        private async void OnDeleteClick(int id)
        {
            var result = await RoleService.FakeDelete(id);
            if (result)
            {
                roles = roles.Remove(roles.FirstOrDefault(x => x.Id == id));
                MessaheSvr.Success("删除成功");
                InvokeAsync(StateHasChanged);
            }
        }
        /// <summary>
        /// 点击编辑按钮
        /// </summary>
        /// <param name="roleDto"></param>
        private async void OnEditClick(RoleDto roleDto)
        {
            drawerTitle = "编辑";
            roleDto.Adapt(editModel);
            await InvokeAsync(StateHasChanged);
            drawerVisible = true;
        }
        /// <summary>
        /// 点击添加按钮
        /// </summary>
        private async void OnAddClick()
        {
            new RoleDto().Adapt(editModel);
            drawerTitle = "添加";
            await InvokeAsync(StateHasChanged);
            drawerVisible = true;
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
                var role = await RoleService.Insert(editModel);
                if (role?.Id > 0)
                {
                    await OnInitializedAsync();
                    drawerVisible = false;
                    formIsLoading = false;
                    MessaheSvr.Success("添加成功");
                }
                _pageIndex = 1;
                _name = string.Empty;
            }
            else
            {
                //修改
                var result = await RoleService.Update(editModel);
                if (result)
                {
                    await OnInitializedAsync();
                    MessaheSvr.Success("修改成功", 1);
                    drawerVisible = false;
                }
                formIsLoading = false;


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
            new RoleDto().Adapt(editModel);
            drawerVisible = false;
        }
        /// <summary>
        /// 点击删除选中按钮
        /// </summary>
        private async void OnDeletesClick()
        {
            DeleteSelected();
        }
        /// <summary>
        /// 删除选中
        /// </summary>
        /// <returns></returns>
        private async Task DeleteSelected()
        {
            if (selectedRows == null || selectedRows.Count() == 0)
            {
                MessaheSvr.Warn("未选中任何行");
            }
            else
            {
                var result = await RoleService.FakeDeletes(selectedRows.Select(x => x.Id).ToArray());
                if (result)
                {
                    roles = roles.Where(x => !selectedRows.Any(y => y.Id == x.Id)).ToArray();
                    MessaheSvr.Success("删除成功");
                    InvokeAsync(StateHasChanged);
                }
            }
        }
    }
}
