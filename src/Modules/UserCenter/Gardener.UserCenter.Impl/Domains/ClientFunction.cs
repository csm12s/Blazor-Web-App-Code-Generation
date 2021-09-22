// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.UserCenter.Impl.Domains
{
    /// <summary>
    /// 客户端功能信息
    /// </summary>
    [Description("客户端功能信息")]
    public class ClientFunction : IEntity
    {
        /// <summary>
        /// 客户端编号
        /// </summary>
        [Required]
        [DisplayName("客户端编号")]
        public Guid ClientId { get; set; }

        /// <summary>
        /// 客户端
        /// </summary>
        [DisplayName("客户端")]
        public Client Client { get; set; }

        /// <summary>
        /// 功能Id
        /// </summary>
        [Required]
        [DisplayName("功能编号")]
        public Guid FunctionId { get; set; }

        /// <summary>
        /// 功能
        /// </summary>
        [DisplayName("功能")]
        public Function Function { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;
        
    }
}
