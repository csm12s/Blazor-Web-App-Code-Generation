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
using Gardener.UserCenter.Services;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Gardener.Client.AntDesignUi.Base.Components
{
    /// <summary>
    /// 编辑，详情弹框-支持多租户
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TLocalResource">本地化资源类</typeparam>
    /// <typeparam name="TOperationDialogInput">弹框输入参数，需要继承 OperationDialogInput<TKey></typeparam>
    /// <typeparam name="TOperationDialogOutput"></typeparam>
    public class EditOperationDialogBase<TDto, TKey, TLocalResource, TOperationDialogInput, TOperationDialogOutput> : OperationDialogBase<TOperationDialogInput, TOperationDialogOutput, TLocalResource>
        where TDto : class, new()
        where TOperationDialogInput : OperationDialogInput<TKey>, new()
        where TOperationDialogOutput : OperationDialogOutput, new()
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
        /// 租户服务    
        /// </summary>
        [Inject]
        protected ITenantService tenantService { get; set; } = null!;
        /// <summary>
        /// 身份状态管理
        /// </summary>
        [Inject]
        protected IAuthenticationStateManager AuthenticationStateManager { get; set; } = null!;
        /// <summary>
        /// 当前正在编辑的对象
        /// </summary>
        protected TDto _editModel = new();

        /// <summary>
        /// 租户列表
        /// </summary>
        protected IEnumerable<SystemTenantDto>? _tenants { get; set; }

        /// <summary>
        /// 加载当前数据<see cref="EditOperationDialogBase{TDto, TKey, TLocalResource, TOperationDialogInput, TOperationDialogOutput}.LoadEditModelData"/>
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await StartLoading();
            var task1 = LoadEditModelData();
            if (!IsTenant())
            {
                await LoadTenants();
            }
            await task1;
            await base.OnInitializedAsync();
            await StopLoading();
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
                        MessageService.Error(Localizer[SharedLocalResourceKeys.DataNotFound]);
                    }
                }

            }
        }

        /// <summary>
        /// 加载租户数据
        /// </summary>
        /// <returns></returns>
        protected async Task LoadTenants()
        {
            _tenants = await tenantService.GetAll();

        }
        /// <summary>
        /// 是否是租户
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsTenant()
        {
            bool isTenant = AuthenticationStateManager.CurrentUserIsTenant();
            return isTenant;
        }

        /// <summary>
        /// 取消
        /// </summary>
        protected virtual Task OnFormCancel()
        {
            TOperationDialogOutput operationDialogOutput = new TOperationDialogOutput();
            operationDialogOutput.IsCancel();
            return CloseAsync(operationDialogOutput);
        }
        /// <summary>
        /// 表单完成时
        /// </summary>
        /// <param name="editContext"></param>
        /// <returns></returns>
        protected virtual async Task OnFormFinish(EditContext editContext)
        {
            await StartLoading();
            var operationDialogOutput = new OperationDialogOutput<TKey>();
            //开始请求
            if (this.Options.Type.Equals(OperationDialogInputType.Add))
            {
                //添加
                var result = await BaseService.Insert(_editModel);

                if (result != null)
                {
                    MessageService.Success(Localizer.Combination(SharedLocalResourceKeys.Add, SharedLocalResourceKeys.Success));
                    operationDialogOutput.IsSucceed(GetKey(result));
                    await CloseAsync(operationDialogOutput as TOperationDialogOutput);
                }
                else
                {
                    MessageService.Error(Localizer.Combination(SharedLocalResourceKeys.Add, SharedLocalResourceKeys.Fail));
                }
            }
            else
            {
                //修改
                var result = await BaseService.Update(_editModel);
                if (result)
                {
                    MessageService.Success(Localizer.Combination(SharedLocalResourceKeys.Edit, SharedLocalResourceKeys.Success));
                    operationDialogOutput.IsSucceed();
                    await CloseAsync(operationDialogOutput as TOperationDialogOutput);
                }
                else
                {
                    MessageService.Error(Localizer.Combination(SharedLocalResourceKeys.Edit, SharedLocalResourceKeys.Fail));
                }
            }
            await StopLoading();
        }
        /// <summary>
        /// 获取主键
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected virtual TKey GetKey(TDto dto)
        {
            if (dto is IModelId<TKey> temp)
            {
                return temp.Id;
            }
            else
            {
                throw new ArgumentException($"{Localizer[SharedLocalResourceKeys.Error]}:{typeof(TDto).Name} no implement {nameof(IModelId<TKey>)}");
            }
        }
    }

    /// <summary>
    /// 编辑，详情弹框-支持多租户
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TLocalResource">本地化资源类</typeparam>
    /// <remarks>
    /// <para>
    /// 弹框输入参数，默认是 <![CDATA[OperationDialogInput<Tkey>]]>
    /// </para>
    /// <para>
    /// 弹框输出参数，默认是 <see cref="OperationDialogOutput"/>
    /// </para>
    /// </remarks>
    public class EditOperationDialogBase<TDto, TKey, TLocalResource> : EditOperationDialogBase<TDto, TKey, TLocalResource, OperationDialogInput<TKey>, OperationDialogOutput<TKey>>
        where TDto : class, new()
    {

    }

    /// <summary>
    /// 编辑，详情弹框-支持多租户
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <remarks>
    /// <para>本地化资源类,默认是 <see cref="SharedLocalResourceKeys"/></para>
    /// <para>弹框输入参数，默认是 <![CDATA[OperationDialogInput<Tkey>]]></para>
    /// <para>弹框输出参数，默认是 <see cref="OperationDialogOutput"/></para>
    /// </remarks>
    public class EditOperationDialogBase<TDto, TKey> : EditOperationDialogBase<TDto, TKey, SharedLocalResourceKeys>
        where TDto : class, new()
    {

    }
}
