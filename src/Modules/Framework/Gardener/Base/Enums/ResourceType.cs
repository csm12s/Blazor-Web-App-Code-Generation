// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------
using Gardener.Attributes;
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
        [Description("根节点")]
        [IgnoreOnConvertToMap]
        Root = 0,
        /// <summary>
        /// 菜单
        /// </summary>
        [Description("菜单")]
        Menu = 1000,
        /// <summary>
        /// 操作
        /// </summary>
        [Description("操作")]
        Action = 2000,
        /// <summary>
        /// 视图
        /// </summary>
        [Description("视图")]
        View = 3000,
    }
}
