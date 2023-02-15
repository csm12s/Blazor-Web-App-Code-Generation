// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Attachment.Dtos;
using Gardener.Attachment.Enums;
using Gardener.Client.Base;
using Gardener.Common;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.UserView
{
    public partial class UserUploadAvatar : FeedbackComponent<UserUploadAvatarParams, string>
    {
        bool loading = false;

        string imageUrl;
        [Inject]
        MessageService messagerService { get; set; }
        [Inject]
        IOptions<ApiSettings> apiSettings { get; set; }
        [Inject]
        IUserService userService { get; set; }
        [Inject]
        IAuthenticationStateManager authenticationStateManager { get; set; }
        
        [Inject]
        protected IClientLocalizer localizer { get; set; }
        /// <summary>
        /// 上传地址
        /// </summary>
        public string UploadUrl
        {
            get
            {
                return apiSettings.Value.BaseAddres + apiSettings.Value.UploadPath;
            }
        }
        /// <summary>
        /// 上传附件附带参数
        /// </summary>
        private Dictionary<string,object> uploadAttachmentInput=new Dictionary<string, object>() 
        {
            { "BusinessType", AttachmentBusinessType.Avatar}
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
        /// <summary>
        /// 新的头像上传成功
        /// </summary>
        private bool uploadSucceed = false;
        async Task HandleChange(UploadInfo fileinfo)
        {
            loading = fileinfo.File.State == UploadState.Uploading;

            if (fileinfo.File.State == UploadState.Success)
            {
                ApiResult<UploadAttachmentOutput> apiResult= fileinfo.File.GetResponse<ApiResult<UploadAttachmentOutput>>(new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive=true });
                if (apiResult.Succeeded)
                {
                    uploadSucceed = true;
                    imageUrl=apiResult.Data.Url;
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
        /// <summary>
        /// 保存或返回
        /// </summary>
        /// <returns></returns>
        async Task OnOkClick() 
        {
            //失败，直接返回
            if (!uploadSucceed)
            {
                await this.FeedbackRef.CloseAsync();
                return;
            }

            //不需要保存，直接返回
            if (!saveDb)
            {
                userDto.Avatar= imageUrl;
                await this.FeedbackRef.CloseAsync();
                return;
            }
            //更新到数据库
            var state = await userService.UpdateAvatar(new UserUpdateAvatarInput { Id = userDto.Id, Avatar = imageUrl });
            if (state)
            {
                messagerService.Success("头像修改成功");
                await this.FeedbackRef.CloseAsync(string.Empty);
            }
            else
            {
                messagerService.Success("头像修改失败");
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
