// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 新闻信息
    /// </summary>
    [Description("新闻信息")]
    public class News : GardenerEntityBase<long>
    {
        /// <summary>
        /// 标题
        /// </summary>
        [MaxLength(100), Required]
        [DisplayName("标题")]
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [MaxLength(1000), Required]
        [DisplayName("内容")]
        public string Content { get; set; }
    }
}
