using Gardener.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.CodeGeneration.Dtos;

public partial class CodeGenConfigSearchDto
{
    [DisplayName("_CodeGenConfig.NetColumnName")]
    public string NetColumnName { get; set; }

    [DisplayName("_CodeGenConfig.ColumnName")]
    public string ColumnName { get; set; }

    [DisplayName("_CodeGenConfig.NetType")]
    public string NetType { get; set; }

    [DisplayName("_CodeGenConfig.DbDataType")]
    public string DbDataType { get; set; }

    [DisplayName("_CodeGenConfig.IsCommon")]
    public bool IsCommon { get; set; }
}