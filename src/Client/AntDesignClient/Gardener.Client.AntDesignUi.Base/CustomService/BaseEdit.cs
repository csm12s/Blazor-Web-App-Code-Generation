// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.Base;
using Gardener.Client.Base.Services;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Gardener.Client.AntDesignUi.Base.CustomService
{
    /// <summary>
    /// 编辑，详情弹框（无主键表，TDto不继承BaseDto）
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class BaseEdit<TDto, TKey, TLocalResource> 
        : OperationDialogBase<OperationDialogInput<TKey>, OperationDialogOutput<TKey>> 
        where TDto : class, new()
    {
        /// <summary>
        /// 当前对象的基础服务
        /// </summary>
        [Inject]
        protected IServiceBase<TDto, TKey> BaseService { get; set; } = null!;
        /// <summary>
        /// 消息提示服务
        /// </summary>
        [Inject]
        protected IClientMessageService MessageService { get; set; } = null!;
        /// <summary>
        /// 确认提示服务
        /// </summary>
        [Inject]
        protected ConfirmService ConfirmService { get; set; } = null!;
        /// <summary>
        /// 本地化服务
        /// </summary>
        [Inject]
        protected IClientLocalizer<TLocalResource> Localizer { get; set; } = null!;
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
                TKey? id = this.Options.Id;
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
            //TODO: 无主键表编辑，未进入这里
            _isLoading = true;
            //开始请求
            if (this.Options.Type.Equals(DrawerInputType.Add))
            {
                //添加
                var result = await BaseService.Insert(_editModel);

                if (result != null)
                {
                    MessageService.Success(Localizer.Combination(SharedLocalResource.Add, SharedLocalResource.Success));
                    await base.FeedbackRef.CloseAsync();//OperationDialogOutput<TKey>.Succeed(result.Id)
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
                    await base.FeedbackRef.CloseAsync();//OperationDialogOutput<TKey>.Succeed(_editModel.Id)
                }
                else
                {
                    MessageService.Error(Localizer.Combination(SharedLocalResource.Edit, SharedLocalResource.Fail));
                }
            }
            _isLoading = false;
        }

    }
}
