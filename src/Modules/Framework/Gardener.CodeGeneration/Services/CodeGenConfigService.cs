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
using Gardener.Enums;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gardener.CodeGeneration.Services;

/// <summary>
/// 代码生成配置 - DB First
/// </summary>
[ApiDescriptionSettings(Groups = new[] { "SystemBaseServices" })] //, ForceWithRoutePrefix = true, KeepName = true, KeepVerb = true, LowercaseRoute = false, SplitCamelCase = false
public class CodeGenConfigService : ServiceBase<CodeGenConfig, CodeGenConfigDto>
    , ICodeGenConfigService, ITransient
{
    private readonly IRepository<CodeGenConfig> repository;
    private readonly SqlSugarRepository<CodeGenConfig> codeGenConfigSugarRep;

    public CodeGenConfigService(
        IRepository<CodeGenConfig> repository,
        SqlSugarRepository<CodeGenConfig> codeGenConfigSugarRep) : base(repository)
    {
        this.repository = repository;
        this.codeGenConfigSugarRep = codeGenConfigSugarRep;
    }

    [NonAction]
    public async Task DeleteAndAddList(List<TableColumnInfo> dbColumnInfos, CodeGenDto codeGen)
    {
        if (dbColumnInfos == null) 
            return;

        var list = new List<CodeGenConfig>();

        foreach (var column in dbColumnInfos)
        {
            var codeGenConfig = new CodeGenConfig();

            codeGenConfig.CodeGenId = codeGen.Id;
            
            codeGenConfig.ColumnName = column.DbColumnName;
            codeGenConfig.NetColumnName = GetNetColumnName(column, codeGen);
            
            // Comment
            var desc = !string.IsNullOrEmpty(column.ColumnDescription) ? 
                    column.ColumnDescription 
                    : codeGenConfig.NetColumnName;
            codeGenConfig.ColumnDescription = desc.Replace("\r", "").Replace("\n", "");
            codeGenConfig.ColumnComment = desc.Replace("\r", "").Replace("\n", "");

            // Data type
            codeGenConfig.DataType = column.DataType;
            codeGenConfig.NetType = column.NetType;
            codeGenConfig.ColumnKey = column.ColumnKey;
            codeGenConfig.DbDataTypeText = column.DbDataTypeText;
            
            // bools
            codeGenConfig.IsPrimaryKey = column.IsPrimarykey;
            codeGenConfig.IsIdentity = column.IsIdentity;
            codeGenConfig.IsNullable = column.IsNullable;
            codeGenConfig.IsRequired = column.IsPrimarykey || !column.IsNullable;
            
            codeGenConfig.IsSearch = false;
            //codeGenConfig.SearchType = "==";
            codeGenConfig.IsView = false;
            codeGenConfig.IsCreate = false;
            codeGenConfig.IsEdit = false;
            codeGenConfig.IsBatchEdit = false;

            // client component type
            codeGenConfig.ClientComponentType = GetClientComponentType(codeGenConfig.NetType);
            codeGenConfig.CustomSearchType = GetCustomSearchType(codeGenConfig.NetType);

            list.Add(codeGenConfig);
        }

        // Delete old:
        // sugar
        //codeGenConfigSugarRep.Context.Deleteable<CodeGenConfig>()
        //    .Where(x => x.CodeGenId == codeGen.Id)
        //    .ExecuteCommand();
        // ef - Zack.EFCore.Batch_NET6
        // repository.Context
        // .DeleteRange<CodeGenConfig>(x => x.CodeGenId == codeGenId);

        var oldList = repository.Where(it => it.CodeGenId == codeGen.Id).ToList();
        await repository.DeleteNowAsync(oldList);
           
        // Insert new:
        await repository.InsertAsync(list);
    }

    private ClientComponentType GetCustomSearchType(string netType)
    {
        var type = netType.Replace("?", "");

        switch (type)
        {
            case "string":
                return ClientComponentType.Select;

            default:
                return ClientComponentType.Input;
        }
    }

    /// <summary>
    /// edit
    /// </summary>
    /// <param name="netType"></param>
    /// <returns></returns>
    private ClientComponentType GetClientComponentType(string netType)
    {
        var type = netType.Replace("?", "");

        switch (type)
        {
            case "int":
            case "double":
                return ClientComponentType.InputNumber;
            case "bool":
                return ClientComponentType.Switch;
            case "datetime":
                return ClientComponentType.DateTime;


            default:
                return ClientComponentType.Input;
        }
    }

    private string GetNetColumnName(TableColumnInfo column, CodeGenDto codeGen)
    {
        var newColumnName = column.DbColumnName
            .ToUpperCamel();

        // number
        // 处理属性名开头为数字情况
        if (System.Text.RegularExpressions.Regex.IsMatch(newColumnName.Substring(0, 1), "[0-9]"))
        {
            newColumnName = "_" + newColumnName;
        }

        // column name is class name
        // 处理属性名不能等于类名
        if (newColumnName == codeGen.ClassName)
        {
            newColumnName = "_" + newColumnName;
        }

        return newColumnName;
    }

    /// <summary>
    /// Get net type via sugar
    /// </summary>
    /// <param name="column"></param>
    /// <returns></returns>
    private string GetNetType(TableColumnInfo column)
    {
        // Special type
        if (!string.IsNullOrEmpty(column.PropertyName)
            && Regex.IsMatch(column.PropertyName, @"\[.+\]"))
        {
            return Regex.Match(column.PropertyName, @"\[(.+)\]").Groups[1].Value;
        }

        // Normal type
        string type = column.PropertyType != null ?
            column.PropertyType.Name : codeGenConfigSugarRep.Context.Ado.DbBind.GetPropertyTypeName(column.DataType);

        if (type == "byte") // tinyint
        {
            type = "bool";
        }
        else if (type == "short")
        {
            type = "int";
        }

        if (type == "String")
        {
            type = "string";
        }
        if (type == "Int32")
        {
            type = "int";
        }

        #region Nullable
        var nullable = column.IsNullable;
        var isStringNullable = true;

        if (nullable)
        {
            if (type != "string"
                && type != "byte[]"
                && type != "object") // NameModel.IsSpecialType
            {
                type += "?";
            }
            if (type == "string" && isStringNullable)
            {
                type += "?";
            }
        }
        #endregion

        return type;
    }

    [NonAction]
    public async Task DeleteByCodeGenId(int codeGenId)
    {
        var codeGenConfigList = await _repository
            .Where(u => u.CodeGenId == codeGenId)
            .ToListAsync();
        await _repository.DeleteAsync(codeGenConfigList);
    }

    public async Task<List<CodeGenConfigDto>> GetCodeGenConfigsByCodeGenId(int codeGenId)
    {
        var list = await GetAllUsable();
        return list.Where(it => it.CodeGenId == codeGenId).ToList();

        // Sugar
        //var list = await codeGenConfigSugarRep
        //    .GetListAsync(it => it.CodeGenId == codeGenId);
        //return list.MapTo<CodeGenConfigDto>();
    }

    public async Task<bool> SaveAll(List<CodeGenConfigDto> listDto)
    {
        var list = listDto.MapTo<CodeGenConfig>();
        await _repository.UpdateAsync(list);
        return true;
    }
}
