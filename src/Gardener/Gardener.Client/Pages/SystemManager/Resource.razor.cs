// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using AntDesign.TableModels;
using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Gardener.Client.Pages.UserCenter;
using Mapster;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client.Pages.SystemManager
{
    public partial class Resource
    {
        ITable _table;
        List<ResourceDto> _resources;
        IEnumerable<ResourceDto> _selectedRows;

        bool _tableIsLoading = false;
        [Inject]
        public MessageService messageService { get; set; }
        [Inject]
        public IResourceService resourceService { get; set; }
        [Inject]
        ConfirmService confirmService { get; set; }
        [Inject]
        DrawerService drawerService { get; set; }
        [Inject]
        IAuthorizeService authorizeService { get; set; }

        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <returns></returns>
        private async Task ReLoadTable()
        {
            _tableIsLoading = true;
            _resources = await resourceService.GetTree();
            if (_resources == null)
            {
                messageService.Error("加载失败");
            }
            _tableIsLoading = false;
        }
        /// <summary>
        /// 刷新页面
        /// </summary>
        /// <returns></returns>
        private async Task OnReLoadTable()
        {
            await ReLoadTable();
        }
        /// <summary>
        /// 查询变化
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        private async Task onChange(QueryModel<ResourceDto> queryModel)
        {
            await ReLoadTable();
        }
        /// <summary>
        /// 点击删除按钮
        /// </summary>
        /// <param name="id"></param>
        private async Task OnDeleteClick(ResourceDto resource)
        {
            if (await confirmService.YesNoDelete() == ConfirmResult.Yes)
            {
                //找到所有子集
                List<Guid> ids = new List<Guid>() { resource.Id };
                GetTreeAllChildrenNodes(resource,ids);
                var result = await resourceService.FakeDeletes(ids.ToArray());
                if (result)
                {
                    messageService.Success("删除成功");
                    DeleteTreeNode(resource.ParentId.Value,resource.Id,_resources);
                }
                else
                {
                    messageService.Error("删除失败");
                }
            }
        }
        /// <summary>
        /// 获取所有子集节点id
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="ids"></param>
        private void GetTreeAllChildrenNodes(ResourceDto resource, List<Guid> ids)
        {
            if (resource.Children != null)
            {
                resource.Children.ForEach(x => { 
                    ids.Add(x.Id);
                    GetTreeAllChildrenNodes(x,ids);
                });
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="id"></param>
        /// <param name="resourceDtos"></param>
        private bool DeleteTreeNode(Guid pId,Guid id,List<ResourceDto> resourceDtos)
        {
            foreach (ResourceDto dto in resourceDtos)
            {
                if (dto.Id.Equals(pId))
                {
                    dto.Children = dto.Children.Where(x => !x.Id.Equals(id)).ToList();
                    return true;
                }
                if (dto.Children!=null) 
                {
                    if (DeleteTreeNode(pId, id, dto.Children.ToList()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 点击编辑按钮
        /// </summary>
        /// <param name="roleDto"></param>
        private async Task OnEditClick(ResourceDto resource)
        {
            var result = await drawerService.CreateDialogAsync<ResourceEdit, ResourceEditOption, bool>(
                   new ResourceEditOption() { Type = 1, SelectedResourceId = resource.Id },
                   true,
                   title: "编辑",
                   width: 400,
                   placement: "right");
            if (result)
            {
                var newEntity=await resourceService.Get(resource.Id);
                newEntity.Adapt(resource);
            }
        }
        /// <summary>
        /// 点击添加按钮
        /// </summary>
        private async Task OnAddClick()
        {
            var result = await drawerService.CreateDialogAsync<ResourceEdit, ResourceEditOption, bool>(
                   new ResourceEditOption() { Type = 0, SelectedResourceId = Guid.Empty },
                   true,
                   title: "添加",
                   width: 400,
                   placement: "right");
            if (result) { await ReLoadTable(); }
        }
        
        /// <summary>
        /// 点击删除选中按钮
        /// </summary>
        private async Task OnDeletesClick()
        {
            if (_selectedRows == null || _selectedRows.Count() == 0)
            {
                messageService.Warn("未选中任何行");
            }
            else
            {
                if (await confirmService.YesNoDelete() == ConfirmResult.Yes)
                {
                    List<Guid> ids = new List<Guid>();
                    ids.AddRange(_selectedRows.Select(x => x.Id).ToArray());
                    _selectedRows.ForEach(x => { GetTreeAllChildrenNodes(x, ids); });
                    var result = await resourceService.FakeDeletes(ids.Distinct().ToArray());
                    if (result)
                    {
                        messageService.Success("删除成功");
                        await ReLoadTable();
                    }
                    else
                    {
                        messageService.Error($"删除失败");
                    }
                    //await InvokeAsync(StateHasChanged);
                }
            }
        }
        /// <summary>
        /// 点击锁定按钮
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isLocked"></param>
        private async Task OnChangeIsLocked(ResourceDto model, bool isLocked)
        {
            Task.Run(async () =>
            {
                var result = await resourceService.Lock(model.Id, isLocked);
                if (!result)
                {
                    model.IsLocked = !isLocked;
                    messageService.Error("锁定失败");
                }
            });
        }

        /// <summary>
        /// 点击展示关联接口
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task OnShowFunctionClick(Guid id)
        {
            var result = await drawerService.CreateDialogAsync<ResourceFunctionEdit, ResourceFunctionEditOption, bool>(
                      new ResourceFunctionEditOption { Id=id,Type=0},
                      true,
                      title: "关联接口",
                      width: 1200,
                      placement: "right");
        }
    }
}
