using Gardener.SystemManager.Dtos;
using System.Collections.Generic;

namespace Gardener.CodeGeneration.Dtos;

public class CodeGenNameModel
{
    public string AppName { get; set; } = "";
    public string TableName { get; set; } = "";
    public string TableLocaleKey { get; set; }
    public string TableDesc { get; set; } = "";
    public string ClassName { get; set; } = "";

    // local variables
    public string ClassNameLower { get; set; }
    // module / package
    public string Module { get; set; } = "";
    // module / package name to url path
    public string ModuleToUrl { get; set; } = "";

    public List<CodeGenConfigDto> CodeGenConfigs { get; set; }
    public bool HasCustomSearch { get; set; } = false;
    public List<ResourceDto> Menus { get; set; }
    public List<CodeGenLocaleItem> LocaleItems { get; set; } = new List<CodeGenLocaleItem>();
}
