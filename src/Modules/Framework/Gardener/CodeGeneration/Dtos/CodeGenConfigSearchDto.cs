using Gardener.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.CodeGeneration.Dtos;

public partial class CodeGenConfigSearchDto
{
    [DisplayName("NetColumnName")]
    public string NetColumnName { get; set; }

    [DisplayName("ColumnName")]
    public string ColumnName { get; set; }

    [DisplayName("NetType")]
    public string NetType { get; set; }

    [DisplayName("DbDataType")]
    public string DbDataType { get; set; }

    [DisplayName("IsCommon")]
    public bool IsCommon { get; set; }
}