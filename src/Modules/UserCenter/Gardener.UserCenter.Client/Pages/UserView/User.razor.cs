// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign.TableModels;
using AntDesign;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Gardener.Common;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;
using Gardener.EntityFramwork.Dto;
using Gardener.Client.Base;
using Gardener.Enums;
using Gardener.EntityFramwork.Enums;
using Gardener.Client.Base.Components;

namespace Gardener.UserCenter.Client.Pages.UserView
{
    public partial class User : TableBase<UserDto, int>
    {
        private Tree<DeptDto> _deptTree;
        private List<DeptDto> depts;
        private bool _deptTreeIsLoading = false;
        [Inject]
        IDeptService deptService { get; set; }
        int _currentDeptId = 0;
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
        private async Task SelectedKeyChanged(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                _currentDeptId = 0;
            }
            else 
            {
                int newId = int.Parse(key);
                _currentDeptId = newId;
            }
            await ReLoadTable();
        }
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="pageRequest"></param>
        /// <returns></returns>
        protected override PageRequest ConfigurationPageRequest(PageRequest pageRequest)
        {
            if (_currentDeptId>0)
            {
                var node = TreeTools.QueryNode(depts, d => d.Id.Equals(_currentDeptId), d => d.Children);
                List<int> ids = TreeTools.GetAllChildrenNodes(node, d => d.Id, d => d.Children);
                if (ids != null)
                {
                    pageRequest.FilterGroups.Add(new FilterGroup().AddRule(new FilterRule(nameof(UserDto.DeptId), ids, FilterOperate.In)));
                }
            }

            return pageRequest;
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
                pageRequest.PageIndex = 1;
                await ReLoadTable();
            }
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
