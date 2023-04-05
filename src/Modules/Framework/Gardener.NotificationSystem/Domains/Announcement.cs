using Gardener.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.NotificationSystem.Domains
{
    /// <summary>
    /// 公告信息
    /// </summary>
    [Description("公告信息")]
    public class Announcement : GardenerEntityBase
    {
        /// <summary>
        /// 标题
        /// </summary>
        [MaxLength(100), Required]
        [DisplayName("标题")]
        public string Title { get; set; } = null!;

        /// <summary>
        /// 概述
        /// </summary>
        [MaxLength(500), Required]
        [DisplayName("概述")]
        public string? Summary { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [MaxLength(5000)]
        [DisplayName("内容")]
        public string? Content { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        [DisplayName("是否置顶")]
        public bool FixTop { get; set; }

        /// <summary>
        /// 反对数量
        /// </summary>
        [DisplayName("反对数量")]
        public int OpposeCount { get; set; }

        /// <summary>
        /// 赞同数量
        /// </summary>
        [DisplayName("赞同数量")]
        public int FavourCount { get; set; }

        /// <summary>
        /// 回复数量
        /// </summary>
        [DisplayName("回复数量")]
        public int ReplyCount { get; set; }

    }
}
