using System.ComponentModel;

namespace Gardener.CodeGeneration.Dtos;

/// <summary>
/// 代码生成配置检索Dto
/// </summary>
public partial class CodeGenConfigSearchDto
{
    /// <summary>
    /// 净列名
    /// </summary>
    [DisplayName("NetColumnName")]
    public string NetColumnName { get; set; }

    /// <summary>
    /// 列名
    /// </summary>
    [DisplayName("ColumnName")]
    public string ColumnName { get; set; }

    /// <summary>
    /// net 类型
    /// </summary>
    [DisplayName("NetType")]
    public string NetType { get; set; }

    /// <summary>
    /// 数据库数据类型
    /// </summary>
    [DisplayName("DbDataType")]
    public string DbDataType { get; set; }

    /// <summary>
    /// 是否常用
    /// </summary>
    [DisplayName("IsCommon")]
    public bool IsCommon { get; set; }
}