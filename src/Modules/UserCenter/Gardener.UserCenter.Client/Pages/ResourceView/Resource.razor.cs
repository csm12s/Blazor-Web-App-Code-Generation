// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.Base.Components;
using Gardener.Client.Base.Model;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.ResourceView
{
    public partial class Resource : TreeTableBase<ResourceDto,Guid,ResourceEdit>
    {

        public Resource() : base(new DrawerSettings { Width = 800 })
        {

        }
        [Inject]
        DrawerService drawerService { get; set; }

        [Inject]
        IResourceService resourceService { get; set; }

        /// <summary>
        /// 点击展示关联接口
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task OnShowFunctionClick(ResourceDto model)
        {
            var result = await drawerService.CreateDialogAsync<ResourceFunctionEdit, ResourceFunctionEditOption, bool>(
                      new ResourceFunctionEditOption { Id=model.Id,Type=0,Name=model.Name},
                      true,
                      title: $"{localizer["绑定接口"]}-[{model.Name}]",
                      width: 1200,
                      placement: "right");
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        private async Task OnDownloadClick()
        {
            var result = await drawerService.CreateDialogAsync<ResourceDownload, string, bool>(
                      string.Empty,
                       true,
                       title: localizer["种子数据"],
                       width: 1300,
                       placement: "right");
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
            return dto.ParentId.HasValue? dto.ParentId.Value:Guid.Empty;
        }

        protected override ICollection<ResourceDto> SortChildren(ICollection<ResourceDto> children)
        {
            return children.OrderBy(x => x.Order).ToList();
        }
    }
}
