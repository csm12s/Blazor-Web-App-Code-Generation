using System;

namespace Gardener.CodeGeneration.Dtos;

/// <summary>
/// 数据库表列
/// </summary>
public class TableColumnInfo
{
    #region Db first fields
    /// <summary>
    /// System.String
    /// </summary>
    public string SysDataType { get; set; } = "";
    /// <summary>
    /// C# type: string
    /// </summary>
    public string NetType { get; set; } = null!;
    /// <summary>
    /// C# type with ?: string?
    /// </summary>
    public string NetTypeStr { get; set; } = null!;

    /// <summary>
    /// DB raw data type: nvarchar
    /// </summary>
    public string DbDataType { get; set; } = "";

    /// <summary>
    /// nvarchar(100), for example in SqlServer
    /// </summary>
    public string DbDataTypeText { get; set; } = null!;

    /// <summary>
    /// [MaxLength(20)]
    /// </summary>
    public string? MaxLengthText { get; set; }

    /// <summary>
    /// 主外键
    /// </summary>
    public string ColumnKey { get; set; } = null!;
    #endregion

    #region SqlSugar.DbColumnInfo
    /// <summary>
    /// TableName
    /// </summary>
    public string TableName { get; set; } = null!;
    /// <summary>
    /// TableId
    /// </summary>
    public int TableId { get; set; }
    /// <summary>
    /// DbColumnName
    /// </summary>
    public string DbColumnName { get; set; } = null!;
    /// <summary>
    /// PropertyName
    /// </summary>
    public string PropertyName { get; set; } = null!;
    /// <summary>
    /// DataType
    /// </summary>
    public string DataType { get; set; } = null!;
    /// <summary>
    /// PropertyType
    /// </summary>
    public Type PropertyType { get; set; } = null!;
    /// <summary>
    /// Length  -1: Max
    /// </summary>
    public int? Length { get; set; }
    /// <summary>
    /// ColumnDescription
    /// </summary>
    public string? ColumnDescription { get; set; }
    /// <summary>
    /// DefaultValue
    /// </summary>
    public string? DefaultValue { get; set; }
    /// <summary>
    /// IsNullable
    /// </summary>
    public bool IsNullable { get; set; }
    /// <summary>
    /// IsIdentity
    /// </summary>
    public bool IsIdentity { get; set; }
    /// <summary>
    /// IsPrimarykey
    /// </summary>
    public bool IsPrimarykey { get; set; }
    /// <summary>
    /// Value
    /// </summary>
    public object? Value { get; set; }
    /// <summary>
    /// DecimalDigits
    /// </summary>
    public int DecimalDigits { get; set; }
    /// <summary>
    /// Scale
    /// </summary>
    public int Scale { get; set; }
    /// <summary>
    /// IsArray
    /// </summary>
    public bool IsArray { get; set; }
    /// <summary>
    /// IsJson
    /// </summary>
    public bool IsJson { get; set; }
    /// <summary>
    /// IsUnsigned
    /// </summary>
    public bool? IsUnsigned { get; set; }
    /// <summary>
    /// CreateTableFieldSort
    /// </summary>
    public int CreateTableFieldSort { get; set; }
    /// <summary>
    /// SqlParameterDbType
    /// </summary>
    internal object? SqlParameterDbType { get; set; }
    #endregion

    
}
