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
    public string NetType { get; set; }
    /// <summary>
    /// C# type with ?: string?
    /// </summary>
    public string NetTypeStr { get; set; }

    /// <summary>
    /// DB raw data type: nvarchar
    /// </summary>
    public string DbDataType { get; set; } = "";

    /// <summary>
    /// nvarchar(100), for example in SqlServer
    /// </summary>
    public string DbDataTypeText { get; set; }

    /// <summary>
    /// [MaxLength(20)]
    /// </summary>
    public string MaxLengthText { get; set; }

    /// <summary>
    /// 主外键
    /// </summary>
    public string ColumnKey { get; set; }
    #endregion

    #region SqlSugar.DbColumnInfo
    /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; }
    /// <summary>
    /// 表id
    /// </summary>
    public int TableId { get; set; }
    /// <summary>
    /// 数据库列名
    /// </summary>
    public string DbColumnName { get; set; }
    /// <summary>
    /// 属性名
    /// </summary>
    public string PropertyName { get; set; }
    /// <summary>
    /// 数据类型
    /// </summary>
    public string DataType { get; set; }
    /// <summary>
    /// 属性类型
    /// </summary>
    public Type PropertyType { get; set; }
    /// <summary>
    /// 长度
    /// -1: Max
    /// </summary>
    public int? Length { get; set; }
    /// <summary>
    /// 列描述
    /// </summary>
    public string ColumnDescription { get; set; }
    /// <summary>
    /// 默认值
    /// </summary>
    public string DefaultValue { get; set; }
    /// <summary>
    /// 是否可空
    /// </summary>
    public bool IsNullable { get; set; }
    /// <summary>
    /// 是否身份标识
    /// </summary>
    public bool IsIdentity { get; set; }
    /// <summary>
    /// 是否主键
    /// </summary>
    public bool IsPrimarykey { get; set; }
    /// <summary>
    /// 值
    /// </summary>
    public object Value { get; set; }
    /// <summary>
    /// 小数位数
    /// </summary>
    public int DecimalDigits { get; set; }
    /// <summary>
    /// 比例
    /// </summary>
    public int Scale { get; set; }
    /// <summary>
    /// 是否排列
    /// </summary>
    public bool IsArray { get; set; }
    /// <summary>
    /// 是否json
    /// </summary>
    public bool IsJson { get; set; }
    /// <summary>
    /// 是否未签名
    /// </summary>
    public bool? IsUnsigned { get; set; }
    /// <summary>
    /// 创建表字段的顺序
    /// </summary>
    public int CreateTableFieldSort { get; set; }
    /// <summary>
    /// sql参数数据库类型
    /// </summary>
    internal object SqlParameterDbType { get; set; }
    #endregion

    
}
