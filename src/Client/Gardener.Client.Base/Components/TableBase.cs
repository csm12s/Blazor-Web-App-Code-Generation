// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using AntDesign.TableModels;
using Gardener.Base;
using Gardener.Client.Base.Constants;
using Gardener.Client.Base.Services;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client.Base.Components
{
    /// <summary>
    /// 普通table基类
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class TableBase<TDto, TKey> : ReuseTabsPageBase where TDto : BaseDto<TKey>, new()
    {
        /// <summary>
        /// table引用
        /// </summary>
        protected ITable _table;
        /// <summary>
        /// 数据集合
        /// </summary>
        protected IEnumerable<TDto> _datas;
        /// <summary>
        /// 选择的行
        /// </summary>
        protected IEnumerable<TDto> _selectedRows;
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
        /// table加载中控制
        /// </summary>
        protected bool _tableIsLoading = false;
        /// <summary>
        /// 多选删除按钮加载中控制
        /// </summary>
        protected bool _deletesBtnLoading = false;
        /// <summary>
        /// 默认搜索值
        /// </summary>
        protected Dictionary<string, object> _defaultSearchValue = new Dictionary<string, object>();
        /// <summary>
        /// 预设搜索过滤
        /// </summary>
        protected List<FilterGroup> _presetSearchFilterGroups;

        [Inject]
        protected IServiceBase<TDto, TKey> _service { get; set; }
        [Inject]
        protected MessageService messageService { get; set; }
        [Inject]
        protected ConfirmService confirmService { get; set; }
        [Inject]
        protected IOperationDialogService operationDialogService { get; set; }
        [Inject]
        protected IClientLocalizer localizer { get; set; }
        [Inject]
        protected NavigationManager navigation { get; set; }
        [Inject]
        protected IJsTool jsTool { get; set; }
        /// <summary>
        /// 在构建完成前配置搜索请求
        /// </summary>
        /// <param name="pageRequest"></param>
        /// <returns></returns>
        protected virtual void ConfigurationPageRequest(PageRequest pageRequest)
        {
        }
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

        /// <summary>
        /// 首次渲染后
        /// </summary>
        bool firstRenderAfter = false;

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
            ConfigurationPageRequest(pageRequest);
            return pageRequest;
        }

        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <returns></returns>
        protected virtual async Task ReLoadTable()
        {
            await ReLoadTable(false);
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
        /// <param name="firstPage">是否从首页加载</param>
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
                messageService.Error(localizer.Combination("加载", "失败"));
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
        /// 点击删除按钮
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
                    messageService.Success(localizer.Combination("删除", "成功"));
                }
                else
                {
                    messageService.Error(localizer.Combination("删除", "失败"));
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
                messageService.Warn(localizer["未选中任何行"]);
            }
            else
            {
                _deletesBtnLoading = true;
                if (await confirmService.YesNoDelete() == ConfirmResult.Yes)
                {
                    var result = await _service.FakeDeletes(_selectedRows.Select(x => x.Id).ToArray());
                    if (result)
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
                        messageService.Success(localizer.Combination("删除", "成功"));
                    }
                    else
                    {
                        messageService.Error(localizer.Combination("删除", "失败"));
                    }
                }
                _deletesBtnLoading = false;
            }
        }

        /// <summary>
        /// 点击锁定按钮
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isLocked"></param>
        protected virtual async Task OnChangeIsLocked(TDto model, bool isLocked)
        {
            var result = await _service.Lock(model.Id, isLocked);
            if (!result)
            {
                model.IsLocked = !isLocked;
                string msg = isLocked ? localizer["锁定"] : localizer["解锁"];

                messageService.Error($"{msg} {localizer["失败"]}");
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
            await OpenOperationDialogAsync<TShowSeedDataDrawer, string, bool>(localizer["种子数据"], seedData, operationDialogSettings: drawerSettings);
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
        /// 导出数据加载中绑定数据
        /// </summary>
        protected bool _exportDataLoading = false;

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

        /// <summary>
        /// 操作会话配置
        /// </summary>
        /// <returns></returns>
        protected virtual OperationDialogSettings GetOperationDialogSettings()
        {
            OperationDialogSettings dialogSettings = new OperationDialogSettings();
            ClientConstant.DefaultOperationDialogSettings.Adapt(dialogSettings);
            return dialogSettings;
        }

        /// <summary>
        /// 打开操作对话框
        /// </summary>
        /// <param name="title"></param>
        /// <param name="input"></param>
        /// <param name="onClose"></param>
        /// <param name="operationDialogSettings "></param>
        /// <param name="width "></param>
        /// <returns></returns>
        protected async Task OpenOperationDialogAsync<TComponent, TComponentOptions, TResult>(string title, TComponentOptions input, Func<TResult, Task> onClose = null, OperationDialogSettings operationDialogSettings = null, int? width = null) where TComponent : FeedbackComponent<TComponentOptions, TResult>
        {
            OperationDialogSettings settings = operationDialogSettings ?? GetOperationDialogSettings();
            if (width.HasValue)
            {
                settings.Width = width.Value;
            }
            await operationDialogService.OpenAsync<TComponent, TComponentOptions, TResult>(title, input, onClose, settings);
        }
    }

    /// <summary>
    /// 普通table基类
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEditDrawer"></typeparam>
    public abstract class TableBase<TDto, TKey, TEditDrawer> : TableBase<TDto, TKey> where TDto : BaseDto<TKey>, new() where TEditDrawer : FeedbackComponent<OperationDialogInput<TKey>, OperationDialogOutput<TKey>>
    {
        /// <summary>
        /// 点击添加按钮
        /// </summary>
        protected async Task OnClickAdd()
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

            await OpenOperationDialogAsync(localizer["编辑"], input, onClose);
        }
        /// <summary>
        /// 点击编辑按钮
        /// </summary>
        /// <param name="model"></param>
        protected async Task OnClickEdit(TKey id)
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
            await OpenOperationDialogAsync(localizer["添加"], input, onClose);
        }

        /// <summary>
        /// 点击编辑按钮
        /// </summary>
        /// <param name="roleDto"></param>
        protected async Task OnClickDetail(TKey id)
        {
            OperationDialogInput<TKey> input = OperationDialogInput<TKey>.IsSelect(id);
            await OpenOperationDialogAsync(localizer["详情"], input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="input"></param>
        /// <param name="onClose"></param>
        /// <param name="operationDialogSettings "></param>
        /// <returns></returns>
        protected async Task OpenOperationDialogAsync(string title, OperationDialogInput<TKey> input, Func<OperationDialogOutput<TKey>, Task> onClose = null, OperationDialogSettings operationDialogSettings = null)
        {
            OperationDialogSettings settings = operationDialogSettings ?? GetOperationDialogSettings();
            await OpenOperationDialogAsync<TEditDrawer, OperationDialogInput<TKey>, OperationDialogOutput<TKey>>(title, input, onClose, settings);
        }
    }
}
