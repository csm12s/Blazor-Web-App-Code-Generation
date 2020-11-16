// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using System;

namespace Gardener.Application.Dtos
{
    /// <summary>
    /// 用户扩展数据
    /// </summary>
    public class UserExtensionDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        public string WeChat { get; set; }
        /// <summary>
        /// 城市ID
        /// </summary>
        public int? CityId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserDto User { get; set; }
    }
}
