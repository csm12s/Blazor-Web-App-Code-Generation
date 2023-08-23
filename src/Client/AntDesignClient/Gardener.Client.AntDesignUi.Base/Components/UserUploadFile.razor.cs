using AntDesign;
using Gardener.Attachment.Dtos;
using Gardener.Attachment.Enums;
using Gardener.Client.Base;
using Gardener.Common;
using Gardener.LocalizationLocalizer;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

namespace Gardener.Client.AntDesignUi.Base.Components
{
    public partial class UserUploadFile : FeedbackComponent<UserUploadFileParams, string>
    {
        bool loading = false;

        [Inject]
        MessageService messagerService { get; set; } = null!;
        [Inject]
        IOptions<ApiSettings> apiSettings { get; set; } = null!;
        [Inject]
        IUserService userService { get; set; } = null!;
        [Inject]
        IAuthenticationStateManager authenticationStateManager { get; set; } = null!;

        [Inject]
        protected ILocalizationLocalizer localizer { get; set; } = null!;
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
        private Dictionary<string, object> uploadAttachmentInput =
            new Dictionary<string, object>()
            {
            };

        private Dictionary<string, string> headers =new Dictionary<string, string>();

        private UserUploadFileParams uploadParams = null!;
        private UserDto userDto = null!;
        //private bool saveDb = false;

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            if (this.Options != null)
            {
                uploadParams = this.Options;
                userDto = this.Options.User;

                //上传附件附带参数
                uploadAttachmentInput.Add(nameof(UploadAttachmentInput.BusinessId),
                    userDto.Id.ToString());
                uploadAttachmentInput.Add(nameof(UploadAttachmentInput.BusinessType),
                    uploadParams.AttachmentBusinessType);
                uploadAttachmentInput.Add(nameof(UploadAttachmentInput.FileSavePath),
                    uploadParams.FileSavePath);
                uploadAttachmentInput.Add(nameof(UploadAttachmentInput.FileSaveFolder),
                    uploadParams.FileSaveFolder);
                //上传附件附带身份信息
                var authDic = await authenticationStateManager.GetCurrentTokenHeaders();
                if(authDic != null)
                {
                    foreach(var auth in authDic)
                    {
                        headers.Add(auth.Key, auth.Value);
                    }
                }
            }
            await base.OnInitializedAsync();
        }
        /// <summary>
        /// 上传前
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool BeforeUpload(UploadFileItem file)
        {
            var typeOk = false;
            foreach (var fileType in uploadParams.UploadFileType)
            {
                if (file.Ext.ToLower() == fileType.ToLower())
                {
                    typeOk = true;
                }
            }
            if (!typeOk)
            {
                messagerService.Error("文件格式不支持");
            }

            // Kb
            var maxSize = uploadParams.FileMaxSizeMb * 1024;
            var sizeOk = file.Size / 1024 < maxSize;
            if (!sizeOk)
            {
                messagerService.Error($"文件必须小于{maxSize}KB！");
            }
            return typeOk && sizeOk;
        }
        /// <summary>
        /// 上传成功
        /// </summary>
        private bool uploadSucceed = false;
        /// <summary>
        /// 文件地址
        /// </summary>
        private string fileUrl = "";
        /// <summary>
        /// 处理变化
        /// </summary>
        /// <param name="fileinfo"></param>
        private void HandleChange(UploadInfo fileinfo)
        {
            loading = fileinfo.File.State == UploadState.Uploading;

            if (fileinfo.File.State == UploadState.Success)
            {
                ApiResult<UploadAttachmentOutput> apiResult =
                    fileinfo.File.GetResponse<ApiResult<UploadAttachmentOutput>>(new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (apiResult.Succeeded && apiResult.Data!=null)
                {
                    uploadSucceed = true;
                    fileUrl = apiResult.Data.Url;
                }
                else
                {
                    messagerService.Error($"{apiResult.Errors} [{apiResult.StatusCode}]");
                    messagerService.Error("上传失败");
                }
            }
            else if (fileinfo.File.State == UploadState.Fail)
            {
                messagerService.Error("上传失败");
            }
        }
        /// <summary>
        /// 
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

            await this.FeedbackRef.CloseAsync(fileUrl);
            await messagerService.Success("上传成功");
        }
    }
    /// <summary>
    /// 用户上传文件参数
    /// </summary>
    public class UserUploadFileParams
    {
        /// <summary>
        /// 用户
        /// </summary>
        public UserDto User { get; set; } = null!;
        /// <summary>
        /// 附件业务类型
        /// </summary>
        public AttachmentBusinessType AttachmentBusinessType { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public List<string> UploadFileType { get; set; } = null!;

        /// <summary>
        /// 文件保存路径
        /// 如果设置，文件上传成功后拷贝到此位置
        /// </summary>
        public string FileSavePath { get; set; } = "";

        /// <summary>
        /// 文件保存文件夹
        /// 如果设置，文件上传成功后拷贝到此位置
        /// </summary>
        public string FileSaveFolder { get; set; } = "";

        /// <summary>
        /// 1Mb=1024Kb
        /// </summary>
        public int FileMaxSizeMb { get; set; } = 100;
    }

    /// <summary>
    /// upload filter by extensions
    /// </summary>
    public class UploadFileType
    {
        public static List<string> Excel = new List<string>() { _xlsx };
        public static List<string> Avatar = new List<string> { _jpeg, _png, _gif };

        public const string _jpeg = ".jpeg"; // "image/jpeg";
        public const string _png = ".png";
        public const string _gif = ".gif";
        public const string _bmp = ".bmp";
        public const string _xlsx = ".xlsx"; // 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
    }

    #region 使用示例
    /*
     * OperationDialogSettings settings = base.GetOperationDialogSettings();
            settings.Width = 300;
            settings.DrawerPlacement = Placement.Left;
            await OpenOperationDialogAsync<UserUploadFile, UserUploadFileParams, string>
                (localizer[SharedLocalResource.Upload],
                new UserUploadFileParams
                {
                    UploadFileType = UploadFileType.Excel,
                    AttachmentBusinessType = Attachment.Enums.AttachmentBusinessType.Report,
                    FileSavePath = reportButtonItem.TemplatePath,
                },
                async resUrl =>
                {
                    if(resUrl.IsValid())
                    {
                        await ReloadPage();
                    }
                }
            , settings);
     */
    #endregion
}
