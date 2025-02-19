﻿
namespace Gardener.CodeGeneration.Dtos;

/// <summary>
/// 数据库表列表参数
/// </summary>
public class TableOutput
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public string DatabaseName { get; set; } = null!;
    /// <summary>
    /// 
    /// </summary>
    public string? DatabaseComment { get; set; }

    /// <summary>
    /// 表名（字母形式的）
    /// </summary>
    public string TableName { get; set; } = null!;

    /// <summary>
    /// 实体名称
    /// </summary>
    public string EntityName { get; set; } = null!;

    /// <summary>
    /// 创建时间
    /// </summary>
    public string CreateTime { get; set; } = null!;

    /// <summary>
    /// 更新时间
    /// </summary>
    public string? UpdateTime { get; set; }

    /// <summary>
    /// 表名称描述（注释）（功能名）
    /// </summary>
    public string? TableComment { get; set; }

    /// <summary>
    /// Client select label
    /// </summary>
    public string? ClientSelectLabelText { get; set; }
}
