// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Client.Base
{
    /// <summary>
    /// 操作框输出
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class OperationDialogOutput<TData> : OperationDialogOutput
    {
        /// <summary>
        /// 数据
        /// </summary>
        public TData? Data { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static OperationDialogOutput<TData> Succeed(TData? data = default)
        {
            return new OperationDialogOutput<TData>() { Data = data, Succeeded = true, Type = OperationDialogOutputType.Succeeded };
        }
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static OperationDialogOutput<TData> Fail(TData? data = default)
        {
            return new OperationDialogOutput<TData>() { Data = data, Succeeded = false, Type = OperationDialogOutputType.Failed };
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static OperationDialogOutput<TData> Cancel(TData? data = default)
        {
            return new OperationDialogOutput<TData>() { Data = data, Succeeded = false, Type = OperationDialogOutputType.Canceled };
        }
    }
    /// <summary>
    /// 操作框输出
    /// </summary
    public class OperationDialogOutput
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Succeeded { get; set; }
        /// <summary>
        /// 操作框结束类型
        /// </summary>
        public OperationDialogOutputType Type { get; set; }
        /// <summary>
        /// 成功
        /// </summary>
        /// <returns></returns>
        public static OperationDialogOutput Succeed()
        {
            return new OperationDialogOutput() { Succeeded = true, Type = OperationDialogOutputType.Succeeded };
        }
        /// <summary>
        /// 失败
        /// </summary>
        /// <returns></returns>
        public static OperationDialogOutput Fail()
        {
            return new OperationDialogOutput() { Succeeded = false, Type = OperationDialogOutputType.Failed };
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <returns></returns>
        public static OperationDialogOutput Cancel()
        {
            return new OperationDialogOutput() { Succeeded = false, Type = OperationDialogOutputType.Canceled };
        }
    }
}
