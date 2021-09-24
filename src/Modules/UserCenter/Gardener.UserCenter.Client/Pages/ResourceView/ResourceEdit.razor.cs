// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.Base;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Enums;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.ResourceView
{
    public partial class ResourceEdit : FeedbackComponent<EditInput<Guid>, EditOutput<Guid>>
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

            EditInput<Guid> option = this.Options;

            if (option.Type.Equals(EditInputType.Add))
            {
                //添加
                var newResource = new ResourceDto();
                newResource.Id = Guid.Empty;
                newResource.ParentId = option.Id;
                //不能创建root节点
                newResource.Type = ResourceType.Menu;
                _editModel = newResource;
            }
            else if (option.Type.Equals(EditInputType.Edit))
            {

                var selectedResource = await resourceService.Get(option.Id);
                if (selectedResource == null)
                {
                    messageService.Error("所选资源不存在");
                    await base.FeedbackRef.CloseAsync(EditOutput<Guid>.Fail());
                    return;
                }
                _editModel = selectedResource;
                
            }
            _resourceCascaderValue = _editModel.ParentId.ToString();
            //记录当前对象的类型
            _currentEditResourceType = _editModel.Type;

            //所有资源
            var resources = await resourceService.GetTree();


            //生成级联对象
            if (resources != null)
            {
                _resourceCascaderNodes = ComponentUtils.DtoConvertToCascaderNode<ResourceDto>(resources, dto => dto.Children, dto => dto.Name, dto => dto.Id.ToString(),new []{ _editModel.Id.ToString() });

                //_resourceCascaderNodes = new List<CascaderNode>();
                //foreach (ResourceDto item in resources)
                //{
                //    CascaderNode node = new CascaderNode();
                //    node.Label = item.Name;
                //    node.Value = item.Id.ToString();
                //    node.Children = GetChildrenCascaderNodes(item);
                //    _resourceCascaderNodes.Add(node);
                //}
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
                    await base.FeedbackRef.CloseAsync(EditOutput<Guid>.Succeed(_editModel.Id));
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

                    await base.FeedbackRef.CloseAsync(EditOutput<Guid>.Succeed(result.Id));
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
            await base.FeedbackRef.CloseAsync(EditOutput<Guid>.Cancel());
        }

    }
}
