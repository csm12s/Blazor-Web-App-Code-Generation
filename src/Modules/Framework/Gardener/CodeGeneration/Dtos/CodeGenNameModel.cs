using Gardener.SystemManager.Dtos;
using System.Collections.Generic;

namespace Gardener.CodeGeneration.Dtos;

/// <summary>
/// 代码生成命名模型
/// </summary>
public class CodeGenNameModel
{
    /// <summary>
    /// 应用名
    /// </summary>
    public string AppName { get; set; } = "";
    /// <summary>
    /// 表明
    /// </summary>
    public string TableName { get; set; } = "";
    /// <summary>
    /// 表格语言环境键
    /// </summary>
    public string TableLocaleKey { get; set; }

    /// <summary>
    /// DB Description
    /// </summary>
    public string TableDesc { get; set; } = "";

    /// <summary>
    /// 表摘要
    /// C# Summary, 示例：
    /// User
    /// Table: Sys_User
    /// </summary>
    public string TableSummary { get; set; } = "";
    /// <summary>
    /// 类名
    /// </summary>
    public string ClassName { get; set; } = "";

    /// <summary>
    /// local variables
    /// </summary>
    public string ClassNameLower { get; set; }
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
    /// 代码生成Dto
    /// </summary>
    public CodeGenDto CodeGen { get; set; }
    /// <summary>
    /// 代码生成配置
    /// </summary>
    public List<CodeGenConfigDto> CodeGenConfigs { get; set; }
    /// <summary>
    /// 是否包含自定义检索
    /// </summary>
    public bool HasCustomSearch { get; set; } = false;
    /// <summary>
    /// 是否包含远程图像
    /// </summary>
    public bool HasRemoteImage { get; set; } = false;
    /// <summary>
    /// 菜单
    /// </summary>
    public List<ResourceDto> Menus { get; set; }
    /// <summary>
    /// 本地项
    /// </summary>
    public List<CodeGenLocaleItem> LocaleItems { get; set; } = new List<CodeGenLocaleItem>();
    /// <summary>
    /// 菜单本地项
    /// </summary>
    public CodeGenLocaleItem MenuLocaleItem { get; set; }
}
