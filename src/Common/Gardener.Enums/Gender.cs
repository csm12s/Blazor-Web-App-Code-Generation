﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Attributes;
using System.ComponentModel;

namespace Gardener.Enums
{
    /// <summary>
    /// 性别枚举
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        [TagColor(ClientAntPresetColor.Blue)]
        Male,

        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        [TagColor(ClientAntPresetColor.Magenta)]
        Female
    }
}