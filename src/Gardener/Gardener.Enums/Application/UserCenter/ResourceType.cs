﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------
using Gardener.Common;
using System.ComponentModel;

namespace Gardener.Enums
{
    /// <summary>
    /// 权限类型
    /// 资源子级类型只能>=父级类型
    /// </summary>
    public enum ResourceType:int
    {
        /// <summary>
        /// 根节点
        /// </summary>
        [Description("根节点")]
        [IgnoreConvert]
        ROOT =0,
        /// <summary>
        /// 菜单
        /// </summary>
        [Description("客户端")]
        CLIENT = 500,
        /// <summary>
        /// 菜单
        /// </summary>
        [Description("菜单")]
        MENU = 1000,
        /// <summary>
        /// BUTTON
        /// </summary>
        [Description("按钮")]
        BUTTON = 2000,
        /// <summary>
        /// API
        /// </summary>
        [Description("接口")]
        API=3000,
    }
}