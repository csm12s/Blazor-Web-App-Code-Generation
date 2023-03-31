using Gardener.Base;
using Gardener.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.CodeGeneration.Domains;

/// <summary>
/// 代码生成字段配置表
/// </summary>
[Table("Sys_CodeGenConfig")]
[Description("代码生成字段配置表")]
public class CodeGenConfig: GardenerEntityBase<Guid>//, IEntityTypeBuilder<CodeGenConfig>
{
    /// <summary>
    /// 代码生成主表ID
    /// </summary>
    public Guid CodeGenId { get; set; }

    /// <summary>
    /// 数据库字段名
    /// </summary>
    [Required, MaxLength(100)]
    public string ColumnName { get; set; } = null!;

    /// <summary>
    /// Net Column Name
    /// </summary>
    public string NetColumnName { get; set; } = null!;

    /// <summary>
    /// DB Description
    /// </summary>
    public string? ColumnDescription { get; set; }

    /// <summary>
    /// Comment in code
    /// </summary>
    public string? ColumnSummary { get; set; }

    /// <summary>
    /// .NET数据类型
    /// </summary>
    [MaxLength(50)]
    public string NetType { get; set; } = null!;

    /// <summary>
    /// .NET数据类型, 不带问号
    /// </summary>
    public string? NetTypeRaw { get; set; }

    /// <summary>
    /// 数据库中类型（物理类型）
    /// </summary>
    [MaxLength(50)]
    public string DbDataType { get; set; } = null!;

    /// <summary>
    /// view
    /// </summary>
    public ClientComponentType? ViewComponentType { get; set; }
    /// <summary>
    /// edit
    /// </summary>
    public ClientComponentType? EditComponentType { get; set; }
    /// <summary>
    /// EditComponentLength
    /// </summary>
    public int? EditComponentLength { get; set; }
    /// <summary>
    /// search
    /// </summary>
    public ClientComponentType? CustomSearchType { get; set; }
    /// <summary>
    /// CustomSearchLength
    /// </summary>
    public int? CustomSearchLength { get; set; }

    /// <summary>
    /// 作用类型（字典）
    /// </summary>
    [MaxLength(50)]
    public string? EffectType { get; set; }

    /// <summary>
    /// 外键实体名称
    /// </summary>
    [MaxLength(50)]
    public string? FkEntityName { get; set; }

    /// <summary>
    /// 外键显示字段
    /// </summary>
    [MaxLength(50)]
    public string? FkColumnName { get; set; }

    /// <summary>
    /// 外键显示字段.NET类型
    /// </summary>
    [MaxLength(50)]
    public string? FkColumnNetType { get; set; }

    /// <summary>
    /// 字典code
    /// </summary>
    [MaxLength(50)]
    public string? DictTypeCode { get; set; }

    /// <summary>
    /// 列表是否缩进（字典）
    /// </summary>
    public bool? IsDictRetract { get; set; } = false;

    /// <summary>
    /// 是否必填（字典）
    /// </summary>
    public bool? IsDictRequired { get; set; } = false;

    /// <summary>
    /// 是否是查询条件
    /// </summary>
    public bool? IsSearch { get; set; } = false;
    /// <summary>
    /// 启用自定义搜索
    /// </summary>
    public bool? IsCustomSearch { get; set; } = false;

    /// <summary>
    /// 查询方式
    /// </summary>
    [MaxLength(10)]
    public string? SearchType { get; set; }

    /// <summary>
    /// 列表显示
    /// </summary>
    public bool? IsView { get; set; } = false;

    /// <summary>
    /// Create
    /// </summary>

    public bool? IsCreate { get; set; } = false;
    /// <summary>
    /// 是否可空
    /// </summary>
    public bool? IsRequired { get; set; } = false;
    /// <summary>
    /// 改
    /// </summary>
    public bool? IsEdit { get; set; } = false;
    /// <summary>
    /// 开启批量编辑
    /// </summary>
    public bool? IsBatchEdit { get; set; } = false;

    /// <summary>
    /// 是否通用字段
    /// </summary>
    public bool? IsCommon { get; set; } = false;
    
    /// <summary>
    /// 是否是Entity字段，用于表建表
    /// </summary>
    public bool? IsEntity { get; set; } = true;

    /// <summary>
    /// 主键
    /// </summary>
    [MaxLength(5)]
    public string? ColumnKey { get; set; }

    #region Entity DB First
    /// <summary>
    /// 主键
    /// </summary>
    public bool? IsPrimaryKey { get; set; }
    /// <summary>
    /// 自增列 
    /// </summary>
    public bool? IsIdentity { get; set; }
    /// <summary>
    /// 是否是为NULL
    /// </summary>
    public bool? IsNullable { get; set; }
    /// <summary>
    /// 精度
    /// </summary>
    public int? DecimalDigits { get; set; }
    /// <summary>
    /// 长度
    /// </summary>
    public int? Length { get; set; }

    // todo
    // DefaultValue 
    /// <summary>
    /// 是否忽略
    /// </summary>
    public bool? IsIgnore { get; set; }
    /// <summary>
    /// 特殊类型
    /// </summary>
    public bool? IsSpecialType { get; set; }

    /// <summary>
    /// DB data type with length: nvarchar(20)
    /// </summary>
    public string? DbDataTypeText { get; set; }

    #endregion

    #region EF 外键
    //public CodeGen CodeGen { get; set; }

    //public void Configure(EntityTypeBuilder<CodeGenConfig> entityBuilder, DbContext dbContext, Type dbContextLocator)
    //{
    //    entityBuilder
    //           .HasOne(it => it.CodeGen)
    //           .WithMany(p => p.CodeGenConfigs)
    //           .HasForeignKey(it => it.CodeGenId)
    //           .OnDelete(DeleteBehavior.ClientSetNull);
    //}
    #endregion

}
