using Furion.DatabaseAccessor;
using Furion.DatabaseAccessor.Extensions;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Gardener.Base;
using Gardener.Base.Enums;
using Gardener.CodeGeneration.Domains;
using Gardener.CodeGeneration.Dtos;
using Gardener.Common;
using Gardener.EntityFramwork;
using Gardener.Enums;
using Gardener.SqlSugar;
using Gardener.SystemManager.Dtos;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorEngine;
using RazorEngine.Templating;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
        // 这里不检查重复表名，因为有时候需要根据一个表生成另一个表，例如同步表
        //var bIsExist = await codeGenRepository.AnyAsync(x => x.TableName == input.TableName);
        //if (bIsExist)
        //{
        //    throw Oops.Bah(ExceptionCode.Table_Name_Exist);
        //}

        var newCodeGenDto = await base.Insert(input);

        // config
        await codeGenConfigService.DeleteAndAddList(GetDBColumnInfos(input), newCodeGenDto);

        return newCodeGenDto;
    }
    #endregion

    #region Update
    public override async Task<bool> Update(CodeGenDto input)
    {
        // 这里不检查重复表名，因为有时候需要根据一个表生成另一个表，例如同步表
        //var isExist = await codeGenRepository
        //    .AnyAsync(u => u.TableName == input.TableName && u.Id != input.Id);
        //if (isExist)
        //    throw Oops.Bah(ExceptionCode.Table_Name_Exist);

        // crud filter
        input.SetPropertyValue(nameof(GardenerEntityBase.UpdatedTime), DateTimeOffset.Now);

        // Update
        var entityEntry = await _repository
            .UpdateExcludeAsync(input.Adapt<CodeGen>(), new[] { nameof(GardenerEntityBase.CreatedTime), nameof(GardenerEntityBase.CreateBy), nameof(GardenerEntityBase.CreateIdentityType) });
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
    private List<TableColumnInfo> GetDBColumnInfos([FromQuery] CodeGenDto codeGenDto)
    {
        #region EF，TODO: 这里只能获取到本项目的Model
        //// 可以参考OpenAuth.Core：_dbExtension.GetDbTableStructure(obj.TableName)
        //var dbContext = Db.GetDbContext();

        //// Entity type
        //var entityType = dbContext.GetService<IDesignTimeModel>().Model.GetEntityTypes()
        //    .FirstOrDefault(it => it.GetTableName() == codeGenDto.TableName); // u => u.ClrType.Name: Entity name
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
                .GetColumnInfosByTableName(codeGenDto.TableName);
        var columnInfos = sugarColumnInfos.MapTo<TableColumnInfo>();

        // Prepare info
        foreach (var column in columnInfos)
        {
            // Data type
            column.SysDataType = column.PropertyType?.ToString();
            column.DbDataType = column.DataType;
            // 同步表设置，用于初始化IsNullable
            if (codeGenDto.EntityFromTable && codeGenDto.AllowNull)
            {
                column.IsNullable = true;
            }

            // Net type
            //EF, TODO: 如果EF可以获取所有数据库表，这里可以使用EF, column.SysDataType已经是EF通过Model获取到的NetType
            //GetNetTypeByDBType 只有SqlServer 其他数据库 可以参考OpenAuth.Core：_dbExtension.GetDbTableStructure(obj.TableName)
            // 也可以参考GetNetType(column);
            //codeGenConfig.NetType = CodeGenUtil.GetNetTypeBySystemType(column.SysDataType);

            // EF SqlServer
            //column.NetType = CodeGenUtil.GetNetTypeByDBType(column.DbDataType);
            // 这里面需要根据IsNullable加个？

            // Sugar, 支持多库
            column.NetType = GetNetType(column);
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
    private async Task GenerateCustomCodeAsync(CodeGenDto codeGenDto, string baseGenPath)
    {
        string appName = ProjectConstants.AppName;

        var itemList = new List<CodeGenTemplateItem>();
        var templatePath = Path.Combine(env.WebRootPath, "Template");

        // 表建表时，搜索母表模块下的多语言文件
        var localeFileModule = codeGenDto.Module;
        if (codeGenDto.EntityFromTable)
        {
            localeFileModule = codeGenDto.OriginModule;
        }
        // Locale file path: Gardener\src\Modules\XXX\Gardener.XXX\DB Locale
        var localePath = Path.Combine(FileHelper.GetParentDirectory(env.ContentRootPath), 
            "Modules",
            localeFileModule,
            appName + "." + localeFileModule,
            "DB Locale");
        //var localePath = Path.Combine(env.WebRootPath, "DB Locale");

        //\Modules\Mes
        // var modulePath = Path.Combine(baseGenPath, appName, "Modules", genTable.PackageName);
        var modulePath = Path.Combine(baseGenPath, codeGenDto.Module);
        //\Modules\Mes\Gardener.Mes
        var baseModulePath = Path.Combine(modulePath, appName + "." + codeGenDto.Module);
        //\Modules\Mes\Gardener.Mes.Client
        var clientModulePath = Path.Combine(modulePath, appName + "." + codeGenDto.Module + ".Client");
        //\Modules\Mes\Gardener.Mes.Server
        var serverModulePath = Path.Combine(modulePath, appName + "." + codeGenDto.Module + ".Server");

        #region Name model
        // CodeGenConfig
        var codeGenConfigs = await codeGenConfigRepository
            .Where(u => u.CodeGenId == codeGenDto.Id)
            .ToListAsync();
        var codeGenConfigDtos = codeGenConfigs.MapTo<CodeGenConfigDto>();

        #region Entity Attributes
        foreach (var column in codeGenConfigDtos)
        {
            #region MaxLengthText
            var maxLengthStr = (column.NetType.Contains("string")
                && column.Length != null
                && column.Length > 0) ?
                "[MaxLength(" + column.Length.ToString() + ")]"
                : "";
            column.MaxLengthText = maxLengthStr;
            #endregion

            #region DbDataTypeText
            if (true)// sql server
            {
                // TODO: 这里处理string类型和长度，例如SqlServer的varchar，nvarchar,
                // 反应到生成的Entity，可以比较直观的看到对应数据库的类型
                // 如果图方便可以这里不做处理，在Entity设置MaxLength
                // string
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

                // datetime
                // EF CodeFirst会将datetime自动映射成datetime2, 这里需要手动设置一下
                // https://learn.microsoft.com/zh-cn/ef/core/modeling/entity-properties?tabs=data-annotations%2Cwithout-nrt
                // 也可以设置[Precision(0)]?
                if (column.DbDataType.Contains("datetime"))
                {
                    column.DbDataTypeText = column.DbDataType.FirstToUpper();
                }
            }
            #endregion
        }

        #endregion

        #region Get Locale from file
        var readLocaleItems = new List<CodeGenLocaleItem>();
        var tableLocale = new CodeGenLocaleItem();
        var columnLocales = new List<CodeGenLocaleItem>();

        var localeFilePath = Path.Combine(localePath, //genTable.Module,
            codeGenDto.TableName + ExcelHelper.Extension);
        if (File.Exists(localeFilePath))
        {
            readLocaleItems = await ExcelHelper.GetListAsync<CodeGenLocaleItem>(localeFilePath);
            foreach (var item in readLocaleItems)
            {
                if (string.IsNullOrEmpty(item.ValueCH))
                {
                    item.ValueCH = item.ValueEN;
                }
            }
            tableLocale = readLocaleItems.FirstOrDefault();
            columnLocales = readLocaleItems.Skip(1).ToList();
        }
        else // no locale file
        {
            tableLocale = new CodeGenLocaleItem()
            {
                Name = codeGenDto.TableName,
                Key = codeGenDto.ClassName,
                ValueEN = codeGenDto.TableDescriptionEN,
                ValueCH = codeGenDto.TableDescriptionCH,
            };
        }

        #region Generate a new locale excel
        if (!File.Exists(localeFilePath)
            || codeGenDto.EntityFromTable)//表建表模式
        {
            var newLocaleItems = new List<CodeGenLocaleItem>();
            newLocaleItems.Add(tableLocale);

            // 表建表处理, 修改表名
            if (codeGenDto.EntityFromTable)
            {
                newLocaleItems.FirstOrDefault().Name = codeGenDto.ClassName;
                newLocaleItems.FirstOrDefault().Key = codeGenDto.ClassName;
            }
            foreach (var configDto in codeGenConfigDtos)
            {
                // 表建表处理, 只输出选中的字段
                if (codeGenDto.EntityFromTable)
                {
                    if (!configDto.IsEntity)
                        continue;
                }

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
                        ValueCH = configDto.ColumnDescription,
                    });
                }
            }

            var filePath = Path.Combine(modulePath,
                newLocaleItems.FirstOrDefault().Name + ExcelHelper.Extension);
            try
            {
                await ExcelHelper.SaveAsReplaceAsync(filePath, newLocaleItems);
            }
            catch (Exception ex)
            {
            }
        }
        #endregion


        #region set locale key for code gen columns
        // change key to: _Module._Model.ColumnName
        // Table: _Module._Model
        tableLocale.Key = "_" + codeGenDto.Module +
            "._" + tableLocale.Key;

        // 中文Key
        if (codeGenDto.UseChineseKey)
        {
            tableLocale.Key = "_" + codeGenDto.Module +
            "._" + tableLocale.ValueCH;
        }

        foreach (var item in columnLocales)
        {
            // Column: _Module._Model.ColumnName
            item.Key = tableLocale.Key +
                "." + item.Key;

            // 中文Key
            if (codeGenDto.UseChineseKey)
            {
                item.Key = tableLocale.Key +
                "." + item.ValueCH;
            }
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

        #region Prepare new name model
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

        // Locale
        nameModel.TableLocaleKey = tableLocale.Key;
        // Menu name
        if (!string.IsNullOrEmpty(codeGenDto.MenuNameEN))
        {
            nameModel.LocaleItems.Add(new CodeGenLocaleItem()
            {
                Name = codeGenDto.MenuNameEN,
                Key = codeGenDto.MenuNameEN,
                ValueEN = codeGenDto.MenuNameEN,
                ValueCH = codeGenDto.MenuNameCH,
            });
        }
        else
        {
            codeGenDto.MenuNameEN = tableLocale.Key;
        }
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
            ParentId = codeGenDto.MenuParentId,
            Name = codeGenDto.MenuNameEN,
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
        menus.Add(NewAction(AuthKeys.Search, menus.First(), codeGenDto));
        menus.Add(NewAction(AuthKeys.Add, menus.First(), codeGenDto));
        menus.Add(NewAction(AuthKeys.Edit, menus.First(), codeGenDto));
        menus.Add(NewAction(AuthKeys.Delete, menus.First(), codeGenDto));
        menus.Add(NewAction(AuthKeys.Lock, menus.First(), codeGenDto));

        nameModel.Menus = menus;
        #endregion

        // Code gen template items
        #region Base Module
        // csproj.razor
        if (codeGenDto.GenerateProjectFile)
        {
            itemList.Add(new CodeGenTemplateItem(nameModel)
            {
                TemplatePath = Path.Combine(templatePath, "Base.csproj.razor"),
                GenPath = Path.Combine(baseModulePath, appName + "." + codeGenDto.Module + ".csproj")
            });
        }

        // Dto
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "EntityDto.cs.razor"),
            GenPath = Path.Combine(baseModulePath, "Dto", codeGenDto.ClassName + "Dto.cs")
        });

        // SearchDto
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "EntitySearchDto.cs.razor"),
            GenPath = Path.Combine(baseModulePath, "Dto", codeGenDto.ClassName + "SearchDto.cs")
        });

        // IBaseController
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "IBaseController.cs.razor"),
            GenPath = Path.Combine(baseModulePath, "I" + codeGenDto.Module + "BaseController.cs")
        });

        // IController
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "IController.cs.razor"),
            GenPath = Path.Combine(baseModulePath, "IController", "I" + codeGenDto.ClassName + "Controller.cs")
        });

        #endregion

        #region Client module
        // csproj.razor
        if (codeGenDto.GenerateProjectFile)
        {
            itemList.Add(new CodeGenTemplateItem(nameModel)
            {
                TemplatePath = Path.Combine(templatePath, "Client.csproj.razor"),
                GenPath = Path.Combine(clientModulePath,
                    appName + "." + codeGenDto.Module + ".Client.csproj")
            });
        }

        // base client controller
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "BaseClientController.cs.razor"),
            GenPath = Path.Combine(clientModulePath, codeGenDto.Module + "BaseClientController.cs")
        });

        // controller
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "ClientController.cs.razor"),
            GenPath = Path.Combine(clientModulePath, "Controller", codeGenDto.ClassName + "ClientController.cs")
        });

        // module base table
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "BaseTable.cs.razor"),
            GenPath = Path.Combine(clientModulePath, codeGenDto.Module + "BaseTable.cs")
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
            GenPath = Path.Combine(clientModulePath, "Views", codeGenDto.ClassName, codeGenDto.ClassName + "View.razor")
        });
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "ClientView.razor.cs.razor"),
            GenPath = Path.Combine(clientModulePath, "Views", codeGenDto.ClassName, codeGenDto.ClassName + "View.razor.cs")
        });

        // edit view
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "ClientEdit.razor"),
            GenPath = Path.Combine(clientModulePath, "Views", codeGenDto.ClassName, codeGenDto.ClassName + "Edit.razor")
        });
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "ClientEdit.razor.cs.razor"),
            GenPath = Path.Combine(clientModulePath, "Views", codeGenDto.ClassName, codeGenDto.ClassName + "Edit.razor.cs")
        });

        // sql server
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "SqlServer - Insert Menu.sql.razor"),
            GenPath = Path.Combine(modulePath, codeGenDto.ClassName + " - SqlServer Insert Menu.sql")
        });

        // Swagger settings
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "SwaggerSetting.razor"),
            GenPath = Path.Combine(modulePath, codeGenDto.Module + " - SwaggerSetting.Add.json")
        });

        #endregion

        #region Server module
        // csproj.razor
        if (codeGenDto.GenerateProjectFile)
        {
            itemList.Add(new CodeGenTemplateItem(nameModel)
            {
                TemplatePath = Path.Combine(templatePath, "Server.csproj.razor"),
                GenPath = Path.Combine(serverModulePath,
                    appName + "." + codeGenDto.Module + ".Server.csproj")
            });
        }

        // base model
        //itemList.Add(new CodeGenTemplateItem(nameModel)
        //{
        //    TemplatePath = Path.Combine(templatePath, "BaseEntity.cs.razor"),
        //    GenPath = Path.Combine(serverModulePath, genTable.Module + "BaseModel.cs")
        //});

        // Model
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "Entity.cs.razor"),
            GenPath = Path.Combine(serverModulePath, "Model", codeGenDto.ClassName + ".cs")
        });

        // Base Service
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "BaseService.cs.razor"),
            GenPath = Path.Combine(serverModulePath, codeGenDto.Module + "BaseService.cs")
        });

        //Service
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "Service.cs.razor"),
            GenPath = Path.Combine(serverModulePath, "Service", codeGenDto.ClassName + "Service.cs")
        });

        // base controller
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "BaseController.cs.razor"),
            GenPath = Path.Combine(serverModulePath, codeGenDto.Module + "BaseController.cs")
        });

        //Controller
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "Controller.cs.razor"),
            GenPath = Path.Combine(serverModulePath, "Controller", codeGenDto.ClassName + "Controller.cs")
        });
        #endregion

        #region Locale
        // en
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "DBLocale.EN.razor"),
            GenPath = Path.Combine(modulePath, "Locale", "EN",
                codeGenDto.ClassName + " - DBLocale.EN.txt")
        });
        // ch
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "DBLocale.CH.razor"),
            GenPath = Path.Combine(modulePath, "Locale", "CH",
                codeGenDto.ClassName + " - DBLocale.CH.txt")
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
                // TODO: 未显示报错信息： "文件不存在："
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
            // Template error
            var tempPath = genItem.TemplatePath;
            throw Oops.Oh(ExceptionCode.Code_Gen_Template_Compile_Error);
        }
    }

    public Task<bool> OpenCodeGenFolder()
    {
        string baseGenPath = ProjectConstants.CodeGenPath;
        FileHelper.OpenFolder(baseGenPath);

        return Task.FromResult(true);
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
            column.PropertyType.Name 
            : codeGenSugarRep.Context.Ado.DbBind.GetPropertyTypeName(column.DataType);

        // 返回准确的类型比如byte->byte
        if (type == "byte")
        {
            type = "Byte";//"bool";
        }
        else if (type == "short")
        {
            type = "Int16";//"int";
        }
        else if (type == "Int32")
        {
            type = "int";
        }
        else if (type == "String")
        {
            type = NetType._string;
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

    // End
}
