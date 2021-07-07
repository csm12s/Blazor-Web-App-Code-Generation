// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Gardener.Client.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Pages.SystemManager.UserView
{
    public partial class UserUploadAvatar : FeedbackComponent<UserUploadAvatarParams, string>
    {
        bool loading = false;

        string imageUrl;
        [Inject]
        MessageService messagerService { get; set; }
        [Inject]
        IOptions<ApiSettings> apiOptions { get; set; }
        [Inject]
        IUserService userService { get; set; }
        [Inject]
        IAuthenticationStateManager authenticationStateManager { get; set; }
        /// <summary>
        /// 上传地址
        /// </summary>
        public string UploadUrl
        {
            get
            {
                ApiSettings apiSettings = apiOptions.Value;
                return apiSettings.BaseAddres + apiSettings.UploadUrl;
            }
        }
        /// <summary>
        /// 上传附件附带参数
        /// </summary>
        private Dictionary<string,object> uploadAttachmentInput=new Dictionary<string, object>() 
        {
            { "BusinessType", Enums.AttachmentBusinessType.Avatar}
        };


        private Dictionary<string, string> headers;

        private UserDto userDto;
        private bool saveDb = false;
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            if (this.Options != null)
            {
                userDto = this.Options.User;
                saveDb = this.Options.SaveDb;

                if (userDto != null && !string.IsNullOrEmpty(userDto.Avatar))
                {
                    imageUrl = userDto.Avatar;
                }
                //上传附件附带参数
                uploadAttachmentInput.Add("BusinessId", userDto != null ? userDto.Id.ToString() : null);
                //上传附件附带身份信息
                headers = await authenticationStateManager.GetCurrentTokenHeaders();
            }
            await base.OnInitializedAsync();
        }

        bool BeforeUpload(UploadFileItem file)
        {
            var typeOk = file.Type == "image/jpeg" || file.Type == "image/png" || file.Type == "image/gif";
            if (!typeOk)
            {
                messagerService.Error("头像只能选择JPG/PNG/GIF文件！");
            }
            var sizeOk= file.Size / 1024 <500;
            if (!sizeOk)
            {
                messagerService.Error("头像必须小于500KB！");
            }
            return typeOk && sizeOk;
        }

        async Task HandleChange(UploadInfo fileinfo)
        {
            loading = fileinfo.File.State == UploadState.Uploading;

            if (fileinfo.File.State == UploadState.Success)
            {
                ApiResult<UploadAttachmentOutput> apiResult= fileinfo.File.GetResponse<ApiResult<UploadAttachmentOutput>>(new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive=true });
                if (apiResult.Succeeded)
                {
                    //把用户头像改掉
                    userDto.Avatar = apiResult.Data.Url;
                    //不需要保存，直接返回
                    if (!saveDb) 
                    {
                        imageUrl = userDto.Avatar;
                        return;
                    }
                    //更新到数据库
                    var state=await userService.UpdateAvatar(new UserUpdateAvatarInput { Id=userDto.Id,Avatar= userDto.Avatar });
                    if (state)
                    {
                        imageUrl = userDto.Avatar;
                        messagerService.Success("头像修改成功");
                    }
                    else 
                    {
                        messagerService.Success("头像修改失败");
                    }
                }
                else 
                {
                    messagerService.Error($"{apiResult.Errors} [{apiResult.StatusCode}]");
                    messagerService.Error("上传失败");
                }
            }
            else if(fileinfo.File.State == UploadState.Fail)
            {
                messagerService.Error("上传失败");
            }
        }        
    }

    public class UserUploadAvatarParams
    {
        /// <summary>
        /// 用户
        /// </summary>
        public UserDto User { get; set; }
        /// <summary>
        /// 保存数据
        /// </summary>
        public bool SaveDb { get; set; } = false;
    }
}
