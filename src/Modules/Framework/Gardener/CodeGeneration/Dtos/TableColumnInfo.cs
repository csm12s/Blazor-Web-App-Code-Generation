using System;

namespace Gardener.CodeGeneration.Dtos;

/// <summary>
/// 数据库表列
/// </summary>
public class TableColumnInfo
{
    #region EF
    /// <summary>
    /// nvarchar: System.String
    /// </summary>
    public string SysDataType { get; set; } = "";

    /// <summary>
    /// DB raw data type: nvarchar
    /// </summary>
    public string DbDataType { get; set; } = "";

    /// <summary>
    /// nvarchar(100), for example in SqlServer
    /// </summary>
    public string DbDataTypeText { get; set; }

    /// <summary>
    /// 主外键
    /// </summary>
    public string ColumnKey { get; set; }
    #endregion

    #region SqlSugar.DbColumnInfo
    public string TableName { get; set; }
    public int TableId { get; set; }
    public string DbColumnName { get; set; }
    public string PropertyName { get; set; }
    public string DataType { get; set; }
    public Type PropertyType { get; set; }
    public int? Length { get; set; }
    public string ColumnDescription { get; set; }
    public string DefaultValue { get; set; }
    public bool IsNullable { get; set; }
    public bool IsIdentity { get; set; }
    /// <summary>
    /// K
    /// </summary>
    public bool IsPrimarykey { get; set; }
    public object Value { get; set; }
    public int DecimalDigits { get; set; }
    public int Scale { get; set; }
    public bool IsArray { get; set; }
    public bool IsJson { get; set; }
    public bool? IsUnsigned { get; set; }
    public int CreateTableFieldSort { get; set; }
    internal object SqlParameterDbType { get; set; }
    #endregion

    
}
