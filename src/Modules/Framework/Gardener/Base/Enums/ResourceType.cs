﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------
using System.ComponentModel;

namespace Gardener.Base.Enums
{
    /// <summary>
    /// 权限类型
    /// </summary>
    public enum ResourceType : int
    {
        /// <summary>
        /// 根节点
        /// </summary>
        [Description("Root")]
        //[IgnoreOnConvertToMap]
        Root = 0,
        /// <summary>
        /// 菜单
        /// </summary>
        [Description("Menu")]
        Menu = 1000,
        /// <summary>
        /// 操作
        /// </summary>
        [Description("Action")]
        Action = 2000,
        /// <summary>
        /// 视图
        /// </summary>
        [Description("View")]
        View = 3000,
    }
}
