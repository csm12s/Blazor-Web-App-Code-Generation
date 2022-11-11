
using Gardener.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gardener.CodeGeneration.Dtos;

public partial class CodeGenDto: BaseDto<int>
{
    #region Base
    [MaxLength(100)]
    public string TableName { get; set; }
    [Required(ErrorMessage = "Required")]
    public string ClassName { get; set; }
    public string ClassNameLower { get; set; }
    [Required(ErrorMessage = "Required")]
    public string Module { get; set; }
    // url path
    public string ModuleToUrl { get; set; }

    public string TableDescriptionEN { get; set; }
    public string TableDescriptionCH { get; set; }

    public string MenuName { get; set; }
    public Guid?  MenuParentId { get; set; }

    [MaxLength(5)]
    public string TablePrefix { get; set; }

    [MaxLength(100)]
    public string NameSpace { get; set; }

    public bool UseCustomTemplate { get; set; } = false;
    #endregion
}

