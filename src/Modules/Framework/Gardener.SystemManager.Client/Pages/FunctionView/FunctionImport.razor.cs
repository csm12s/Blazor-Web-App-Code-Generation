// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.Base;
using Gardener.Swagger.Dtos;
using Gardener.Swagger.Services;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Services;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

namespace Gardener.SystemManager.Client.Pages.FunctionView
{
    public partial class FunctionImport : FeedbackComponent<int, bool>
    {
        List<SwaggerSpecificationOpenApiInfoDto> apiInfos = new List<SwaggerSpecificationOpenApiInfoDto>();
        [Inject]
        ISwaggerService swaggerService { get; set; }
        [Inject]
        IFunctionService functionService { get; set; }
        [Inject]
        IOptions<ApiSettings> apiSettings { get; set; }
        [Inject]
        MessageService messageService { get; set; }
        [Inject]
        NotificationService noticeService { get; set; }
        [Inject]
        IClientLocalizer localizer { get; set; }
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
            Console.WriteLine(apiSettings);
            Uri uri = new Uri(apiSettings.Value.BaseAddres);
            
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
            var result = await swaggerService.GetFunctionsFromJson(_apiJsonUrl);
            if (result != null)
            {
                result.ForEach(x => { 
                    x.Group = _selectedGroup.Title;
                    _functionDtos.Add(x.Adapt<FunctionDto>());
                });
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
            //todo: Add operation logic here
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <returns></returns>
        private async Task OnCancleClick()
        {
            await base.FeedbackRef!.CloseAsync(false);
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
                messageService.Warn(localizer["未选中任何行"]);
                _importLoading = false;
                return;
            }
            //开始导入
            _importIsBegin = true;
            _importPercent = 0;
            int count = 0, insertCount = 0,errorCount=0, repetitionCount=0;
            foreach (var item in _selectedFunctionDtos)
            {
                count++;
                FunctionDto dto =await functionService.GetByKey(item.Key);
                if (dto==null)
                {
                    FunctionDto function = await functionService.Insert(item);
                    if (function == null)
                    {
                        errorCount++;
                    }
                    else
                    {
                        insertCount++;
                    }
                }
                else 
                {
                    item.Id = dto.Id;
                    bool result= await functionService.Update(item);
                    if (result)
                    {
                         repetitionCount++;
                    }
                    else 
                    {
                        errorCount++;
                    }
                    
                }
                _importPercent = Math.Round((count / (double)_selectedFunctionDtos.Count) * 100, 2);
                await InvokeAsync(StateHasChanged);
                
            }
            await noticeService.Open(new NotificationConfig()
            {
                Message = "导入结果通知",
                Description = $"共选择{count}条,更新已存在{repetitionCount}条,导入{insertCount}条,失败{errorCount}条",
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
