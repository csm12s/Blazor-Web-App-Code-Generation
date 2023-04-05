using System.ComponentModel;

namespace Gardener.CodeGeneration.Dtos;
/// <summary>
/// CodeGenConfigSearchDto
/// </summary>
public partial class CodeGenConfigSearchDto
{
    /// <summary>
    /// NetColumnName
    /// </summary>
    [DisplayName("NetColumnName")]
    public string NetColumnName { get; set; } = null!;
    /// <summary>
    /// ColumnName
    /// </summary>
    [DisplayName("ColumnName")]
    public string ColumnName { get; set; } = null!;
    /// <summary>
    /// NetType
    /// </summary>
    [DisplayName("NetType")]
    public string NetType { get; set; } = null!;
    /// <summary>
    /// DbDataType
    /// </summary>
    [DisplayName("DbDataType")]
    public string DbDataType { get; set; } = null!;
    /// <summary>
    /// IsCommon
    /// </summary>
    [DisplayName("IsCommon")]
    public bool IsCommon { get; set; }
}