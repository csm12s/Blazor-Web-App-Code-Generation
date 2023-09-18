﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Base.Entity
{
    /// <summary>
    /// 字典信息
    /// </summary>
    [Description("Code")]
    public class Code : GardenerEntityBase, IEntityTypeBuilder<Code>
    {
        /// <summary>
        /// 字段类型编号
        /// </summary>
        [DisplayName("CodeTypeId")]
        [Required]
        public int CodeTypeId { get; set; }
        /// <summary>
        /// 字典值
        /// </summary>
        [DisplayName("CodeValue")]
        [Required, MaxLength(50)]
        public string CodeValue { get; set; } = null!;
        /// <summary>
        /// 字典名称
        /// </summary>
        [DisplayName("CodeName")]
        [Required, MaxLength(50)]
        public string CodeName { get; set; } = null!;
        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("Order")]
        [Required]
        public int Order { get; set; }
        /// <summary>
        /// 扩展参数
        /// </summary>
        [DisplayName("ExtendParams")]
        [MaxLength(500)]
        public string? ExtendParams { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        [DisplayName("Color")]
        [MaxLength(50)]
        public string? Color { get; set; }
        /// <summary>
        /// 字典类型
        /// </summary>
        public CodeType CodeType { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(EntityTypeBuilder<Code> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasOne(x => x.CodeType).WithMany(x => x.Codes).HasForeignKey(x => x.CodeTypeId);
        }
    }
}