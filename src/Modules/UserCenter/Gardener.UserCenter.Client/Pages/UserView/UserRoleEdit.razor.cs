// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.Base;
using Gardener.Client.Base.Services;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Resources;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.UserView
{
    public partial class UserRoleEdit: FeedbackComponent<int, bool>
    {
        private bool _isLoading = false;
        private CheckboxOption[] _roleOptions = new CheckboxOption[] { };
        private int _userId = 0;
        [Inject]
        private IUserService UserService { get; set; } = null!;
        [Inject]
        private IClientMessageService MessageService { get; set; } = null!;
        [Inject]
        private IRoleService RoleService { get; set; } = null!;
        [Inject]
        private IClientLocalizer<UserCenterResource> Localizer { get; set; } = null!;
        protected override async Task OnInitializedAsync()
        {
            _isLoading = true;
            _userId = this.Options;
            if (_userId > 0)
            {
                var rolesResult = await RoleService.GetAllUsable();
                if (rolesResult == null || !rolesResult.Any()) 
                {
                    MessageService.Error(Localizer[UserCenterResource.NoRoleNeedAdd]);
                    return;
                }
                var userRoles = await UserService.GetRoles(_userId);
                userRoles = userRoles ?? new List<RoleDto>();
                _roleOptions = rolesResult.Select(x => new CheckboxOption
                {
                    Label = x.Name,
                    Value = x.Id.ToString(),
                    Checked = userRoles.Any(y => y.Id == x.Id)
                }).ToArray();
            }
            _isLoading = false;
            await base.OnInitializedAsync();
        }
        /// <summary>
        /// 当角色选择有变化时
        /// </summary>
        /// <param name="values"></param>
        private Task OnEditUserRoleChange(string[] values)
        {
            //todo: Add operation logic here
            return Task.CompletedTask;
        }
        /// <summary>
        /// 
        /// </summary>
        private async Task OnEditRoleSaveClick()
        {
            _isLoading = true;
            string[] selectRoles = _roleOptions.Where(x => x.Checked).Select(x => x.Value).ToArray();

            var result = await UserService.Role(_userId, selectRoles.Select(x => int.Parse(x)).ToArray());
            if (result)
            {
                MessageService.Success(Localizer.Combination(UserCenterResource.Setting,UserCenterResource.Success));
                await base.FeedbackRef.CloseAsync(true);
            }
            else
            {
                MessageService.Error(Localizer.Combination(UserCenterResource.Setting, UserCenterResource.Fail));
            }
            _isLoading = false;
        }
        /// <summary>
        /// 取消
        /// </summary>
        private async Task OnFormCancel()
        {
            await base.FeedbackRef.CloseAsync(false);
        }
        #region 全选
        private bool _indeterminateRole => _roleOptions.Count(o => o.Checked) > 0 && _roleOptions.Count(o => o.Checked) < _roleOptions.Count();

        private bool _checkAllRole => _roleOptions.All(o => o.Checked);
        /// <summary>
        /// 全选
        /// </summary>
        /// <returns></returns>
        private void CheckAllRoleChanged()
        {
            bool allChecked = _checkAllRole;
            _roleOptions.ForEach(o => o.Checked = !allChecked);
        }
        #endregion
    }
}
