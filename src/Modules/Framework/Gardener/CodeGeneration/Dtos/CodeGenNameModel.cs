using Gardener.SystemManager.Dtos;
using System.Collections.Generic;

namespace Gardener.CodeGeneration.Dtos;

public class CodeGenNameModel
{
    public string AppName { get; set; } = "";
    public string TableName { get; set; } = "";
    public string TableLocaleKey { get; set; }

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
    public string ClassName { get; set; } = "";

    // local variables
    public string ClassNameLower { get; set; }
    // module / package
    public string Module { get; set; } = "";
    // module / package name to url path
    public string ModuleToUrl { get; set; } = "";

    /// <summary>
    /// _sys.tool -> SysTool
    /// </summary>
    public string ModuleUpper { get; set; } = "";
    /// <summary>
    /// _sys.tool -> sysTool
    /// </summary>
    public string ModuleLower { get; set; } = "";

    public CodeGenDto CodeGen { get; set; }
    public List<CodeGenConfigDto> CodeGenConfigs { get; set; }
    public bool HasCustomSearch { get; set; } = false;
    public bool HasRemoteImage { get; set; } = false;
    public List<ResourceDto> Menus { get; set; }
    public List<CodeGenLocaleItem> LocaleItems { get; set; } = new List<CodeGenLocaleItem>();
}
