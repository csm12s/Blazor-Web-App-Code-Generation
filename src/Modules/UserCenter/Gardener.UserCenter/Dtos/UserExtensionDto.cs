// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Gardener.UserCenter.Dtos
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
        [MaxLength(15, ErrorMessage = "最大长度不能大于{1}")]
        public string? QQ { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        [MaxLength(20, ErrorMessage = "最大长度不能大于{1}")]
        public string? WeChat { get; set; }
        /// <summary>
        /// 城市ID
        /// </summary>
        public int? CityId { get; set; }
    }
}
