// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using AntDesign.TableModels;
using Gardener.Base;
using Gardener.Client.Base.Model;
using Gardener.Client.Base.Services;
using Gardener.EntityFramwork.Dto;
using Microsoft.AspNetCore.Components;
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
    public abstract class TableBase<TDto,TKey> : ComponentBase where TDto:BaseDto<TKey>,new()
    {
        protected ITable _table;
        protected TDto[] _datas;
        protected IEnumerable<TDto> _selectedRows;
        protected int _total = 0;
        protected bool _tableIsLoading = false;
        protected bool _deletesBtnLoading = false;
        protected PageRequest pageRequest = new PageRequest();
        [Inject]
        protected IServiceBase<TDto, TKey> _service { get; set; }
        [Inject]
        protected MessageService messageService { get; set; }
        [Inject]
        protected ConfirmService confirmService { get; set; }
        [Inject]
        protected DrawerService drawerService { get; set; }
        [Inject]
        protected IClientLocalizer localizer { get; set; }
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="pageRequest"></param>
        /// <returns></returns>
        protected virtual PageRequest ConfigurationPageRequest(PageRequest pageRequest)
        {
            return pageRequest;
        }
        /// <summary>
        /// 页面初始化完成
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            _tableIsLoading = true;
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
        }

        /// <summary>
        /// 首次渲染后
        /// </summary>
        bool firstRenderAfter = false;

        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <returns></returns>
        protected virtual async Task ReLoadTable()
        {
            _tableIsLoading = true;
            pageRequest = _table?.GetPageRequest() ?? new PageRequest();
            pageRequest= ConfigurationPageRequest(pageRequest);
            var pagedListResult = await _service.Search(pageRequest);
            if (pagedListResult != null)
            {
                var pagedList = pagedListResult;
                _datas = pagedList.Items.ToArray();
                _total = pagedList.TotalCount;
            }
            else
            {
                messageService.Error(localizer.Combination("加载","失败"));
            }
            _tableIsLoading = false;
        }
        
        /// <summary>
        /// 查询变化
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
                    await ReLoadTable();
                    _datas = _datas.Remove(_datas.FirstOrDefault(x => x.Id.Equals(id)));
                    //当前页被删完了
                    if(pageRequest.PageIndex>1 && _datas.Length==0)
                    {
                        pageRequest.PageIndex = pageRequest.PageIndex - 1;
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
                        //删除整页
                        if (_selectedRows.Count() == pageRequest.PageSize && pageRequest.PageIndex * pageRequest.PageSize >= _total)
                        {
                            pageRequest.PageIndex = pageRequest.PageIndex - 1;
                        }
                        await ReLoadTable();
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
    }
    /// <summary>
    /// 普通table基类
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TDrawer"></typeparam>
    public abstract class TableBase<TDto, TKey, TDrawer> : TableBase<TDto, TKey> where TDto : BaseDto<TKey>, new() where TDrawer : FeedbackComponent<DrawerInput<TKey>, DrawerOutput<TKey>>
    {


        private DrawerSettings drawerSettings = new DrawerSettings { Width = 500 };

        protected TableBase(DrawerSettings drawerSettings)
        {
            this.drawerService = drawerService;
        }
        protected TableBase()
        {
        }
        /// <summary>
        /// 点击添加按钮
        /// </summary>
        protected async Task OnClickAdd()
        {
            DrawerInput<TKey> input = DrawerInput<TKey>.IsAdd();
            var result = await drawerService.CreateDialogAsync<TDrawer, DrawerInput<TKey>, DrawerOutput<TKey>>(input, true, title: localizer["添加"], width: this.drawerSettings.Width);

            if (result.Succeeded)
            {
                //刷新列表
                pageRequest.PageIndex = 1;
                await ReLoadTable();
            }
        }
        /// <summary>
        /// 点击编辑按钮
        /// </summary>
        /// <param name="model"></param>
        protected async Task OnClickEdit(TKey id)
        {
            DrawerInput<TKey> input = DrawerInput<TKey>.IsEdit(id);
            var result = await drawerService.CreateDialogAsync<TDrawer, DrawerInput<TKey>, DrawerOutput<TKey>>(input, true, title: localizer["编辑"], width: this.drawerSettings.Width);

            if (result.Succeeded)
            {
                await ReLoadTable();
            }
        }

        /// <summary>
        /// 点击编辑按钮
        /// </summary>
        /// <param name="roleDto"></param>
        public async Task OnClickDetail(TKey id)
        {
            DrawerInput<TKey> input = DrawerInput<TKey>.IsSelect(id);
            var result = await drawerService.CreateDialogAsync<TDrawer, DrawerInput<TKey>, DrawerOutput<TKey>>(input, true, title: localizer["详情"], width: this.drawerSettings.Width);
        }
    }
}
