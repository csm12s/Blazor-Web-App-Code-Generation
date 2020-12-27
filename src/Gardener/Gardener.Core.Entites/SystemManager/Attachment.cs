// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 附件
    /// </summary>
    public class Attachment : Entity<Guid>
    {
        /// <summary>
        /// 业务ID
        /// </summary>
        [MaxLength(64)]
        public string BusinessId { get; set; }
        /// <summary>
        /// 附件业务类型
        /// </summary>
        public AttachmentBusinessType BusinessType { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public AttachmentFileType FileType { get; set; }
        /// <summary>
        /// 原始类型
        /// </summary>
        [Required, MaxLength(20)]
        public string ContentType { get; set; }
        /// <summary>
        /// 文件大小 byte
        /// </summary>
        public long Size { get; set; }
        /// <summary>
        /// 存储地址 无name
        /// </summary>
        [Required, MaxLength(200)]
        public string Path { get; set; }

        /// <summary>
        /// 文件名称 随机生成
        /// </summary>
        [Required, MaxLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// 原始文件名
        /// </summary>
        [MaxLength(100)]
        public string OriginalName { get; set; }
        /// <summary>
        /// 访问地址
        /// </summary>
        [Required, MaxLength(200)]
        public string Url { get; set; }
        /// <summary>
        /// 后缀
        /// .jpg
        /// </summary>
        [Required, MaxLength(20)]
        public string Suffix { get; set; }
    }
}
