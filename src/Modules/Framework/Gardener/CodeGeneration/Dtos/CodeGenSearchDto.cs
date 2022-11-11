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
    [DisabledSearchField]
    public IEnumerable<string> TableName { get; set; }

    [CustomSearchField]
    [DisabledSearchField]
    public string ClassName { get; set; }
    #endregion

    // default select: 
    public string Module { get; set; }

    public string NameSpace { get; set; }

    [DisplayName("创建时间")]
    public DateTimeOffset CreatedTime { get; set; }
}

