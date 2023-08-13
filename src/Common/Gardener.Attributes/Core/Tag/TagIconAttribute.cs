// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;

namespace Gardener.Attributes
{
    /// <summary>
    /// 标签Icon
    /// </summary>
    public class TagIconAttribute : Attribute
    {
        /// <summary>
        /// 标签Icon
        /// </summary>
        /// <param name="icon"></param>
        /// <param name="theme">'fill' | 'outline' | 'twotone'</param>
        /// <param name="spin"></param>
        public TagIconAttribute(string icon, string? theme=null,bool spin=false)
        {
            this.Icon = icon;
            this.Theme = theme;
            this.Spin = spin;
        }

        /// <summary>
        /// 标签Icon
        /// </summary>
        /// <param name="icon">标签Icon</param>
        public TagIconAttribute(string icon)
        {
            Icon = icon;
        }


        /// <summary>
        /// 标签Icon
        /// </summary>
        public string Icon { get; private set; }

        /// <summary>
        /// Set the tag's icon theme
        /// </summary>
        /// <remarks>'fill' | 'outline' | 'twotone'</remarks>
        public string? Theme { get; private set; }

        /// <summary>
        /// Set the tag's icon spin
        /// </summary>
        public bool Spin { get; private set; } = false;
    }
}
