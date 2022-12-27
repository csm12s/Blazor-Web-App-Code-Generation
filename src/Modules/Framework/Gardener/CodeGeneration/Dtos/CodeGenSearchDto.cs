using Gardener.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Gardener.CodeGeneration.Dtos;

/// <summary>
/// 搜索实体
/// </summary>
public partial class CodeGenSearchDto
{
    #region Custom select
    /// <summary>
    /// 表名
    /// </summary>
    [CustomSearchField]
    [DisplayName("TableName")]
    public IEnumerable<string> TableName { get; set; }
    /// <summary>
    /// 类名
    /// </summary>
    [DisplayName("ClassName")]
    [CustomSearchField]
    public string ClassName { get; set; }
    #endregion
    /// <summary>
    /// 模块
    /// </summary>
    [DisplayName("Module")]
    public string Module { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    [DisplayName("CreatedTime")]
    public DateTimeOffset CreatedTime { get; set; }
}

