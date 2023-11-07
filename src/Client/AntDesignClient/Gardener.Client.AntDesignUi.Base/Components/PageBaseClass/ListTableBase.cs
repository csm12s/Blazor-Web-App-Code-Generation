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
using Gardener.Client.AntDesignUi.Base.Components.PageBaseClass;
using Gardener.Client.AntDesignUi.Base.Constants;
using Gardener.Common;
using Mapster;
using System.Reflection;

namespace Gardener.Client.AntDesignUi.Base.Components
{
    /// <summary>
    /// table列表基类(可以被当作OperationDialog打开)-支持多租户
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TLocalResource">本地化资源</typeparam>
    /// <typeparam name="TSelfOperationDialogInput">自身作为OperationDialog接收的参数</typeparam>
    /// <typeparam name="TSelfOperationDialogOutput">自身作为OperationDialog返回的参数</typeparam>
    /// <remarks>
    /// 包含列表加载、删除、导出、种子数据
    /// </remarks>
    public abstract class ListTableBase<TDto, TKey, TLocalResource, TSelfOperationDialogInput, TSelfOperationDialogOutput> : MultiTenantTableBase<TDto, TKey, TLocalResource, TSelfOperationDialogInput, TSelfOperationDialogOutput> where TDto : class, new()
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
        protected int _pageSize = ClientConstant.PageSize;

        #region override mothed
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }
        /// <summary>
        /// 首次渲染后
        /// </summary>
        //bool firstRenderAfter = false;

        /// <summary>
        /// OnParametersSetAsync
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override Task OnParametersSetAsync()
        {
            //this.firstRenderAfter = true;
            //await ReLoadTable();
            return base.OnParametersSetAsync();
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
        /// 获取请求参数
        /// </summary>
        /// <param name="filterGroups">附加的参数</param>
        /// <returns></returns>
        protected virtual PageRequest GetPageRequest(List<FilterGroup>? filterGroups = null)
        {
            PageRequest pageRequest = _table?.GetPageRequest() ?? new PageRequest();
            //如果有搜索条件提供者 就拼接上
            if (_tableSearchFilterGroupProviders.Any())
            {
                _tableSearchFilterGroupProviders.ForEach(p =>
                {
                    var items = p.Invoke();
                    if (items != null)
                    {
                        pageRequest.FilterGroups.AddRange(items);
                    }
                });
            }
            if (filterGroups != null)
            {
                pageRequest.FilterGroups.AddRange(filterGroups);
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
        /// </summary>
        /// <returns></returns>
        protected virtual Task ReLoadTable()
        {
            return ReLoadTable(false);
        }
        /// <summary>
        /// 点击TableSearch搜索
        /// </summary>
        /// <param name="filterGroups"></param>
        protected virtual Task OnTableSearch(List<FilterGroup> filterGroups)
        {
            return ReLoadTable(true, false);
        }

        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <param name="firstPage">是否从首页加载</param>
        /// <param name="forceRender">是否强制渲染</param>
        /// <param name="filterGroups">附加的参数</param>
        /// <returns></returns>
        protected virtual Task ReLoadTable(bool firstPage, bool forceRender = false, List<FilterGroup>? filterGroups = null)
        {
            if (firstPage && _pageIndex > 1)
            {
                //设置页码后会触发OnChange
                _pageIndex = 1;
                return Task.CompletedTask;
            }
            else
            {
                PageRequest pageRequest = GetPageRequest(filterGroups);
                return ReLoadTable(pageRequest, forceRender);
            }

        }

        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <param name="firstPage">是否从首页加载</param>
        /// <param name="forceRender">是否强制渲染</param>
        /// <returns></returns>
        protected virtual async Task ReLoadTable(PageRequest pageRequest, bool forceRender = false)
        {
            StartTableLoading(forceRender);
            var pagedListResult = await BaseService.Search(pageRequest);
            if (pagedListResult != null)
            {
                var pagedList = pagedListResult;
                IEnumerable<TDto> _dataTemps = pagedList.Items ?? new List<TDto>(0);
                if (_dataTemps.Any())
                {
                    //如果有租户数据，装配一下
                    if (typeof(TDto).IsAssignableTo(typeof(IModelTenant)))
                    {
                        foreach (TDto item in _dataTemps)
                        {
                            if (item is IModelTenant modelTenant)
                            {
                                modelTenant.Tenant = GetTenant(modelTenant.TenantId);
                            }
                        }
                    }
                }

                PageListDataHadnle(_dataTemps);
                _total = pagedList.TotalCount;
                _datas = _dataTemps;
            }
            else
            {
                MessageService.Error(Localizer.Combination(nameof(SharedLocalResource.Load), nameof(SharedLocalResource.Fail)));
            }
            StopTableLoading(forceRender);
        }

        /// <summary>
        /// 列表接口返回后，对页面列表数据进行处理
        /// </summary>
        /// <param name="datas"></param>
        protected virtual void PageListDataHadnle(IEnumerable<TDto> datas)
        {
        }

        private int lastPageIndex = 0;

        /// <summary>
        /// table查询变化
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        protected virtual async Task OnChange(QueryModel<TDto> queryModel)
        {
            if (lastPageIndex == queryModel.PageIndex)
            {
                lastPageIndex = queryModel.PageIndex;
                //不是翻页，回首页
                await ReLoadTable(true);
            }
            else
            {
                lastPageIndex = queryModel.PageIndex;
                await ReLoadTable(false);
            }
        }

        #region Fake delete

        /// <summary>
        /// 点击删除按钮(逻辑删除)
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
                    MessageService.Success(Localizer.Combination(nameof(SharedLocalResource.Delete), nameof(SharedLocalResource.Success)));
                    await ReLoadTable();
                }
                else
                {
                    MessageService.Error(Localizer.Combination(nameof(SharedLocalResource.Delete), nameof(SharedLocalResource.Fail)));
                }
            }

        }

        /// <summary>
        /// 点击删除选中按钮(逻辑删除)
        /// </summary>
        protected virtual async Task OnClickDeletes()
        {
            if (_selectedRows == null || _selectedRows.Count() == 0)
            {
                MessageService.Warn(Localizer[nameof(SharedLocalResource.NoRowsAreSelected)]);
            }
            else
            {
                _deletesBtnLoading = true;
                if (await ConfirmService.YesNoDelete() == ConfirmResult.Yes)
                {
                    var result = await BaseService.FakeDeletes(_selectedRows.Select(x => GetKey(x)).ToArray());
                    if (result)
                    {
                        MessageService.Success(Localizer.Combination(nameof(SharedLocalResource.Delete), nameof(SharedLocalResource.Success)));
                        //删除整页，且是最后一页
                        if (_selectedRows.Count() == _pageSize && _pageIndex * _pageSize >= _total)
                        {
                            await ReLoadTable(true, true);
                        }
                        else
                        {
                            await ReLoadTable(false, true);
                        }
                    }
                    else
                    {
                        MessageService.Error(Localizer.Combination(nameof(SharedLocalResource.Delete), nameof(SharedLocalResource.Fail)));
                    }
                }

                _deletesBtnLoading = false;
            }
        }
        #endregion

        #region True Delete
        /// <summary>
        /// 点击删除按钮(物理删除)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected virtual async Task OnClickTrueDelete(TKey id)
        {
            if (await ConfirmService.YesNoDelete() == ConfirmResult.Yes)
            {
                var result = await BaseService.Delete(id);
                if (result)
                {
                    MessageService.Success(Localizer.Combination(nameof(SharedLocalResource.Delete), nameof(SharedLocalResource.Success)));
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
                    MessageService.Error(Localizer.Combination(nameof(SharedLocalResource.Delete), nameof(SharedLocalResource.Fail)));
                }
            }

        }
        /// <summary>
        /// 点击删除选中按钮(物理删除)
        /// </summary>
        /// <returns></returns>
        protected virtual async Task OnClickTrueDeletes()
        {
            if (_selectedRows == null || _selectedRows.Count() == 0)
            {
                MessageService.Warn(Localizer[nameof(SharedLocalResource.NoRowsAreSelected)]);
            }
            else
            {
                _deletesBtnLoading = true;
                if (await ConfirmService.YesNoDelete() == ConfirmResult.Yes)
                {

                    var result = await BaseService.Deletes(_selectedRows.Select(x => GetKey(x)).ToArray());
                    if (result)
                    {
                        MessageService.Success(Localizer.Combination(nameof(SharedLocalResource.Delete), nameof(SharedLocalResource.Success)));

                        //删除整页，且是最后一页
                        if (_selectedRows.Count() == _pageSize && _pageIndex * _pageSize >= _total)
                        {
                            await ReLoadTable(true, true);
                        }
                        else
                        {
                            await ReLoadTable(false, true);
                        }
                    }
                    else
                    {
                        MessageService.Error(Localizer.Combination(nameof(SharedLocalResource.Delete), nameof(SharedLocalResource.Fail)));
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
            await OpenOperationDialogAsync<TShowSeedDataDrawer, Task<string>, bool>(Localizer[nameof(SharedLocalResource.SeedData)], seedData, operationDialogSettings: drawerSettings);
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
    }

    /// <summary>
    /// table列表基类(可以被当作OperationDialog打开)-支持多租户
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
    {

    }

    /// <summary>
    /// 列表table基类-支持多租户
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
}
