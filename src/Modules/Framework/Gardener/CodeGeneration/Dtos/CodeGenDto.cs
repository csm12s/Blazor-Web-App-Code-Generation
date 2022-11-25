
using Gardener.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gardener.CodeGeneration.Dtos;

public partial class CodeGenDto: BaseDto<int>
{
    #region Base
    [MaxLength(100)]
    public string TableName { get; set; }
    [Required(ErrorMessage = "Required")]
    public string ClassName { get; set; }
    public string ClassNameLower { get; set; }
    [Required(ErrorMessage = "Required")]
    public string Module { get; set; }

    public string Remark { get; set; }

    // url path
    public string ModuleToUrl { get; set; }

    public string TableDescriptionEN { get; set; }
    public string TableDescriptionCH { get; set; }

    public string MenuNameEN { get; set; }
    public string MenuNameCH { get; set; }
    public Guid?  MenuParentId { get; set; }

    [MaxLength(5)]
    public string TablePrefix { get; set; }

    [MaxLength(100)]
    public string NameSpace { get; set; }

    public bool UseCustomTemplate { get; set; } = false;
    public bool GenerateProjectFile { get; set; } = true;
    public bool UseChineseKey { get; set; } = false;
    #endregion

    #region Views
    /// <summary>
    /// 有的表没有主键，指定一个主键
    /// </summary>
    public string PrimaryKeyName { get; set; } = "Id";//nameof()

    /// <summary>
    /// string or not
    /// </summary>
    public string PrimaryKeyType { get; set; } = "string";

    // Dto fields:
    // For tables with no primary key: 
    public string EditFormInherits { get; set; } = "EditOperationDialogBase";//BaseEdit
    public string MainTableInherits { get; set; } = "ListTableBase";//BaseMainTable


    // TODO: 有时间的话或许可以用这种方式
    // AuthKeys
    // public List<string> Buttons { get; set; }
    // 在模板里：@if(Model.CodeGen.Buttons.Contains(AuthKeys.BatchDelete))
    public bool HasAdd { get; set; } = true;
    public bool HasEdit         {get; set;}   = true;
    public bool HasBatchEdit    {get; set;}   = true;
    public bool HasDelete       {get; set;}   = true;
    public bool HasBatchDelete  {get; set;}   = true;
    public bool HasLock         {get; set;}   = true;
    public bool HasImport       {get; set;}   = true;
    public bool HasExport       { get; set; } = true;

    #endregion

    #region 表建表，根据某一表生成另一表

    /// <summary>
    /// 开启表建表
    /// </summary>
    public bool EntityFromTable { get; set; } = false;

    /// <summary>
    /// 母表所在的模块，用来搜索母表的多语言文件
    /// </summary>
    public string? OriginModule { get; set; }

    // 如果为了保持项目简洁，可以不要AllowNull字段，自己在CodeGenConfigView设置即可，这里暂时保留
    // 因为页面有点卡
    /// <summary>
    /// 1，正常生成Entity不需要修改
    /// 2，根据某一表生成表建表, 设为true将在初始化CodeGenConfig时将IsNullable设置为true
    /// 然后自己在CodeGenConfig中手动设置
    /// </summary>
    public bool AllowNull { get; set; } = false;
    #endregion

    #region Dto only fields
    public bool UpdateCodeGenConfig { get; set; } = true;
    #endregion
    
}

