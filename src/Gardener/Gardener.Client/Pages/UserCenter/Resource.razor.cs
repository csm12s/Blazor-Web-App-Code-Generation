// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Application.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;
using System.Linq;
using System;
using Gardener.Enums;
using Gardener.Application.Interfaces;

namespace Gardener.Client.Pages.UserCenter
{
    public partial class Resource
    {
        private bool treeIsLoading;
        [Inject]
        IResourceService ResourceService { get; set; }
        [Inject]
        MessageService MessageService { get; set; }
        [Inject]
        ConfirmService ConfirmSvr { get; set; }
        Tree tree;
        private bool isExpanded;
        /// <summary>
        /// 页面初始化完成
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await LoadTreeData();
        }
        /// <summary>
        /// 加载树数据
        /// </summary>
        /// <returns></returns>
        private async Task LoadTreeData()
        {
            treeIsLoading = true;
            var resourceResult = await ResourceService.GetTree();
            if (resourceResult!=null)
            {
                tree.DataSource = resourceResult;
            }
            else
            {
                MessageService.Error("资源节点未加载到数据");
            }
            treeIsLoading = false;
        }
        /// <summary>
        /// 递归展开或关闭节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        private async Task Expand(List<TreeNode> nodes, bool flag)
        {
            foreach (var node in nodes)
            {
                node.Expand(flag);
                if (node.ChildNodes != null && node.ChildNodes.Count > 0)
                {
                    await Expand(node.ChildNodes, flag);
                }
            }
        }
        /// <summary>
        /// 当展开关闭点击时触发
        /// </summary>
        /// <returns></returns>
        private async Task OnExpandClick()
        {
            isExpanded = !isExpanded;

            var selectedNode = tree.SelectedNodes?.FirstOrDefault();
            if (selectedNode != null)
            {
                //仅操作选中的节点
                await Expand(new List<TreeNode> { selectedNode }, isExpanded);
            }
            else
            {
                //操作所有的节点
                await Expand(tree.ChildNodes, isExpanded);
            }
        }
        /// <summary>
        /// 当点击刷新时触发
        /// </summary>
        /// <returns></returns>
        private async Task OnReloadClick()
        {
            await LoadTreeData();
        }
        /// <summary>
        /// 加载子节点
        /// </summary>
        /// <param name="args"></param>
        private async Task OnNodeLoadDelayAsync(TreeEventArgs args)
        {
            treeIsLoading = true;
            var parentNode = ((ResourceDto)args.Node.DataItem);
            parentNode.Children = new List<ResourceDto>();
            var resourceResult = await ResourceService.GetChildren(parentNode.Id);
            if (resourceResult!=null)
            {
                resourceResult.ForEach(x =>
                {
                    if (x.Children == null)
                    {
                        x.Children = new List<ResourceDto>();
                    }
                    parentNode.Children.Add(x);
                });
            }
            else
            {
                MessageService.Error("资源节点未加载到数据");
            }
            treeIsLoading = false;
        }
        /// <summary>
        /// 点击节点
        /// </summary>
        /// <param name="args"></param>
        private async Task OnNodeClick(TreeEventArgs args)
        {
            descriptionsIsLoading = true;
            isExpanded = args.Node.IsExpanded;
            ((ResourceDto)args.Node.DataItem).Adapt(editModel);
            descriptionsIsLoading = false;
        }
        private bool drawerVisible;
        private bool formIsLoading;
        private bool descriptionsIsLoading;
        private string drawerTitle = string.Empty;
        private ResourceDto editModel = new ResourceDto();
        private string editModelHttpMethodType
        {
            get
            {
                return editModel.Method?.ToString();
            }
            set
            {
                editModel.Method = (HttpMethodType)Enum.Parse(typeof(HttpMethodType), value);
            }
        }
        private ResourceType currentEditResourceType = ResourceType.API;
        /// <summary>
        /// 删除选中节点
        /// </summary>
        /// <returns></returns>
        private async Task OnDeleteSelectedNodeClick()
        {
            var selectedNode = tree.SelectedNodes?.FirstOrDefault();
            if (selectedNode != null)
            {
                var resource = ((ResourceDto)selectedNode.DataItem);
                if (resource.Type.Equals(ResourceType.ROOT))
                {
                    MessageService.Error("根节点无法删除");
                    treeIsLoading = false;
                    return;
                }
                treeIsLoading = true;
                if (await ConfirmSvr.YesNoDelete() == ConfirmResult.Yes)
                {

                    var ids = GetAllDeleteResourceId(resource);
                    var result = await ResourceService.FakeDeletes(ids.ToArray());
                    if (result)
                    {
                        var parentNode = ((ResourceDto)selectedNode.ParentNode.DataItem);
                        parentNode.Children.Remove(resource);
                        MessageService.Success("删除成功");
                    }
                    else
                    {
                        MessageService.Error("删除失败");
                    }
                }
                treeIsLoading = false;
            }
            else
            {
                MessageService.Warn("请先选择节点");
            }
        }
        /// <summary>
        /// 递归获取所有要删除的id
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        private List<Guid> GetAllDeleteResourceId(ResourceDto resource)
        {

            var result = new List<Guid>() { resource.Id };

            if (resource.Children == null || !resource.Children.Any())
            {
                return result;
            }
            resource.Children.ForEach(x =>
            {

                result.AddRange(GetAllDeleteResourceId(x));
            });

            return result;
        }
        /// <summary>
        /// 编辑选中节点
        /// </summary>
        /// <returns></returns>
        private async Task OnEditSelectedNodeClick()
        {
            var selectedNode = tree.SelectedNodes?.FirstOrDefault();
            if (selectedNode != null)
            {
                var resource = ((ResourceDto)selectedNode.DataItem);
                currentEditResourceType = resource.Type;
                resource.Adapt(editModel);
                drawerTitle = "编辑";
                drawerVisible = true;
            }
            else
            {
                MessageService.Warn("请先选择节点");
            }
        }
        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <returns></returns>
        private async Task OnAddChildNodeClick()
        {
            var selectedNode = tree.SelectedNodes?.FirstOrDefault();
            if (selectedNode != null)
            {
                drawerTitle = "添加";
                var pResource = ((ResourceDto)selectedNode.DataItem);
                var newNode = new ResourceDto();
                newNode.Id = Guid.Empty;
                newNode.ParentId = pResource.Id;
                newNode.Key = pResource.Type.Equals(ResourceType.ROOT) ? "" : pResource.Key + "_";
                //不能创建root节点
                currentEditResourceType = newNode.Type = pResource.Type.Equals(ResourceType.ROOT) ? ResourceType.MENU : pResource.Type;
                newNode.Order = (pResource.Children == null || !pResource.Children.Any() ? 0 : pResource.Children.Last().Order + 1);
                newNode.Adapt(editModel);
                drawerVisible = true;
            }
            else
            {
                MessageService.Warn("请先选择父节点");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="editContext"></param>
        /// <returns></returns>
        private async Task OnFormFinish(EditContext editContext)
        {
            formIsLoading = true;
            var selectedNode = tree.SelectedNodes?.FirstOrDefault();
            var resource = ((ResourceDto)selectedNode.DataItem);
            if (editModel.Id != Guid.Empty)
            {
                //更新
                var result = await ResourceService.Update(editModel);
                if (result)
                {
                    editModel.Adapt(resource);
                    if (selectedNode.ParentNode != null)
                    {
                        var parentResource = (ResourceDto)selectedNode.ParentNode.DataItem;
                        parentResource.Children = parentResource.Children.OrderBy(x => x.Order).ToList();
                    }
                    MessageService.Success("更新成功");
                    drawerVisible = false;
                }
                else
                {
                    MessageService.Error("更新失败");
                }
            }
            else
            {
                editModel.Id = Guid.NewGuid();
                //新增
                var result = await ResourceService.Insert(editModel);
                if (result!=null)
                {
                    if (resource.Children == null)
                    {
                        resource.Children = new List<ResourceDto>();
                    }
                    resource.Children.Add(result);

                    resource.Children = resource.Children.OrderBy(x => x.Order).ToList();

                    MessageService.Success("添加成功");
                    drawerVisible = false;
                }
                else
                {
                    MessageService.Error("添加失败");
                }
            }
            formIsLoading = false;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="editContext"></param>
        private async Task OnFormFinishFailed(EditContext editContext)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task OnDrawerClose()
        {
            drawerVisible = false;
        }
    }
}
