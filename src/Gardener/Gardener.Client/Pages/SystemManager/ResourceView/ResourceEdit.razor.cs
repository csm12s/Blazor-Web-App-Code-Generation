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

namespace Gardener.Client.Pages.SystemManager.ResourceView
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

    public partial class ResourceEdit : FeedbackComponent<ResourceEditOption, bool>
    {
        private bool _isLoading = false;

        private ResourceDto _editModel;

        private ResourceType _currentEditResourceType = ResourceType.Menu;
        [Inject]
        MessageService messageService { get; set; }
        [Inject]
        IResourceService resourceService { get; set; }
        /// <summary>
        /// 父级选择数据
        /// </summary>
        private List<CascaderNode> _resourceCascaderNodes;
        /// <summary>
        /// 选择器绑定值
        /// </summary>
        private string _resourceCascaderValue =String.Empty;
        /// <summary>
        /// 父级选择数据
        /// </summary>
        /// <param name="selectedNodes"></param>
        private void CascaderOnChange(CascaderNode[] selectedNodes)
        {
            _editModel.ParentId = Guid.Parse(_resourceCascaderValue);
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            _isLoading = true;

            ResourceEditOption option = this.Options;

           

            if (option.Type == 0)
            {
                //添加
                var newResource = new ResourceDto();
                newResource.Id = Guid.Empty;
                newResource.ParentId = Guid.Empty;
                //newResource.Key = selectedResource.Type.Equals(ResourceType.Root) ? "" : selectedResource.Key + "_";
                //不能创建root节点
                newResource.Type = ResourceType.Menu;
                //newResource.Order = (children == null || !children.Any() ? 0 : children.Max(x => x.Order) + 1);
                _editModel = newResource;
            }
            else if (option.Type == 1)
            {

                var selectedResource = await resourceService.Get(option.SelectedResourceId);
                if (selectedResource == null)
                {
                    messageService.Error("所选资源不存在");
                    DrawerRef<bool> drawerRef = base.FeedbackRef as DrawerRef<bool>;
                    await drawerRef!.CloseAsync(false);
                    return;
                }

                _editModel = selectedResource;
                _resourceCascaderValue = _editModel.ParentId.ToString();
            }
            //记录当前对象的类型
            _currentEditResourceType = _editModel.Type;

            //所有资源
            var resources = await resourceService.GetTree();
            //生成级联对象
            if (resources != null)
            {
                _resourceCascaderNodes=new List<CascaderNode>();
                foreach (ResourceDto item in resources)
                {
                    CascaderNode node = new CascaderNode();
                    node.Label = item.Name;
                    node.Value = item.Id.ToString();
                    node.Children = GetChildrenCascaderNodes(item);
                    _resourceCascaderNodes.Add(node);
                }
            }

            _isLoading = false;
            await base.OnInitializedAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        private List<CascaderNode> GetChildrenCascaderNodes(ResourceDto resource)
        {
            List<CascaderNode> cascaderNodes = new List<CascaderNode>();

            if (resource.Children != null)
            {
                foreach (ResourceDto item in resource.Children)
                {
                    CascaderNode node = new CascaderNode();
                    node.Label = item.Name;
                    node.Value = item.Id.ToString();
                    node.Children = GetChildrenCascaderNodes(item);
                    cascaderNodes.Add(node);
                }
            }
            return cascaderNodes;
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
                    DrawerRef<bool> drawerRef = base.FeedbackRef as DrawerRef<bool>;
                    await drawerRef!.CloseAsync(true);
                }
                else
                {
                    messageService.Error("更新失败");
                }
            }
            else
            {
                _editModel.Id = Guid.NewGuid();

                if (_editModel.ParentId.Equals(Guid.Empty)) 
                {
                    messageService.Warn("请选择父级");
                    _isLoading = false;
                    return;
                }

                //新增
                var result = await resourceService.Insert(_editModel);
                if (result != null)
                {
                    messageService.Success("添加成功");
                    DrawerRef<bool> drawerRef = base.FeedbackRef as DrawerRef<bool>;
                    await drawerRef!.CloseAsync(true);
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
            DrawerRef<bool> drawerRef = base.FeedbackRef as DrawerRef<bool>;
            await drawerRef!.CloseAsync(false);
        }

    }
}
