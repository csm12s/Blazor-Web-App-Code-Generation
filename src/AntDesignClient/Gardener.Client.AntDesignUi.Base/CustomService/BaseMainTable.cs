
using AntDesign;
using AntDesign.TableModels;
using Gardener.Attributes;
using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.AntDesignUi.Base.Constants;
using Gardener.Client.Base;
using Gardener.Common;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System.Reflection;

namespace Gardener.Client.AntDesignUi.Base.CustomService
{
    /// <summary>
    /// 列表table基类（无主键表，TDto不继承BaseDto）
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseMainTable<TDto, TKey, TLocalResource> 
        : BaseTable<TDto, TKey, TLocalResource> 
        where TDto : class, new()
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
        /// <summary>
        /// 默认搜索值
        /// </summary>
        protected Dictionary<string, object> _defaultSearchValue = new Dictionary<string, object>();
        /// <summary>
        /// 预设搜索过滤
        /// </summary>
        protected List<FilterGroup> _presetSearchFilterGroups = new ();
        protected List<FilterGroup> _customSearchFilterGroups = new ();
        /// <summary>
        /// 确认提示服务
        /// </summary>
        [Inject]
        protected ConfirmService confirmService { get; set; }

        /// <summary>
        /// 路由导航服务
        /// </summary>
        [Inject]
        protected NavigationManager navigation { get; set; }
        /// <summary>
        /// javascript 工具
        /// </summary>
        [Inject]
        protected IJsTool jsTool { get; set; }

        protected string searchInputStyle = $"margin-right:8px;margin-bottom:2px;width:100px";

        #region override mothed
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        protected override Task OnInitializedAsync()
        {
            _tableIsLoading = true;
            return base.OnInitializedAsync();
        }

        /// <summary>
        /// 首次渲染后
        /// </summary>
        bool firstRenderAfter = false;

        /// <summary>
        /// 组件渲染后
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                this.firstRenderAfter = true;
                await ReLoadTable();
                await InvokeAsync(StateHasChanged);
            }
            await base.OnAfterRenderAsync(firstRender);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnParametersSetAsync()
        {
            var url = new Uri(navigation.Uri);
            var query = url.Query;
            Dictionary<string, StringValues> urlParams = QueryHelpers.ParseQuery(query);
            if (urlParams != null && urlParams.Count() > 0)
            {
                urlParams.ForEach(x =>
                {
                    _defaultSearchValue.Add(x.Key, x.Value.ToString());
                });
            }

            await base.OnParametersSetAsync();
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
        /// 重置请求参数
        /// </summary>
        /// <returns></returns>
        protected virtual PageRequest GetPageRequest()
        {
            PageRequest pageRequest = _table?.GetPageRequest() ?? new PageRequest();
            //如果有搜索条件 就拼接上
            if (_presetSearchFilterGroups != null && _presetSearchFilterGroups.Any())
            {
                pageRequest.FilterGroups.AddRange(_presetSearchFilterGroups);
            }
            if (_customSearchFilterGroups != null && _customSearchFilterGroups.Any())
            {
                pageRequest.FilterGroups.AddRange(_customSearchFilterGroups);
            }
            ConfigurationPageRequest(pageRequest);
            return pageRequest;
        }

        /// <summary>
        /// 重新加载table 
        /// </summary>
        /// <returns></returns>
        protected virtual async Task ReLoadTable()
        {
            await ReLoadTable(firstPage: false);
        }

        /// <summary>
        /// 重新加载table, 删除整页，且是最后一页
        /// </summary>
        /// <returns></returns>
        protected virtual async Task ReLoadTableAfterDeleteLastPage()
        {
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

        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <param name="firstPage">是否从首页加载</param>
        /// <returns></returns>
        protected virtual async Task ReLoadTable(bool firstPage)
        {
            if (firstPage)
            {
                _pageIndex = 1;
            }
            PageRequest pageRequest = GetPageRequest();
            await ReLoadTable(pageRequest);
        }

        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <returns></returns>
        protected virtual async Task ReLoadTable(PageRequest pageRequest)
        {
            _tableIsLoading = true;
            var pagedListResult = await _service.Search(pageRequest);
            if (pagedListResult != null)
            {
                var pagedList = pagedListResult;
                _datas = pagedList.Items;
                _total = pagedList.TotalCount;
            }
            else
            {
                await messageService.Error(localizer.Combination(SharedLocalResource.Load, SharedLocalResource.Fail));
            }
            _tableIsLoading = false;
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// table查询变化
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        protected virtual async Task OnChange(QueryModel<TDto> queryModel)
        {
            if (firstRenderAfter)
            {
                await ReLoadTable();
            }
        }

        /// <summary>
        /// 点击删除按钮(FakeDelete)
        /// </summary>
        /// <param name="id"></param>
        protected virtual async Task OnClickDelete(TKey id)
        {
            if (await confirmService.YesNoDelete() == ConfirmResult.Yes)
            {
                var result = await _service.FakeDelete(id);
                if (result)
                {
                    PageRequest pageRequest = GetPageRequest();
                    //当前页被删完了
                    if (_pageIndex > 1 && _datas.Count() == 0)
                    {
                        _pageIndex = _pageIndex - 1;
                    }
                    await ReLoadTable();
                    await messageService.Success(localizer.Combination(SharedLocalResource.Delete, SharedLocalResource.Success));
                }
                else
                {
                    await messageService.Error(localizer.Combination(SharedLocalResource.Delete, SharedLocalResource.Fail));
                }
            }

        }

        /// <summary>
        /// 点击删除按钮(TrueDelete)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected virtual async Task OnClickTrueDelete(TKey id)
        {
            if (await confirmService.YesNoDelete() == ConfirmResult.Yes)
            {
                var result = await _service.Delete(id);
                if (result)
                {
                    PageRequest pageRequest = GetPageRequest();
                    //当前页被删完了
                    if (_pageIndex > 1 && _datas.Count() == 0)
                    {
                        _pageIndex = _pageIndex - 1;
                    }
                    await ReLoadTable();
                    await messageService.Success(localizer.Combination(SharedLocalResource.Delete, SharedLocalResource.Success));
                }
                else
                {
                    await messageService.Error(localizer.Combination(SharedLocalResource.Delete, SharedLocalResource.Fail));
                }
            }

        }

        /// <summary>
        /// 设置预设过滤信息
        /// </summary>
        /// <param name="filterGroups"></param>
        /// <returns></returns>
        protected virtual Task SetPresetFilterGroups(List<FilterGroup> filterGroups)
        {
            _presetSearchFilterGroups = filterGroups;
            return Task.CompletedTask;
        }

        /// <summary>
        /// 种子数据
        /// </summary>
        /// <typeparam name="TShowSeedDataDrawer">展示种子数据抽屉</typeparam>
        /// <returns></returns>
        protected virtual async Task OnClickShowSeedData<TShowSeedDataDrawer>() where TShowSeedDataDrawer : FeedbackComponent<string, bool>
        {
            PageRequest pageRequest = GetPageRequest();
            pageRequest.PageSize = int.MaxValue;
            pageRequest.PageIndex = 1;
            string seedData = await _service.GenerateSeedData(pageRequest);
            OperationDialogSettings drawerSettings = GetOperationDialogSettings();
            drawerSettings.Width = 1300;
            await OpenOperationDialogAsync<TShowSeedDataDrawer, string, bool>(localizer[SharedLocalResource.SeedData], seedData, operationDialogSettings: drawerSettings);
        }

        /// <summary>
        /// 种子数据
        /// </summary>
        /// <typeparam name="TShowSeedDataDrawer">展示种子数据抽屉</typeparam>
        /// <returns></returns>
        protected virtual async Task OnClickShowSeedData()
        {
            await OnClickShowSeedData<ShowSeedDataCode>();
        }

        /// <summary>
        /// 导出数据
        /// </summary>
        protected virtual async Task OnClickExport()
        {
            _exportDataLoading = true;
            PageRequest pageRequest = GetPageRequest();
            string url = await _service.Export(pageRequest);
            await jsTool.Document.DownloadFile(url);
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

        #region Page loading
        /// <summary>
        /// Page start loading
        /// </summary>
        /// <returns></returns>
        protected bool StartLoading()
        {
            _tableIsLoading = true;

            return _tableIsLoading;
        }

        /// <summary>
        /// Page stop loading
        /// </summary>
        /// <returns></returns>
        protected bool StopLoading()
        {
            _tableIsLoading = false;

            return _tableIsLoading;
        }
        #endregion
    }

    /// <summary>
    /// 列表table基类（无主键表，TDto不继承BaseDto）
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TOperationDialog">操作弹框页</typeparam>
    public abstract class BaseMainTable<TDto, TKey, TOperationDialog, TLocalResource> 
        : BaseMainTable<TDto, TKey, TLocalResource> 
        where TDto : class, new() 
        where TOperationDialog : FeedbackComponent<OperationDialogInput<TKey>, OperationDialogOutput<TKey>>
    {
        /// <summary>
        /// 点击添加按钮
        /// </summary>
        protected virtual async Task OnClickAdd()
        {
            OperationDialogSettings drawerSettings = GetOperationDialogSettings();
            OperationDialogInput<TKey> input = OperationDialogInput<TKey>.IsAdd();

            Func<OperationDialogOutput<TKey>, Task> onClose = async (result) =>
            {
                if (result.Succeeded)
                {
                    //刷新列表
                    await ReLoadTable(true);
                }
                return;
            };

            await OpenOperationDialogAsync(localizer[SharedLocalResource.Add], input, onClose);
        }
        /// <summary>
        /// 点击编辑按钮
        /// </summary>
        /// <param name="model"></param>
        protected virtual async Task OnClickEdit(TKey id)
        {
            OperationDialogInput<TKey> input = OperationDialogInput<TKey>.IsEdit(id);
            Func<OperationDialogOutput<TKey>, Task> onClose = async (result) =>
            {
                if (result.Succeeded)
                {
                    //刷新列表
                    await ReLoadTable(true);
                }
                return;
            };
            await OpenOperationDialogAsync(localizer[SharedLocalResource.Edit], input, onClose);
        }

        /// <summary>
        /// 点击编辑按钮
        /// </summary>
        /// <param name="roleDto"></param>
        protected virtual async Task OnClickDetail(TKey id)
        {
            OperationDialogInput<TKey> input = OperationDialogInput<TKey>.IsSelect(id);
            await OpenOperationDialogAsync(localizer[SharedLocalResource.Detail], input);
        }

        /// <summary>
        /// 打开操作对话框
        /// </summary>
        /// <param name="title"></param>
        /// <param name="input"></param>
        /// <param name="onClose"></param>
        /// <param name="operationDialogSettings "></param>
        /// <returns></returns>
        protected async Task OpenOperationDialogAsync(string title, OperationDialogInput<TKey> input, Func<OperationDialogOutput<TKey>, Task> onClose = null, OperationDialogSettings operationDialogSettings = null)
        {
            OperationDialogSettings settings = operationDialogSettings ?? GetOperationDialogSettings();
            await OpenOperationDialogAsync<TOperationDialog, OperationDialogInput<TKey>, OperationDialogOutput<TKey>>(title, input, onClose, settings);
        }

        protected virtual async Task DoClearSearch()
        {
            // TODO: clear search field
            await ReLoadTable(true);
        }

    }

}
