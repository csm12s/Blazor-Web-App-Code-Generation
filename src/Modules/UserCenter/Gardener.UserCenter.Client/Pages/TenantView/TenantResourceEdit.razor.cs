// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.Base.Services;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Services;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.TenantView
{
    public partial class TenantResourceEdit : OperationDialogBase<SystemTenantDto, bool>
    {
        private Tree<ResourceDto>? _tree;
        private bool _isExpanded;
        private List<ResourceDto> _resources = new List<ResourceDto>();
        [Inject]
        private IResourceService ResourceService { get; set; } = null!;
        [Inject]
        private IClientMessageService MessageService { get; set; } = null!;
        [Inject]
        private ITenantService TenantService { get; set; } = null!;
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
            await base.StartLoading();
            var t1 = TenantService.GetResources(this.Options.Id);
            var t2 = ResourceService.GetTree(supportMultiTenant:true);
            //已有资源
            var tenantResourceResult = await t1;
            if (tenantResourceResult != null && tenantResourceResult.Any())
            {
                _defaultCheckedKeys = tenantResourceResult.Where(dto => dto.Children == null || !dto.Children.Any()).Select(dto => dto.Id.ToString()).ToArray();
            }
            //资源树
            var resourceResult = await t2;
            if (resourceResult == null)
            {
                MessageService.Error(Localizer.Combination(SharedLocalResource.Resource, SharedLocalResource.Load, SharedLocalResource.Fail));
                await base.StopLoading();
                return;
            }
            _resources.AddRange(resourceResult);
            await base.StopLoading();
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
            _dialogLoading.Start();

            List<Guid> resourceIds = new List<Guid>();

            if (_tree != null && _tree.CheckedKeys.Length > 0)
            {
                _tree.CheckedKeys.ForEach(x =>
                {
                    TreeNode<ResourceDto> node = _tree.FindFirstOrDefaultNode(node => { return node.Key.Equals(x); }, true);
                    if (node != null)
                    {
                        resourceIds.Add(node.DataItem.Id);
                        List<TreeNode<ResourceDto>> predecessors = node.GetPredecessors();
                        if (predecessors != null)
                        {
                            resourceIds.AddRange(predecessors.Select(x => x.DataItem.Id));
                        }
                    }
                });
            }
            //删除所有资源
            var result = await TenantService.AddResources(this.Options.Id, resourceIds.Distinct().ToArray());
            if (result)
            {
                MessageService.Success(Localizer.Combination(SharedLocalResource.Save, SharedLocalResource.Success));
                await base.FeedbackRef.CloseAsync(true);
            }
            else
            {
                MessageService.Error(Localizer.Combination(SharedLocalResource.Save, SharedLocalResource.Fail));
            }
            _dialogLoading.Stop();
        }

        /// <summary>
        /// 当展开关闭点击时触发
        /// </summary>
        /// <returns></returns>
        private void OnExpandClick()
        {
            if (_tree == null) return;
            _dialogLoading.Start();
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
