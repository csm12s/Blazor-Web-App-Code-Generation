// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace Gardener.Email.Enums
{
    /// <summary>
    /// 邮件服务器标签
    /// 随便自定义
    /// </summary>
    public enum EmailServerTag
    {
        /// <summary>
        /// 基础
        /// </summary>
        [Description("基础")]
        Base = 0,
        /// <summary>
        /// 活动
        /// </summary>
        [Description("活动")]
        Activity,
        /// <summary>
        /// QQ
        /// </summary>
        [Description("QQ")]
        QQ,
        /// <summary>
        /// Gmail
        /// </summary>
        [Description("Gmail")]
        Gmail


    }
}
