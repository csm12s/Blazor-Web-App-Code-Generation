// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gardener.Common;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;
using Gardener.Client.Base.Components;
using Gardener.Base;
using Microsoft.AspNetCore.Components.Web;
using Gardener.Client.Base;

namespace Gardener.UserCenter.Client.Pages.UserView
{
    public partial class User : ListTableBase<UserDto, int, UserEdit>
    {
        private Tree<DeptDto> _deptTree;
        private List<DeptDto> depts;
        private bool _deptTreeIsLoading = false;
        [Inject]
        IDeptService deptService { get; set; }
        int _currentDeptId = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dialogSettings"></param>
        protected override void SetOperationDialogSettings(OperationDialogSettings dialogSettings)
        {
            dialogSettings.Width = 1000;
        }

        /// <summary>
        /// 页面初始化完成
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await ReLoadDepts(null);
            await base.OnInitializedAsync();
        }
        /// <summary>
        /// 重载部门信息
        /// </summary>
        /// <returns></returns>
        private async Task ReLoadDepts(MouseEventArgs eventArgs)
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
        protected override void ConfigurationPageRequest(PageRequest pageRequest)
        {
            if (_currentDeptId > 0)
            {
                var node = TreeTools.QueryNode(depts, d => d.Id.Equals(_currentDeptId), d => d.Children);
                List<int> ids = TreeTools.GetAllChildrenNodes(node, d => d.Id, d => d.Children);
                if (ids != null)
                {
                    pageRequest.FilterGroups.Add(new FilterGroup().AddRule(new FilterRule(nameof(UserDto.DeptId), ids, FilterOperate.In)));
                }
            }
        }

        /// <summary>
        /// 点击分配角色
        /// </summary>
        /// <param name="userId"></param>
        private async Task OnEditUserRoleClick(int userId)
        {
            await OpenOperationDialogAsync<UserRoleEdit, int, bool>(localizer["设置角色"], userId, async r =>
            {
                await ReLoadTable();
            },width:500);
        }
        /// <summary>
        /// 点击头像
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task OnAvatarClick(UserDto user)
        {
            OperationDialogSettings settings = base.GetOperationDialogSettings();
            settings.Width = 300;
            settings.DrawerPlacement = Placement.Left;
            await OpenOperationDialogAsync<UserUploadAvatar, UserUploadAvatarParams, string>(localizer["上传头像"],
                new UserUploadAvatarParams
                {
                    User = user,
                    SaveDb = true
                },
                async r =>
                {
                    await ReLoadTable();
                }
            , settings);
        }

    }
}
