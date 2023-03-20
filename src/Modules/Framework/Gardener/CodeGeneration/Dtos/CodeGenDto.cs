﻿#nullable enable

using Gardener.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gardener.CodeGeneration.Dtos;

/// <summary>
/// 代码生成 Dto
/// </summary>
public partial class CodeGenDto: BaseDto<Guid>
{
    #region Custom Dto

    /// <summary>
    /// 是否更新代码生成配置
    /// </summary>
    public bool UpdateCodeGenConfig { get; set; } = true;

    // public bool HasPrimarykey { get; set; }
    // For tables with no primary key: 
    /// <summary>
    /// BaseEdit
    /// </summary>
    public string EditFormInherits { get; set; } = "EditOperationDialogBase";
    /// <summary>
    /// BaseMainTable
    /// </summary>
    public string MainTableInherits { get; set; } = "ListTableBase";

    /// <summary>
    /// 模块URL
    /// url path: _sys.tool -> sys/tool 
    /// </summary>
    public string ModuleToUrl { get; set; }

    /// <summary>
    /// upper name: _sys.tool -> SysTool 
    /// </summary>
    public string ModuleUpper { get; set; }

    /// <summary>
    /// lower name: _sys.tool -> sysTool 
    /// </summary>
    public string ModuleLower { get; set; }

    /// <summary>
    /// 生成 TypeName 注解，不支持多库
    /// 示例：[Column("Username", TypeName = "Nvarchar(20)")]
    /// </summary>
    public bool GenerateDbDataTypeText { get; set; } = false;

    /// <summary>
    /// 字段Summary示例：
    /// Username 用户名
    /// </summary>
    public bool UseChineseSummary { get; set; } = true;

    /// <summary>
    /// "menu:ResourceDto.Key"
    /// </summary>
    public string MenuLocaleKey { get; set; }
    /// <summary>
    /// ResourceDto.Key: Module_ClassName
    /// </summary>
    public string MenuKey { get; set; }

    #endregion

    #region Base
    /// <summary>
    /// 表名
    /// </summary>
    [Required(ErrorMessage = "Required")]
    public string TableName { get; set; }

    /// <summary>
    /// 类名
    /// </summary>
    [Required(ErrorMessage = "Required")]
    public string ClassName { get; set; }

    /// <summary>
    /// 小写类名
    /// </summary>
    public string ClassNameLower { get; set; }

    /// <summary>
    /// 模块
    /// </summary>
    [Required(ErrorMessage = "Required")]
    public string Module { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 表英文描述
    /// </summary>
    public string TableDescriptionEN { get; set; }
    /// <summary>
    /// 表中文描述
    /// </summary>
    public string TableDescriptionCH { get; set; }
    /// <summary>
    /// 图标
    /// </summary>
    public string IconName { get; set; }
    /// <summary>
    /// 菜单英文名
    /// </summary>
    public string MenuNameEN { get; set; }
    /// <summary>
    /// 菜单中文名
    /// </summary>
    public string MenuNameCH { get; set; }
    /// <summary>
    /// 上级菜单Id
    /// </summary>
    public Guid?  MenuParentId { get; set; }

    /// <summary>
    /// 表头
    /// </summary>
    [MaxLength(5)]
    public string TablePrefix { get; set; }

    /// <summary>
    /// 命名空间
    /// </summary>
    [MaxLength(100)]
    public string NameSpace { get; set; }

    /// <summary>
    /// 为View或Edit下的Select生成对应字段
    /// </summary>
    public bool GenerateSelectFields { get; set; } = true;
    /// <summary>
    /// 使用自定义模板
    /// </summary>
    public bool UseCustomTemplate { get; set; } = false;
    /// <summary>
    /// 生成项目文件
    /// </summary>
    public bool GenerateProjectFile { get; set; } = false;
    /// <summary>
    /// XxxBaseService, XxxBaseController, _Imports.razor, SwaggerSetting.Add.json, ...
    /// </summary>
    public bool GenerateBaseClass { get; set; } = false;

    /// <summary>
    /// 生成Service/Controller
    /// </summary>
    public bool GenerateService { get; set; } = false;

    /// <summary>
    /// DB多语言Excel文件和多语言添加文件
    /// </summary>
    public bool GenerateLocaleFile { get; set; } = false;

    /// <summary>
    /// 是否使用中文key
    /// </summary>
    public bool UseChineseKey { get; set; } = false;

    /// <summary>
    /// 多语言使用C#Class名、列名作Key
    /// </summary>
    public bool UseNetColumnAsKey { get; set; } = false;

    #region Views
    /// <summary>
    /// 有的表没有主键，指定一个主键
    /// </summary>
    public string PrimaryKeyName { get; set; } = "Id";//nameof()

    /// <summary>
    /// 主键的类型
    /// </summary>
    public string PrimaryKeyType { get; set; } = "string";

    // TODO: 有时间的话或许可以用这种方式
    // AuthKeys
    // public List<string> Buttons { get; set; }
    // 在模板里：@if(Model.CodeGen.Buttons.Contains(AuthKeys.BatchDelete))
    /// <summary>
    /// 是否有添加
    /// </summary>
    public bool HasAdd { get; set; } = true;
    /// <summary>
    /// 是否有编辑
    /// </summary>
    public bool HasEdit         {get; set;}   = true;
    /// <summary>
    /// 是否有批量编辑
    /// </summary>
    public bool HasBatchEdit    {get; set;}   = true;
    /// <summary>
    /// 是否有删除
    /// </summary>
    public bool HasDelete       {get; set;}   = true;
    /// <summary>
    /// 是否有批量删除
    /// </summary>
    public bool HasBatchDelete  {get; set;}   = true;
    /// <summary>
    /// 是否有锁定
    /// </summary>
    public bool HasLock         {get; set;}   = true;
    /// <summary>
    /// 是否有导入
    /// </summary>
    public bool HasImport       {get; set;}   = true;
    /// <summary>
    /// 是否有导出
    /// </summary>
    public bool HasExport       { get; set; } = true;

    #endregion

    #region 表建表，根据某一表生成另一表的Entity和多语言文件

    /// <summary>
    /// 开启表建表
    /// </summary>
    public bool EntityFromTable { get; set; } = false;

    /// <summary>
    /// 母表所在的模块，用来搜索母表的多语言文件
    /// </summary>
    public string? OriginModule { get; set; }

    /// <summary>
    /// 新表名
    /// </summary>
    public string? NewTableName { get; set; }

    // 如果为了保持项目简洁，可以不要AllowNull字段，自己在CodeGenConfigView设置即可，这里暂时保留
    // 因为页面有点卡
    /// <summary>
    /// 1, false，正常生成Entity
    /// 2, true，根据某一表生成表建表, 设为true将在初始化CodeGenConfig时将IsNullable设置为true
    /// 然后自己在CodeGenConfig中手动设置
    /// </summary>
    public bool AllowNull { get; set; } = false;
    #endregion

    #endregion
}

