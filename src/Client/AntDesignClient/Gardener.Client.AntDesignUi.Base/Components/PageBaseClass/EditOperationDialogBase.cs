// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.Client.Base;
using Gardener.Client.Base.Services;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Gardener.Client.AntDesignUi.Base.Components
{
    /// <summary>
    /// 编辑，详情弹框
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TLocalResource"></typeparam>
    public class EditOperationDialogBase<TDto, TKey, TLocalResource> : OperationDialogBase<OperationDialogInput<TKey>, OperationDialogOutput<TKey>, TLocalResource> where TDto : BaseDto<TKey>, new() where TLocalResource : SharedLocalResource
    {
        [Inject]
        protected IServiceBase<TDto, TKey> BaseService { get; set; } = null!;
        [Inject]
        protected IClientMessageService MessageService { get; set; } = null!;
        [Inject]
        protected ConfirmService ConfirmService { get; set; } = null!;
        [Inject]
        protected DrawerService DrawerService { get; set; } = null!;

        /// <summary>
        /// 当前正在编辑的对象
        /// </summary>
        protected TDto _editModel = new();
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            StartLoading();
            await LoadEditModelData();
            StopLoading();
        }
        /// <summary>
        /// 加载编辑对象数据
        /// </summary>
        /// <returns></returns>
        protected async Task LoadEditModelData()
        {
            if (this.Options.Type.Equals(OperationDialogInputType.Edit) || this.Options.Type.Equals(OperationDialogInputType.Select))
            {
                TKey? id = this.Options.Data;
                if (id != null)
                {
                    //更新 回填数据
                    var model = await BaseService.Get(id);
                    if (model != null)
                    {
                        //赋值给编辑对象
                        model.Adapt(_editModel);
                    }
                    else
                    {
                        MessageService.Error(Localizer[SharedLocalResource.DataNotFound]);
                    }
                }

            }
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
            StartLoading();
            //开始请求
            if (this.Options.Type.Equals(OperationDialogInputType.Add))
            {
                //添加
                var result = await BaseService.Insert(_editModel);

                if (result != null)
                {
                    MessageService.Success(Localizer.Combination(SharedLocalResource.Add, SharedLocalResource.Success));
                    await base.FeedbackRef.CloseAsync(OperationDialogOutput<TKey>.Succeed(result.Id));
                }
                else
                {
                    MessageService.Error(Localizer.Combination(SharedLocalResource.Add, SharedLocalResource.Fail));
                }
            }
            else
            {
                //修改
                var result = await BaseService.Update(_editModel);
                if (result)
                {
                    MessageService.Success(Localizer.Combination(SharedLocalResource.Edit, SharedLocalResource.Success));
                    await base.FeedbackRef.CloseAsync(OperationDialogOutput<TKey>.Succeed(_editModel.Id));
                }
                else
                {
                    MessageService.Error(Localizer.Combination(SharedLocalResource.Edit, SharedLocalResource.Fail));
                }
            }
            StopLoading();
        }
    }

    /// <summary>
    /// 编辑，详情弹框
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class EditOperationDialogBase<TDto, TKey> : EditOperationDialogBase<TDto, TKey, SharedLocalResource> where TDto : BaseDto<TKey>, new()
    {

    }
}
