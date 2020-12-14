// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Application.Dtos
{
    /// <summary>
    /// 附件
    /// </summary>
    public class AttachmentDto
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 上传的文件类型
        /// </summary>
        public AttachmentFileType FileType { get; set; }
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
        /// 后缀
        /// .jpg
        /// </summary>
        [Required, MaxLength(20)]
        public string Suffix { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTimeOffset CreatedTime { get; set; }
        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTimeOffset? UpdatedTime { get; set; }
    }
}
