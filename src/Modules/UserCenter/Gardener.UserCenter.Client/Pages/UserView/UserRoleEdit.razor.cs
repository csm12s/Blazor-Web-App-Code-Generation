// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.Base;
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
        IUserService userService { get; set; } = null!;
        [Inject]
        MessageService messageService { get; set; } = null!;
        [Inject]
        IRoleService roleService { get; set; } = null!;
        [Inject]
        IClientLocalizer<UserCenterResource> localizer { get; set; } = null!;
        protected override async Task OnInitializedAsync()
        {
            _isLoading = true;
            _userId = this.Options;
            if (_userId > 0)
            {
                var rolesResult = await roleService.GetAllUsable();
                if (rolesResult == null || !rolesResult.Any()) 
                {
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                    messageService.Error(localizer[UserCenterResource.NoRoleNeedAdd]);
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                    return;
                }
                var userRoles = await userService.GetRoles(_userId);
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

            var result = await userService.Role(_userId, selectRoles.Select(x => int.Parse(x)).ToArray());
            if (result)
            {
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                messageService.Success(localizer.Combination(UserCenterResource.Setting,UserCenterResource.Success));
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                await base.FeedbackRef.CloseAsync(true);
            }
            else
            {
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                messageService.Error(localizer.Combination(UserCenterResource.Setting, UserCenterResource.Fail));
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
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
