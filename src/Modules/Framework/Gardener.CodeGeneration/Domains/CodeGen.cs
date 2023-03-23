#nullable enable

using Furion.DatabaseAccessor;
using Gardener.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.CodeGeneration.Domains;

/// <summary>
/// 系统代码生成
/// </summary>
[Table("Sys_CodeGen")]
[Description("Code Gen - DB First")]
public class CodeGen: GardenerEntityBase<Guid>, IEntityTypeBuilder<CodeGen>
{
    /// <summary>
    /// 表名
    /// </summary>
    [MaxLength(100)]
    public string TableName { get; set; } = "";
    /// <summary>
    /// 类名
    /// </summary>
    public string ClassName { get; set; } = "";
    /// <summary>
    /// 模块
    /// </summary>
    public string Module { get; set; } = "";
    /// <summary>
    /// 备注
    /// </summary>

    public string? Remark { get; set; }
    /// <summary>
    /// 表格英文描述
    /// </summary>

    public string? TableDescriptionEN { get; set; }
    /// <summary>
    /// 表格中文描述
    /// </summary>
    public string? TableDescriptionCH { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    public string? IconName { get; set; }
    /// <summary>
    /// 菜单英文名
    /// </summary>
    public string? MenuNameEN { get; set; }
    /// <summary>
    /// 菜单中文名
    /// </summary>
    public string? MenuNameCH { get; set; }
    /// <summary>
    /// 上级菜单Id
    /// </summary>
    public Guid? MenuParentId { get; set; }

    /// <summary>
    /// 表格头
    /// </summary>
    [MaxLength(10)]
    public string? TablePrefix { get; set; }

    /// <summary>
    /// 命名空间
    /// </summary>
    [MaxLength(100)]
    public string? NameSpace { get; set; }

    /// <summary>
    /// 为View或Edit下的Select生成对应字段
    /// </summary>
    public bool? GenerateSelectFields { get; set; } = true;
    /// <summary>
    /// 使用自定义模板
    /// </summary>
    public bool? UseCustomTemplate { get; set; } = false;
    /// <summary>
    /// 生成项目文件
    /// </summary>
    public bool? GenerateProjectFile { get; set; } = false;

    /// <summary>
    /// XxxBaseService, XxxBaseController, _Imports.razor, ...
    /// </summary>
    public bool? GenerateBaseClass { get; set; } = false;
    /// <summary>
    /// 生成服务
    /// </summary>
    public bool? GenerateService { get; set; } = false;
    /// <summary>
    /// 生成本地文件
    /// </summary>
    public bool? GenerateLocaleFile { get; set; } = false;

    /// <summary>
    /// 使用中文key
    /// </summary>
    public bool? UseChineseKey { get; set; } = false;
    /// <summary>
    /// 用net列作为key
    /// </summary>
    public bool? UseNetColumnAsKey { get; set; } = false;

    /// <summary>
    /// 代码生成配置
    /// </summary>
    public ICollection<CodeGenConfig> CodeGenConfigs { get; set; } = new List<CodeGenConfig>();

    #region Views
    /// <summary>
    /// 主键名
    /// </summary>
    public string PrimaryKeyName { get; set; } = "Id";
    /// <summary>
    /// 
    /// </summary>
    public bool? HasAdd { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool? HasEdit { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool? HasBatchEdit { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool? HasDelete { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool? HasBatchDelete { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool? HasLock { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool? HasImport { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool? HasExport { get; set; }
    #endregion

    #region 表建表，根据某一表生成另一表

    /// <summary>
    /// 开启表建表
    /// </summary>
    public bool? EntityFromTable { get; set; } = false;

    /// <summary>
    /// 母表所在的模块，用来搜索母表的多语言文件
    /// </summary>
    public string? OriginModule { get; set; }

    /// <summary>
    /// 新表名
    /// </summary>
    public string? NewTableName { get; set; }
    /// <summary>
    /// 是否允许空值
    /// 1，正常生成Entity不需要修改
    /// 2，根据某一表生成新表, 设为true将在初始化CodeGenConfig时将IsNullable设置为true
    /// 然后自己在CodeGenConfig中手动设置
    /// </summary>
    public bool? AllowNull { get; set; } = false;
    #endregion

    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="entityBuilder"></param>
    /// <param name="dbContext"></param>
    /// <param name="dbContextLocator"></param>
    public void Configure(EntityTypeBuilder<CodeGen> entityBuilder, DbContext dbContext, Type dbContextLocator)
    {
    }

}
