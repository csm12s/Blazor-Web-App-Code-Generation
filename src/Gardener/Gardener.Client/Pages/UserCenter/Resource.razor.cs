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
        IResourceService resourceService { get; set; }
        [Inject]
        MessageService messageService { get; set; }
        [Inject]
        ConfirmService confirmService { get; set; }
        [Inject]
        DrawerService drawerService { get; set; }
        //资源树
        Tree<ResourceDto> tree;
        //当前节点展开状态
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
            var resourceResult = await resourceService.GetTree();
            if (resourceResult != null)
            {
                tree.DataSource = resourceResult;
            }
            else
            {
                messageService.Error("资源节点未加载到数据");
            }
            treeIsLoading = false;
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
                await tree.ExpandAll(new List<TreeNode<ResourceDto>> { selectedNode }, isExpanded);
            }
            else
            {
                //操作所有的节点
                await tree.ExpandAll(tree.ChildNodes, isExpanded);
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
        private async Task OnNodeLoadDelayAsync(TreeEventArgs<ResourceDto> args)
        {
            treeIsLoading = true;
            var parentNode = ((ResourceDto)args.Node.DataItem);
            parentNode.Children = new List<ResourceDto>();
            var resourceResult = await resourceService.GetChildren(parentNode.Id);
            if (resourceResult != null)
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
                messageService.Error("资源节点未加载到数据");
            }
            treeIsLoading = false;
        }
        /// <summary>
        /// 点击节点
        /// </summary>
        /// <param name="args"></param>
        private async Task OnNodeClick(TreeEventArgs<ResourceDto> args)
        {
            descriptionsIsLoading = true;
            isExpanded = args.Node.Expanded;
            var id = args.Node.DataItem.Id;
            var resource = await resourceService.Get(id);
            if (resource != null)
            {
                selectedModel = resource;
            }
            else
            {
                messageService.Error("资源不存在");
            }

            descriptionsIsLoading = false;
        }
        private bool descriptionsIsLoading;
        //信息展示区域显示
        private ResourceDto selectedModel = new ResourceDto();
        /// <summary>
        /// 删除选中节点
        /// </summary>
        /// <returns></returns>
        private async Task OnDeleteSelectedNodeClick(TreeNode<ResourceDto> node)
        {
            var selectedNode = node ?? tree.SelectedNodes?.FirstOrDefault();
            if (selectedNode != null)
            {
                var resource = selectedNode.DataItem;
                if (resource.Type.Equals(ResourceType.Root))
                {
                    messageService.Error("根节点无法删除");
                    treeIsLoading = false;
                    return;
                }
                treeIsLoading = true;
                if (await confirmService.YesNoDelete() == ConfirmResult.Yes)
                {

                    var ids = GetAllDeleteResourceId(resource);
                    var result = await resourceService.FakeDeletes(ids.ToArray());
                    if (result)
                    {
                        var parentNode = selectedNode.ParentNode.DataItem;
                        parentNode.Children.Remove(resource);
                        messageService.Success("删除成功");
                    }
                    else
                    {
                        messageService.Error("删除失败");
                    }
                }
                treeIsLoading = false;
            }
            else
            {
                messageService.Warn("请先选择节点");
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
        private async Task OnEditSelectedNodeClick(TreeNode<ResourceDto> node)
        {
            var selectedNode = node ?? tree.SelectedNodes?.FirstOrDefault();
            if (selectedNode != null)
            {
                var result = await drawerService.CreateDialogAsync<ResourceEdit, ResourceEditOption, bool>(
                    new ResourceEditOption() { Type = 1, SelectedResourceId = ((ResourceDto)selectedNode.DataItem).Id },
                    true,
                    title: "编辑",
                    width: 400,
                    placement: "left");
                if (result) { await LoadTreeData(); }
            }
            else
            {
                messageService.Warn("请先选择节点");
            }
        }
        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <returns></returns>
        private async Task OnAddChildNodeClick(TreeNode<ResourceDto> node)
        {
            var selectedNode = node ?? tree.SelectedNodes?.FirstOrDefault();
            if (selectedNode != null)
            {
                var result = await drawerService.CreateDialogAsync<ResourceEdit, ResourceEditOption, bool>(
                    new ResourceEditOption() { Type = 0, SelectedResourceId = ((ResourceDto)selectedNode.DataItem).Id },
                    true,
                    title: "添加",
                    width: 400,
                    placement: "left");
                if (result) { await LoadTreeData(); }
            }
            else
            {
                messageService.Warn("请先选择父节点");
            }

        }
    }
}
