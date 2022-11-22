using Gardener.Enums;
using System.ComponentModel.DataAnnotations;

namespace Gardener.CodeGeneration.Dtos;

public partial class CodeGenConfigSearchDto
{
    public string NetColumnName { get; set; }

    public string ColumnName { get; set; }


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

    /// <summary>
    /// 数据库中类型（物理类型）
    /// </summary>
    [MaxLength(50)]
    public string DbDataType { get; set; }

    /// <summary>
    /// DB data type with length: nvarchar(20)
    /// </summary>
    public string DBDataTypeText { get; set; }

    // edit
    public ClientComponentType ClientComponentType { get; set; }
    // search
    public ClientComponentType CustomSearchType { get; set; }
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
}