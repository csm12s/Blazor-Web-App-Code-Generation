// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using AntDesign.TableModels;
using Gardener.Base;
using Gardener.Client.Base.Model;
using Mapster;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client.Base.Components
{
    /// <summary>
    /// 树形table基类
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TDrawer"></typeparam>
    /// <typeparam name="TDrawerOption"></typeparam>
    /// <typeparam name="TDrawerResult"></typeparam>
    public abstract class TreeTableBase<TDto, TKey, TDrawer, TDrawerOption, TDrawerResult> : ReuseTabsPageBase where TDrawer : FeedbackComponent<TDrawerOption, TDrawerResult> where TDrawerResult : DrawerOutput<TKey> where TDto : BaseDto<TKey>, new()
    {
       
        protected ITable _table;
        protected bool _tableIsLoading = false;
        protected List<TDto> _dtos;
        protected IEnumerable<TDto> _selectedRows;
        [Inject]
        protected IServiceBase<TDto, TKey> serviceBase { get; set; }
        [Inject]
        protected MessageService messageService { get; set; }
        [Inject]
        protected ConfirmService confirmService { get; set; }
        [Inject]
        protected DrawerService drawerService { get; set; }
        [Inject]
        protected IClientLocalizer localizer { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract Task<List<TDto>> GetTree();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract ICollection<TDto> GetChildren(TDto dto);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract void SetChildren(TDto dto, ICollection<TDto> children);
        /// <summary>
        /// 获取主键
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected TKey GetKey(TDto dto) 
        {
            return dto.Id;
        }
        /// <summary>
        /// 获取父级主键
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected abstract TKey GetParentKey(TDto dto);
        /// <summary>
        /// 根据<TDto>获取查看时传入抽屉的数据项<TEditOption>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected abstract TDrawerOption GetSelectOption(TDto dto);
        /// <summary>
        /// 根据<TDto>获取编辑时传入抽屉的数据项<TEditOption>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected abstract TDrawerOption GetEditOption(TDto dto);
        /// <summary>
        /// 根据<TDto>获取添加时传入抽屉的数据项<TEditOption>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected abstract TDrawerOption GetAddOption(TDto dto);
        // <summary>
        /// 获取添加时传入抽屉的数据项<TEditOption>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected abstract TDrawerOption GetAddOption();
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="children"></param>
        /// <returns></returns>
        protected abstract ICollection<TDto> SortChildren(ICollection<TDto> children);
        /// <summary>
        /// 抽屉配置
        /// </summary>
        /// <returns></returns>
        protected virtual DrawerSettings GetDrawerSettings()
        {
            return new DrawerSettings { Width = 500 };
        }
        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <returns></returns>
        protected async Task ReLoadTable()
        {
            _tableIsLoading = true;
            _dtos = await GetTree();
            if (_dtos == null)
            {
                messageService.Error("加载失败");
            }
            _tableIsLoading = false;
        }
        /// <summary>
        /// 刷新页面
        /// </summary>
        /// <returns></returns>
        protected async Task OnReLoadTable()
        {
            await ReLoadTable();
        }
        /// <summary>
        /// 查询变化
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        protected async Task onChange(QueryModel<TDto> queryModel)
        {
            await ReLoadTable();
        }
        /// <summary>
        /// 点击删除按钮
        /// </summary>
        /// <param name="id"></param>
        protected async Task OnClickDelete(TDto dto)
        {
            if (await confirmService.YesNoDelete() == ConfirmResult.Yes)
            {
                //找到所有子集
                List<TKey> ids = new List<TKey>() { GetKey(dto) };
                GetTreeAllChildrenNodes(dto, ids);
                var result = await serviceBase.FakeDeletes(ids.ToArray());
                if (result)
                {
                    messageService.Success("删除成功");
                    if (DeleteTreeNode(GetParentKey(dto), GetKey(dto), _dtos))
                    {
                        await InvokeAsync(StateHasChanged);

                    }
                    else 
                    {
                        await ReLoadTable();
                    }
                }
                else
                {
                    messageService.Error("删除失败");
                }
            }
        }

        /// <summary>
        /// 点击编辑按钮
        /// </summary>
        /// <param name="roleDto"></param>
        protected async Task OnClickEdit(TDto dto)
        {
            TDrawerOption option = GetEditOption(dto);
            DrawerSettings settings = GetDrawerSettings();
            var result = await drawerService.CreateDialogAsync<TDrawer, TDrawerOption, TDrawerResult>(
                   option,
                   settings.Closable,
                   title: localizer["编辑"],
                   width: settings.Width,
                   placement: settings.Placement.ToString().ToLower());
            if (result.Succeeded)
            {
                //最新的数据
                var newEntity = await serviceBase.Get(GetKey(dto));
                ICollection<TDto> children = GetChildren(dto);
                //重新赋值给界面对象
                newEntity.Adapt(dto);
                //子集也重新赋值给他
                SetChildren(dto, children);
                //给子集重新排队
                _dtos.ForEach(x =>
                {
                    var p = GetNodeFromTree(GetParentKey(x), x);
                    if (p != null)
                    {
                        ICollection<TDto> children = GetChildren(x);
                        if (children != null)
                        {
                            SetChildren(p, SortChildren(children));
                        }
                    }
                });

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="isLocked"></param>
        protected void SetIsLocked(TDto dto, bool isLocked) 
        {
            dto.IsLocked = isLocked;
        }
        /// <summary>
        /// 点击锁定按钮
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isLocked"></param>
        protected async Task OnChangeIsLocked(TDto dto, bool isLocked)
        {
            Task.Run(async () =>
            {
                var result = await serviceBase.Lock(GetKey(dto), isLocked);
                if (!result)
                {
                    SetIsLocked(dto, !isLocked);
                    messageService.Error((isLocked ? "锁定" : "解锁") + "失败");
                }
                else
                {
                    SetIsLocked(dto, isLocked);
                }
            });
        }

        /// <summary>
        /// 点击添加按钮
        /// </summary>
        protected async Task OnClickAdd()
        {
            DrawerSettings settings = GetDrawerSettings();
            var result = await drawerService.CreateDialogAsync<TDrawer, TDrawerOption, TDrawerResult>(
                  GetAddOption(),
                   settings.Closable,
                   title: localizer["添加"],
                   width: settings.Width,
                   placement: settings.Placement.ToString().ToLower());
            if (result.Succeeded) 
            {
                //最新的数据
                await ReLoadTable();
            }
        }
        /// <summary>
        /// 点击添加子项按钮
        /// </summary>
        /// <param name="dto">点击的项</param>
        /// <returns></returns>
        protected async Task OnClickAddChildren(TDto dto)
        {
            DrawerSettings settings = GetDrawerSettings();
            var result = await drawerService.CreateDialogAsync<TDrawer, TDrawerOption, TDrawerResult>(
                   GetAddOption(dto),
                   settings.Closable,
                   title: localizer["添加"],
                   width: settings.Width,
                   placement: settings.Placement.ToString().ToLower());
            if (result.Succeeded)
            {
                //最新的数据
                var newEntity = await serviceBase.Get(result.Id);
                ICollection<TDto> children = GetChildren(dto) ?? new List<TDto>();

                children.Add(newEntity);
                SetChildren(dto, SortChildren(children));
            }
        }
        /// <summary>
        /// 点击删除选中按钮
        /// </summary>
        protected async Task OnClickDeletes()
        {
            if (_selectedRows == null || _selectedRows.Count() == 0)
            {
                messageService.Warn("未选中任何行");
            }
            else
            {
                if (await confirmService.YesNoDelete() == ConfirmResult.Yes)
                {
                    List<TKey> ids = new List<TKey>();
                    ids.AddRange(_selectedRows.Select(x => GetKey(x)).ToArray());
                    _selectedRows.ForEach(x => { GetTreeAllChildrenNodes(x, ids); });
                    var result = await serviceBase.FakeDeletes(ids.Distinct().ToArray());
                    if (result)
                    {
                        messageService.Success("删除成功");
                        await ReLoadTable();
                    }
                    else
                    {
                        messageService.Error($"删除失败");
                    }
                }
            }
        }

        /// <summary>
        /// 点击编辑按钮
        /// </summary>
        /// <param name="roleDto"></param>
        protected async Task OnClickDetail(TDto dto)
        {
            TDrawerOption option = GetSelectOption(dto);
            DrawerSettings settings = GetDrawerSettings();
            var result = await drawerService.CreateDialogAsync<TDrawer, TDrawerOption, TDrawerResult>(
                   option,
                   settings.Closable,
                   title: localizer["详情"],
                   width: settings.Width,
                   placement: settings.Placement.ToString().ToLower());
            if (result.Succeeded) 
            {
                await ReLoadTable();
            }
        }

        #region tree tool
        /// <summary>
        /// 获取所有子集节点id
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="ids"></param>
        private void GetTreeAllChildrenNodes(TDto dto, List<TKey> ids)
        {
            var children = GetChildren(dto);
            if (children != null)
            {
                children.ForEach(x =>
                {
                    ids.Add(GetKey(x));
                    GetTreeAllChildrenNodes(x, ids);
                });
            }


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="id"></param>
        /// <param name="resourceDtos"></param>
        private bool DeleteTreeNode(TKey pId, TKey id, ICollection<TDto> items)
        {
            foreach (TDto dto in items)
            {
                var children = GetChildren(dto);
                if (GetKey(dto).Equals(pId))
                {
                    children = children.Where(x => !GetKey(x).Equals(id)).ToList();
                    SetChildren(dto, children);
                    return true;
                }
                if (children != null)
                {
                    if (DeleteTreeNode(pId, id, children))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 从树种找到节点
        /// </summary>
        /// <param name="id"></param>
        /// <param name="resource"></param>
        /// <returns></returns>
        private TDto GetNodeFromTree(TKey id, TDto dto)
        {
            if (GetKey(dto).Equals(id))
            {
                return dto;
            }
            else
            {
                if (GetChildren(dto) != null)
                {
                    foreach (TDto item in GetChildren(dto))
                    {
                        var node = GetNodeFromTree(id, item);
                        if (node != null)
                        {
                            return node;
                        }
                    }
                }
            }
            return default(TDto);
        }

        
        #endregion
    }
    /// <summary>
    /// 树形table基类
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TDrawer"></typeparam>
    public abstract class TreeTableBase<TDto, TKey, TDrawer> : TreeTableBase<TDto, TKey,TDrawer,DrawerInput<TKey>,DrawerOutput<TKey>> where TDrawer : FeedbackComponent<DrawerInput<TKey>, DrawerOutput<TKey>> where TDto : BaseDto<TKey>, new()
    {
        /// <summary>
        /// 根据<TDto>获取查看时传入抽屉的数据项<TEditOption>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected override DrawerInput<TKey> GetSelectOption(TDto dto)
        {
            return DrawerInput<TKey>.IsSelect(dto.Id);
        }
        /// <summary>
        /// 根据<TDto>获取编辑时传入抽屉的数据项<TEditOption>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected override DrawerInput<TKey> GetEditOption(TDto dto)
        {
            return DrawerInput<TKey>.IsEdit(dto.Id);
        }
        /// <summary>
        /// 根据<TDto>获取添加时传入抽屉的数据项<TEditOption>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected override DrawerInput<TKey> GetAddOption(TDto dto)
        {
            return DrawerInput<TKey>.IsAdd(dto.Id);
        }
        // <summary>
        /// 获取添加时传入抽屉的数据项<TEditOption>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected override DrawerInput<TKey> GetAddOption()
        {
            return DrawerInput<TKey>.IsAdd();
        }
    }

   
}
