// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Client.Base
{
    /// <summary>
    /// 操作框输入类型
    /// </summary>
    public enum OperationDialogInputType
    {
        /// <summary>
        /// 添加
        /// </summary>
        Add = 0,
        /// <summary>
        /// 编辑
        /// </summary>
        Edit = 1,
        /// <summary>
        /// 查看
        /// </summary>
        Select = 2,
        /// <summary>
        /// 其它
        /// </summary>
        Other = 99
    }
}
