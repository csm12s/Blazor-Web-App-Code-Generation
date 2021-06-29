// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client.Pages.SystemManager
{
    public class ResourceFunctionEditOption
    {
        /// <summary>
        /// 选中的资源id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 0 展示
        /// 1 添加
        /// </summary>
        public int Type { get; set; }
    }
    public partial class ResourceFunctionEdit : FeedbackComponent<ResourceFunctionEditOption, bool>
    {
        [Inject]
        IFunctionService functionService { get; set; }
        [Inject]
        IResourceService resourceService { get; set; }
        [Inject]
        MessageService messageService { get; set; }

        [Inject]
        DrawerService drawerService { get; set; }
        [Inject]
        NotificationService noticeService { get; set; }
        private List<FunctionDto> _functionDtos = new List<FunctionDto>();
        private List<FunctionDto> _selectedFunctionDtos = new List<FunctionDto>();
        private bool _loading = false;
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            //根据资源编号获取关联的接口
            List<FunctionDto> _oldFunctionDtos = await resourceService.GetFunctions(this.Options.Id);
            if (this.Options.Type == 0)
            {
                //根据资源编号获取关联的接口
                _functionDtos = _oldFunctionDtos;
            }
            else if (this.Options.Type == 1)
            {
                //查看可用的接口
                _functionDtos=await functionService.GetEffective();
                //选择的
                _selectedFunctionDtos = _oldFunctionDtos;
            }
            
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
        }
        /// <summary>
        /// 点击显示关联按钮
        /// </summary>
        private async Task OnShowFunctionAddPageClick(Guid id)
        {
            var result = await drawerService.CreateDialogAsync<ResourceFunctionEdit, ResourceFunctionEditOption, bool>(
                     new ResourceFunctionEditOption { Id = id, Type = 1 },
                     true,
                     title: "关联接口",
                     width: 1200,
                     placement: "right");
        }
        /// <summary>
        /// 点击关联选中按钮
        /// </summary>
        private async Task OnFunctionAddClick()
        {
            if (_selectedFunctionDtos == null || _selectedFunctionDtos.Count <= 0)
            {
                messageService.Warn("请选择至少一项");
                return;
            }
        }
    }
}
