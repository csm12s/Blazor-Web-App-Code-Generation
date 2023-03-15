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
[Table("Sys_CodeGen")]
[Description("Code Gen - DB First")]
public class CodeGen: GardenerEntityBase<Guid>, IEntityTypeBuilder<CodeGen>
{
    /// <summary>
    /// 表名
    /// </summary>
    [MaxLength(100)]
    public string TableName { get; set; }
    /// <summary>
    /// 类名
    /// </summary>
    public string ClassName { get; set; }
    /// <summary>
    /// 模块
    /// </summary>
    public string Module { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
    /// <summary>
    /// 表英文描述
    /// </summary>
    public string? TableDescriptionEN { get; set; }
    /// <summary>
    /// 表中文描述
    /// </summary>
    public string? TableDescriptionCH { get; set; }
    /// <summary>
    /// 图标名称
    /// </summary>
    public string? IconName { get; set; }
    /// <summary>
    /// 菜单名-英文
    /// </summary>
    public string? MenuNameEN { get; set; }
    /// <summary>
    /// 菜单名-中文
    /// </summary>
    public string? MenuNameCH { get; set; }
    /// <summary>
    /// 菜单父级编号
    /// </summary>
    public Guid? MenuParentId { get; set; }
    /// <summary>
    /// 表前缀
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
    /// 生成本地化文件
    /// </summary>
    public bool? GenerateLocaleFile { get; set; } = false;
    /// <summary>
    /// 使用中文key
    /// </summary>
    public bool? UseChineseKey { get; set; } = false;
    /// <summary>
    /// 使用.NET列名作为KEY
    /// </summary>
    public bool? UseNetColumnAsKey { get; set; } = false;
    /// <summary>
    /// 代码生成配置列表
    /// </summary>
    public ICollection<CodeGenConfig> CodeGenConfigs { get; set; }

    #region Views
    /// <summary>
    /// 主键名
    /// </summary>
    public string PrimaryKeyName { get; set; } = "Id";
    /// <summary>
    /// 是否包含添加
    /// </summary>
    public bool? HasAdd { get; set; }
    /// <summary>
    /// 是否包含编辑
    /// </summary>
    public bool? HasEdit { get; set; }
    /// <summary>
    /// 是否包含批量编辑
    /// </summary>
    public bool? HasBatchEdit { get; set; }
    /// <summary>
    /// 是否包含删除
    /// </summary>
    public bool? HasDelete { get; set; }
    /// <summary>
    /// 是否包含批量删除
    /// </summary>
    public bool? HasBatchDelete { get; set; }
    /// <summary>
    /// 是否包含锁定
    /// </summary>
    public bool? HasLock { get; set; }
    /// <summary>
    /// 是否包含导入
    /// </summary>
    public bool? HasImport { get; set; }
    /// <summary>
    /// 是否包含导出
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
    /// 1，正常生成Entity不需要修改
    /// 2，根据某一表生成新表, 设为true将在初始化CodeGenConfig时将IsNullable设置为true
    /// 然后自己在CodeGenConfig中手动设置
    /// </summary>
    public bool? AllowNull { get; set; } = false;
    #endregion

    public void Configure(EntityTypeBuilder<CodeGen> entityBuilder, DbContext dbContext, Type dbContextLocator)
    {
    }

}
