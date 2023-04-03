﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Enums;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.Base;
using Gardener.Common;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Services;
using Microsoft.AspNetCore.Components;

namespace Gardener.SystemManager.Client.Pages.ResourceView
{
    public partial class ResourceEdit : EditOperationDialogBase<ResourceDto,Guid>
    {
        private ResourceType currentResourceTypeCopy = ResourceType.Root;

        [Inject]
        IResourceService resourceService { get; set; } = null!;
        /// <summary>
        /// 资源父级
        /// </summary>
        public string ParentId 
        {
            get 
            {
                return _editModel.ParentId?.ToString() ?? string.Empty;
            }
            set 
            {
                if (!string.IsNullOrEmpty(value))
                { 
                    _editModel.ParentId = Guid.Parse(value);
                }
            }
        
        }
        /// <summary>
        /// 父级资源
        /// </summary>
        private List<ResourceDto> resources=new List<ResourceDto>();
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _isLoading = true;
            resources = await resourceService.GetTree();
            
            if (this.Options.Type.Equals(DrawerInputType.Add))
            {
                _editModel.Id = Guid.NewGuid();

                if (!Guid.Empty.Equals(this.Options.Id))
                {
                    _editModel.ParentId = this.Options.Id;

                    ResourceDto parent = await resourceService.Get(this.Options.Id);

                    _editModel.Type = parent.Type;

                }
            }
            currentResourceTypeCopy = _editModel.Type;
            _isLoading = false;
        }
    }
}
