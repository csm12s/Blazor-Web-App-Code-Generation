// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.Base;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.ClientView
{
    public class ClientFunctionEditOption
    {
        /// <summary>
        /// 选中的资源id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 0 展示
        /// 1 添加
        /// </summary>
        public int Type { get; set; }
    }
    public partial class ClientFunctionEdit : FeedbackComponent<ClientFunctionEditOption, bool>
    {
        [Inject]
        IFunctionService functionService { get; set; }
        [Inject]
        IClientFunctionService clientFunctionService { get; set; }
        [Inject]
        IClientService clientService { get; set; }
        [Inject]
        MessageService messageService { get; set; }

        [Inject]
        DrawerService drawerService { get; set; }
        [Inject]
        ConfirmService confirmService { get; set; }
        [Inject]
        NotificationService noticeService { get; set; }
        [Inject]
        IClientLocalizer localizer { get; set; }
        private List<FunctionDto> _functionDtos = new List<FunctionDto>();
        private List<FunctionDto> _selectedFunctionDtos = new List<FunctionDto>();
        List<TableFilter<string>> groupFilters = null;
        List<TableFilter<string>> serviceFilters = null;
        private bool _loading = false;
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            _loading = true;
            //根据资源编号获取关联的接口
            List<FunctionDto> _oldFunctionDtos = await clientService.GetFunctions(this.Options.Id);
            if (this.Options.Type == 0)
            {
                //根据资源编号获取关联的接口
                _functionDtos = _oldFunctionDtos;
            }
            else if (this.Options.Type == 1)
            {
                //查看可用的接口
                List<FunctionDto> tempFunctionDtos = await functionService.GetAllUsable();
                //移除已选择的
                if (_oldFunctionDtos != null)
                {
                    _functionDtos = tempFunctionDtos.Where(y => !_oldFunctionDtos.Any(x => x.Id.Equals(y.Id))).ToList();
                }
                else
                {
                    _functionDtos = tempFunctionDtos;
                }
                
            }
            //groupFilters = new List<TableFilter<string>>();
            //serviceFilters = new List<TableFilter<string>>();

            //_functionDtos.Select(x => x.Group).Distinct().ForEach(x =>
            //{
            //    groupFilters.Add(new TableFilter<string>() { Text=x,Value=x });

            //});
            //_functionDtos.Select(x => x.Service).Distinct().ForEach(x =>
            //{
            //    serviceFilters.Add(new TableFilter<string>() { Text = x, Value = x });

            //});
            _loading = false;

        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <returns></returns>
        private async Task OnCancleClick()
        {
            DrawerRef<bool> drawerRef = base.FeedbackRef as DrawerRef<bool>;
            await drawerRef!.CloseAsync(false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="functions"></param>
        /// <returns></returns>
        private void SelectedRowsChanged(IEnumerable<FunctionDto> functions)
        {
            _selectedFunctionDtos = functions.ToList();
        }

        /// <summary>
        /// 点击删除选中按钮
        /// </summary>
        private async Task OnFunctionDeletesClick()
        {
            if (_selectedFunctionDtos == null || _selectedFunctionDtos.Count <= 0)
            {
                messageService.Warn(localizer["未选中任何行"]);
                return;
            }
            if (await confirmService.YesNoDelete() == ConfirmResult.Yes)
            {
                foreach (var item in _selectedFunctionDtos)
                {
                    await clientFunctionService.Delete(this.Options.Id, item.Id);
                }
                messageService.Success(localizer.Combination("删除", "成功"));
                await OnInitializedAsync();
            }
        }
        /// <summary>
        /// 点击显示关联按钮
        /// </summary>
        private async Task OnShowFunctionAddPageClick(Guid id)
        {
            var result = await drawerService.CreateDialogAsync<ClientFunctionEdit, ClientFunctionEditOption, bool>(
                     new ClientFunctionEditOption { Id = id, Type = 1 },
                     true,
                     title: $"{localizer["绑定接口"]}-[{this.Options.Name}]",
                     width: 1200,
                     placement: "right");
            if (result)
            {
                await OnInitializedAsync();
            }
        }
        /// <summary>
        /// 点击关联选中按钮
        /// </summary>
        private async Task OnFunctionAddClick()
        {
            if (_selectedFunctionDtos == null || _selectedFunctionDtos.Count <= 0)
            {
                messageService.Warn(localizer["未选中任何行"]);
                return;
            }

            bool result = await clientFunctionService.Add(_selectedFunctionDtos.Select(x =>
            {
                return new ClientFunctionDto
                {
                    ClientId = this.Options.Id,
                    FunctionId = x.Id,
                    CreatedTime = DateTimeOffset.Now
                };
            }).ToList());
            if (result)
            {
                messageService.Success(localizer.Combination("绑定","成功"));
                await (base.FeedbackRef as DrawerRef<bool>).CloseAsync(true);
            }
            else
            {
                messageService.Error(localizer.Combination("绑定", "失败"));
            }
        }
    }
}
