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

        string? imageUrl;
        [Inject]
        MessageService messagerService { get; set; } = null!;
        [Inject]
        IOptions<ApiSettings> apiSettings { get; set; } = null!;
        [Inject]
        IUserService userService { get; set; } = null!;
        [Inject]
        IAuthenticationStateManager authenticationStateManager { get; set; } = null!;
        [Inject]
        protected IClientLocalizer localizer { get; set; } = null!;
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
        private Dictionary<string, object> uploadAttachmentInput = new Dictionary<string, object>()
        {
            { "BusinessType", AttachmentBusinessType.Avatar}
        };

        private Dictionary<string, string> headers = new Dictionary<string, string>();
        private UserDto userDto = null!;
        private bool saveDb = false;
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            userDto = this.Options.User;
            saveDb = this.Options.SaveDb;
            imageUrl = userDto.Avatar;
            //上传附件附带参数
            uploadAttachmentInput.Add("BusinessId", userDto != null ? userDto.Id.ToString() : string.Empty);
            //上传附件附带身份信息
            headers = await authenticationStateManager.GetCurrentTokenHeaders() ?? new Dictionary<string, string>();
            await base.OnInitializedAsync();
        }

        bool BeforeUpload(UploadFileItem file)
        {
            var typeOk = file.Type == "image/jpeg" || file.Type == "image/png" || file.Type == "image/gif";
            if (!typeOk)
            {
                messagerService.Error("头像只能选择JPG/PNG/GIF文件！");
            }
            var sizeOk = file.Size / 1024 < 500;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileinfo"></param>
        /// <returns></returns>
        private Task HandleChange(UploadInfo fileinfo)
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
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                    messagerService.Error($"{apiResult.Errors} [{apiResult.StatusCode}]");
                    messagerService.Error(localizer.Combination(SharedLocalResource.Upload, SharedLocalResource.Success));
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                }
            }
            else if (fileinfo.File.State == UploadState.Fail)
            {
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                messagerService.Error(localizer.Combination(SharedLocalResource.Upload, SharedLocalResource.Fail));
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
            }
            return Task.CompletedTask;
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
                await this.FeedbackRef.CloseAsync();
                return;
            }
            //更新到数据库
            var state = await userService.UpdateAvatar(new UserUpdateAvatarInput { Id = userDto.Id, Avatar = imageUrl });
            if (state)
            {
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                messagerService.Success(localizer.Combination(SharedLocalResource.Avatar, SharedLocalResource.Edit, SharedLocalResource.Success));
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                await this.FeedbackRef.CloseAsync(string.Empty);
            }
            else
            {
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                messagerService.Success(localizer.Combination(SharedLocalResource.Avatar, SharedLocalResource.Edit, SharedLocalResource.Fail));
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
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
