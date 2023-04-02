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
        private List<DeptDto>? deptDatas;
        private List<PositionDto>? positions;
        [Inject]
        private IDeptService DeptService { get; set; } = null!;
        [Inject]
        private IPositionService PositionService { get; set; } = null!;
        //部门树
        /// <summary>
        /// 部门编号
        /// </summary>
        protected string? DeptId
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
            await base.OnInitializedAsync();
            if(_editModel!=null)
            {
                _editModel.Password = null;
            }
        }
        protected override async Task OnParametersSetAsync()
        {
            _isLoading = true;
            //岗位
            positions = await PositionService.GetAllUsable();
            //部门
            deptDatas = await DeptService.GetTree();
            await base.OnParametersSetAsync();
            _isLoading = false;
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
                Localizer[SharedLocalResource.UplaodAvatar],
                new UserUploadAvatarParams(user, false),
                width: avatarDrawerWidth);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        static private void OnSelectedItemChangedHandler(PositionDto value)
        {

        }
    }
}
