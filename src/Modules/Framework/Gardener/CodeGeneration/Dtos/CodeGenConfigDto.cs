using Gardener.Base;
using Gardener.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.CodeGeneration.Dtos;
/// <summary>
/// 代码生成配置
/// </summary>
public partial class CodeGenConfigDto: BaseDto<int> 
{
    //: CodeGenConfig // TODO: DTO是不是可以继承Entity，
    //请参考Admin.Net，Admin.NET.Application 引用了Admin.NET.Core
    //因为Entity引用了ORM框架相关依赖，接口是api和client的约束，所以接口中的dto应该保持无任何具体实现的影子
    #region Custom Dto

    /// <summary>
    /// 最大长度
    /// </summary>
    /// <remarks>
    /// [MaxLength(20)]
    /// </remarks>
    [DisplayName("最大长度")]
    public string MaxLengthText { get; set; } = "";

    #endregion


    /// <summary>
    /// 代码生成主表ID
    /// </summary>
    [DisplayName("生成主键")]
    public int CodeGenId { get; set; }

    /// <summary>
    /// 数据库字段名
    /// </summary>
    [Required, MaxLength(100)]
    [DisplayName("列名")]
    public string ColumnName { get; set; }

    /// <summary>
    /// Net Column Name
    /// </summary>
    [DisplayName("实体字段名")]
    public string NetColumnName { get; set; }

    /// <summary>
    /// DB Description
    /// </summary>
    [DisplayName("实体字段描述")]
    public string ColumnDescription { get; set; }

    /// <summary>
    /// Summary in code
    /// </summary>
    [DisplayName("实体字段概要")]
    public string ColumnSummary { get; set; }

    
    /// <summary>
    /// 
    /// </summary>
    public string ColumnLocaleKey { get; set; } = "";

    /// <summary>
    /// .NET数据类型
    /// </summary>
    [MaxLength(50)]
    [DisplayName("实体数据类型")]
    public string NetType { get; set; }

    /// <summary>
    /// .NET数据类型, 不带问号
    /// </summary>
    [DisplayName("实体数据类型, 不带问号")]
    public string NetTypeRaw { get; set; }

    /// <summary>
    /// 数据库中类型（物理类型）
    /// </summary>
    [MaxLength(50)]
    [DisplayName("数据库数据类型")]
    public string DbDataType { get; set; }

    /// <summary>
    /// DB data type with length: nvarchar(20)
    /// </summary>
    public string DbDataTypeText { get; set; } = "";

    // view
    public ClientComponentType ViewComponentType { get; set; }
    // edit
    public ClientComponentType EditComponentType { get; set; }
    public int EditComponentLength { get; set; } = 150;
    // search
    public ClientComponentType CustomSearchType { get; set; }
    public int CustomSearchLength { get; set; } = 150;

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
    public bool IsView { get; set; } = true;

    /// <summary>
    /// Create
    /// </summary>

    public bool IsCreate { get; set; } = true;

    public bool IsRequired { get; set; } = false;
    /// <summary>
    /// 改
    /// </summary>
    public bool IsEdit { get; set; } = true;

    public bool IsBatchEdit { get; set; } = false;

    /// <summary>
    /// 是否通用字段
    /// </summary>
    public bool IsCommon { get; set; } = false;

    /// <summary>
    /// 是否是Entity字段，用于表建表
    /// </summary>
    public bool IsEntity { get; set; } = true;

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
    
    // todo
    // DefaultValue 

    /// <summary>
    /// 是否忽略
    /// </summary>
    public bool IsIgnore { get; set; }
    /// <summary>
    /// 特殊类型
    /// </summary>
    public bool IsSpecialType { get; set; }

    #endregion

}