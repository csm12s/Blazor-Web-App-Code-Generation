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
/// 代码生成
/// </summary>
[Table("Sys_Code_Gen")]
[Description("Code Generate")]
public class CodeGen: GardenerEntityBase<int>, IEntityTypeBuilder<CodeGen>
{
    [MaxLength(100)]
    public string TableName { get; set; }
    public string ClassName { get; set; }
    public string Module { get; set; }

    public string? Remark { get; set; }

    public string TableDescriptionEN { get; set; }
    public string TableDescriptionCH { get; set; }

    public string? IconName { get; set; }
    public string MenuNameEN { get; set; }
    public string MenuNameCH { get; set; }
    public Guid? MenuParentId { get; set; }


    [MaxLength(10)]
    public string? TablePrefix { get; set; }

    [MaxLength(100)]
    public string? NameSpace { get; set; }

    public bool UseCustomTemplate { get; set; } = false;
    public bool GenerateProjectFile { get; set; } = false;

    /// <summary>
    /// XxxBaseService, XxxBaseController, _Imports.razor, ...
    /// </summary>
    public bool? GenerateBaseClass { get; set; } = false;
    public bool? UseChineseKey { get; set; } = false;

    public ICollection<CodeGenConfig> CodeGenConfigs { get; set; }

    #region Views
    public string PrimaryKeyName { get; set; } = "Id";

    public bool HasAdd { get; set; }
    public bool HasEdit { get; set; }
    public bool HasBatchEdit { get; set; }
    public bool HasDelete { get; set; }
    public bool HasBatchDelete { get; set; }
    public bool HasLock { get; set; }
    public bool HasImport { get; set; }
    public bool HasExport { get; set; }
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

    public string? NewTableName { get; set; }
    /// <summary>
    /// 1，正常生成Entity不需要修改
    /// 2，根据某一表生成新表, 设为true将在初始化CodeGenConfig时将IsNullable设置为true
    /// 然后自己在CodeGenConfig中手动设置
    /// </summary>
    public bool AllowNull { get; set; } = false;
    #endregion

    public void Configure(EntityTypeBuilder<CodeGen> entityBuilder, DbContext dbContext, Type dbContextLocator)
    {
    }

}
