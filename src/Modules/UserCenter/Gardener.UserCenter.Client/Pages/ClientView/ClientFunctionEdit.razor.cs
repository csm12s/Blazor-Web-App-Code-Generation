// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.Base.Services;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Services;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.ClientView
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientFunctionEditOption
    {
        /// <summary>
        /// 选中的id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 0 展示
        /// 1 添加
        /// </summary>
        public int Type { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public partial class ClientFunctionEdit : OperationDialogBase<ClientFunctionEditOption, bool>
    {
        [Inject]
        private IFunctionService FunctionService { get; set; } = null!;
        [Inject]
        private IClientFunctionService ClientFunctionService { get; set; } = null!;
        [Inject]
        private IClientService ClientService { get; set; } = null!;
        [Inject]
        private IClientMessageService MessageService { get; set; } = null!;
        [Inject]
        private ConfirmService ConfirmService { get; set; } = null!;

        private List<FunctionDto> _functionDtos = new List<FunctionDto>();
        private List<FunctionDto> _selectedFunctionDtos = new List<FunctionDto>();
        //List<TableFilter<string>> groupFilters = null;
        //List<TableFilter<string>> serviceFilters = null;
        private bool _loading = false;
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            _loading = true;
            //根据资源编号获取关联的接口
            List<FunctionDto> _oldFunctionDtos = await ClientService.GetFunctions(this.Options.Id);
            if (this.Options.Type == 0)
            {
                //根据资源编号获取关联的接口
                _functionDtos = _oldFunctionDtos;
            }
            else if (this.Options.Type == 1)
            {
                //查看可用的接口
                List<FunctionDto> tempFunctionDtos = await FunctionService.GetAllUsable();
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
            //    groupFilters.Add(new TableFilter<string>() { Text=x,Data=x });

            //});
            //_functionDtos.Select(x => x.BaseService).Distinct().ForEach(x =>
            //{
            //    serviceFilters.Add(new TableFilter<string>() { Text = x, Data = x });

            //});
            _loading = false;

        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <returns></returns>
        private async Task OnCancleClick()
        {
            await this.CloseAsync(false);
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
                MessageService.Warn(Localizer[nameof(SharedLocalResource.NoRowsAreSelected)]);
                return;
            }
            if (await ConfirmService.YesNoDelete() == ConfirmResult.Yes)
            {
                foreach (var item in _selectedFunctionDtos)
                {
                    await ClientFunctionService.Delete(this.Options.Id, item.Id);
                }
                MessageService.Success(Localizer.Combination(nameof(SharedLocalResource.Delete), nameof(SharedLocalResource.Success)));
                await OnInitializedAsync();
            }
        }
        /// <summary>
        /// 点击显示关联按钮
        /// </summary>
        private async Task OnShowFunctionAddPageClick(Guid id)
        {
            await this.OpenOperationDialogAsync<ClientFunctionEdit, ClientFunctionEditOption, bool>(
                $"{Localizer["BindingApi"]}-[{this.Options.Name}]",
                     new ClientFunctionEditOption { Id = id, Type = 1 },
                     width: 1200,
                     onClose: async result =>
                     {
                         if (result)
                         {
                             await OnInitializedAsync();
                             await base.RefreshPageDom();
                         }
                     });

        }
        /// <summary>
        /// 点击关联选中按钮
        /// </summary>
        private async Task OnFunctionAddClick()
        {
            if (_selectedFunctionDtos == null || _selectedFunctionDtos.Count <= 0)
            {
                MessageService.Warn(Localizer[nameof(SharedLocalResource.NoRowsAreSelected)]);
                return;
            }

            bool result = await ClientFunctionService.Add(_selectedFunctionDtos.Select(x =>
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
                MessageService.Success(Localizer.Combination(nameof(SharedLocalResource.Binding), nameof(SharedLocalResource.Success)));
                await this.CloseAsync(true);
            }
            else
            {
                MessageService.Error(Localizer.Combination(nameof(SharedLocalResource.Binding), nameof(SharedLocalResource.Fail)));
            }
        }
    }
}
