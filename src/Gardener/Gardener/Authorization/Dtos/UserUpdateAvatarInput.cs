// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Gardener.Authorization.Dtos
{
    /// <summary>
    /// 更新头像
    /// </summary>
    public class UserUpdateAvatarInput
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 头像
        /// </summary>
        [MaxLength(100, ErrorMessage = "最大长度不能大于{1}")]
        public string Avatar { get; set; }
    }
}
