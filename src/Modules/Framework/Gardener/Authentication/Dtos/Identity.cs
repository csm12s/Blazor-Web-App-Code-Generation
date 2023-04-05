// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Enums;
using System.ComponentModel;

namespace Gardener.Authentication.Dtos
{
    /// <summary>
    /// 身份信息
    /// </summary>
    [Description("身份信息")]
    public class Identity
    {
        /// <summary>
        /// 身份唯一编号
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// 身份唯一名称
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// 身份昵称
        /// </summary>
        public string? NickName { get; set; }
        /// <summary>
        /// 身份类型
        /// </summary>
        public IdentityType IdentityType { get; set; }
        /// <summary>
        /// 客户端类型
        /// </summary>
        public LoginClientType LoginClientType { get; set; }
        /// <summary>
        /// 获取或设置 登录Id(每次登录该Id自动生成)
        /// </summary>
        public string LoginId { get; set; } = null!;
        /// <summary>
        /// 租户编号
        /// </summary>
        public int TenantId { get; set; } = 0;
    }
}
