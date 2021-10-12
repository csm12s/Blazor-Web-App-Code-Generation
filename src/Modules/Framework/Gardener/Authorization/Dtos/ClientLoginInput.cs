// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Authorization.Dtos
{
    /// <summary>
    /// 客户端登录输入
    /// </summary>
    public class ClientLoginInput
    {
        /// <summary>
        /// 客户端编号
        /// </summary>
        [DisplayName("客户端编号")]
        [Required]
        public Guid ClientId { get; set; }

        /// <summary>
        /// 密匙
        /// </summary>
        [Required, StringLength(64)]
        [DisplayName("密匙")]
        public string SecretKey { get; set; }
    }
}
