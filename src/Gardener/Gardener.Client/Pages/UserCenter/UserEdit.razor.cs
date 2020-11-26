// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Application.Dtos;
using Gardener.Client.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Components.Forms;
using System;

namespace Gardener.Client.Pages.UserCenter
{
    public partial class UserEdit: DrawerTemplate<int?, UserDto>
    {

        [Inject]
        MessageService MessaheSvr { get; set; }
        [Inject]
        IUserService UserService { get; set; }

        private UserDto editModel;
        private bool formIsLoading;
        protected override async Task OnInitializedAsync()
        {
            formIsLoading = true;
            if (base.Options.HasValue)
            {
                var result =await UserService.Get(base.Options.Value);
                if (result.Successed)
                {
                    editModel = result.Data;
                }
            }
            else {
                editModel = new UserDto();
            }
            await base.OnInitializedAsync();
            formIsLoading = false;
        }
        /// <summary>
        /// 表单完成时
        /// </summary>
        /// <param name="editContext"></param>
        /// <returns></returns>
        private async Task OnFormFinish(EditContext editContext)
        {
            formIsLoading = true;
            //开始请求
            if (editModel.Id == 0)
            {
                //添加
                var result = await UserService.Insert(editModel);
                formIsLoading = false;
                if (result.Successed)
                {
                    MessaheSvr.Success("添加成功");
                    await base.CloseAsync(result.Data);
                }
                else
                {
                    MessaheSvr.Error("添加失败");
                }
            }
            else
            {
                editModel.Roles = null;
                editModel.UserRoles = null;
                //修改
                var result = await UserService.Update(editModel);
                if (result.Successed)
                {
                    MessaheSvr.Success("修改成功", 1);
                    await base.CloseAsync(editModel);
                }
                else
                {
                    MessaheSvr.Error("修改失败", 1);
                }
            }
        }
        /// <summary>
        /// 表单失败时
        /// </summary>
        /// <param name="editContext"></param>
        private void OnFormFinishFailed(EditContext editContext)
        {
            //drawerVisible = false;
        }
        /// <summary>
        /// 表单取消
        /// </summary>
        private async void OnFormCancel()
        {
            new UserDto().Adapt(editModel);
            await base.CloseAsync(null);
        }

    }

   
}
