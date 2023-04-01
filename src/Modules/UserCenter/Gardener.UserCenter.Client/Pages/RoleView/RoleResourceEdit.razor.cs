﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Base.Resources;
using Gardener.Client.Base;
using Gardener.Client.Base.Services;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Services;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.RoleView
{
    public partial class RoleResourceEdit : FeedbackComponent<OperationDialogInput<int>, bool>
    {
        private Tree<ResourceDto>? _tree;
        private bool _isExpanded;
        private bool _isLoading;
        private int _roleId = 0;
        private List<ResourceDto> _resources = new List<ResourceDto>();
        [Inject]
        private IResourceService ResourceService { get; set; } = null!;
        [Inject]
        private IClientMessageService MessageService { get; set; } = null!;
        [Inject]
        private IRoleService RoleService { get; set; } = null!;
        [Inject]
        private IClientLocalizer Localizer { get; set; } = null!;
        /// <summary>
        /// 默认选择
        /// </summary>
        private string[] _defaultCheckedKeys { get; set; } = null!;
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            _isLoading = true;
            _roleId = this.Options.Id;
            if (_roleId > 0)
            {
                //已有资源
                var roleResourceResult = await RoleService.GetResource(_roleId);
                if (roleResourceResult != null && roleResourceResult.Any())
                {
                    _defaultCheckedKeys = roleResourceResult.Where(dto => dto.Children == null || !dto.Children.Any()).Select(dto => dto.Id.ToString()).ToArray();
                }
                //资源树
                var resourceResult = await ResourceService.GetTree();
                if (resourceResult == null)
                {
                    MessageService.Error(Localizer.Combination(SharedLocalResource.Resource, SharedLocalResource.Load, SharedLocalResource.Fail));
                    _isLoading = false;
                    return;
                }
                _resources.AddRange(resourceResult);
            }
            await base.OnInitializedAsync();
            _isLoading = false;
        }
       

        /// <summary>
        /// 点击取消
        /// </summary>
        private async Task OnCancelClick()
        {
            await base.FeedbackRef.CloseAsync(false);
        }

        /// <summary>
        /// 点击保存
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private async Task OnSaveClick()
        {
            _isLoading = true;

            List<Guid> resourceIds = new List<Guid>();

            if (_tree!=null && _tree.CheckedKeys.Length > 0)
            {
                _tree.CheckedKeys.ForEach(x =>
                {
                   TreeNode<ResourceDto> node = _tree.FindFirstOrDefaultNode(node => { return node.Key.Equals(x);}, true);
                    if (node != null)
                    {
                        resourceIds.Add(node.DataItem.Id);
                        List<TreeNode<ResourceDto>> predecessors = node.GetPredecessors();
                        if (predecessors != null)
                        {
                            resourceIds.AddRange(predecessors.Select(x=>x.DataItem.Id));
                        }
                    }
                });
            }
            //删除所有资源
            var result = await RoleService.Resource(_roleId, resourceIds.Distinct().ToArray());
            if (result)
            {
                MessageService.Success(Localizer.Combination(SharedLocalResource.Save, SharedLocalResource.Success));
                await base.FeedbackRef.CloseAsync(true);
            }
            else
            {
                MessageService.Error(Localizer.Combination(SharedLocalResource.Save, SharedLocalResource.Fail));
            }
            _isLoading = false;
        }
        
        /// <summary>
        /// 当展开关闭点击时触发
        /// </summary>
        /// <returns></returns>
        private void OnExpandClick()
        {
            if(_tree==null) return;
            _isLoading = true;
            _isExpanded = !_isExpanded;
            //操作所有的节点
            if (_isExpanded) 
            { 
                _tree.ExpandAll();
            } 
            else 
            { 
                _tree.CollapseAll();
            }
        }
    }
}
