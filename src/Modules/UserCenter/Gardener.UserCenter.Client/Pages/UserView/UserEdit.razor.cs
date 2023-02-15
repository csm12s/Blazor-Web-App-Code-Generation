// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Resources;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.UserView
{
    public partial class UserEdit : EditOperationDialogBase<UserDto, int, UserCenterResource>
    {
        private List<PositionDto> positions = new List<PositionDto>();
        [Inject]
        IDeptService deptService { get; set; }
        [Inject]
        IPositionService positionService { get; set; }
        //部门树
        List<DeptDto> deptDatas = new List<DeptDto>();
        /// <summary>
        /// 部门编号
        /// </summary>
        protected string deptId
        {
            get
            {
                return _editModel.DeptId?.ToString();
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _editModel.DeptId = int.Parse(value);
                }
                else
                {
                    _editModel.DeptId = null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            _isLoading = true;
            positions = await positionService.GetAllUsable();
            //部门
            deptDatas = await deptService.GetTree();
            _isLoading = false;
            await base.OnInitializedAsync();
            _editModel.Password = null;
            _editModel.UserExtension = _editModel.UserExtension ?? new UserExtensionDto() { UserId = _editModel.Id };
        }

        /// <summary>
        /// 点击头像
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task OnAvatarClick(UserDto user)
        {
            int avatarDrawerWidth = 300;
            await OpenOperationDialogAsync<UserUploadAvatar, UserUploadAvatarParams, string>(
                localizer[SharedLocalResource.UplaodAvatar],
                new UserUploadAvatarParams { User = user, SaveDb = false },
                width: avatarDrawerWidth);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private void OnSelectedItemChangedHandler(PositionDto value)
        {

        }
    }
}
