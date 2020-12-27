// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace Gardener.Client.Pages.UserCenter
{
    public partial class UserEdit: DrawerTemplate<int, bool>
    {
        private bool _formIsLoading = false;
        private UserDto _editModel = new UserDto();
        [Inject]
        IUserService userService { get; set; }
        [Inject]
        MessageService messageService { get; set; }
        [Inject]
        DrawerService drawerService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            _formIsLoading = true;

            int id = this.Options;

            if (id > 0)
            {
                //更新 回填数据
                var user = await userService.Get(id);
                if (user != null)
                {
                    if (user.UserExtension == null) user.UserExtension = new UserExtensionDto() { UserId=user.Id };
                    //赋值给编辑对象
                    user.Adapt(_editModel);
                }
                else
                {
                    messageService.Error("用户不存在");
                }
            }
            _formIsLoading = false;
            await base.OnInitializedAsync();
        }
        /// <summary>
        /// 表单完成时
        /// </summary>
        /// <param name="editContext"></param>
        /// <returns></returns>
        private async Task OnFormFinish(EditContext editContext)
        {
            _formIsLoading = true;
            //开始请求
            if (_editModel.Id == 0)
            {
                //添加
                var result = await userService.Insert(_editModel);
                
                if (result != null)
                {
                    messageService.Success("添加成功");
                    await base.CloseAsync(true);
                }
                else
                {
                    messageService.Error("添加失败");
                }
            }
            else
            {
                //修改
                var result = await userService.Update(_editModel);
                if (result)
                {
                    messageService.Success("修改成功");
                    await base.CloseAsync(true);
                }
                else
                {
                    messageService.Error("修改失败");
                }
            }
            _formIsLoading = false;
        }
        /// <summary>
        /// 取消
        /// </summary>
        private async Task OnFormCancel()
        {
            await base.CloseAsync(false);
        }
        

        /// <summary>
        /// 点击头像
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task OnAvatarClick(UserDto user)
        {
            int avatarDrawerWidth = 300;
            //this.DrawerRef.Options.Width += avatarDrawerWidth;
            await drawerService.CreateDialogAsync<UserUploadAvatar, UserUploadAvatarParams, string>(new UserUploadAvatarParams { User =user }, true, title: "上传头像", width: avatarDrawerWidth, placement: "right");
            //this.DrawerRef.Options.Width -= avatarDrawerWidth;
        }
    }
}
