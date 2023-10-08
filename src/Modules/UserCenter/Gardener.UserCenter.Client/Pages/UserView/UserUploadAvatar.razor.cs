// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Attachment.Dtos;
using Gardener.Attachment.Enums;
using Gardener.Base.Resources;
using Gardener.Client.Base;
using Gardener.Client.Base.Services;
using Gardener.Common;
using Gardener.LocalizationLocalizer;
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

        /// <summary>
        /// 上传附件附带参数
        /// </summary>
        private Dictionary<string, object> uploadAttachmentInput = new Dictionary<string, object>()
        {
            { "BusinessType", AttachmentBusinessType.Avatar}
        };

        private Dictionary<string, string> headers = new Dictionary<string, string>();
        private UserDto userDto = null!;
        private bool saveDb = false;
        private bool loading = false;
        private bool btnLoading = false;
        private string? imageUrl;
        /// <summary>
        /// 上传地址
        /// </summary>
        private string UploadUrl
        {
            get
            {
                return ApiSettings.Value.BaseAddres + ApiSettings.Value.UploadPath;
            }
        }

        [Inject]
        private IClientMessageService MessagerService { get; set; } = null!;
        [Inject]
        private IOptions<ApiSettings> ApiSettings { get; set; } = null!;
        [Inject]
        private IUserService UserService { get; set; } = null!;
        [Inject]
        private IAuthenticationStateManager AuthenticationStateManager { get; set; } = null!;
        [Inject]
        private ILocalizationLocalizer Localizer { get; set; } = null!;

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            btnLoading = true;
            userDto = this.Options.User;
            saveDb = this.Options.SaveDb;
            imageUrl = userDto.Avatar;
            //上传附件附带参数
            uploadAttachmentInput.Add("BusinessId", userDto != null ? userDto.Id.ToString() : string.Empty);
            //测试token是否可用
            if (await AuthenticationStateManager.TestToken("UserUploadAvatar"))
            {
                //上传附件附带身份信息
                headers = await AuthenticationStateManager.GetCurrentTokenHeaders() ?? new Dictionary<string, string>();
            }
            btnLoading =false;
        }
        /// <summary>
        /// 上传前拦截
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool BeforeUpload(UploadFileItem file)
        {
            var typeOk = file.Type == "image/jpeg" || file.Type == "image/png" || file.Type == "image/gif";
            if (!typeOk)
            {
                MessagerService.Error("头像只能选择JPG/PNG/GIF文件！");
            }
            var sizeOk = file.Size / 1024 < 500;
            if (!sizeOk)
            {
                MessagerService.Error("头像必须小于500KB！");
            }
            return typeOk && sizeOk;
        }
        /// <summary>
        /// 新的头像上传成功
        /// </summary>
        private bool uploadSucceed = false;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileinfo"></param>
        /// <returns></returns>
        private void HandleChange(UploadInfo fileinfo)
        {
            loading = fileinfo.File.State == UploadState.Uploading;
            if (fileinfo.File.State == UploadState.Success)
            {
                ApiResult<UploadAttachmentOutput> apiResult = fileinfo.File.GetResponse<ApiResult<UploadAttachmentOutput>>(new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (apiResult.Succeeded && apiResult.Data != null)
                {
                    uploadSucceed = true;
                    imageUrl = apiResult.Data.Url;
                }
                else
                {
                    MessagerService.Error($"{apiResult.Errors} [{apiResult.StatusCode}]");
                    MessagerService.Error(Localizer.Combination(nameof(SharedLocalResource.Upload), nameof(SharedLocalResource.Fail)));
                }
            }
            else if (fileinfo.File.State == UploadState.Fail)
            {
                MessagerService.Error(Localizer.Combination(nameof(SharedLocalResource.Upload), nameof(SharedLocalResource.Fail)));
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
                userDto.Avatar = imageUrl;
                await this.FeedbackRef.CloseAsync(imageUrl);
                return;
            }
            //更新到数据库
            var state = await UserService.UpdateAvatar(new UpdateUserAvatarInput { Id = userDto.Id, Avatar = imageUrl });
            if (state)
            {
                MessagerService.Success(Localizer.Combination(nameof(SharedLocalResource.Avatar), nameof(SharedLocalResource.Edit), nameof(SharedLocalResource.Success)));
                await this.FeedbackRef.CloseAsync(imageUrl);
            }
            else
            {
                MessagerService.Error(Localizer.Combination(nameof(SharedLocalResource.Avatar), nameof(SharedLocalResource.Edit), nameof(SharedLocalResource.Fail)));
            }

        }
    }
    /// <summary>
    /// 上传头像参数
    /// </summary>
    public class UserUploadAvatarParams
    {
        /// <summary>
        /// 上传头像参数
        /// </summary>
        /// <param name="user"></param>
        public UserUploadAvatarParams(UserDto user)
        {
            User = user;
        }

        /// <summary>
        /// 上传头像参数
        /// </summary>
        /// <param name="user"></param>
        /// <param name="saveDb"></param>
        public UserUploadAvatarParams(UserDto user, bool saveDb)
        {
            User = user;
            SaveDb = saveDb;
        }

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
