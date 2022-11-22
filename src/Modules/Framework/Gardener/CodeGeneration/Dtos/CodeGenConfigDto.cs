using Gardener.Base;
using Gardener.Enums;
using System.ComponentModel.DataAnnotations;

namespace Gardener.CodeGeneration.Dtos;

public partial class CodeGenConfigDto: BaseDto<int> //: CodeGenConfig // TODO: DTO是不是可以继承Entity，
                                                    //请参考Admin.Net，Admin.NET.Application 引用了Admin.NET.Core
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

    // Locale
    public string ColumnLocaleKey { get; set; } = "";

    /// <summary>
    /// .NET数据类型
    /// </summary>
    [MaxLength(50)]
    public string NetType { get; set; }

    /// <summary>
    /// 数据库中类型（物理类型）
    /// </summary>
    [MaxLength(50)]
    public string DbDataType { get; set; }

    /// <summary>
    /// DB data type with length: nvarchar(20)
    /// </summary>
    public string DbDataTypeText { get; set; }

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

    public bool IsEditRequired { get; set; } = false;

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