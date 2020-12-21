﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client.Pages.UserCenter
{
    public partial class RoleResourceEdit : DrawerTemplate<int, bool>
    {
        private Tree _tree;
        private bool _isExpanded;
        private bool _isLoading;
        private int _roleId = 0;
        private List<ResourceDto> _resources = new List<ResourceDto>();
        [Inject]
        IResourceService resourceService { get; set; }
        [Inject]
        MessageService messageService { get; set; }
        [Inject]
        IRoleService roleService { get; set; }
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            _isLoading = true;

            _roleId = this.Options;

            if (_roleId > 0)
            {
                var resourceResult = await resourceService.GetTree();
                if (resourceResult == null)
                {
                    messageService.Error("资源节点未加载到数据");
                    _isLoading = false;
                    return;
                }
                _resources.AddRange(resourceResult);
            }
            _isLoading = false;
            await base.OnInitializedAsync();
        }
        /// <summary>
        /// 递归展开或关闭节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        private async Task Check(List<TreeNode> nodes, Func<Guid, bool> flagFunc)
        {
            foreach (var node in nodes)
            {
                var flag = flagFunc(((ResourceDto)node.DataItem).Id);
                //有变化再进行变更
                if (node.IsChecked != flag)
                {
                    node.SetChecked(flag);
                }
                await Check(node.ChildNodes, flagFunc);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _isLoading = true;
                //选中已有资源
                var roleResourceResult = await roleService.GetResource(_roleId);
                if (roleResourceResult == null)
                {
                    messageService.Error("已分配资源加载失败");
                    _isLoading = false;
                    return;
                }
                ////回填
                await Check(_tree.ChildNodes, (id) =>
                {
                    if (roleResourceResult == null) return false;
                    return roleResourceResult.Any(x => x.Id == id && (x.Children == null || !x.Children.Any()));
                });
                _isLoading = false;
            }
        }

        /// <summary>
        /// 点击取消
        /// </summary>
        private async Task OnCancelClick()
        {
            await base.CloseAsync(false);
        }
        /// <summary>
        /// 点击保存
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private async Task OnSaveClick()
        {
            _isLoading = true;

            Guid[] resourceIds = new Guid[] { };

            if (_tree.CheckedNodes?.Count > 0)
            {
                List<TreeNode> parents = new List<TreeNode>();
                _tree.CheckedNodes.ForEach(x =>
                {
                    parents.AddRange(GetParents(x));
                });
                parents.AddRange(_tree.CheckedNodes);
                resourceIds = parents.Select(x => ((ResourceDto)x.DataItem).Id).Distinct().ToArray();
            }
            //删除所有资源
            var result = await roleService.Resource(_roleId, resourceIds);
            if (result)
            {
                messageService.Success("保存成功");
                await base.CloseAsync(true);
            }
            else
            {
                messageService.Error("保存失败");
            }
            _isLoading = false;
        }
        /// <summary>
        /// 获取所有父级
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private List<TreeNode> GetParents(TreeNode node)
        {
            List<TreeNode> ids = new List<TreeNode>();
            if (node.ParentNode != null)
            {
                ids.Add(node.ParentNode);
                ids.AddRange(GetParents(node.ParentNode));
            }
            return ids;
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
            _isExpanded = !_isExpanded;

            var selectedNode = _tree.SelectedNodes?.FirstOrDefault();
            if (selectedNode != null)
            {
                //仅操作选中的节点
                await Expand(new List<TreeNode> { selectedNode }, _isExpanded);
            }
            else
            {
                //操作所有的节点
                await Expand(_tree.ChildNodes, _isExpanded);
            }
        }
    }
}