// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Client.Base
{
    /// <summary>
    /// 弹出操作框输出参数
    /// </summary>
    /// <typeparam name="TData">数据类型</typeparam>
    public class OperationDialogInput<TData> : OperationDialogInput
    {
        /// <summary>
        /// 数据
        /// </summary>
        public TData? Data { get; set; } = default;

        /// <summary>
        /// 添加操作
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static OperationDialogInput<TData> IsAdd(TData? data = default)
        {
            return new OperationDialogInput<TData> { Data = data, Type = OperationDialogInputType.Add };
        }
        /// <summary>
        /// 编辑操作
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static OperationDialogInput<TData> IsEdit(TData? data = default)
        {
            return new OperationDialogInput<TData> { Data = data, Type = OperationDialogInputType.Edit };
        }

        /// <summary>
        /// 查询操作
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static OperationDialogInput<TData> IsSelect(TData? data = default)
        {
            return new OperationDialogInput<TData> { Data = data, Type = OperationDialogInputType.Select };
        }

        /// <summary>
        /// 其它操作
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static OperationDialogInput<TData> Other(TData? data = default)
        {
            return new OperationDialogInput<TData> { Data = data, Type = OperationDialogInputType.Other };
        }


    }

    /// <summary>
    /// 弹出操作框输出参数
    /// </summary>
    public class OperationDialogInput
    {
        /// <summary>
        /// 操作框输入类型
        /// </summary>
        /// <remarks>
        /// 默认<see cref="OperationDialogInputType.Other"/>
        /// </remarks>
        public OperationDialogInputType Type { get; set; } = OperationDialogInputType.Other;

        /// <summary>
        /// 添加操作
        /// </summary>
        /// <returns></returns>
        public static OperationDialogInput IsAdd()
        {
            return new OperationDialogInput { Type = OperationDialogInputType.Add };
        }
        /// <summary>
        /// 编辑操作
        /// </summary>
        /// <returns></returns>
        public static OperationDialogInput IsEdit()
        {
            return new OperationDialogInput { Type = OperationDialogInputType.Edit };
        }

        /// <summary>
        /// 查询操作
        /// </summary>
        /// <returns></returns>
        public static OperationDialogInput IsSelect()
        {
            return new OperationDialogInput { Type = OperationDialogInputType.Select };
        }

        /// <summary>
        /// 其它操作
        /// </summary>
        /// <returns></returns>
        public static OperationDialogInput Other()
        {
            return new OperationDialogInput { Type = OperationDialogInputType.Other };
        }

    }
}
