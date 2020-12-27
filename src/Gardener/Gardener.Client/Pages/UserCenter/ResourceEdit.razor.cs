// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Gardener.Enums;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client.Pages.UserCenter
{

    public class ResourceEditOption
    {
        /// <summary>
        /// 选中的节点
        /// </summary>
        public Guid SelectedResourceId { get; set; }
        /// <summary>
        /// 0添加
        /// 1编辑
        /// </summary>
        public int Type { get; set; }
    }

    public partial class ResourceEdit: DrawerTemplate<ResourceEditOption, bool>
    {
        private bool _isLoading = false;

        private ResourceDto _editModel;
        private string _currentEditModelHttpMethodType
        {
            get
            {
                return _editModel.Method?.ToString();
            }
            set
            {
                _editModel.Method = (HttpMethodType)Enum.Parse(typeof(HttpMethodType), value);
            }
        }
        private ResourceType _currentEditResourceType = ResourceType.Api;
        [Inject]
        MessageService messageService { get; set; }
        [Inject]
        IResourceService resourceService { get; set; }

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            _isLoading = true;

            ResourceEditOption option = this.Options;

            var selectedResource = await resourceService.Get(option.SelectedResourceId);
            if (selectedResource == null)
            {
                messageService.Error("所选资源不存在");
                await base.CloseAsync(false);
                return;
            }

            if (option.Type == 0)
            {
                //添加
                if (selectedResource != null)
                {
                    var children = await resourceService.GetChildren(selectedResource.Id);
                    var newResource = new ResourceDto();
                    newResource.Id = Guid.Empty;
                    newResource.ParentId = selectedResource.Id;
                    newResource.Key = selectedResource.Type.Equals(ResourceType.Root) ? "" : selectedResource.Key + "_";
                    //不能创建root节点
                    newResource.Type = selectedResource.Type.Equals(ResourceType.Root) ? ResourceType.Menu : selectedResource.Type;
                    newResource.Order = (children == null || !children.Any() ? 0 : children.Max(x=>x.Order) + 1);
                    _editModel = newResource;
                }
            }
            else if (option.Type == 1)
            {
                _editModel = selectedResource;
            }
            //记录当前对象的类型
            _currentEditResourceType = _editModel.Type;

            _isLoading = false;
            await base.OnInitializedAsync();
        }

        /// <summary>
        /// 表单完成时
        /// </summary>
        /// <param name="editContext"></param>
        /// <returns></returns>
        private async Task OnFormFinish(EditContext editContext)
        {
            _isLoading = true;
            if (_editModel.Id != Guid.Empty)
            {
                //更新
                var result = await resourceService.Update(_editModel);
                if (result)
                {
                    messageService.Success("更新成功");
                    await base.CloseAsync(true);
                }
                else
                {
                    messageService.Error("更新失败");
                }
            }
            else
            {
                _editModel.Id = Guid.NewGuid();
                //新增
                var result = await resourceService.Insert(_editModel);
                if (result != null)
                {
                    messageService.Success("添加成功");
                    await base.CloseAsync(true);
                }
                else
                {
                    messageService.Error("添加失败");
                }
            }
            _isLoading = false;
        }
        /// <summary>
        /// 取消
        /// </summary>
        private async Task OnFormCancel()
        {
            await base.CloseAsync(false);
        }

    }
}
