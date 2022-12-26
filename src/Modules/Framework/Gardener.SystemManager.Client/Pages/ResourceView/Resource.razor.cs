// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.Client.Base;
using Gardener.Client.Base.Components;
using Gardener.Common;
using Gardener.Enums;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Services;
using Microsoft.AspNetCore.Components;

namespace Gardener.SystemManager.Client.Pages.ResourceView
{
    public partial class Resource : TreeTableBase<ResourceDto, Guid, ResourceEdit>
    {

        protected override OperationDialogSettings GetOperationDialogSettings()
        {
            var settings = base.GetOperationDialogSettings();
            settings.Width = 800;
            return settings;
        }

        // 改为引用继承中的声明，如果发生bug，取消此处注释
        //[Inject]
        //DrawerService drawerService { get; set; }

        [Inject]
        IResourceService resourceService { get; set; }

        /// <summary>
        /// 点击展示关联接口
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task OnShowFunctionClick(ResourceDto model)
        {
            await OpenOperationDialogAsync<ResourceFunctionEdit, ResourceFunctionEditOption, bool>(
                      $"{localizer["BindingApi"]}-[{model.Name}]",
                      new ResourceFunctionEditOption { Resource = model, Type = 0, Name = model.Name },
                      width: 1200);
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
            resourceIds.AddRange(TreeTools.GetAllChildrenNodes(dto, dto => dto.Id, dto => dto.Children));

            string data = await _service.GenerateSeedData(new PageRequest()
            {
                PageIndex = 1,
                PageSize = int.MaxValue,
                FilterGroups = new List<FilterGroup>()
                {
                   new FilterGroup().AddRule(new FilterRule()
                   {
                        Field=nameof(ResourceDto.Id),
                        Operate=FilterOperate.In,
                        Value=resourceIds
                   })

                },
                OrderConditions = new List<ListSortDirection>()
                {
                    new ListSortDirection()
                    {
                        FieldName=nameof(ResourceDto.Key),
                        SortType=ListSortType.Asc
                    }
                }
            });

            await OpenOperationDialogAsync<ShowSeedDataCode, string, bool>(
                        localizer[SharedLocalResource.SeedData],
                        data,
                        width: 1300);
        }

        protected override async Task<List<ResourceDto>> GetTree()
        {
            return await resourceService.GetTree();

        }


        protected override ICollection<ResourceDto> GetChildren(ResourceDto dto)
        {
            return dto.Children;
        }


        protected override void SetChildren(ResourceDto dto, ICollection<ResourceDto> children)
        {
            dto.Children = children;
        }

        protected override Guid GetParentKey(ResourceDto dto)
        {
            return dto.ParentId.HasValue ? dto.ParentId.Value : Guid.Empty;
        }

        protected override ICollection<ResourceDto> SortChildren(ICollection<ResourceDto> children)
        {
            return children.OrderBy(x => x.Order).ToList();
        }
    }
}
