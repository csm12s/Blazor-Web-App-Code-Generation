
using Gardener.Base;
using System;
using System.Collections.Generic;
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
    // url path
    public string ModuleToUrl { get; set; }

    public string TableDescriptionEN { get; set; }
    public string TableDescriptionCH { get; set; }

    public string MenuName { get; set; }
    public Guid?  MenuParentId { get; set; }

    [MaxLength(5)]
    public string TablePrefix { get; set; }

    [MaxLength(100)]
    public string NameSpace { get; set; }

    public bool UseCustomTemplate { get; set; } = false;
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

    public bool UpdateCodeGenConfig { get; set; } = true;
}

