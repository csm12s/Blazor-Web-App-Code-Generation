// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Base;
using Gardener.Client.Base.Constants;
using Gardener.Client.Base.Services;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Threading.Tasks;

namespace Gardener.Client.Base
{
    public class OperationDialogBase<TDto, TKey> : FeedbackComponent<OperationDialogInput<TKey>, OperationDialogOutput<TKey>> where TDto : BaseDto<TKey>, new()
    {
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
        /// 操作对话框
        /// </summary>
        [Inject]
        protected IOperationDialogService operationDialogService { get; set; }
        /// <summary>
        /// 编辑区域的加载中标识
        /// </summary>
        protected bool _isLoading = false;
        /// <summary>
        /// 当前正在编辑的对象
        /// </summary>
        protected TDto _editModel = new TDto();

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            _isLoading = true;

            if (this.Options.Type.Equals(DrawerInputType.Edit) || this.Options.Type.Equals(DrawerInputType.Select))
            {
                TKey id = this.Options.Id;

                //更新 回填数据
                var model = await _service.Get(id);
                if (model != null)
                {
                    //赋值给编辑对象
                    model.Adapt(_editModel);
                }
                else
                {
                    messageService.Error(localizer["数据未找到"]);
                }
            }
            _isLoading = false;
            await base.OnInitializedAsync();
        }

        /// <summary>
        /// 取消
        /// </summary>
        protected virtual async Task OnFormCancel()
        {
            await base.FeedbackRef.CloseAsync(OperationDialogOutput<TKey>.Cancel());
        }

        /// <summary>
        /// 表单完成时
        /// </summary>
        /// <param name="editContext"></param>
        /// <returns></returns>
        protected virtual async Task OnFormFinish(EditContext editContext)
        {
            _isLoading = true;
            //开始请求
            if (this.Options.Type.Equals(DrawerInputType.Add))
            {
                //添加
                var result = await _service.Insert(_editModel);

                if (result != null)
                {
                    messageService.Success(localizer.Combination("添加", "成功"));
                    await base.FeedbackRef.CloseAsync(OperationDialogOutput<TKey>.Succeed(result.Id));
                }
                else
                {
                    messageService.Error(localizer.Combination("添加", "失败"));
                }
            }
            else
            {
                //修改
                var result = await _service.Update(_editModel);
                if (result)
                {
                    messageService.Success(localizer.Combination("编辑", "成功"));
                    await base.FeedbackRef.CloseAsync(OperationDialogOutput<TKey>.Succeed(_editModel.Id));
                }
                else
                {
                    messageService.Error(localizer.Combination("编辑", "失败"));
                }
            }
            _isLoading = false;
        }


        /// <summary>
        /// 获取操作会话配置
        /// </summary>
        /// <returns></returns>
        protected virtual OperationDialogSettings GetOperationDialogSettings()
        {
            OperationDialogSettings dialogSettings = new OperationDialogSettings();
            ClientConstant.DefaultOperationDialogSettings.Adapt(dialogSettings);
            SetOperationDialogSettings(dialogSettings);
            return dialogSettings;
        }

        /// <summary>
        /// 设置操作会话配置
        /// </summary>
        /// <param name="dialogSettings"></param>
        protected virtual void SetOperationDialogSettings(OperationDialogSettings dialogSettings)
        {
            //set OperationDialogSettings
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
}
