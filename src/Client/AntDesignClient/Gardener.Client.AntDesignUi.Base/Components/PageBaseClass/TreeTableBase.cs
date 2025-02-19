﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using AntDesign.TableModels;
using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Components.PageBaseClass;
using Gardener.Client.AntDesignUi.Base.Constants;
using Gardener.Client.Base;
using Mapster;

namespace Gardener.Client.AntDesignUi.Base.Components
{
    /// <summary>
    /// 树形table基类-支持多租户
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TOperationDialog"></typeparam>
    /// <typeparam name="TDialogInput"></typeparam>
    /// <typeparam name="TDialogOutput"></typeparam>
    /// <typeparam name="TLocalResource"></typeparam>
    public abstract class TreeTableBase<TDto, TKey, TOperationDialog, TDialogInput, TDialogOutput, TLocalResource> : MultiTenantTableBase<TDto, TKey, TLocalResource>
        where TOperationDialog : OperationDialogBase<TDialogInput, TDialogOutput, TLocalResource>
        where TDto : class, new()
        where TKey : notnull
    {

        #region abstract mothed
        /// <summary>
        /// 获取树
        /// </summary>
        /// <returns></returns>
        protected abstract Task<List<TDto>> GetTree();
        /// <summary>
        /// 获取子集
        /// </summary>
        /// <returns></returns>
        protected abstract ICollection<TDto>? GetChildren(TDto dto);
        /// <summary>
        /// 设置子集
        /// </summary>
        /// <returns></returns>
        protected abstract void SetChildren(TDto dto, ICollection<TDto>? children);
        /// <summary>
        /// 获取父级主键
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected abstract TKey? GetParentKey(TDto dto);
        /// <summary>
        /// 根据<TDto>获取查看时传入抽屉的数据项<TEditOption>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected abstract TDialogInput GetSelectOption(TDto dto);
        /// <summary>
        /// 根据<TDto>获取编辑时传入抽屉的数据项<TEditOption>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected abstract TDialogInput GetEditOption(TDto dto);
        /// <summary>
        /// 根据<TDto>获取添加时传入抽屉的数据项<TEditOption>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected abstract TDialogInput GetAddOption(TDto dto);
        // <summary>
        /// 获取添加时传入抽屉的数据项<TEditOption>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected abstract TDialogInput GetAddOption();
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="children"></param>
        /// <returns></returns>
        protected abstract ICollection<TDto>? SortChildren(ICollection<TDto>? children);
        /// <summary>
        /// 当编辑结束
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="dialogOutput"></param>
        protected abstract Task OnEditFinish(TDto dto, TDialogOutput? dialogOutput);
        /// <summary>
        /// 当添加结束
        /// </summary>
        /// <param name="dialogOutput"></param>
        protected abstract Task OnAddFinish(TDialogOutput? dialogOutput);
        /// <summary>
        /// 当添加子节点结束
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="dialogOutput"></param>
        protected abstract Task OnAddChildrenFinish(TDto dto, TDialogOutput? dialogOutput);

        #endregion

        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <param name="forceRender">是否强制渲染</param>
        /// <returns></returns>
        protected async Task ReLoadTable(bool forceRender = false)
        {
            StartTableLoading(forceRender);
            var _tempDatas = await GetTree();
            if (_tempDatas == null)
            {
                MessageService.Error(this.Localizer.Combination(nameof(SharedLocalResource.Load), nameof(SharedLocalResource.Fail)));
            }
            else
            {
                if (_tempDatas.Any())
                {
                    if (typeof(TDto).IsAssignableTo(typeof(IModelTenant)))
                    {
                        foreach (TDto dto in _tempDatas)
                        {
                            SetTenant(dto);
                        }
                    }
                }
                PageListDataHadnle(_tempDatas);
                _datas = _tempDatas;
            }
            StopTableLoading(forceRender);
        }
        /// <summary>
        /// 设置租户
        /// </summary>
        /// <param name="dto"></param>
        protected void SetTenant(TDto dto)
        {
            RecursionTree(dto, item =>
            {
                if (item is IModelTenant modelTenant)
                {
                    modelTenant.Tenant = GetTenant(modelTenant.TenantId);
                }
            });
        }

        /// <summary>
        /// 刷新页面
        /// </summary>
        /// <returns></returns>
        protected Task OnReLoadTable()
        {
            return ReLoadTable();
        }

        /// <summary>
        /// 查询变化
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        protected Task onChange(QueryModel<TDto> queryModel)
        {
            return ReLoadTable();
        }

        /// <summary>
        /// 列表接口返回后，对页面列表数据进行处理
        /// </summary>
        /// <param name="datas"></param>
        protected virtual void PageListDataHadnle(IEnumerable<TDto> datas)
        {
        }

        #region delete
        /// <summary>
        /// 点击删除按钮
        /// </summary>
        /// <param name="id"></param>
        protected Task OnClickDelete(TDto dto)
        {
            return OnClickDelete(dto, ClientConstant.TableListDeleteUseTrueDelete);
        }

        /// <summary>
        /// 点击删除按钮
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trueDelete">真实删除</param>
        protected async Task OnClickDelete(TDto dto, bool trueDelete)
        {
            if (await ConfirmService.YesNoDelete() == ConfirmResult.Yes)
            {
                //找到所有子集
                List<TKey> ids = new List<TKey>() { GetKey(dto) };
                GetTreeAllChildrenNodes(dto, ids);
                var result = trueDelete ? await BaseService.Deletes(ids.ToArray()) : await BaseService.FakeDeletes(ids.ToArray());
                if (result)
                {
                    MessageService.Success(this.Localizer.Combination(nameof(SharedLocalResource.Delete), nameof(SharedLocalResource.Success)));
                    var pKey = GetParentKey(dto);
                    if (_datas != null && pKey != null && DeleteTreeNode(pKey, GetKey(dto), _datas))
                    {
                        await RefreshPageDom();
                    }
                    else
                    {
                        await ReLoadTable(true);
                    }
                }
                else
                {
                    MessageService.Error(this.Localizer.Combination(nameof(SharedLocalResource.Delete), nameof(SharedLocalResource.Fail)));
                }
            }
        }

        /// <summary>
        /// 点击删除选中按钮
        /// </summary>
        /// <param name="trueDelete">真实删除</param>
        protected async Task OnClickDeletes(bool trueDelete)
        {
            if (_selectedRows == null || _selectedRows.Count() == 0)
            {
                MessageService.Warn(Localizer[nameof(SharedLocalResource.NoRowsAreSelected)]);
            }
            else
            {
                if (await ConfirmService.YesNoDelete() == ConfirmResult.Yes)
                {
                    List<TKey> ids = new List<TKey>();
                    ids.AddRange(_selectedRows.Select(x => GetKey(x)).ToArray());
                    _selectedRows.ForEach(x => { GetTreeAllChildrenNodes(x, ids); });
                    var result = trueDelete ? await BaseService.Deletes(ids.Distinct().ToArray()) : await BaseService.FakeDeletes(ids.Distinct().ToArray());
                    if (result)
                    {
                        MessageService.Success(this.Localizer.Combination(nameof(SharedLocalResource.Delete), nameof(SharedLocalResource.Success)));
                        await ReLoadTable(true);
                    }
                    else
                    {
                        MessageService.Error(this.Localizer.Combination(nameof(SharedLocalResource.Delete), nameof(SharedLocalResource.Fail)));
                    }
                }
            }
        }

        /// <summary>
        /// 点击删除选中按钮
        /// </summary>
        protected Task OnClickDeletes()
        {
            return OnClickDeletes(ClientConstant.TableListDeleteUseTrueDelete);
        }
        #endregion

        /// <summary>
        /// 点击编辑按钮
        /// </summary>
        /// <param name="roleDto"></param>
        protected Task OnClickEdit(TDto dto)
        {
            TDialogInput option = GetEditOption(dto);
            return OpenOperationDialogAsync(Localizer[nameof(SharedLocalResource.Edit)],
                option,
                 result =>
                {
                    return OnEditFinish(dto, result);
                });
        }

        /// <summary>
        /// 点击添加按钮
        /// </summary>
        protected Task OnClickAdd()
        {
            return OpenOperationDialogAsync(Localizer[nameof(SharedLocalResource.Add)],
                GetAddOption(),
                result =>
                {
                    return OnAddFinish(result);
                });
        }

        /// <summary>
        /// 点击添加子项按钮
        /// </summary>
        /// <param name="dto">点击的项</param>
        /// <returns></returns>
        protected Task OnClickAddChildren(TDto dto)
        {
            return OpenOperationDialogAsync(Localizer[nameof(SharedLocalResource.Add)],
                GetAddOption(dto),
                 result =>
                {
                    return OnAddChildrenFinish(dto, result);
                });
        }


        /// <summary>
        /// 点击编辑按钮
        /// </summary>
        /// <param name="roleDto"></param>
        protected Task OnClickDetail(TDto dto)
        {
            TDialogInput option = GetSelectOption(dto);
            return OpenOperationDialogAsync(Localizer[nameof(SharedLocalResource.Detail)], option);
        }

        /// <summary>
        /// 打开操作对话框
        /// </summary>
        /// <param name="title"></param>
        /// <param name="input"></param>
        /// <param name="onClose"></param>
        /// <param name="operationDialogSettings "></param>
        /// <returns></returns>
        private Task OpenOperationDialogAsync(string title, TDialogInput input, Func<TDialogOutput?, Task>? onClose = null, OperationDialogSettings? operationDialogSettings = null)
        {
            return OpenOperationDialogAsync<TOperationDialog, TDialogInput, TDialogOutput>(title, input, onClose, operationDialogSettings);
        }

        #region tree tool
        /// <summary>
        /// 递归遍历树执行<see cref="Action{TDto}"/>
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="action"></param>
        protected void RecursionTree(TDto dto, Action<TDto> action)
        {
            action(dto);
            ICollection<TDto>? list = GetChildren(dto);
            if (list != null)
            {
                foreach (TDto item in list)
                {
                    RecursionTree(item, action);
                }
            }
        }
        /// <summary>
        /// 获取所有子集节点id
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="ids"></param>
        protected void GetTreeAllChildrenNodes(TDto dto, List<TKey> ids)
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
        protected bool DeleteTreeNode(TKey pId, TKey id, IEnumerable<TDto> items)
        {
            foreach (TDto dto in items)
            {
                var children = GetChildren(dto);
                children = children ?? new List<TDto>();
                TKey key = GetKey(dto);
                if (key.Equals(pId))
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
        /// 从树中找到节点
        /// </summary>
        /// <param name="id"></param>
        /// <param name="resource"></param>
        /// <returns></returns>
        protected TDto? GetNodeFromTree(TKey id, TDto dto)
        {
            var key = GetKey(dto);
            if (key != null && key.Equals(id))
            {
                return dto;
            }
            else
            {
                var children = GetChildren(dto);
                if (children != null)
                {
                    foreach (TDto item in children)
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
    /// 树形table基类-支持多租户
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TOperationDialog"></typeparam>
    /// <typeparam name="TDialogInput"></typeparam>
    /// <typeparam name="TDialogOutput"></typeparam>
    public abstract class TreeTableBase<TDto, TKey, TOperationDialog, TDialogInput, TDialogOutput> : TreeTableBase<TDto, TKey, TOperationDialog, TDialogInput, TDialogOutput, SharedLocalResource>
        where TOperationDialog : OperationDialogBase<TDialogInput, TDialogOutput, SharedLocalResource>
        where TDto : class, new()
        where TKey : notnull
    { }
    /// <summary>
    /// 树形table基类-支持多租户
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TOperationDialog"></typeparam>
    /// <typeparam name="TLocalResource"></typeparam>
    public abstract class TreeTableBase<TDto, TKey, TOperationDialog, TLocalResource> : TreeTableBase<TDto, TKey, TOperationDialog, OperationDialogInput<TKey>, OperationDialogOutput<TKey>, TLocalResource>
        where TOperationDialog : OperationDialogBase<OperationDialogInput<TKey>, OperationDialogOutput<TKey>, TLocalResource>
        where TDto : class, new() where TKey : notnull
    {
        /// <summary>
        /// 根据<TDto>获取查看时传入抽屉的数据项<TEditOption>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected override OperationDialogInput<TKey> GetSelectOption(TDto dto)
        {
            return OperationDialogInput<TKey>.Select(GetKey(dto));
        }
        /// <summary>
        /// 根据<TDto>获取编辑时传入抽屉的数据项<TEditOption>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected override OperationDialogInput<TKey> GetEditOption(TDto dto)
        {
            return OperationDialogInput<TKey>.Edit(GetKey(dto));
        }
        /// <summary>
        /// 根据<TDto>获取添加时传入抽屉的数据项<TEditOption>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected override OperationDialogInput<TKey> GetAddOption(TDto dto)
        {
            return OperationDialogInput<TKey>.Add(GetKey(dto));
        }
        // <summary>
        /// 获取添加时传入抽屉的数据项<TEditOption>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected override OperationDialogInput<TKey> GetAddOption()
        {
            return OperationDialogInput<TKey>.Add();
        }
        /// <summary>
        /// 当编辑结束
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="dialogOutput"></param>
        /// <returns></returns>
        protected override async Task OnEditFinish(TDto dto, OperationDialogOutput<TKey>? dialogOutput)
        {
            if (dialogOutput != null && dialogOutput.Succeeded)
            {
                //最新的数据
                var newEntity = await BaseService.Get(GetKey(dto));
                //父级变化重新加载列表
                if (newEntity != null)
                {
                    var pKey = GetParentKey(newEntity);
                    if (pKey != null && !pKey.Equals(GetParentKey(dto)))
                    {
                        //最新的数据
                        await ReLoadTable(true);
                        return;
                    }
                }
                //父级未变化直接变化本地对象
                ICollection<TDto>? children = GetChildren(dto);
                //重新赋值给界面对象
                newEntity.Adapt(dto);
                //设置租户
                SetTenant(dto);
                if (children != null)
                {
                    //子集也重新赋值给他
                    SetChildren(dto, children);
                }
                //给子集重新排队
                _datas.ForEach(x =>
                {
                    var pKey = GetParentKey(x);
                    if (pKey != null)
                    {
                        var p = GetNodeFromTree(pKey, x);
                        if (p != null)
                        {
                            ICollection<TDto>? children = GetChildren(x);
                            if (children != null)
                            {
                                SetChildren(p, SortChildren(children));
                            }
                        }
                    }

                });
                await RefreshPageDom();
            }
        }

        /// <summary>
        /// 当添加结束
        /// </summary>
        /// <param name="dialogOutput"></param>
        /// <returns></returns>
        protected override Task OnAddFinish(OperationDialogOutput<TKey>? dialogOutput)
        {
            if (dialogOutput != null && dialogOutput.Succeeded)
            {
                //最新的数据
                return ReLoadTable(true);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// 当添加子节点结束 
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="dialogOutput"></param>
        /// <returns></returns>
        protected override async Task OnAddChildrenFinish(TDto dto, OperationDialogOutput<TKey>? dialogOutput)
        {
            if (dialogOutput != null && dialogOutput.Succeeded && dialogOutput.Data != null)
            {
                //最新的数据
                var newEntity = await BaseService.Get(dialogOutput.Data);
                ICollection<TDto> children = GetChildren(dto) ?? new List<TDto>();
                //设置租户
                SetTenant(newEntity);
                children.Add(newEntity);
                SetChildren(dto, SortChildren(children));

                await RefreshPageDom();
            }
        }
    }
    /// <summary>
    /// 树形table基类-支持多租户
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TOperationDialog"></typeparam>
    public abstract class TreeTableBase<TDto, TKey, TOperationDialog> : TreeTableBase<TDto, TKey, TOperationDialog, SharedLocalResource> where TOperationDialog : OperationDialogBase<OperationDialogInput<TKey>, OperationDialogOutput<TKey>, SharedLocalResource> where TDto : class, new() where TKey : notnull
    { }
}
