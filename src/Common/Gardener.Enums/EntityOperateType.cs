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
    public enum EntityOperateType
    {
        /// <summary>
        /// 添加
        /// </summary>
        [Description("添加")]
        Insert,
        /// <summary>
        /// 修改
        /// </summary>
        [Description("修改")]
        Update,
        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete,
        /// <summary>
        /// 批量删除
        /// </summary>
        [Description("批量删除")]
        Deletes,
        /// <summary>
        /// 逻辑删除
        /// </summary>
        [Description("逻辑删除")]
        FakeDelete,
        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        [Description("批量逻辑删除")]
        FakeDeletes,
        /// <summary>
        /// 锁定
        /// </summary>
        [Description("锁定")]
        Lock
    }
}
