// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;
using Gardener.Client.Base;

namespace Gardener.Client.AntDesignUi.Base.Components
{

    /// <summary>
    /// table列表基类(可以被当作OperationDialog打开，也能快速打开其它操作框)-支持多租户
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TOperationDialog">操作弹框页</typeparam>
    /// <typeparam name="TOperationDialogInput">操作弹框页输入参数</typeparam>
    /// <typeparam name="TOperationDialogOutput">操作弹框页输出参数</typeparam>
    /// <typeparam name="TLocalResource">本地化资源</typeparam>
    /// <typeparam name="TSelfOperationDialogInput">自身作为OperationDialog接收的参数</typeparam>
    /// <typeparam name="TSelfOperationDialogOutput">自身作为OperationDialog返回的参数</typeparam>
    /// <remarks>
    /// 包含列表加载、删除、导出、种子数据、添加、修改、详情
    /// 快递打开操作框，输入 OperationDialogInput_TKey
    /// 快递打开操作框，输出 OperationDialogOutput_TKey
    /// </remarks>
    public abstract class ListOperateTableBase<TDto, TKey, TOperationDialog, TOperationDialogInput, TOperationDialogOutput, TLocalResource, TSelfOperationDialogInput, TSelfOperationDialogOutput> : ListTableBase<TDto, TKey, TLocalResource, TSelfOperationDialogInput, TSelfOperationDialogOutput>
        where TDto : class, new()
        where TOperationDialog : OperationDialogBase<TOperationDialogInput, TOperationDialogOutput, TLocalResource>
        where TOperationDialogInput : OperationDialogInput<TKey>, new()
        where TOperationDialogOutput : OperationDialogOutput, new()
    {
        /// <summary>
        /// 点击添加按钮
        /// </summary>
        protected virtual async Task OnClickAdd()
        {
            TOperationDialogInput input = new TOperationDialogInput() { Type = OperationDialogInputType.Add };
            if (!await OnClickAddRunBefore(input))
            {
                return;
            }
            Func<TOperationDialogOutput?, Task> onClose = async (result) =>
            {
                if (result != null && result.Succeeded)
                {
                    //刷新列表
                    await ReLoadTable(false, true);
                }
                await OnAddDialogCloseAfter(input, result);
            };

            await OpenOperationDialogAsync<TOperationDialog, TOperationDialogInput, TOperationDialogOutput>(Localizer[SharedLocalResourceKeys.Add], input, onClose);
        }

        /// <summary>
        /// 当点击添加时，执行之前拦截处理
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>
        /// 可用于处理输入弹框的参数，和拦截弹框弹出
        /// </remarks>
        /// <returns></returns>
        protected virtual Task<bool> OnClickAddRunBefore(TOperationDialogInput input)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// 当添加框关闭时
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <remarks>
        /// 可用于处理关闭后操作
        /// </remarks>
        /// <returns></returns>
        protected virtual Task<bool> OnAddDialogCloseAfter(TOperationDialogInput input, TOperationDialogOutput? output)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// 点击编辑按钮
        /// </summary>
        /// <param name="model"></param>
        protected virtual async Task OnClickEdit(TKey id)
        {
            TOperationDialogInput input = new TOperationDialogInput() { Type = OperationDialogInputType.Edit, Data = id };
            if (!await OnClickEditRunBefore(input))
            {
                return;
            }
            Func<TOperationDialogOutput?, Task> onClose = async (result) =>
            {
                if (result != null && result.Succeeded)
                {
                    //刷新列表
                    await ReLoadTable(false, true);
                }
                await OnEditDialogCloseAfter(input, result);
            };
            await OpenOperationDialogAsync<TOperationDialog, TOperationDialogInput, TOperationDialogOutput>(Localizer[SharedLocalResourceKeys.Edit], input, onClose);
        }

        /// <summary>
        /// 当点击添加时，执行之前拦截处理
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>
        /// 可用于处理输入弹框的参数，和拦截弹框弹出
        /// </remarks>
        /// <returns></returns>
        protected virtual Task<bool> OnClickEditRunBefore(TOperationDialogInput input)
        {
            return Task.FromResult(true);
        }
        /// <summary>
        /// 当编辑框关闭时
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <remarks>
        /// 可用于处理关闭后操作
        /// </remarks>
        /// <returns></returns>
        protected virtual Task<bool> OnEditDialogCloseAfter(TOperationDialogInput input, TOperationDialogOutput? output)
        {

            return Task.FromResult(true);
        }
        /// <summary>
        /// 点击编辑按钮
        /// </summary>
        /// <param name="roleDto"></param>
        protected virtual async Task OnClickDetail(TKey id)
        {
            TOperationDialogInput input = new TOperationDialogInput() { Type = OperationDialogInputType.Select, Data = id };
            if (!await OnClickDetailRunBefore(input))
            {
                return;
            }
            await OpenOperationDialogAsync<TOperationDialog, TOperationDialogInput, TOperationDialogOutput>(Localizer[SharedLocalResourceKeys.Detail], input, output => { return OnDetailDialogCloseAfter(input, output); } );
        }

        /// <summary>
        /// 当点击添加时，执行之前拦截处理
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>
        /// 可用于处理输入弹框的参数，和拦截弹框弹出
        /// </remarks>
        /// <returns></returns>
        protected virtual Task<bool> OnClickDetailRunBefore(TOperationDialogInput input)
        {
            return Task.FromResult(true);
        }
        /// <summary>
        /// 当详情框关闭时
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <remarks>
        /// 可用于处理关闭后操作
        /// </remarks>
        /// <returns></returns>
        protected virtual Task<bool> OnDetailDialogCloseAfter(TOperationDialogInput input, TOperationDialogOutput? output)
        {
            return Task.FromResult(true);
        }
    }

    /// <summary>
    /// table列表基类(可以被当作OperationDialog打开，也能快速打开其它操作框)-支持多租户
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TOperationDialog">操作弹框页</typeparam>
    /// <typeparam name="TLocalResource">本地化资源</typeparam>
    /// <typeparam name="TSelfOperationDialogInput">自身作为OperationDialog接收的参数</typeparam>
    /// <typeparam name="TSelfOperationDialogOutput">自身作为OperationDialog返回的参数</typeparam>
    /// <remarks>
    /// 包含列表加载、删除、导出、种子数据、添加、修改、详情
    /// 快递打开操作框，输入 OperationDialogInput_TKey
    /// 快递打开操作框，输出 OperationDialogOutput_TKey
    /// </remarks>
    public abstract class ListOperateTableBase<TDto, TKey, TOperationDialog, TLocalResource, TSelfOperationDialogInput, TSelfOperationDialogOutput> : ListOperateTableBase<TDto, TKey, TOperationDialog, OperationDialogInput<TKey>, OperationDialogOutput<TKey>, TLocalResource, TSelfOperationDialogInput, TSelfOperationDialogOutput>
        where TDto : class, new()
        where TOperationDialog : OperationDialogBase<OperationDialogInput<TKey>, OperationDialogOutput<TKey>, TLocalResource>
    {
        /// <summary>
        /// 打开操作对话框
        /// </summary>
        /// <param name="title"></param>
        /// <param name="input"></param>
        /// <param name="onClose"></param>
        /// <param name="operationDialogSettings "></param>
        /// <returns></returns>
        protected Task OpenOperationDialogAsync(string title, OperationDialogInput<TKey> input, Func<OperationDialogOutput<TKey>?, Task>? onClose = null, OperationDialogSettings? operationDialogSettings = null)
        {
            OperationDialogSettings settings = operationDialogSettings ?? GetOperationDialogSettings();
            return OpenOperationDialogAsync<TOperationDialog, OperationDialogInput<TKey>, OperationDialogOutput<TKey>>(title, input, onClose, settings);
        }
    }

    /// <summary>
    /// table列表基类(可以被当作OperationDialog打开，也能快速打开其它操作框)-支持多租户
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TOperationDialog">操作弹框页</typeparam>
    /// <typeparam name="TLocalResource">本地化资源</typeparam>
    /// <remarks>
    /// 包含列表加载、删除、导出、种子数据、添加、修改、详情
    /// 快递打开操作框，输入 OperationDialogInput_TKey
    /// 快递打开操作框，输出 OperationDialogOutput_TKey
    /// 
    /// 此基类方便那些不需要弹出或弹出时没有输入输出时使用
    /// 自身作为OperationDialog接收的参数，默认为类型 <see cref="TKey"/>
    /// 自身作为OperationDialog返回的参数，默认为类型 <see cref="bool"/>
    /// </remarks>
    public abstract class ListOperateTableBase<TDto, TKey, TOperationDialog, TLocalResource> : ListOperateTableBase<TDto, TKey, TOperationDialog, TLocalResource, TKey, bool>
        where TDto : class, new()
        where TOperationDialog : OperationDialogBase<OperationDialogInput<TKey>, OperationDialogOutput<TKey>, TLocalResource>
    {
    }

    /// <summary>
    /// table列表基类(可以被当作OperationDialog打开，也能快速打开其它操作框)-支持多租户
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TOperationDialog">操作弹框页</typeparam>
    /// <remarks>
    /// 包含列表加载、删除、导出、种子数据、添加、修改、详情
    /// 本地化资源，默认使用<see cref="SharedLocalResourceKeys"/>
    /// 快递打开操作框，输入 OperationDialogInput_TKey
    /// 快递打开操作框，输出 OperationDialogOutput_TKey
    /// 
    /// 此基类方便那些不需要弹出或弹出时没有输入输出时使用
    /// 自身作为OperationDialog接收的参数，默认为类型 <see cref="TKey"/>
    /// 自身作为OperationDialog返回的参数，默认为类型 <see cref="bool"/>
    /// </remarks>
    public abstract class ListOperateTableBase<TDto, TKey, TOperationDialog> : ListOperateTableBase<TDto, TKey, TOperationDialog, SharedLocalResourceKeys>
        where TDto : class, new()
        where TOperationDialog : OperationDialogBase<OperationDialogInput<TKey>, OperationDialogOutput<TKey>, SharedLocalResourceKeys>
    {
    }
}
