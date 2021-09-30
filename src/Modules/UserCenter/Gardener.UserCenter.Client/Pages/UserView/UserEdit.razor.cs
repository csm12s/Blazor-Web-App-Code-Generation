// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.Base;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.UserView
{
    public partial class UserEdit: FeedbackComponent<int, bool>
    {
        private bool formIsLoading = false;
        private UserDto editModel = new UserDto();
        private List<PositionDto> positions = new List<PositionDto>();
        [Inject]
        IUserService userService { get; set; }
        [Inject]
        MessageService messageService { get; set; }
        [Inject]
        DrawerService drawerService { get; set; }
        [Inject]
        IDeptService deptService { get; set; }
        [Inject]
        IPositionService positionService { get; set; }
        //部门树
        List<DeptDto> deptDatas;
        private string deptId;
        TreeSelect<DeptDto> treeSelect;
        protected override async Task OnInitializedAsync()
        {
            formIsLoading = true;

            positions=await positionService.GetAllUsable();

            int id = this.Options;

            if (id > 0)
            {
                //更新 回填数据
                var user = await userService.Get(id);
                if (user != null)
                {
                    user.Password = null;
                    if (user.UserExtension == null) user.UserExtension = new UserExtensionDto() { UserId=user.Id };
                    //赋值给编辑对象
                    user.Adapt(editModel);
                    deptId = editModel.DeptId?.ToString();
                }
                else
                {
                    messageService.Error("用户不存在");
                }
            }

            //部门
            deptDatas = await deptService.GetTree();
            formIsLoading = false;
            await base.OnInitializedAsync();
        }
        /// <summary>
        /// 表单完成时
        /// </summary>
        /// <param name="editContext"></param>
        /// <returns></returns>
        private async Task OnFormFinish(EditContext editContext)
        {
            formIsLoading = true;
            if (!string.IsNullOrEmpty(deptId)) 
            {
                editModel.DeptId = int.Parse(deptId);
            }
            //开始请求
            if (editModel.Id == 0)
            {
                //添加
                var result = await userService.Insert(editModel);
                
                if (result != null)
                {
                    messageService.Success("添加成功");
                    await (base.FeedbackRef as DrawerRef<bool>).CloseAsync(true);
                }
                else
                {
                    messageService.Error("添加失败");
                }
            }
            else
            {
                //修改
                var result = await userService.Update(editModel);
                if (result)
                {
                    messageService.Success("修改成功");
                    await (base.FeedbackRef as DrawerRef<bool>).CloseAsync(true);
                }
                else
                {
                    messageService.Error("修改失败");
                }
            }
            formIsLoading = false;
        }
        /// <summary>
        /// 取消
        /// </summary>
        private async Task OnFormCancel()
        {
            await (base.FeedbackRef as DrawerRef<bool>).CloseAsync(false);
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

        private void OnSelectedItemChangedHandler(PositionDto value)
        {
            Console.WriteLine($"selected: ${value?.Name}");
        }
    }
}
