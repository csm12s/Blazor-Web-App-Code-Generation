// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.Base;
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
    public partial class RoleResourceEdit : FeedbackComponent<DrawerInput<int>, bool>
    {
        private Tree<ResourceDto> _tree;
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
        [Inject]
        IClientLocalizer localizer { get; set; }
        /// <summary>
        /// 默认选择
        /// </summary>
        private string[] _defaultCheckedKeys { get; set; }
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
                var roleResourceResult = await roleService.GetResource(_roleId);
                if (roleResourceResult != null && roleResourceResult.Any())
                {
                    _defaultCheckedKeys = roleResourceResult.Where(dto => dto.Children == null || !dto.Children.Any()).Select(dto => dto.Id.ToString()).ToArray();
                }
                //资源树
                var resourceResult = await resourceService.GetTree();
                if (resourceResult == null)
                {
                    messageService.Error(localizer.Combination("资源","加载", "失败"));
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
            await (base.FeedbackRef as DrawerRef<bool>).CloseAsync(false);
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

            if (_tree.CheckedKeys.Length > 0)
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
            var result = await roleService.Resource(_roleId, resourceIds.Distinct().ToArray());
            if (result)
            {
                messageService.Success(localizer.Combination("保存", "成功"));
                await (base.FeedbackRef as DrawerRef<bool>).CloseAsync(true);
            }
            else
            {
                messageService.Error(localizer.Combination("保存", "失败"));
            }
            _isLoading = false;
        }
        
        /// <summary>
        /// 当展开关闭点击时触发
        /// </summary>
        /// <returns></returns>
        private void OnExpandClick()
        {
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
