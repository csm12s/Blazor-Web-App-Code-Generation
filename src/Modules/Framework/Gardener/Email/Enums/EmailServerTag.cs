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
        /// 默认
        /// </summary>
        [Description("默认")]
        Base = 0,
        /// <summary>
        /// QQ邮箱
        /// </summary>
        [Description("QQ邮箱")]
        QQ,
        /// <summary>
        /// 163邮箱
        /// </summary>
        [Description("163邮箱")]
        E163,
        /// <summary>
        /// Gmail邮箱
        /// </summary>
        [Description("Gmail邮箱")]
        Gmail,
        /// <summary>
        /// 企业邮箱
        /// </summary>
        [Description("企业邮箱")]
        Enterprise,
        /// <summary>
        /// 活动
        /// </summary>
        [Description("活动邮箱")]
        Activity
    }
}
