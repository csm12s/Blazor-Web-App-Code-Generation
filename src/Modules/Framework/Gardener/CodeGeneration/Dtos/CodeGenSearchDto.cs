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
    [DisplayName("表名")]
    public IEnumerable<string> TableName { get; set; }
    /// <summary>
    /// 类名
    /// </summary>
    [DisplayName("类名")]
    [CustomSearchField]
    public string ClassName { get; set; }
    #endregion
    /// <summary>
    /// 模块
    /// </summary>
    [DisplayName("模块")]
    public string Module { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    [DisplayName("创建时间")]
    public DateTimeOffset CreatedTime { get; set; }
}

