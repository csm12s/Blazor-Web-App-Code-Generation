// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace Gardener.Enums
{
    /// <summary>
    /// 权限类型
    /// </summary>
    public enum ResourceType:int
    {
        /// <summary>
        /// 根节点
        /// </summary>
        [Description("根节点")]
        ROOT =-999,
        /// <summary>
        /// API
        /// </summary>
        [Description("接口")]
        API=0,
        /// <summary>
        /// BUTTON
        /// </summary>
        [Description("按钮")]
        BUTTON=1 ,
        /// <summary>
        /// 菜单
        /// </summary>
        [Description("菜单")]
        MENU =2,
        
    }
}
