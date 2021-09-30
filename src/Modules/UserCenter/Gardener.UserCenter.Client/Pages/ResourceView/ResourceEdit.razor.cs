// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.Base;
using Gardener.Common;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Enums;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.ResourceView
{
    public partial class ResourceEdit : EditDrawerBase<ResourceDto,Guid>
    {
        [Inject]
        IResourceService resourceService { get; set; }
        /// <summary>
        /// 资源父级
        /// </summary>
        public string ParentId 
        {
            get {
                return _editModel.ParentId?.ToString();
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
        private List<ResourceDto> resources;
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<ResourceType, string> resourceTypes = new Dictionary<ResourceType, string>();
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

            if (this.Options.Type.Equals(DrawerInputType.Select))
            {
                resourceTypes.Add(_editModel.Type, _editModel.Type.GetEnumDescription());
            }
            else
            {
                foreach (var gitem in EnumHelper.EnumToDictionary<ResourceType>())
                {
                    if ((int)gitem.Key >= (int)_editModel.Type)
                    {
                        resourceTypes.Add(gitem.Key, gitem.Value);
                    }
                }
            }
            _isLoading = false;
        }
    }
}
