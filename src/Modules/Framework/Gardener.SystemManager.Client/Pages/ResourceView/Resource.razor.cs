﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.CodeGeneration.Resources;
using Gardener.Common;
using Gardener.Enums;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Resources;
using Gardener.SystemManager.Services;
using Microsoft.AspNetCore.Components;

namespace Gardener.SystemManager.Client.Pages.ResourceView
{
    public partial class Resource : TreeTableBase<ResourceDto, Guid, ResourceEdit, SystemManagerResource>
    {

        protected override OperationDialogSettings GetOperationDialogSettings()
        {
            var settings = base.GetOperationDialogSettings();
            settings.Width = 800;
            return settings;
        }

        [Inject]
        private IResourceService ResourceService { get; set; } = null!;

        /// <summary>
        /// 点击展示关联接口
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task OnShowFunctionClick(ResourceDto model)
        {
            await OpenOperationDialogAsync<ResourceFunctionEdit, ResourceFunctionEditOption, bool>(
                      $"{Localizer[nameof(SystemManagerResource.BindingApi)]}-[{model.Name}]",
                      new ResourceFunctionEditOption(model, model.Name, 0),
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
            resourceIds.AddRange(TreeHelper.GetAllChildrenNodes(dto, dto => dto.Id, dto => dto.Children));

            Task<string> data = BaseService.GenerateSeedData(new PageRequest()
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

            await OpenOperationDialogAsync<ShowCode, ShowCodeOptions, bool>(
                        Localizer[nameof(SharedLocalResource.SeedData)],
                       new ShowCodeOptions(data),
                        width: 1300);
        }

        protected override async Task<List<ResourceDto>> GetTree()
        {
            return await ResourceService.GetTree();

        }


        protected override ICollection<ResourceDto>? GetChildren(ResourceDto dto)
        {
            return dto.Children;
        }


        protected override void SetChildren(ResourceDto dto, ICollection<ResourceDto>? children)
        {
            dto.Children = children;
        }

        protected override Guid GetParentKey(ResourceDto dto)
        {
            return dto.ParentId.HasValue ? dto.ParentId.Value : Guid.Empty;
        }

        protected override ICollection<ResourceDto>? SortChildren(ICollection<ResourceDto>? children)
        {
            return children?.OrderBy(x => x.Order).ToList();
        }
    }
}
