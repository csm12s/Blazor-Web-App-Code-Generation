using Gardener.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Gardener.CodeGeneration.Dtos;

public partial class CodeGenSearchDto
{
    #region Custom select
    /// <summary>
    /// api select
    /// </summary>
    [CustomSearchField]
    [DisplayName("_CodeGen.TableName")]
    public IEnumerable<string> TableName { get; set; }

    [CustomSearchField]
    [DisplayName("_CodeGen.ClassName")]
    public string ClassName { get; set; }
    #endregion

    [DisplayName("_CodeGen.Module")]
    public string Module { get; set; }

    [DisplayName("创建时间")]
    //[DisplayName("_CodeGen.CreatedTime")]
    public DateTimeOffset CreatedTime { get; set; }
}

