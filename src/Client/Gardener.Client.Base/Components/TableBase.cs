// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using AntDesign.TableModels;
using Gardener.Base;
using Gardener.EntityFramwork.Dto;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client.Base.Components
{
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
        protected IApplicationServiceBase<TDto, TKey> _service { get; set; }
        [Inject]
        protected MessageService messageService { get; set; }
        [Inject]
        protected ConfirmService confirmService { get; set; }
        [Inject]
        protected DrawerService drawerService { get; set; }
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
                messageService.Error("加载失败");
            }
            _tableIsLoading = false;
        }
        
        /// <summary>
        /// 查询变化
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        protected async Task OnChange(QueryModel<TDto> queryModel)
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
        protected virtual async Task OnDeleteClick(TKey id)
        {
            if (await confirmService.YesNoDelete() == ConfirmResult.Yes)
            {
                var result = await _service.FakeDelete(id);
                if (result)
                {
                    await ReLoadTable();
                    _datas = _datas.Remove(_datas.FirstOrDefault(x => x.Id.Equals(id)));
                    messageService.Success("删除成功");
                }
                else
                {
                    messageService.Error("删除失败");
                }
            }

        }

        /// <summary>
        /// 点击删除选中按钮
        /// </summary>
        protected virtual async Task OnDeletesClick()
        {
            if (_selectedRows == null || _selectedRows.Count() == 0)
            {
                messageService.Warn("未选中任何行");
            }
            else
            {
                _deletesBtnLoading = true;
                if (await confirmService.YesNoDelete() == ConfirmResult.Yes)
                {
                    var result = await _service.FakeDeletes(_selectedRows.Select(x => x.Id).ToArray());
                    if (result)
                    {
                        _datas = _datas.Where(x => !_selectedRows.Any(y => y.Id.Equals(x.Id))).ToArray();
                        messageService.Success("删除成功");
                    }
                    else
                    {
                        messageService.Error($"删除失败");
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
                messageService.Error("锁定/解锁失败");
            }
        }
    }
}
