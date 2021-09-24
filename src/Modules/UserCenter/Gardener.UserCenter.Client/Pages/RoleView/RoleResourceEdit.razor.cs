// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client.Pages.SystemManager.RoleView
{
    public partial class RoleResourceEdit : FeedbackComponent<int, bool>
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
        /// 渲染后
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
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
                foreach (ResourceDto dto in roleResourceResult) 
                {
                    TreeNode<ResourceDto> node= _tree.FindFirstOrDefaultNode(node => 
                    {
                        return dto.Id.Equals(node.DataItem.Id);
                    }, true);
                    if (node != null && node.IsLeaf) { node.SetChecked(true); }
                }

                _isLoading = false;
            }
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
                messageService.Success("保存成功");
                await (base.FeedbackRef as DrawerRef<bool>).CloseAsync(true);
            }
            else
            {
                messageService.Error("保存失败");
            }
            _isLoading = false;
        }
        
        /// <summary>
        /// 当展开关闭点击时触发
        /// </summary>
        /// <returns></returns>
        private async Task OnExpandClick()
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
