﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using AntDesign.TableModels;
using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client.Pages.SystemManager.Audit
{
    public partial class AuditOperation
    {
        ITable _table;
        AuditOperationDto[] _datas;
        IEnumerable<AuditOperationDto> _selectedRows;
        AuditOperationSearchInput searchInput = new AuditOperationSearchInput();
        int _total = 0;
        string _name = string.Empty;
        bool _tableIsLoading = false;
        [Inject]
        public MessageService messageService { get; set; }
        [Inject]
        public IAuditOperationService AuditOperationService { get; set; }
        [Inject]
        ConfirmService confirmService { get; set; }
        [Inject]
        DrawerService drawerService { get; set; }
        /// <summary>
        /// 页面初始化完成
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
        }
        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <returns></returns>
        private async Task ReLoadTable()
        {
            _tableIsLoading = true;
            var pagedListResult = await AuditOperationService.Search(searchInput);
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
        private async Task OnChange(QueryModel<AuditOperationDto> queryModel)
        {

            searchInput.OrderConditions = queryModel.
                SortModel.
                Where(x => x.SortType.Value > 0).
                Select(x => x.Adapt<SearchSort>()).ToArray();
            if (searchInput.OrderConditions.Length == 0)
            {
                searchInput.OrderConditions = new[] { 
                    new SearchSort() 
                    { 
                        FieldName=nameof(AuditOperationDto.CreatedTime),
                        SortType=SearchSortType.Desc 
                    } 
                };
            }
            await ReLoadTable();
        }
        /// <summary>
        /// 点击删除按钮
        /// </summary>
        /// <param name="id"></param>
        private async Task OnDeleteClick(Guid id)
        {
            if (await confirmService.YesNoDelete() == ConfirmResult.Yes)
            {
                var result = await AuditOperationService.Delete(id);
                if (result)
                {
                    await ReLoadTable();
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
                    var result = await AuditOperationService.Deletes(_selectedRows.Select(x => x.Id).ToArray());
                    if (result)
                    {
                        await ReLoadTable();
                        messageService.Success("删除成功");
                    }
                    else
                    {
                        messageService.Error($"删除失败");
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditEntity"></param>
        /// <returns></returns>
        private async Task OnDetailClick(Guid id)
        {
            List<AuditEntityDto>  auditEntityDtos= await AuditOperationService.GetAuditEntity(id);

            await drawerService.CreateDialogAsync<AuditEntityDetailDrawer, ICollection<AuditEntityDto>, bool>(auditEntityDtos, title: "数据变更详情", width: 960, placement: "left");
        }
    }
}