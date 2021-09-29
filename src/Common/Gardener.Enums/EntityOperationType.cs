// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace Gardener.Enums
{
    /// <summary>
    /// 数据实体操作类型
    /// </summary>
    public enum EntityOperationType
    {
        /// <summary>
        /// 添加
        /// </summary>
        [Description("添加")]
        Add,

        /// <summary>
        /// 修改
        /// </summary>
        [Description("修改")]
        Update,

        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete
    }
}
