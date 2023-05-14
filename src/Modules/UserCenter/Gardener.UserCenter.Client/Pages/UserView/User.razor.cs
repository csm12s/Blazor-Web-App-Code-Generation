// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gardener.Common;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;
using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.UserCenter.Resources;
using Gardener.Client.AntDesignUi.Base.Components;
using Microsoft.AspNetCore.Components.Web;
using Gardener.Client.AntDesignUi.Base;
using AntDesign;

namespace Gardener.UserCenter.Client.Pages.UserView
{
    public partial class User : ListOperateTableBase<UserDto, int, UserEdit, UserCenterResource>
    {
        private Tree<DeptDto>? _deptTree;
        private List<DeptDto>? depts;
        private bool _deptTreeIsLoading = false;
        private int _currentDeptId = 0;
        [Inject]
        private IDeptService deptService { get; set; } = null!;
        /// <summary>
        /// 排除搜索字段
        /// </summary>
        private List<string> excludeSearchFields = new List<string>() { nameof(UserDto.Password), nameof(UserDto.Avatar) };
      
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
            base.AddExcludeSearchFields(nameof(UserDto.Password), nameof(UserDto.Avatar));
            await base.OnInitializedAsync();
        }
        /// <summary>
        /// 组件首次渲染后
        /// </summary>
        /// <returns></returns>
        protected override Task OnFirstAfterRenderAsync()
        {
            return Task.WhenAll(ReLoadDepts(null), base.OnFirstAfterRenderAsync());
        }
        /// <summary>
        /// 重载部门信息
        /// </summary>
        /// <returns></returns>
        private async Task ReLoadDepts(MouseEventArgs? eventArgs)
        {
            _deptTreeIsLoading = true;
            depts = await deptService.GetTree();
            _deptTreeIsLoading = false;
        }

        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <returns></returns>
        private Task SelectedKeyChanged(string key)
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
            return ReLoadTable();
        }
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="pageRequest"></param>
        /// <returns></returns>
        protected override void ConfigurationPageRequest(PageRequest pageRequest)
        {
            if (_currentDeptId > 0 && depts != null)
            {
                var node = TreeHelper.QueryNode(depts, d => d.Id.Equals(_currentDeptId), d => d.Children);
                if (null != node)
                {
                    List<int> ids = TreeHelper.GetAllChildrenNodes(node, d => d.Id, d => d.Children);
                    if (ids != null)
                    {
                        pageRequest.FilterGroups.Add(new FilterGroup().AddRule(new FilterRule(nameof(UserDto.DeptId), ids, FilterOperate.In)));
                    }
                }
            }
        }

        /// <summary>
        /// 点击分配角色
        /// </summary>
        /// <param name="user"></param>
        private Task OnEditUserRoleClick(UserDto user)
        {
            return OpenOperationDialogAsync<UserRoleEdit, UserDto, bool>(Localizer["SettingRoles"], user, width: 500);
        }
        /// <summary>
        /// 点击头像
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private Task OnAvatarClick(UserDto user)
        {
            OperationDialogSettings settings = base.GetOperationDialogSettings();
            settings.Width = 300;
            settings.DrawerPlacement = Placement.Left;
            return OpenOperationDialogAsync<UserUploadAvatar, UserUploadAvatarParams, string>(Localizer[SharedLocalResource.UplaodAvatar],
                new UserUploadAvatarParams(user, true),
                async r =>
                {
                    if (r != null)
                    {
                        await ReLoadTable();
                    }
                }
            , settings);
        }

    }
}
