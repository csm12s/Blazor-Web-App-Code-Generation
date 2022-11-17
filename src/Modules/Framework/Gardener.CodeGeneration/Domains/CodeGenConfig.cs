using Furion.DatabaseAccessor;
using Gardener.Base;
using Gardener.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.CodeGeneration.Domains;

/// <summary>
/// 代码生成字段配置表
/// </summary>
[Table("Sys_Code_Gen_Config")]
[Description("代码生成字段配置表")]
public class CodeGenConfig: GardenerEntityBase<int>, IEntityTypeBuilder<CodeGenConfig>
{
    /// <summary>
    /// 代码生成主表ID
    /// </summary>
    public int CodeGenId { get; set; }

    /// <summary>
    /// 数据库字段名
    /// </summary>
    [Required, MaxLength(100)]
    public string ColumnName { get; set; }

    /// <summary>
    /// Net Column Name
    /// </summary>
    public string NetColumnName { get; set; }

    /// <summary>
    /// DB Description
    /// </summary>
    public string ColumnDescription { get; set; }

    /// <summary>
    /// Comment in code
    /// </summary>
    public string ColumnComment { get; set; }

    /// <summary>
    /// .NET数据类型
    /// </summary>
    [MaxLength(50)]
    public string NetType { get; set; }

    [MaxLength(50)]
    public string DataType { get; set; }

    /// <summary>
    /// 数据库中类型（物理类型）
    /// </summary>
    [MaxLength(50)]
    public string DbDataType { get; set; }

    public int DataLength { get; set; }
    public int DataDecimal { get; set; }
    // todo
    // DefaultValue 

    // edit
    public ClientComponentType ClientComponentType { get; set; }
    public int ClientComponentLength { get; set; }
    // search
    public ClientComponentType CustomSearchType { get; set; }
    public int CustomSearchLength { get; set; }

    /// <summary>
    /// 作用类型（字典）
    /// </summary>
    [MaxLength(50)]
    public string EffectType { get; set; }

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
    public bool IsDictRetract { get; set; } = false;

    /// <summary>
    /// 是否必填（字典）
    /// </summary>
    public bool IsDictRequired { get; set; } = false;

    /// <summary>
    /// 是否是查询条件
    /// </summary>
    public bool IsSearch { get; set; } = false;
    public bool IsCustomSearch { get; set; } = false;

    /// <summary>
    /// 查询方式
    /// </summary>
    [MaxLength(10)]
    public string? SearchType { get; set; }

    /// <summary>
    /// 列表显示
    /// </summary>
    public bool IsView { get; set; } = false;

    /// <summary>
    /// Create
    /// </summary>

    public bool IsCreate { get; set; } = false;

    //TODO: IsRequired
    public bool IsRequired { get; set; } = false;
    /// <summary>
    /// 改
    /// </summary>
    public bool IsEdit { get; set; } = false;

    public bool IsBatchEdit { get; set; } = false;

    public bool IsEditRequired { get; set; } = false;

    /// <summary>
    /// 是否通用字段
    /// </summary>
    public bool IsCommon { get; set; } = false;

    /// <summary>
    /// 主键
    /// </summary>
    [MaxLength(5)]
    public string ColumnKey { get; set; }

    #region Entity DB First
    /// <summary>
    /// 主键
    /// </summary>
    public bool IsPrimaryKey { get; set; }
    /// <summary>
    /// 自增列 
    /// </summary>
    public bool IsIdentity { get; set; }
    /// <summary>
    /// 是否是为NULL
    /// </summary>
    public bool IsNullable { get; set; }
    /// <summary>
    /// 精度
    /// </summary>
    public int? DecimalDigits { get; set; }
    /// <summary>
    /// 长度
    /// </summary>
    public int? Length { get; set; }

    /// <summary>
    /// 是否忽略
    /// </summary>
    public bool IsIgnore { get; set; }
    /// <summary>
    /// 特殊类型
    /// </summary>
    public bool IsSpecialType { get; set; }

    /// <summary>
    /// DB data type with length: nvarchar(20)
    /// </summary>
    public string DbDataTypeText { get; set; }
    #endregion

    #region EF 外键
    [SugarColumn(IsIgnore = true)]
    public CodeGen CodeGen { get; set; }

    public void Configure(EntityTypeBuilder<CodeGenConfig> entityBuilder, DbContext dbContext, Type dbContextLocator)
    {
        entityBuilder
               .HasOne(it => it.CodeGen)
               .WithMany(p => p.CodeGenConfigs)
               .HasForeignKey(it => it.CodeGenId)
               .OnDelete(DeleteBehavior.ClientSetNull);
    }
    #endregion

}
