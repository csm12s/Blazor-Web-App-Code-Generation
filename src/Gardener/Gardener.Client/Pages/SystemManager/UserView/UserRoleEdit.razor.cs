// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client.Pages.SystemManager.UserView
{
    public partial class UserRoleEdit: FeedbackComponent<int, bool>
    {
        private bool _isLoading = false;
        private CheckboxOption[] _roleOptions = new CheckboxOption[] { };
        private int _userId = 0;
        [Inject]
        IUserService userService { get; set; }
        [Inject]
        MessageService messageService { get; set; }
        [Inject]
        IRoleService roleService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            _isLoading = true;
            _userId = this.Options;
            if (_userId > 0)
            {
                var rolesResult = await roleService.GetEffective();
                if (rolesResult == null || !rolesResult.Any()) 
                {
                    messageService.Error("没有可用角色，请先添加角色");
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
        private async Task OnEditUserRoleChange(string[] values)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        private async Task OnEditRoleSaveClick()
        {
            _isLoading = true;
            string[] selectRoles = _roleOptions.Where(x => x.Checked).Select(x => x.Value).ToArray();
            var result = await userService.Role(_userId, selectRoles?.Select(x => int.Parse(x)).ToArray());
            if (result)
            {
                messageService.Success("设置成功");
                await (base.FeedbackRef as DrawerRef<bool>).CloseAsync(true);
            }
            else
            {
                messageService.Error("设置失败");
            }
            _isLoading = false;
        }
        /// <summary>
        /// 取消
        /// </summary>
        private async Task OnFormCancel()
        {
            await (base.FeedbackRef as DrawerRef<bool>).CloseAsync(false);
        }
        #region 全选
        private bool _indeterminateRole => _roleOptions.Count(o => o.Checked) > 0 && _roleOptions.Count(o => o.Checked) < _roleOptions.Count();

        private bool _checkAllRole => _roleOptions.All(o => o.Checked);
        /// <summary>
        /// 全选
        /// </summary>
        /// <returns></returns>
        private async Task CheckAllRoleChanged()
        {
            bool allChecked = _checkAllRole;
            _roleOptions.ForEach(o => o.Checked = !allChecked);
        }
        #endregion
    }
}
