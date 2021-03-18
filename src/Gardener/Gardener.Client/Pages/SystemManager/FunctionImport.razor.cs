// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Gardener.Common;
using Gardener.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client.Pages.SystemManager
{
    public partial class FunctionImport : DrawerTemplate<int, bool>
    {
        List<SwaggerSpecificationOpenApiInfoDto> apiInfos = new List<SwaggerSpecificationOpenApiInfoDto>();
        [Inject]
        ISwaggerService swaggerService { get; set; }
        [Inject]
        IFunctionService functionService { get; set; }
        [Inject]
        IOptions<ApiSettings> apiOptions { get; set; }
        [Inject]
        MessageService messageService { get; set; }
        [Inject]
        NotificationService noticeService { get; set; }
        private List<FunctionDto> _functionDtos = new List<FunctionDto>();
        private List<FunctionDto> _selectedFunctionDtos = new List<FunctionDto>();

        private string _apiJsonUrl = string.Empty;
        private string _selectedGroupValue = string.Empty;
        private SwaggerSpecificationOpenApiInfoDto _selectedGroup ;
        private bool _loading = false;
        private bool _importLoading = false;
        private bool _importIsBegin = false;
        private double _importPercent = 0;
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            apiInfos=await swaggerService.GetApiGroup();
            if (apiInfos != null && apiInfos.Any()) 
            {
                OnSelectedItemChangedHandler(apiInfos.First());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private void OnSelectedItemChangedHandler(SwaggerSpecificationOpenApiInfoDto value)
        {
            _functionDtos = new List<FunctionDto>();
            _selectedGroup = value;

            Uri uri = new Uri(apiOptions.Value.BaseAddres);
            
            _apiJsonUrl = $"{uri.Scheme}://{uri.Host}:{uri.Port}/swagger/{value.Group}/swagger.json";
        }
        /// <summary>
        /// 加载
        /// </summary>
        /// <returns></returns>
        private async Task OnLoad()
        {
            _loading = true;
            _functionDtos = new List<FunctionDto>();
            SwaggerModel swaggerModel= await swaggerService.Analysis(_apiJsonUrl);
            if (swaggerModel != null && swaggerModel.paths != null)
            {
                Dictionary<string, SwaggerTagInfo> tagMap = swaggerModel.tags.ToDictionary<SwaggerTagInfo, string>(x => x.name);
                foreach (var item in swaggerModel.paths)
                {
                    foreach (var m in item.Value)
                    {
                        string tags = m.Value.tags==null?null: string.Join("_", m.Value.tags.Select(x => tagMap.ContainsKey(x) ? tagMap[x].description : x));
                        FunctionDto function = new FunctionDto()
                        {
                            Path=item.Key,
                            Method= (HttpMethodType)Enum.Parse(typeof(HttpMethodType),m.Key.ToUpper()),
                            Key= MD5Encryption.Encrypt(item.Key+ m.Key.ToUpper()),
                            Summary =m.Value.summary,
                            Description=m.Value.description,
                            Group=_selectedGroup.Title,
                            Service= tags,
                            IsLocked=false,
                            EnableAudit=true
                        };
                        if (HttpMethodType.GET.Equals(function.Method))
                        {
                            function.EnableAudit = false;
                        }
                        _functionDtos.Add(function);
                    }
                }
            }
            _loading = false;

        }
        // <summary>
        /// 点击启用审计按钮
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isLocked"></param>
        private async Task OnChangeEnableAudit(FunctionDto model, bool enableAudit)
        {
            
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <returns></returns>
        private async Task OnCancleClick()
        {
            base.CloseAsync(false);
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <returns></returns>
        private async Task OnImportClick()
        {
            _importLoading = true;
            if (_selectedFunctionDtos == null || !_selectedFunctionDtos.Any())
            {
                messageService.Warn("请至少选中一行");
                _importLoading = false;
                return;
            }
            //开始导入
            _importIsBegin = true;
            _importPercent = 0;
            int count = 0;
            int insertCount = 0;
            foreach (var item in _selectedFunctionDtos)
            {
                count++;
                bool exists=await functionService.Exists(item.Method, item.Path);
                if (!exists)
                {
                    await functionService.Insert(item);
                    insertCount++;
                }
                _importPercent = Math.Round((count / (double)_selectedFunctionDtos.Count) * 100, 2);
                await InvokeAsync(StateHasChanged);
                
            }
            await noticeService.Open(new NotificationConfig()
            {
                Message = "导入结果通知",
                Description = $"已存在{_selectedFunctionDtos.Count - insertCount}条,导入成功{insertCount}条",
                NotificationType = NotificationType.Success,
                Duration=2
            });

            _importIsBegin = false;
            _importLoading = false;
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="functions"></param>
        /// <returns></returns>
        private void SelectedRowsChanged(IEnumerable<FunctionDto> functions)
        {
            _selectedFunctionDtos = functions.ToList();
        }
    }
}
