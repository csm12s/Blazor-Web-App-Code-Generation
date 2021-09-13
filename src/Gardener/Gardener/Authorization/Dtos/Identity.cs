// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authorization.Enums;
using Gardener.Enums;
using System.ComponentModel;

namespace Gardener.Authorization.Dtos
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
        public string Id { get; set; }
        /// <summary>
        /// 身份唯一名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 身份昵称
        /// </summary>
        public string GivenName { get; set; }
        /// <summary>
        /// 身份类型
        /// </summary>
        public IdentityType IdentityType { get; set; }
        /// <summary>
        /// 客户端类型
        /// </summary>
        public LoginClientType LoginClientType { get; set; }
        /// <summary>
        /// 获取或设置 客户端Id
        /// </summary>
        public string ClientId { get; set; }
    }
}
