using Furion.DatabaseAccessor;
using Furion.DatabaseAccessor.Extensions;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Gardener.Base;
using Gardener.Base.Enums;
using Gardener.CodeGeneration.Domains;
using Gardener.CodeGeneration.Dtos;
using Gardener.Common;
using Gardener.Enums;
using Gardener.SystemManager.Dtos;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using MiniExcelLibs;
using RazorEngine;
using RazorEngine.Templating;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.CodeGeneration.Services;

/// <summary>
/// 代码生成 - DB First
/// </summary>
[ApiDescriptionSettings(Groups = new[] { "SystemBaseServices" })] //, ForceWithRoutePrefix = true, KeepName = true, KeepVerb = true, LowercaseRoute = false, SplitCamelCase = false
public class CodeGenService : ServiceBase<CodeGen, CodeGenDto>, 
    ICodeGenService, ITransient
{
    #region Init
    private readonly IRepository<CodeGen> codeGenRepository;
    private readonly IRepository<CodeGenConfig> codeGenConfigRepository;
    private readonly SqlSugarRepository<CodeGen> codeGenSugarRep;

    private readonly ICodeGenConfigService codeGenConfigService;
    private readonly IWebHostEnvironment env;

    public CodeGenService(IRepository<CodeGen> repository,
        IRepository<CodeGenConfig> configRepository,
        ICodeGenConfigService codeGenConfigService,
        IWebHostEnvironment env,
        SqlSugarRepository<CodeGen> sugarRepository) : base(repository)
    {
        this.codeGenRepository = repository;
        this.codeGenConfigRepository = configRepository;
        this.codeGenConfigService = codeGenConfigService;
        this.env = env;
        this.codeGenSugarRep = sugarRepository;
    }
    #endregion

    public async Task<List<TableOutput>> GetTableListAsync()//string dbContextLocatorName = ""
    {
        // Sugar, 这里可以取到数据库中所有的表
        var dbTableInfos = codeGenSugarRep.Context.DbMaintenance
                .GetTableInfoList()
                .OrderBy((it) => it.Name)
                .ToList();
        var tableInfos = dbTableInfos.Select(it => new TableOutput
        {
            DatabaseName = "",
            TableName = it.Name,
            EntityName = "",
            TableComment = it.Description
        }).ToList();


        // ef
        //var dbContextLocatorName = ""; // todo, 前端数据库选择
        //var dbContext = Db.GetDbContext();//默认数据库
        //if (!string.IsNullOrEmpty(dbContextLocatorName))
        //{
        //    var dbContentLocator = AppDomain.CurrentDomain.GetAssemblies()
        //               .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IDbContextLocator)))).Where(x => x.Name == dbContextLocatorName).FirstOrDefault();

        //    dbContext = Db.GetDbContext(dbContentLocator);
        //}

        // TODO: 这里只能取到本项目生成的表
        //var tableInfos = dbContext.GetService<IDesignTimeModel>().Model
        //    .GetEntityTypes().Select(it => new TableOutput
        //    {
        //        DatabaseName = dbContextLocatorName,
        //        TableName = it.GetTableName(),
        //        EntityName = it.GetDefaultTableName(),
        //        TableComment = it.GetComment()
        //    }).ToList();

        return tableInfos;
    }

    #region Insert
    public override async Task<CodeGenDto> Insert(CodeGenDto input)
    {
        var bIsExist = await codeGenRepository.AnyAsync(x => x.TableName == input.TableName);
        if (bIsExist)
        {
            throw Oops.Bah(ExceptionCode.Table_Name_Exist);
        }

        var codeGen = input.Adapt<CodeGen>();
        var newCodeGen = await codeGen.InsertNowAsync();

        // config
        await codeGenConfigService.DeleteAndAddList(GetDBColumnInfos(input), newCodeGen.Entity.MapTo<CodeGenDto>());

        return newCodeGen.Entity.Adapt<CodeGenDto>();
    }
    #endregion

    #region Update
    public override async Task<bool> Update(CodeGenDto input)
    {
        var isExist = await codeGenRepository
            .AnyAsync(u => u.TableName == input.TableName && u.Id != input.Id);
        if (isExist)
            throw Oops.Bah(ExceptionCode.Table_Name_Exist);

        // crud filter
        input.SetPropertyValue(nameof(GardenerEntityBase.UpdatedTime), DateTimeOffset.Now);

        // Update
        var entityEntry = await _repository
            .UpdateExcludeAsync(input.Adapt<CodeGen>(), new[] { nameof(GardenerEntityBase.CreatedTime), nameof(GardenerEntityBase.CreatorId), nameof(GardenerEntityBase.CreatorIdentityType) });
        if (input.UpdateCodeGenConfig)
        {
            await codeGenConfigService.DeleteAndAddList(GetDBColumnInfos(input), input);
        }

        //发送通知
        await EntityEventNotityUtil.NotifyUpdateAsync(entityEntry.Entity);
        return true;
    }
    #endregion

    #region Delete
    public override async Task<bool> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        await codeGenConfigService.DeleteByCodeGenId(id);
        
        //发送删除通知
        await EntityEventNotityUtil.NotifyDeleteAsync<CodeGen, int>(id);
        return true;
    }
    #endregion

    [NonAction]
    private List<TableColumnInfo> GetDBColumnInfos([FromQuery] CodeGenDto input)
    {
        #region EF，TODO: 这里只能获取到本项目的Model
        //// 可以参考OpenAuth.Core：_dbExtension.GetDbTableStructure(obj.TableName)
        //var dbContext = Db.GetDbContext();

        //// Entity type
        //var entityType = dbContext.GetService<IDesignTimeModel>().Model.GetEntityTypes()
        //    .FirstOrDefault(it => it.GetTableName() == input.TableName); // u => u.ClrType.Name: Entity name
        //if (entityType == null)
        //    return null;

        //// Get columns
        //var type = entityType.ClrType;
        //if (type == null) return null;

        //// 按原始类型的顺序获取所有实体类型属性（不包含导航属性，会返回null）
        //var efColumnInfos = type.GetProperties()
        //    .Select(propertyInfo => entityType.FindProperty(propertyInfo.Name))
        //    .Where(p => p != null)
        //    .ToList();

        //var columnInfos = efColumnInfos.Select(p => new TableColumnInfo
        //{
        //    DbColumnName = p.Name,
        //    ColumnKey = p.IsKey().ToString(),
        //    PropertyType = p.PropertyInfo.PropertyType,
        //    ColumnDescription = p.GetComment(),

        //    IsPrimarykey = p.IsPrimaryKey(),
        //    IsIdentity = p.IsIndex(),
        //    IsNullable = p.IsNullable,
        //    Length = p.GetMaxLength()
        //}).ToList();
        #endregion

        // Sugar
        List<DbColumnInfo> sugarColumnInfos = codeGenSugarRep.Context.DbMaintenance
                .GetColumnInfosByTableName(input.TableName);
        var columnInfos = sugarColumnInfos.MapTo<TableColumnInfo>();

        // Prepare info
        foreach (var column in columnInfos)
        {
            // Data type
            column.SysDataType = column.PropertyType?.ToString();
            column.DbDataType = column.DataType;

            // Net type
            //EF, TODO: 如果EF可以获取所有数据库表，这里可以使用EF, column.SysDataType已经是EF通过Model获取到的NetType
            //codeGenConfig.NetType = CodeGenUtil.GetNetTypeBySystemType(column.SysDataType);
            column.NetType = CodeGenUtil.GetNetTypeByDBType(column.DbDataType);
            //Sugar
            //codeGenConfig.NetType = GetNetType(column);

            // DbDataTypeText
            if (column.DbDataType.Contains("varchar"))
            {
                var length = "Max";
                if (column.Length > 0)
                {
                    length = column.Length.ToString();
                }

                column.DbDataTypeText = column.DbDataType.FirstToUpper() +
                    "(" + length + ")";
            }
        }

        return columnInfos;
    }

    public async Task<bool> GenerateCode([FromBody] int[] codeGenIds)
    {
        var allCodeGens = await GetAll();
        var codeGens = allCodeGens.Where(it => codeGenIds.Contains(it.Id)).ToList();

        string baseGenPath = ProjectConstants.CodeGenPath;

        foreach (var codeGen in codeGens)
        {
            await GenerateCodeAsync(codeGen, baseGenPath);
        }
        FileHelper.OpenFolder(baseGenPath);

        return true;
    }

    [NonAction]
    private async Task GenerateCodeAsync(CodeGenDto genTable, 
        string baseGenPath)
    {
        if (genTable.UseCustomTemplate)
        {
            await GenerateCustomCodeAsync(genTable, baseGenPath);
        }
        else
        {
            await GenerateDefaultCodeAsync(genTable, baseGenPath);
        }
    }

    [NonAction]
    private async Task GenerateDefaultCodeAsync(CodeGenDto genTable, string baseGenPath)
    {
        // todo: 参考 GenerateCustomCodeAsync，生成Gardener默认的代码
        await GenerateCustomCodeAsync(genTable, baseGenPath);
    }

    [NonAction]
    private async Task GenerateCustomCodeAsync(CodeGenDto genTable, string baseGenPath)
    {
        string appName = "Gardener";

        var itemList = new List<CodeGenTemplateItem>();
        var templatePath = Path.Combine(env.WebRootPath, "Template");
        var localePath = Path.Combine(env.WebRootPath, "DB Locale");

        //\Modules\Mes
        // var modulePath = Path.Combine(baseGenPath, appName, "Modules", genTable.PackageName);
        var modulePath = Path.Combine(baseGenPath, genTable.Module);
        //\Modules\Mes\Gardener.Mes
        var baseModulePath = Path.Combine(modulePath, appName + "." + genTable.Module);
        //\Modules\Mes\Gardener.Mes.Client
        var clientModulePath = Path.Combine(modulePath, appName + "." + genTable.Module + ".Client");
        //\Modules\Mes\Gardener.Mes.Server
        var serverModulePath = Path.Combine(modulePath, appName + "." + genTable.Module + ".Server");

        #region Name model

        // CodeGenConfig
        var codeGenConfigs = await codeGenConfigRepository
            .Where(u => u.CodeGenId == genTable.Id)
            .ToListAsync();
        var codeGenConfigDtos = codeGenConfigs.MapTo<CodeGenConfigDto>();

        #region Get Locale from file
        var readLocaleItems = new List<CodeGenLocaleItem>();
        var tableLocale = new CodeGenLocaleItem();
        var columnLocales = new List<CodeGenLocaleItem>();

        var localeFilePath = Path.Combine(localePath, genTable.Module,
            genTable.TableName + ExcelHelper.Extension);
        if (File.Exists(localeFilePath))
        {
            readLocaleItems = await ExcelHelper.GetListAsync<CodeGenLocaleItem>(localeFilePath);
            foreach (var item in readLocaleItems)
            {
                if (string.IsNullOrEmpty(item.ValueCN))
                {
                    item.ValueCN = item.ValueEN;
                }
            }
            tableLocale = readLocaleItems.FirstOrDefault();
            columnLocales = readLocaleItems.Skip(1).ToList();
        }
        else // no locale file
        {
            tableLocale = new CodeGenLocaleItem()
            {
                Name = genTable.TableName,
                Key = genTable.ClassName,
                ValueEN = genTable.TableDescriptionEN,
                ValueCN = genTable.TableDescriptionCH,
            };
        }

        #region Generate a new locale excel
        var newLocaleItems = new List<CodeGenLocaleItem>();
        newLocaleItems.Add(tableLocale);

        foreach (var configDto in codeGenConfigDtos)
        {
            var matchLocale = columnLocales
                .Where(it => it.Name == configDto.ColumnName)
                .FirstOrDefault();
            if (matchLocale != null)
            {
                newLocaleItems.Add(matchLocale);
            }
            else
            {
                newLocaleItems.Add(new CodeGenLocaleItem()
                {
                    Name = configDto.ColumnName,
                    Key = configDto.ColumnDescription.ToUpperCamel(),
                    ValueEN = configDto.ColumnDescription,
                    ValueCN = configDto.ColumnDescription,
                });
            }
        }

        var filePath = Path.Combine(modulePath,
            genTable.TableName + ExcelHelper.Extension);
        try
        {
            await ExcelHelper.SaveAsReplaceAsync(filePath, newLocaleItems);
        }
        catch (Exception ex)
        {
        }

        // no order by
        //var matchLocales = columnLocales
        //    .Where(it => codeGenConfigDtos.Select(it => it.ColumnName).ToList()
        //        .Contains(it.Name))
        //        .ToList();

        //newLocaleItems.AddRange(matchLocales);
        #endregion

        #region set locale key for code gen columns
        // change key to: _Module._Model.ColumnName
        // Table: _Module._Model
        tableLocale.Key = "_" + genTable.Module +
            "._" + tableLocale.Key;
        foreach (var item in columnLocales)
        {
            // Column: _Module._Model.ColumnName
            item.Key = tableLocale.Key +
                "." + item.Key;
        }

        // set key for client view
        foreach (var configDto in codeGenConfigDtos)
        {
            var matchLocale = columnLocales
                .Where(it => it.Name == configDto.ColumnName)
                .FirstOrDefault();
            if (matchLocale != null)
            {
                configDto.ColumnLocaleKey = matchLocale.Key;
            }
            else
            {
                configDto.ColumnLocaleKey = configDto.ColumnDescription;
            }
        }
        #endregion
        #endregion

        #region New name model
        var codeGenDto = genTable.Adapt<CodeGenDto>();
        codeGenDto.ClassNameLower = codeGenDto.ClassName.ToLowerCamel();
        // url path: sys.tool -> sys/tool 
        codeGenDto.ModuleToUrl = codeGenDto.Module.Replace(".", "/").Replace("_", "");
        // Primary key
        codeGenDto.PrimaryKeyType = await GetPrimaryKeyType(codeGenDto);
        if (codeGenDto.PrimaryKeyName != "Id")
        {
            codeGenDto.EditFormInherits = "BaseEdit";
            codeGenDto.MainTableInherits = "BaseMainTable";
        }

        // TableDesc
        var tableDesc = codeGenDto.ClassName;
        if (!string.IsNullOrEmpty(codeGenDto.TableDescriptionEN))
        {
            tableDesc = codeGenDto.TableDescriptionEN;
        }

        CodeGenNameModel nameModel = new CodeGenNameModel()
        {
            TableName = codeGenDto.TableName,
            ClassName = codeGenDto.ClassName,
            ClassNameLower = codeGenDto.ClassNameLower,
            Module = codeGenDto.Module,
            ModuleToUrl = codeGenDto.ModuleToUrl,
            TableDesc = tableDesc,

            CodeGen = codeGenDto,
            CodeGenConfigs = codeGenConfigDtos
        };
        nameModel.AppName = appName;

        nameModel.TableLocaleKey = tableLocale.Key;
        nameModel.LocaleItems.Add(tableLocale);
        nameModel.LocaleItems.AddRange(columnLocales);
        #endregion
        // Custom search
        if (codeGenConfigDtos.Where(it => it.IsCustomSearch).ToList().Count > 0)
        {
            nameModel.HasCustomSearch = true;
        }

        // HasRemoteImage
        if (codeGenConfigDtos
            .Where(c => c.ClientComponentType == ClientComponentType.RemoteImage)
            .ToList().Any())
        {
            nameModel.HasRemoteImage = true;
        }

        // Menu
        List<ResourceDto> menus = new();
        menus.Add(new ResourceDto()
        {
            Id = Guid.NewGuid(),
            ParentId = genTable.MenuParentId,
            Name = genTable.MenuName,
            Key = nameModel.Module + "_" + nameModel.ClassName,
            Path = string.Format("/{0}/{1}", nameModel.Module, nameModel.ClassName),
            Icon = "Icon Name",
            Order = 0,
            Type = ResourceType.Menu,
            CreatedTime = DateTimeOffset.Now,
            IsLocked = false,
            IsDeleted = false,
        });

        // Menu Buttons / Actions
        menus.Add(NewAction(AuthKeys.Search, menus.First(), genTable));
        menus.Add(NewAction(AuthKeys.Add, menus.First(), genTable));
        menus.Add(NewAction(AuthKeys.Edit, menus.First(), genTable));
        menus.Add(NewAction(AuthKeys.Delete, menus.First(), genTable));
        menus.Add(NewAction(AuthKeys.Lock, menus.First(), genTable));

        nameModel.Menus = menus;
        #endregion

        // Code gen template items
        #region Base Module
        // csproj.razor
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "Base.csproj.razor"),
            GenPath = Path.Combine(baseModulePath, appName + "." + genTable.Module + ".csproj")
        });

        // Dto
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "EntityDto.cs.razor"),
            GenPath = Path.Combine(baseModulePath, "Dto", genTable.ClassName + "Dto.cs")
        });

        // SearchDto
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "EntitySearchDto.cs.razor"),
            GenPath = Path.Combine(baseModulePath, "Dto", genTable.ClassName + "SearchDto.cs")
        });

        // IBaseController
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "IBaseController.cs.razor"),
            GenPath = Path.Combine(baseModulePath, "I" + genTable.Module + "BaseController.cs")
        });

        // IController
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "IController.cs.razor"),
            GenPath = Path.Combine(baseModulePath, "IController", "I" + genTable.ClassName + "Controller.cs")
        });

        #endregion

        #region Client module
        // csproj.razor
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "Client.csproj.razor"),
            GenPath = Path.Combine(clientModulePath,
                appName + "." + genTable.Module + ".Client.csproj")
        });

        // base client controller
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "BaseClientController.cs.razor"),
            GenPath = Path.Combine(clientModulePath, genTable.Module + "BaseClientController.cs")
        });

        // controller
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "ClientController.cs.razor"),
            GenPath = Path.Combine(clientModulePath, "Controller", genTable.ClassName + "ClientController.cs")
        });

        // module base table
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "BaseTable.cs.razor"),
            GenPath = Path.Combine(clientModulePath, genTable.Module + "BaseTable.cs")
        });

        // views/imports
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "ClientImports.razor"),
            GenPath = Path.Combine(clientModulePath, "Views",
                "_Imports.razor")
        });

        // main view
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "ClientView.razor"),
            GenPath = Path.Combine(clientModulePath, "Views", genTable.ClassName, genTable.ClassName + "View.razor")
        });
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "ClientView.razor.cs.razor"),
            GenPath = Path.Combine(clientModulePath, "Views", genTable.ClassName, genTable.ClassName + "View.razor.cs")
        });

        // edit view
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "ClientEdit.razor"),
            GenPath = Path.Combine(clientModulePath, "Views", genTable.ClassName, genTable.ClassName + "Edit.razor")
        });
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "ClientEdit.razor.cs.razor"),
            GenPath = Path.Combine(clientModulePath, "Views", genTable.ClassName, genTable.ClassName + "Edit.razor.cs")
        });

        // sql server
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "SqlServer - Insert Menu.sql.razor"),
            GenPath = Path.Combine(modulePath, genTable.ClassName + " - SqlServer Insert Menu.sql")
        });

        // Swagger settings
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "SwaggerSetting.razor"),
            GenPath = Path.Combine(modulePath, genTable.Module + " - SwaggerSetting.Add.json")
        });

        #endregion

        #region Server module
        // csproj.razor
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "Server.csproj.razor"),
            GenPath = Path.Combine(serverModulePath,
                appName + "." + genTable.Module + ".Server.csproj")
        });

        // Model
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "Entity.cs.razor"),
            //sugar:
            //TemplatePath = Path.Combine(templatePath, "Model.cs.razor"),
            GenPath = Path.Combine(serverModulePath, "Model", genTable.ClassName + ".cs")
        });

        //Service
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "Service.cs.razor"),
            GenPath = Path.Combine(serverModulePath, "Service", genTable.ClassName + "Service.cs")
        });

        // base controller
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "BaseController.cs.razor"),
            GenPath = Path.Combine(serverModulePath, genTable.Module + "BaseController.cs")
        });

        //Controller
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "Controller.cs.razor"),
            GenPath = Path.Combine(serverModulePath, "Controller", genTable.ClassName + "Controller.cs")
        });
        #endregion

        #region Locale
        // en
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "DBLocale.EN.razor"),
            GenPath = Path.Combine(modulePath, genTable.ClassName + " - DBLocale.EN.txt")
        });
        // ch
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "DBLocale.CH.razor"),
            GenPath = Path.Combine(modulePath, genTable.ClassName + " - DBLocale.CH.txt")
        });
        #endregion

        GenerateCodeByTemplate(itemList);
    }

    private async Task<string> GetPrimaryKeyType(CodeGenDto codeGenDto)
    {
        var pkType = "int";

        var codeGenConfigs = await codeGenConfigRepository
            .Where(it => it.CodeGenId == codeGenDto.Id)
            .ToListAsync();

        var codeGenConfig = codeGenConfigs
            .Where(it => it.NetColumnName == codeGenDto.PrimaryKeyName)
            .FirstOrDefault();

        if (codeGenConfig != null)
        {
            pkType = codeGenConfig.NetType.Replace("?", "");
        }

        return pkType;
    }

    private ResourceDto NewAction(string authKey, ResourceDto parentMenu, CodeGenDto genTable)
    {
        var button = new ResourceDto()
        {
            Id = Guid.NewGuid(),
            ParentId = parentMenu.Id,
            Name = authKey + genTable.TableDescriptionEN,
            Key = parentMenu.Key + "_" + authKey,
            Type = ResourceType.Action,
            CreatedTime = DateTimeOffset.Now,
            Order = 0,
            IsLocked = false,
            IsDeleted = false,
        };

        return button;
    }

    [NonAction]
    private void GenerateCodeByTemplate(List<CodeGenTemplateItem> itemList)
    {
        foreach (var genItem in itemList)
        {
            if (!File.Exists(genItem.TemplatePath))
            {
                // TODO: 未报错
                throw Oops.Oh(ExceptionCode.NO_INCLUD_FILE, "文件不存在：" + genItem.TemplatePath);
                //throw Oops.Oh("文件不存在：" + genItem.TemplatePath);
            }

            // razor engine . net core
            var result = GetCodeResult(genItem);
            //System.IO.File.WriteAllText(genItem.GenPath, result, System.Text.Encoding.UTF8);
            FileHelper.CreateFileReplace(genItem.GenPath, result);

            // TODO: Furion
            //var tContent = System.IO.File.ReadAllText(genItem.TemplatePath);
            //var tResult = _viewEngine.RunCompileFromCached(tContent, genItem.Model);
            //System.IO.File.WriteAllText(genItem.GenPath, tResult, System.Text.Encoding.UTF8);
        }
    }

    [NonAction]
    private string GetCodeResult(CodeGenTemplateItem genItem)
    { // Name should be identical, use GetTick()
        var template = File.ReadAllText(genItem.TemplatePath);
        var model = genItem.Model;

        try
        {
            return Engine.Razor.RunCompile(template,
                FileHelper.GetGuid32() + model.ClassName,
                model.GetType(),
                model);
        }
        catch (Exception ex)
        {
            throw Oops.Oh(ExceptionCode.Code_Gen_Template_Compile_Error);
        }
    }

    // End
}
