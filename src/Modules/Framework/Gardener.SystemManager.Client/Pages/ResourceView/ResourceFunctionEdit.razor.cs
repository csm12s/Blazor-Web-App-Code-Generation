// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.Base;
using Gardener.Client.Base.Services;
using Gardener.Common;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Services;
using Microsoft.AspNetCore.Components;

namespace Gardener.SystemManager.Client.Pages.ResourceView
{
    /// <summary>
    /// 
    /// </summary>
    public class ResourceFunctionEditOption
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public ResourceFunctionEditOption(ResourceDto resource, string name, int type)
        {
            Resource = resource;
            Name = name;
            Type = type;
        }

        /// <summary>
        /// 选中的资源
        /// </summary>
        public ResourceDto Resource { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        /// <remarks>
        /// 0 展示
        /// 1 添加
        /// </remarks>
        public int Type { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public partial class ResourceFunctionEdit : OperationDialogBase<ResourceFunctionEditOption, bool>
    {
        [Inject]
        IFunctionService functionService { get; set; } = null!;
        [Inject]
        IResourceFunctionService resourceFunctionService { get; set; } = null!;
        [Inject]
        IResourceService resourceService { get; set; } = null!;
        [Inject]
        IClientMessageService messageService { get; set; } = null!;
        [Inject]
        ConfirmService confirmService { get; set; } = null!;
        [Inject]
        IClientLocalizer localizer { get; set; } = null!;
        private List<FunctionDto> _functionDtos = new List<FunctionDto>();
        private List<FunctionDto> _selectedFunctionDtos = new List<FunctionDto>();
        //List<TableFilter<string>> groupFilters = null;
        //List<TableFilter<string>> serviceFilters = null;
        private bool _loading = false;
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            _loading = true;
            //根据资源编号获取关联的接口
            List<FunctionDto> _oldFunctionDtos = await resourceService.GetFunctions(this.Options.Resource.Id);
            if (this.Options.Type == 0)
            {
                //根据资源编号获取关联的接口
                _functionDtos = _oldFunctionDtos;
            }
            else if (this.Options.Type == 1)
            {
                //查看可用的接口
                List<FunctionDto> tempFunctionDtos = await functionService.GetAllUsable();
                //移除已选择的
                if (_oldFunctionDtos != null)
                {
                    _functionDtos = tempFunctionDtos.Where(y => !_oldFunctionDtos.Any(x => x.Id.Equals(y.Id))).ToList();
                }
                else
                {
                    _functionDtos = tempFunctionDtos;
                }

            }
            //groupFilters = new List<TableFilter<string>>();
            //serviceFilters = new List<TableFilter<string>>();

            //_functionDtos.Select(x => x.Group).Distinct().ForEach(x =>
            //{
            //    groupFilters.Add(new TableFilter<string>() { Text=x,Data=x });

            //});
            //_functionDtos.Select(x => x.BaseService).Distinct().ForEach(x =>
            //{
            //    serviceFilters.Add(new TableFilter<string>() { Text = x, Data = x });

            //});
            _loading = false;

        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <returns></returns>
        private async Task OnCancleClick()
        {
            await base.CloseAsync(false);
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

        /// <summary>
        /// 点击删除选中按钮
        /// </summary>
        private async Task OnFunctionDeletesClick()
        {
            if (_selectedFunctionDtos == null || _selectedFunctionDtos.Count <= 0)
            {
                messageService.Warn(localizer[SharedLocalResource.NoRowsAreSelected]);
                return;
            }
            if (await confirmService.YesNoDelete() == ConfirmResult.Yes)
            {
                foreach (var item in _selectedFunctionDtos)
                {
                    await resourceFunctionService.Delete(this.Options.Resource.Id, item.Id);
                }
                messageService.Success(localizer.Combination(SharedLocalResource.Delete, SharedLocalResource.Success));
                await OnInitializedAsync();
            }
        }
        /// <summary>
        /// 点击显示关联按钮
        /// </summary>
        private async Task OnShowFunctionAddPageClick(ResourceDto resource)
        {
            await OpenOperationDialogAsync<ResourceFunctionEdit, ResourceFunctionEditOption, bool>(
                $"{localizer["BindingApi"]}-[{this.Options.Name}]",
                     new ResourceFunctionEditOption(resource, this.Options.Name, 1),
                     width: 1300,
            onClose: async result =>
            {
                if (result)
                {
                    await OnInitializedAsync();

                    await RefreshPageDom();
                }
            });

        }
        /// <summary>
        /// 绑定加载中
        /// </summary>
        private bool _bindLoading = false;
        /// <summary>
        /// 点击关联选中按钮
        /// </summary>
        private async Task OnFunctionAddClick()
        {
            if (_selectedFunctionDtos == null || _selectedFunctionDtos.Count <= 0)
            {
                messageService.Warn(localizer[SharedLocalResource.NoRowsAreSelected]);
                return;
            }
            _bindLoading = true;

            bool result = await resourceFunctionService.Add(_selectedFunctionDtos.Select(x =>
            {
                return new ResourceFunctionDto
                {
                    ResourceId = this.Options.Resource.Id,
                    FunctionId = x.Id,
                    CreatedTime = DateTimeOffset.Now
                };
            }).ToList());
            if (result)
            {
                messageService.Success(localizer.Combination(SharedLocalResource.Binding, SharedLocalResource.Success));
                await base.CloseAsync(true);
            }
            else
            {
                messageService.Error(localizer.Combination(SharedLocalResource.Binding, SharedLocalResource.Fail));
            }
            _bindLoading = false;
        }


        /// <summary>
        /// 下载种子数据
        /// </summary>
        /// <returns></returns>
        private async Task OnDownloadSeedDataClick(ResourceDto dto)
        {
            //找到所有编号
            List<Guid> resourceIds = new List<Guid>()
            {
                dto.Id
            };
            resourceIds.AddRange(TreeHelper.GetAllChildrenNodes(dto, dto => dto.Id, dto => dto.Children));


            Task<string> data = resourceFunctionService.GetSeedData(resourceIds);

            await OpenOperationDialogAsync<ShowSeedDataCode, Task<string>, bool>(
                       localizer[SharedLocalResource.SeedData],
                       data,
                       width: 1300);
        }
    }
}
