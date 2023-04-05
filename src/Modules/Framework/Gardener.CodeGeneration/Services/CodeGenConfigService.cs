// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Gardener.Base;
using Gardener.CodeGeneration.Domains;
using Gardener.CodeGeneration.Dtos;
using Gardener.Common;
using Gardener.EntityFramwork;
using Gardener.Enums;
using Gardener.Sugar;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Gardener.CodeGeneration.Services;

/// <summary>
/// 代码生成配置 - DB First
/// </summary>
[ApiDescriptionSettings(Groups = new[] { "SystemBaseServices" })] //, ForceWithRoutePrefix = true, KeepName = true, KeepVerb = true, LowercaseRoute = false, SplitCamelCase = false
public class CodeGenConfigService : ServiceBase<CodeGenConfig, CodeGenConfigDto, Guid>
    , ICodeGenConfigService, ITransient
{
    private readonly IRepository<CodeGenConfig> repository;
    private readonly SqlSugarRepository<CodeGenConfig> codeGenConfigSugarRep;
    private readonly IWebHostEnvironment env;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="codeGenConfigSugarRep"></param>
    /// <param name="env"></param>
    public CodeGenConfigService(
        IRepository<CodeGenConfig> repository,
        SqlSugarRepository<CodeGenConfig> codeGenConfigSugarRep,
        IWebHostEnvironment env) : base(repository)
    {
        this.repository = repository;
        this.codeGenConfigSugarRep = codeGenConfigSugarRep;
        this.env = env;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbColumnInfos"></param>
    /// <param name="codeGen"></param>
    /// <returns></returns>
    [NonAction]
    public async Task DeleteAndAddList(List<TableColumnInfo> dbColumnInfos, CodeGenDto codeGen)
    {
        if (dbColumnInfos == null) 
            return;

        var list = new List<CodeGenConfig>();

        #region common field / BaseModelField
        PropertyInfo[] baseModelFields = new PropertyInfo[0];
        // 这里获取不到这种：[SuppressSniffer]
        //var baseModelType = App.EffectiveTypes
        //    .Where(a => !a.IsAbstract
        //        && a.IsClass
        //        && a.Name.Equals(codeGen.Module + "BaseModel"))
        //    .FirstOrDefault();

        
        var dllName = "Gardener." + codeGen.Module + ".Server";
        Assembly a = Assembly.Load(dllName);//这里找不到dll会报错
        var baseModelType = a.GetType(dllName + "." + codeGen.Module + "BaseModel");

        if (baseModelType != null)
        {
            baseModelFields = baseModelType.GetProperties();
        }
       
        #endregion

        foreach (var column in dbColumnInfos)
        {
            var codeGenConfig = new CodeGenConfig();

            codeGenConfig.CodeGenId = codeGen.Id;

            codeGenConfig.ColumnName = column.DbColumnName;
            codeGenConfig.NetColumnName = await GetNetColumnNameAsync(column, codeGen) ?? string.Empty;

            // Data type
            codeGenConfig.DbDataType = column.DbDataType;
            codeGenConfig.NetType = column.NetType;
            codeGenConfig.ColumnKey = column.ColumnKey;
            codeGenConfig.DbDataTypeText = column.DbDataTypeText;
            codeGenConfig.DecimalDigits = column.DecimalDigits;
            codeGenConfig.Length = column.Length;

            // Comment
            var desc = !string.IsNullOrEmpty(column.ColumnDescription) ?
                    column.ColumnDescription
                    : codeGenConfig.NetColumnName;
            codeGenConfig.ColumnDescription = desc.Replace("\r", "").Replace("\n", "");
            codeGenConfig.ColumnSummary = desc.Replace("\r", "").Replace("\n", "");

            // bools
            codeGenConfig.IsPrimaryKey = column.IsPrimarykey;
            codeGenConfig.IsIdentity = column.IsIdentity;
            codeGenConfig.IsNullable = column.IsNullable;
            codeGenConfig.IsRequired = column.IsPrimarykey || !column.IsNullable;

            // 表建表
            if (codeGen.AllowNull)
            {
                codeGenConfig.IsRequired = false;
            }

            codeGenConfig.IsSearch = false;
            //codeGenConfig.SearchType = "==";
            codeGenConfig.IsView = false;
            codeGenConfig.IsCreate = false;
            codeGenConfig.IsEdit = false;
            codeGenConfig.IsBatchEdit = false;

            // client component type
            codeGenConfig.ViewComponentType = GetViewComponentType(codeGenConfig.NetType);
            codeGenConfig.EditComponentType = GetEditComponentType(codeGenConfig.NetType);
            codeGenConfig.EditComponentLength = 150;
            codeGenConfig.CustomSearchType = GetCustomSearchType(codeGenConfig.NetType);
            codeGenConfig.CustomSearchLength = 150;

            // isBaseModelField
            codeGenConfig.IsCommon = baseModelFields
                .Where(it=>it.Name == codeGenConfig.NetColumnName)
                .Any();

            list.Add(codeGenConfig);
        }

        // Delete old:
        var res = await repository.Entities
            .Where(it => it.CodeGenId == codeGen.Id)
            .ExecuteDeleteAsync();
           
        // Insert new:
        await repository.InsertAsync(list);
    }

    private ClientComponentType GetCustomSearchType(string netType)
    {
        var type = netType.Replace("?", "");

        switch (type)
        {
            case NetTypeRaw._string:
                return ClientComponentType.MultiSelect;

            default:
                return ClientComponentType.Input;
        }
    }

    /// <summary>
    /// view
    /// </summary>
    /// <param name="netType"></param>
    /// <returns></returns>
    private ClientComponentType GetViewComponentType(string netType)
    {
        var type = netType.Replace("?", "");

        switch (type)
        {
            case NetTypeRaw._byte:
            case NetTypeRaw._short:
            case NetTypeRaw._int:
            case NetTypeRaw._double:
                return ClientComponentType.InputNumber;
            case NetTypeRaw._bool:
                return ClientComponentType.TagYesNo;
            case NetTypeRaw._DateTime:
            case NetTypeRaw._DateTimeOffset:
                return ClientComponentType.DateTime;

            default:
                return ClientComponentType.Input;
        }
    }


    /// <summary>
    /// edit
    /// </summary>
    /// <param name="netType"></param>
    /// <returns></returns>
    private ClientComponentType GetEditComponentType(string netType)
    {
        var type = netType.Replace("?", "");

        switch (type)
        {
            case NetTypeRaw._byte:
            case NetTypeRaw._short:
            case NetTypeRaw._int:
            case NetTypeRaw._double:
                return ClientComponentType.InputNumber;
            case NetTypeRaw._bool:
                return ClientComponentType.Switch;
            case NetTypeRaw._DateTime:
            case NetTypeRaw._DateTimeOffset:
                return ClientComponentType.DateTime;

            default:
                return ClientComponentType.Input;
        }
    }

    private async Task<string?> GetNetColumnNameAsync(TableColumnInfo column, CodeGenDto codeGenDto)
    {
        // Custom name

        var newColumnName = column.DbColumnName;
        #region Replace db column text, SYS_ -> Sys
        // 列全局替换文本
        // Replace text by module: Gardener\src\Modules\XXX\Gardener.XXX\DB Naming
        // 表建表时，搜索母表模块下的文件
        var localeFileModule = codeGenDto.Module;
        if (codeGenDto.EntityFromTable)
        {
            localeFileModule = codeGenDto.OriginModule;
        }
        
        var appName = ProjectConstants.AppName;
        // Gardener\src\Modules\XXX\Gardener.XXX\DB\DB Naming
        var dir= FileHelper.GetParentDirectory(env.ContentRootPath);
        if(dir== null)
        {
            return null;
        }
        var replaceFolder = Path.Combine(dir,
            "Modules",
            localeFileModule,
            appName + "." + localeFileModule,
            "DB",
            "DB Naming");
        var replaceFilePath = Path.Combine(replaceFolder,
            "ColumnReplaceText" + ExcelHelper.Extension);

        if (File.Exists(replaceFilePath))
        {
            var replaceItems = await ExcelHelper.GetListAsync<CodeGenReplaceItem>(replaceFilePath);

            foreach (var item in replaceItems)
            {
                if (!string.IsNullOrEmpty(item.OriginText) && !string.IsNullOrEmpty(item.ReplacedText))
                {
                    newColumnName = newColumnName.Replace(item.OriginText, item.ReplacedText);
                }

                
            }
        }
        #endregion

        newColumnName = newColumnName
            .ToUpperCamel();

        // number
        // 处理属性名开头为数字情况
        if (System.Text.RegularExpressions.Regex.IsMatch(newColumnName.Substring(0, 1), "[0-9]"))
        {
            newColumnName = "_" + newColumnName;
        }

        // column name is class name
        // 处理属性名不能等于类名
        if (newColumnName == codeGenDto.ClassName)
        {
            newColumnName = "_" + newColumnName;
        }

        return newColumnName;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="codeGenId"></param>
    /// <returns></returns>
    [NonAction]
    public async Task DeleteByCodeGenId(Guid codeGenId)
    {
        var codeGenConfigList = await _repository
            .Where(u => u.CodeGenId == codeGenId)
            .ToListAsync();
        await _repository.DeleteAsync(codeGenConfigList);
    }
    /// <summary>
    /// 获取代码生成配置
    /// </summary>
    /// <param name="codeGenId"></param>
    /// <returns></returns>
    public async Task<List<CodeGenConfigDto>> GetCodeGenConfigsByCodeGenId(Guid codeGenId)
    {
        var list = await GetAllUsable();
        return list.Where(it => it.CodeGenId == codeGenId).ToList();

        // Sugar
        //var list = await codeGenConfigSugarRep
        //    .GetListAsync(it => it.CodeGenId == codeGenId);
        //return list.MapTo<CodeGenConfigDto>();
    }
    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="listDto"></param>
    /// <returns></returns>
    public async Task<bool> SaveAll(List<CodeGenConfigDto> listDto)
    {
        var list = listDto.MapTo<CodeGenConfig>();
#region 处理更新数据
// TODO: 这里在企图更新的时候用Get(item.Id);，会报错
//异常: The instance of entity type 'CodeGenConfig' cannot be tracked
//because another instance with the key value '{Id: 3485}' is already
//being tracked.When attaching existing entities,
//ensure that only one entity instance with a given key value is attached.[500]

        // 如果为了项目简洁，可以不在这里处理，直接把各种字段如IsNullable，IsRequired,
        // NetType在前端呈现，然后在模板里处理字段
        //foreach (var item in list)
        //{
        //    var oldConfig = await Get(item.Id);

        //    // IsRequired is changed
        //    if (item.IsRequired != oldConfig.IsRequired)
        //    {
        //        //必填项不可为Null
        //        if (item.IsRequired) // 改为必填
        //        {
        //            item.IsNullable = false;
        //            item.NetType = item.NetType.Replace("?", "");
        //        }
        //        else // 改为非必填
        //        {
        //            if (!item.IsPrimaryKey)
        //            {
        //                item.IsNullable = true;

        //                if (!item.NetType.Contains("?"))
        //                {
        //                    item.NetType += "?";
        //                }
        //            }
        //        }
        //    }
        //}
        #endregion

        await _repository.UpdateNowAsync(list);
        return true;
    }
}
