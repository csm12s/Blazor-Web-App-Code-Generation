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
        [Inject]
        protected IServiceBase<TDto, TKey> _service { get; set; } = null!;
        [Inject]
        protected MessageService messageService { get; set; } = null!;
        [Inject]
        protected ConfirmService confirmService { get; set; } = null!;
        [Inject]
        protected DrawerService drawerService { get; set; } = null!;
        [Inject]
        protected IClientLocalizer<TLocalResource> localizer { get; set; } = null!;
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
                    var model = await _service.Get(id);
                    if (model != null)
                    {
                        //赋值给编辑对象
                        model.Adapt(_editModel);
                    }
                    else
                    {
    #pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                        messageService.Error(localizer[SharedLocalResource.DataNotFound]);
    #pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
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
                var result = await _service.Insert(_editModel);

                if (result != null)
                {
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                    messageService.Success(localizer.Combination(SharedLocalResource.Add, SharedLocalResource.Success));
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                    await base.FeedbackRef.CloseAsync();//OperationDialogOutput<TKey>.Succeed(result.Id)
                }
                else
                {
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                    messageService.Error(localizer.Combination(SharedLocalResource.Add, SharedLocalResource.Fail));
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                }
            }
            else
            {
                //修改
                var result = await _service.Update(_editModel);
                if (result)
                {
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                    messageService.Success(localizer.Combination(SharedLocalResource.Edit, SharedLocalResource.Success));
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                    await base.FeedbackRef.CloseAsync();//OperationDialogOutput<TKey>.Succeed(_editModel.Id)
                }
                else
                {
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                    messageService.Error(localizer.Combination(SharedLocalResource.Edit, SharedLocalResource.Fail));
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                }
            }
            _isLoading = false;
        }

    }
}
