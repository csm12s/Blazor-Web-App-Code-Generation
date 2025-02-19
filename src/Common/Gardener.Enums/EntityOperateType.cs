﻿// -----------------------------------------------------------------------------
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
        [Description("Insert")]
        Insert,
        /// <summary>
        /// 修改
        /// </summary>
        [Description("Update")]
        Update,
        /// <summary>
        /// 删除
        /// </summary>
        [Description("Delete")]
        Delete,
        /// <summary>
        /// 批量删除
        /// </summary>
        [Description("BatchDelete")]
        Deletes,
        /// <summary>
        /// 逻辑删除
        /// </summary>
        [Description("FakeDelete")]
        FakeDelete,
        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        [Description("BatchFakeDelete")]
        FakeDeletes,
        /// <summary>
        /// 锁定
        /// </summary>
        [Description("Lock")]
        Lock
    }
}
