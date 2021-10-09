// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Base;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace Gardener.Client.Base
{
    public class EditDrawerBase<TDto, TKey> : FeedbackComponent<DrawerInput<TKey>, DrawerOutput<TKey>> where TDto : BaseDto<TKey>, new()
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
                    messageService.Error(localizer["not_find_data"]);
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
            await base.FeedbackRef.CloseAsync(DrawerOutput<TKey>.Cancel());
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
                    messageService.Success(localizer.Combination("add", "success"));
                    await base.FeedbackRef.CloseAsync(DrawerOutput<TKey>.Succeed(result.Id));
                }
                else
                {
                    messageService.Error(localizer.Combination("add", "fail"));
                }
            }
            else
            {
                //修改
                var result = await _service.Update(_editModel);
                if (result)
                {
                    messageService.Success(localizer.Combination("edit", "success"));
                    await base.FeedbackRef.CloseAsync(DrawerOutput<TKey>.Succeed(_editModel.Id));
                }
                else
                {
                    messageService.Error(localizer.Combination("edit", "fail"));
                }
            }
            _isLoading = false;
        }
    }
}
