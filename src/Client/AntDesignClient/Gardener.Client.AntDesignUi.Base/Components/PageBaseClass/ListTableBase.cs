// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using AntDesign.TableModels;
using Gardener.Attributes;
using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Constants;
using Gardener.Client.Base;
using Gardener.Common;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System.Reflection;

namespace Gardener.Client.AntDesignUi.Base.Components
{
    /// <summary>
    /// table列表基类(可以被当作OperationDialog打开)
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TLocalResource">本地化资源</typeparam>
    /// <typeparam name="TSelfOperationDialogInput">自身作为OperationDialog接收的参数</typeparam>
    /// <typeparam name="TSelfOperationDialogOutput">自身作为OperationDialog返回的参数</typeparam>
    /// <remarks>
    /// 包含列表加载、删除、导出、种子数据
    /// </remarks>
    public abstract class ListTableBase<TDto, TKey, TLocalResource, TSelfOperationDialogInput, TSelfOperationDialogOutput> : TableBase<TDto, TKey, TLocalResource, TSelfOperationDialogInput, TSelfOperationDialogOutput> where TDto : class, new() where TLocalResource : SharedLocalResource
    {
        /// <summary>
        /// 显示总数
        /// </summary>
        protected int _total = 0;
        /// <summary>
        /// 控制分页页码
        /// </summary>
        protected int _pageIndex = 1;
        /// <summary>
        /// 控制分页每页数量
        /// </summary>
        protected int _pageSize = ClientConstant.pageSize;

 

        #region services
        /// <summary>
        /// 确认提示服务
        /// </summary>
        [Inject]
        protected ConfirmService ConfirmService { get; set; } = null!;
        /// <summary>
        /// 路由导航服务
        /// </summary>
        [Inject]
        protected NavigationManager Navigation { get; set; } = null!;
        /// <summary>
        /// javascript 工具
        /// </summary>
        [Inject]
        protected IJsTool JsTool { get; set; } = null!;

        #endregion

        #region override mothed
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        protected override Task OnInitializedAsync()
        {
            //从url加载TableSearch参数
            var url = new Uri(Navigation.Uri);
            var query = url.Query;
            Dictionary<string, StringValues> urlParams = QueryHelpers.ParseQuery(query);
            if (urlParams != null && urlParams.Count() > 0)
            {
                urlParams.ForEach(x =>
                {
                    _defaultSearchValue.Add(x.Key, x.Value.ToString());
                });
            }
            //table search 组件提供搜索条件
            _filterGroupProviders.Add(GetTableSearchFilterGroups);
            //租户不需要租户编号搜索
            if (AuthenticationStateManager.CurrentUserIsTenant() && typeof(TDto).IsAssignableTo(typeof(IModelTenantId)))
            {
                this.AddExcludeSearchFields(nameof(IModelTenantId.TenantId));
            }
            return base.OnInitializedAsync();
        }

        /// <summary>
        /// 首次渲染后
        /// </summary>
        bool firstRenderAfter = false;

        /// <summary>
        /// 组件首次渲染后
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnFirstAfterRenderAsync()
        {
            this.firstRenderAfter = true;
            await ReLoadTable();
            await base.OnFirstAfterRenderAsync();
        }

        #endregion

        /// <summary>
        /// 在构建完成前配置搜索请求-基类可以重写
        /// </summary>
        /// <param name="pageRequest"></param>
        /// <returns></returns>
        protected virtual void ConfigurationPageRequest(PageRequest pageRequest)
        {
            //set pageRequest
        }
        /// <summary>
        /// 添加需要排除的字段
        /// </summary>
        /// <param name="fields"></param>
        protected void AddExcludeSearchFields(params string[] fields) 
        {
            foreach(string  field in fields)
            {
                if (!_excludeSearchFields.Contains(field))
                {
                    _excludeSearchFields.Add(field);
                }
            }
        }
        /// <summary>
        /// 重置请求参数
        /// </summary>
        /// <returns></returns>
        protected virtual PageRequest GetPageRequest()
        {
            PageRequest pageRequest = _table?.GetPageRequest() ?? new PageRequest();
            //如果有搜索条件 就拼接上
            if (_filterGroupProviders != null && _filterGroupProviders.Any())
            {
                _filterGroupProviders.ForEach(p =>
                {
                    var items = p.Invoke();
                    if (items != null)
                    {
                        pageRequest.FilterGroups.AddRange(items);
                    }
                });
            }
            ConfigurationPageRequest(pageRequest);
            return pageRequest;
        }

        /// <summary>
        /// 重新加载table, 删除整页，且是最后一页
        /// </summary>
        /// <returns></returns>
        protected virtual Task ReLoadTableAfterDeleteLastPage()
        {
            //删除整页，且是最后一页
            if (_selectedRows?.Count() == _pageSize && _pageIndex * _pageSize >= _total)
            {
                return ReLoadTable(true);
            }
            else
            {
                return ReLoadTable(false);
            }
        }

        /// <summary>
        /// 重新加载table
        /// Todo: 是不是可以叫 ReloadTable
        /// </summary>
        /// <returns></returns>
        protected virtual Task ReLoadTable()
        {
            return ReLoadTable(false);
        }

        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <param name="firstPage">是否从首页加载</param>
        /// <returns></returns>
        protected virtual Task ReLoadTable(bool firstPage)
        {
            if (firstPage && _pageIndex > 1)
            {
                _pageIndex = 1;
                return Task.CompletedTask;
            }
            else
            {
                PageRequest pageRequest = GetPageRequest();
                return ReLoadTable(pageRequest);
            }

        }

        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <param name="firstPage">是否从首页加载</param>
        /// <returns></returns>
        protected virtual async Task ReLoadTable(PageRequest pageRequest)
        {
            StartLoading();
            var pagedListResult = await BaseService.Search(pageRequest);
            if (pagedListResult != null)
            {
                var pagedList = pagedListResult;
                _datas = pagedList.Items ?? new List<TDto>(0);
                //如果有租户数据，装配一下
                if (typeof(TDto).IsAssignableTo(typeof(IModelTenant))) 
                {
                    foreach(TDto item  in _datas)
                    {
                        if (item is IModelTenant modelTenant) 
                        {
                            modelTenant.Tenant = GetTenant(modelTenant.TenantId);
                        }
                    }
                }
                _total = pagedList.TotalCount;
            }
            else
            {
                MessageService.Error(Localizer.Combination(SharedLocalResource.Load, SharedLocalResource.Fail));
            }
            StopLoading();

        }

        /// <summary>
        /// table查询变化
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        protected virtual Task OnChange(QueryModel<TDto> queryModel)
        {
            if (firstRenderAfter)
            {
                return ReLoadTable();
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 点击删除按钮(FakeDelete)
        /// </summary>
        /// <param name="id"></param>
        protected virtual async Task OnClickDelete(TKey id)
        {
            if (await ConfirmService.YesNoDelete() == ConfirmResult.Yes)
            {
                var result = await BaseService.FakeDelete(id);
                if (result)
                {
                    PageRequest pageRequest = GetPageRequest();
                    //当前页被删完了
                    if (_pageIndex > 1 && _datas?.Count() == 0)
                    {
                        _pageIndex = _pageIndex - 1;
                    }
                    MessageService.Success(Localizer.Combination(SharedLocalResource.Delete, SharedLocalResource.Success));
                    await ReLoadTable();
                }
                else
                {
                    MessageService.Error(Localizer.Combination(SharedLocalResource.Delete, SharedLocalResource.Fail));
                }
            }

        }

        /// <summary>
        /// 点击删除选中按钮
        /// </summary>
        protected virtual async Task OnClickDeletes()
        {
            if (_selectedRows == null || _selectedRows.Count() == 0)
            {
                MessageService.Warn(Localizer[SharedLocalResource.NoRowsAreSelected]);
            }
            else
            {
                _deletesBtnLoading = true;
                if (await ConfirmService.YesNoDelete() == ConfirmResult.Yes)
                {
                    var result = await BaseService.FakeDeletes(_selectedRows.Select(x => GetKey(x)).ToArray());
                    if (result)
                    {
                        MessageService.Success(Localizer.Combination(SharedLocalResource.Delete, SharedLocalResource.Success));
                        //删除整页，且是最后一页
                        if (_selectedRows.Count() == _pageSize && _pageIndex * _pageSize >= _total)
                        {
                            await ReLoadTable(true);
                        }
                        else
                        {
                            await ReLoadTable(false);
                        }
                    }
                    else
                    {
                        MessageService.Error(Localizer.Combination(SharedLocalResource.Delete, SharedLocalResource.Fail));
                    }
                }

                _deletesBtnLoading = false;
            }
        }

        #region True Delete
        protected virtual async Task OnClickTrueDelete(TKey id)
        {
            if (await ConfirmService.YesNoDelete() == ConfirmResult.Yes)
            {
                var result = await BaseService.Delete(id);
                if (result)
                {
                    MessageService.Success(Localizer.Combination(SharedLocalResource.Delete, SharedLocalResource.Success));
                    PageRequest pageRequest = GetPageRequest();
                    //当前页被删完了
                    if (_pageIndex > 1 && _datas?.Count() == 0)
                    {
                        _pageIndex = _pageIndex - 1;
                    }
                    await ReLoadTable();
                }
                else
                {
                    MessageService.Error(Localizer.Combination(SharedLocalResource.Delete, SharedLocalResource.Fail));
                }
            }

        }

        protected virtual async Task OnClickTrueDeletes()
        {
            if (_selectedRows == null || _selectedRows.Count() == 0)
            {
                MessageService.Warn(Localizer[SharedLocalResource.NoRowsAreSelected]);
            }
            else
            {
                _deletesBtnLoading = true;
                if (await ConfirmService.YesNoDelete() == ConfirmResult.Yes)
                {

                    var result = await BaseService.Deletes(_selectedRows.Select(x => GetKey(x)).ToArray());
                    if (result)
                    {
                        MessageService.Success(Localizer.Combination(SharedLocalResource.Delete, SharedLocalResource.Success));

                        //删除整页，且是最后一页
                        if (_selectedRows.Count() == _pageSize && _pageIndex * _pageSize >= _total)
                        {
                            await ReLoadTable(true);
                        }
                        else
                        {
                            await ReLoadTable(false);
                        }
                    }
                    else
                    {
                        MessageService.Error(Localizer.Combination(SharedLocalResource.Delete, SharedLocalResource.Fail));
                    }

                }
                _deletesBtnLoading = false;
            }
        }
        #endregion

        /// <summary>
        /// 种子数据
        /// </summary>
        /// <typeparam name="TShowSeedDataDrawer">展示种子数据抽屉</typeparam>
        /// <returns></returns>
        protected virtual async Task OnClickShowSeedData<TShowSeedDataDrawer>() where TShowSeedDataDrawer : FeedbackComponent<Task<string>, bool>
        {
            PageRequest pageRequest = GetPageRequest();
            pageRequest.PageSize = int.MaxValue;
            pageRequest.PageIndex = 1;
            Task<string> seedData = BaseService.GenerateSeedData(pageRequest);
            OperationDialogSettings drawerSettings = GetOperationDialogSettings();
            drawerSettings.Width = 1300;
            await OpenOperationDialogAsync<TShowSeedDataDrawer, Task<string>, bool>(Localizer[SharedLocalResource.SeedData], seedData, operationDialogSettings: drawerSettings);
        }

        /// <summary>
        /// 种子数据
        /// </summary>
        /// <typeparam name="TShowSeedDataDrawer">展示种子数据抽屉</typeparam>
        /// <returns></returns>
        protected virtual Task OnClickShowSeedData()
        {
            return OnClickShowSeedData<ShowSeedDataCode>();
        }

        /// <summary>
        /// 导出数据
        /// </summary>
        protected virtual async Task OnClickExport()
        {
            _exportDataLoading = true;
            PageRequest pageRequest = GetPageRequest();
            string url = await BaseService.Export(pageRequest);
            await JsTool.Document.DownloadFile(url);
            _exportDataLoading = false;
        }

        protected List<FilterGroup> GetCustomSearchFilterGroups<TSearchDto>(TSearchDto searchDto)
        {
            List<FilterGroup> filterGroups = new List<FilterGroup>();

            Type type = typeof(TSearchDto);
            PropertyInfo[] properties = type.GetProperties();
            foreach (var property in properties)
            {
                Type fieldType = property.PropertyType.GetNonNullableType();
                if (property.GetCustomAttribute<CustomSearchFieldAttribute>() != null
                    && (fieldType.IsPrimitive
                        || fieldType.IsEnum
                        || fieldType.Equals(typeof(string)) // input, select
                        || fieldType.IsEnumerable() // select multiple
                        || fieldType.Equals(typeof(Guid))
                        || fieldType.Equals(typeof(DateTime))
                        || fieldType.Equals(typeof(DateTimeOffset))))
                {
                    string fieldName = property.Name;

                    // string
                    if (fieldType.Equals(typeof(string)))
                    {
                        // Get value
                        var value = searchDto.GetPropertyValue<TSearchDto, string>(property.Name);
                        // Set filter
                        if (!string.IsNullOrEmpty(value))
                        {
                            // Filter group by field
                            // 每一个字段一个 Filter Group
                            FilterGroup filterGroup = new FilterGroup();

                            FilterRule rule = new FilterRule()
                            {
                                Field = fieldName,
                                Value = value.CastTo(fieldType),
                                Operate = FilterOperate.Contains
                            };

                            filterGroup.AddRule(rule);

                            filterGroups.Add(filterGroup);
                        }
                    }
                    else if (fieldType.IsEnumerable())
                    {
                        // Get values
                        var values = searchDto.GetPropertyValue<TSearchDto, IEnumerable<string>>(property.Name);
                        // Set filters
                        if (values != null && values.Any())
                        {
                            // Filter group by field
                            // 每一个字段一个 Filter Group
                            FilterGroup filterGroup = new FilterGroup();

                            foreach (string valueTemp in values)
                            {
                                FilterRule rule = new FilterRule()
                                {
                                    Field = fieldName,
                                    Value = valueTemp.CastTo(typeof(string)), // IEnumerable<string>
                                    Condition = FilterCondition.Or,
                                    Operate = FilterOperate.Contains
                                };
                                filterGroup.AddRule(rule);
                            }

                            filterGroups.Add(filterGroup);
                        }
                    }
                }
            }

            return filterGroups;
        }

        protected virtual Task DoClearSearch()
        {
            // TODO: clear search field
            return ReLoadTable(true);
        }

    }

    /// <summary>
    /// table列表基类(可以被当作OperationDialog打开)
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TLocalResource">本地化资源</typeparam>
    /// <remarks>
    /// 包含列表加载、删除、导出、种子数据
    /// 此基类方便那些不需要弹出或弹出时没有输入输出时使用
    /// 自身作为OperationDialog接收的参数，默认为类型 <see cref="TKey"/>
    /// 自身作为OperationDialog返回的参数，默认为类型 <see cref="bool"/>
    /// </remarks>
    public abstract class ListTableBase<TDto, TKey, TLocalResource> : ListTableBase<TDto, TKey, TLocalResource, TKey, bool>
        where TDto : class, new()
        where TLocalResource : SharedLocalResource
    {

    }

    /// <summary>
    /// 列表table基类
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <remarks>
    /// 包含列表加载、删除、导出、种子数据
    /// 本地化资源 默认使用<see cref="SharedLocalResource"/>
    /// 
    /// 此基类方便那些不需要弹出或弹出时没有输入输出时使用
    /// 自身作为OperationDialog接收的参数，默认为类型 <see cref="TKey"/>
    /// 自身作为OperationDialog返回的参数，默认为类型 <see cref="bool"/>
    /// </remarks>
    public abstract class ListTableBase<TDto, TKey> : ListTableBase<TDto, TKey, SharedLocalResource>
        where TDto : class, new()
    {
    }

    /// <summary>
    /// table列表基类(可以被当作OperationDialog打开，也能快速打开其它操作框)
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TOperationDialog">操作弹框页</typeparam>
    /// <typeparam name="TOperationDialogInput">操作弹框页输入参数</typeparam>
    /// <typeparam name="TOperationDialogOutput">操作弹框页输出参数</typeparam>
    /// <typeparam name="TLocalResource">本地化资源</typeparam>
    /// <typeparam name="TSelfOperationDialogInput">自身作为OperationDialog接收的参数</typeparam>
    /// <typeparam name="TSelfOperationDialogOutput">自身作为OperationDialog返回的参数</typeparam>
    /// <remarks>
    /// 包含列表加载、删除、导出、种子数据、添加、修改、详情
    /// 快递打开操作框，输入 OperationDialogInput_TKey
    /// 快递打开操作框，输出 OperationDialogOutput_TKey
    /// </remarks>
    public abstract class ListOperateTableBase<TDto, TKey, TOperationDialog, TOperationDialogInput, TOperationDialogOutput, TLocalResource, TSelfOperationDialogInput, TSelfOperationDialogOutput> : ListTableBase<TDto, TKey, TLocalResource, TSelfOperationDialogInput, TSelfOperationDialogOutput>
        where TDto : class, new()
        where TOperationDialog : OperationDialogBase<TOperationDialogInput, TOperationDialogOutput, TLocalResource>
        where TLocalResource : SharedLocalResource
        where TOperationDialogInput : OperationDialogInput<TKey>, new()
        where TOperationDialogOutput : OperationDialogOutput, new()
    {
        /// <summary>
        /// 点击添加按钮
        /// </summary>
        protected virtual async Task OnClickAdd()
        {
            TOperationDialogInput input = new TOperationDialogInput() { Type = OperationDialogInputType.Add };
            if (!await OnClickAddRunBefore(input))
            {
                return;
            }
            Func<OperationDialogOutput?, Task> onClose = async (result) =>
            {
                if (result != null && result.Succeeded)
                {
                    //刷新列表
                    await ReLoadTable(true);
                }
                return;
            };

            await OpenOperationDialogAsync<TOperationDialog, TOperationDialogInput, TOperationDialogOutput>(Localizer[SharedLocalResource.Add], input, onClose);
        }

        /// <summary>
        /// 当点击添加时，执行之前拦截处理
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>
        /// 可用于处理输入弹框的参数，和拦截弹框弹出
        /// </remarks>
        /// <returns></returns>
        protected virtual Task<bool> OnClickAddRunBefore(TOperationDialogInput input)
        {

            return Task.FromResult(true);
        }

        /// <summary>
        /// 点击编辑按钮
        /// </summary>
        /// <param name="model"></param>
        protected virtual async Task OnClickEdit(TKey id)
        {
            TOperationDialogInput input = new TOperationDialogInput() { Type = OperationDialogInputType.Edit, Data = id };
            if (!await OnClickEditRunBefore(input))
            {
                return;
            }
            Func<OperationDialogOutput?, Task> onClose = async (result) =>
            {
                if (result != null && result.Succeeded)
                {
                    //刷新列表
                    await ReLoadTable(true);
                }
                return;
            };
            await OpenOperationDialogAsync<TOperationDialog, TOperationDialogInput, TOperationDialogOutput>(Localizer[SharedLocalResource.Edit], input, onClose);
        }

        /// <summary>
        /// 当点击添加时，执行之前拦截处理
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>
        /// 可用于处理输入弹框的参数，和拦截弹框弹出
        /// </remarks>
        /// <returns></returns>
        protected virtual Task<bool> OnClickEditRunBefore(TOperationDialogInput input)
        {

            return Task.FromResult(true);
        }

        /// <summary>
        /// 点击编辑按钮
        /// </summary>
        /// <param name="roleDto"></param>
        protected virtual async Task OnClickDetail(TKey id)
        {
            TOperationDialogInput input = new TOperationDialogInput() { Type = OperationDialogInputType.Select, Data = id };
            if (!await OnClickDetailRunBefore(input))
            {
                return;
            }
            await OpenOperationDialogAsync<TOperationDialog, TOperationDialogInput, TOperationDialogOutput>(Localizer[SharedLocalResource.Detail], input);
        }

        /// <summary>
        /// 当点击添加时，执行之前拦截处理
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>
        /// 可用于处理输入弹框的参数，和拦截弹框弹出
        /// </remarks>
        /// <returns></returns>
        protected virtual Task<bool> OnClickDetailRunBefore(TOperationDialogInput input)
        {

            return Task.FromResult(true);
        }
    }

    /// <summary>
    /// table列表基类(可以被当作OperationDialog打开，也能快速打开其它操作框)
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TOperationDialog">操作弹框页</typeparam>
    /// <typeparam name="TLocalResource">本地化资源</typeparam>
    /// <typeparam name="TSelfOperationDialogInput">自身作为OperationDialog接收的参数</typeparam>
    /// <typeparam name="TSelfOperationDialogOutput">自身作为OperationDialog返回的参数</typeparam>
    /// <remarks>
    /// 包含列表加载、删除、导出、种子数据、添加、修改、详情
    /// 快递打开操作框，输入 OperationDialogInput_TKey
    /// 快递打开操作框，输出 OperationDialogOutput_TKey
    /// </remarks>
    public abstract class ListOperateTableBase<TDto, TKey, TOperationDialog, TLocalResource, TSelfOperationDialogInput, TSelfOperationDialogOutput> : ListOperateTableBase<TDto, TKey, TOperationDialog, OperationDialogInput<TKey>, OperationDialogOutput<TKey>, TLocalResource, TSelfOperationDialogInput, TSelfOperationDialogOutput>
        where TDto : class, new()
        where TOperationDialog : OperationDialogBase<OperationDialogInput<TKey>, OperationDialogOutput<TKey>, TLocalResource>
        where TLocalResource : SharedLocalResource
    {


        /// <summary>
        /// 打开操作对话框
        /// </summary>
        /// <param name="title"></param>
        /// <param name="input"></param>
        /// <param name="onClose"></param>
        /// <param name="operationDialogSettings "></param>
        /// <returns></returns>
        protected Task OpenOperationDialogAsync(string title, OperationDialogInput<TKey> input, Func<OperationDialogOutput<TKey>?, Task>? onClose = null, OperationDialogSettings? operationDialogSettings = null)
        {
            OperationDialogSettings settings = operationDialogSettings ?? GetOperationDialogSettings();
            return OpenOperationDialogAsync<TOperationDialog, OperationDialogInput<TKey>, OperationDialogOutput<TKey>>(title, input, onClose, settings);
        }
    }

    /// <summary>
    /// table列表基类(可以被当作OperationDialog打开，也能快速打开其它操作框)
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TOperationDialog">操作弹框页</typeparam>
    /// <typeparam name="TLocalResource">本地化资源</typeparam>
    /// <remarks>
    /// 包含列表加载、删除、导出、种子数据、添加、修改、详情
    /// 快递打开操作框，输入 OperationDialogInput_TKey
    /// 快递打开操作框，输出 OperationDialogOutput_TKey
    /// 
    /// 此基类方便那些不需要弹出或弹出时没有输入输出时使用
    /// 自身作为OperationDialog接收的参数，默认为类型 <see cref="TKey"/>
    /// 自身作为OperationDialog返回的参数，默认为类型 <see cref="bool"/>
    /// </remarks>
    public abstract class ListOperateTableBase<TDto, TKey, TOperationDialog, TLocalResource> : ListOperateTableBase<TDto, TKey, TOperationDialog, TLocalResource, TKey, bool>
        where TDto : class, new()
        where TOperationDialog : OperationDialogBase<OperationDialogInput<TKey>, OperationDialogOutput<TKey>, TLocalResource>
        where TLocalResource : SharedLocalResource
    {
    }

    /// <summary>
    /// table列表基类(可以被当作OperationDialog打开，也能快速打开其它操作框)
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TOperationDialog">操作弹框页</typeparam>
    /// <remarks>
    /// 包含列表加载、删除、导出、种子数据、添加、修改、详情
    /// 本地化资源，默认使用<see cref="SharedLocalResource"/>
    /// 快递打开操作框，输入 OperationDialogInput_TKey
    /// 快递打开操作框，输出 OperationDialogOutput_TKey
    /// 
    /// 此基类方便那些不需要弹出或弹出时没有输入输出时使用
    /// 自身作为OperationDialog接收的参数，默认为类型 <see cref="TKey"/>
    /// 自身作为OperationDialog返回的参数，默认为类型 <see cref="bool"/>
    /// </remarks>
    public abstract class ListOperateTableBase<TDto, TKey, TOperationDialog> : ListOperateTableBase<TDto, TKey, TOperationDialog, SharedLocalResource>
        where TDto : class, new()
        where TOperationDialog : OperationDialogBase<OperationDialogInput<TKey>, OperationDialogOutput<TKey>, SharedLocalResource>
    {
    }
}
