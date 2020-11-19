// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using AntDesign;
using AntDesign.TableModels;
using Gardener.Client.Services;
using Gardener.Core.Dtos;
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
        RoleDto[] roles;
        IEnumerable<RoleDto> selectedRows;
        ITable table;
        int _pageIndex = 1;
        int _pageSize = 10;
        int _total = 0;
        string _name = string.Empty;
        bool visible = false;
        string drawerTitle = String.Empty;
        bool formIsLoading = false;
        bool tableIsLoading = false;
        /// <summary>
        /// form 绑定对象
        /// </summary>
        RoleDto editModel = new RoleDto();
        [Inject]
        public MessageService MessaheSvr { get; set; }
        
        [Inject]
        public IRoleService RoleService { get; set; }

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

        async Task onChange(QueryModel<RoleDto> queryModel)
        {
            var pagedListResult = await RoleService.Search(_name, _pageIndex, _pageSize);
            roles = pagedListResult.Items.ToArray();
            _total = pagedListResult.TotalCount;
        }
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
        /// 
        /// </summary>
        /// <param name="roleDto"></param>
        private async void OnEditClick(RoleDto roleDto)
        {
            drawerTitle = "编辑";
            editModel.Id = roleDto.Id;
            editModel.Name = roleDto.Name;
            editModel.Remark = roleDto.Remark;
            await InvokeAsync(StateHasChanged);
            visible = true;
        }
        private async void OnAddClick()
        {

            editModel.Id = 0;
            editModel.Name = string.Empty;
            editModel.Remark = string.Empty;
            drawerTitle = "添加";
            await InvokeAsync(StateHasChanged);
            visible = true;
        }
        private void Close()
        {
            visible = false;
        }
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
                    visible = false;
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
                    visible = false;
                }
                formIsLoading = false;


            }

        }
        private void OnFormFinishFailed(EditContext editContext)
        {
            //visible = false;
        }
        private void OnFormCancel()
        {
            editModel.Id = 0;
            editModel.Name = string.Empty;
            editModel.Remark = string.Empty;
            visible = false;
        }

        private async void OnDeletesClick()
        {
            DeleteSelected();
        }
        
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
