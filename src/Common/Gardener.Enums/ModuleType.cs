// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------
using System.ComponentModel;

namespace Gardener.Enums
{
    /// <summary>
    /// 模块类型
    /// </summary>
    public enum ModuleType:int
    {
        /// <summary>
        /// 根节点
        /// </summary>
        [Description("根节点")]
        Root =0,
        /// <summary>
        /// 模块组
        /// </summary>
        [Description("模块组")]
        Group = 1000,
        /// <summary>
        /// 菜单
        /// </summary>
        [Description("菜单")]
        Menu = 1500,
        /// <summary>
        /// BUTTON
        /// </summary>
        [Description("按钮")]
        Button = 2000
    }
}
