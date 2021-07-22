// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Application.Dtos;
using Gardener.Application.Interfaces.SystemManager;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client.Pages.SystemManager.PositionManagementView
{
    public partial class PositionEdit : FeedbackComponent<Guid, bool>
    {
        private bool _isLoading = false;
        private PositionDto _editModel = new PositionDto();
        [Inject]
        MessageService messageService { get; set; }
        [Inject]
        IPositionService positionService { get; set; }
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            _isLoading = true;

            Guid id = this.Options;
            if (id != Guid.Empty)
            {
                //更新 回填数据
                var model = await positionService.Get(id);
                if (model != null)
                {
                    //赋值给编辑对象
                    model.Adapt(_editModel);
                }
                else
                {
                    messageService.Error("角色不存在");
                }
            }
            _isLoading = false;
            await base.OnInitializedAsync();
        }

        /// <summary>
        /// 表单完成时
        /// </summary>
        /// <param name="editContext"></param>
        /// <returns></returns>
        private async Task OnFormFinish(EditContext editContext)
        {
            _isLoading = true;
            //开始请求
            if (_editModel.Id == Guid.Empty)
            {
                //添加
                var result = await positionService.Insert(_editModel);

                if (result != null)
                {
                    messageService.Success("添加成功");
                    DrawerRef<bool> drawerRef = base.FeedbackRef as DrawerRef<bool>;
                    await drawerRef!.CloseAsync(true);
                }
                else
                {
                    messageService.Error("添加失败");
                }
            }
            else
            {
                //修改
                var result = await positionService.Update(_editModel);
                if (result)
                {
                    messageService.Success("修改成功");
                    DrawerRef<bool> drawerRef = base.FeedbackRef as DrawerRef<bool>;
                    await drawerRef!.CloseAsync(true);
                }
                else
                {
                    messageService.Error("修改失败");
                }
            }
            _isLoading = false;
        }
        /// <summary>
        /// 取消
        /// </summary>
        private async Task OnFormCancel()
        {
            await (base.FeedbackRef as DrawerRef<bool>).CloseAsync(true);
        }
    }
}
