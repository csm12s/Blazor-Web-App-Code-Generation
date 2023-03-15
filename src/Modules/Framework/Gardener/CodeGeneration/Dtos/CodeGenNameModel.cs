using Gardener.SystemManager.Dtos;
using System.Collections.Generic;

namespace Gardener.CodeGeneration.Dtos;

/// <summary>
/// CodeGenNameModel
/// </summary>
public class CodeGenNameModel
{
    /// <summary>
    /// AppName
    /// </summary>
    public string AppName { get; set; } = "";
    /// <summary>
    /// TableName
    /// </summary>
    public string TableName { get; set; } = "";
    /// <summary>
    /// TableLocaleKey
    /// </summary>
    public string? TableLocaleKey { get; set; }

    /// <summary>
    /// DB Description
    /// </summary>
    public string TableDesc { get; set; } = "";

    /// <summary>
    /// C# Summary, 示例：
    /// User
    /// Table: Sys_User
    /// </summary>
    public string TableSummary { get; set; } = "";
    /// <summary>
    /// ClassName
    /// </summary>
    public string ClassName { get; set; } = "";

    /// <summary>
    /// local variables
    /// </summary>
    public string? ClassNameLower { get; set; }
    /// <summary>
    /// module / package
    /// </summary>
    public string Module { get; set; } = "";
    /// <summary>
    /// module / package name to url path
    /// </summary>
    public string ModuleToUrl { get; set; } = "";

    /// <summary>
    /// _sys.tool -> SysTool
    /// </summary>
    public string ModuleUpper { get; set; } = "";
    /// <summary>
    /// _sys.tool -> sysTool
    /// </summary>
    public string ModuleLower { get; set; } = "";
    /// <summary>
    /// CodeGen
    /// </summary>
    public CodeGenDto? CodeGen { get; set; }
    /// <summary>
    /// CodeGenConfigs
    /// </summary>
    public List<CodeGenConfigDto>? CodeGenConfigs { get; set; }
    /// <summary>
    /// HasCustomSearch
    /// </summary>
    public bool HasCustomSearch { get; set; } = false;
    /// <summary>
    /// HasRemoteImage
    /// </summary>
    public bool HasRemoteImage { get; set; } = false;
    /// <summary>
    /// Menus
    /// </summary>
    public List<ResourceDto>? Menus { get; set; }
    /// <summary>
    /// LocaleItems
    /// </summary>
    public List<CodeGenLocaleItem> LocaleItems { get; set; } = new List<CodeGenLocaleItem>();
    /// <summary>
    /// MenuLocaleItem
    /// </summary>
    public CodeGenLocaleItem? MenuLocaleItem { get; set; }
}
