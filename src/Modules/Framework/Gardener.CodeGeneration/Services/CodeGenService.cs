using Furion;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Gardener.Base;
using Gardener.Base.Enums;
using Gardener.CodeGeneration.Domains;
using Gardener.CodeGeneration.Dtos;
using Gardener.Common;
using Gardener.EntityFramwork;
using Gardener.Enums;
using Gardener.Sugar;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Services;
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
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
    private readonly IResourceService resourceService;

    private readonly IWebHostEnvironment env;

    public CodeGenService(IRepository<CodeGen> repository,
        IRepository<CodeGenConfig> configRepository,
        ICodeGenConfigService codeGenConfigService,
        IWebHostEnvironment env,
        SqlSugarRepository<CodeGen> sugarRepository,
        IResourceService resourceService) : base(repository)
    {
        this.codeGenRepository = repository;
        this.codeGenConfigRepository = configRepository;
        this.codeGenConfigService = codeGenConfigService;
        this.env = env;
        this.codeGenSugarRep = sugarRepository;
        this.resourceService = resourceService;
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

        // Client select label
        foreach (var item in tableInfos)
        {
            item.ClientSelectLabelText = item.TableName;
            if (!string.IsNullOrEmpty(item.TableComment))
            {
                item.ClientSelectLabelText += " : " + item.TableComment;
            }
        }

        return tableInfos;

        // EF:
        //var dbContextLocatorName = ""; // todo, 前端数据库选择
        //var dbContext = Db.GetDbContext();//默认数据库
        //if (!string.IsNullOrEmpty(dbContextLocatorName))
        //{
        //    var dbContentLocator = AppDomain.CurrentDomain.GetAssemblies()
        //               .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IDbContextLocator)))).Where(x => x.Name == dbContextLocatorName).FirstOrDefault();

        //    dbContext = Db.GetDbContext(dbContentLocator);
        //}

        // EF: 这里只能取到本项目生成的表
        //var tableInfos = dbContext.GetService<IDesignTimeModel>().Model
        //    .GetEntityTypes().Select(it => new TableOutput
        //    {
        //        DatabaseName = dbContextLocatorName,
        //        TableName = it.GetTableName(),
        //        EntityName = it.GetDefaultTableName(),
        //        TableComment = it.GetComment()
        //    }).ToList();
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

        if (input.EntityFromTable)
        {
            if (string.IsNullOrEmpty(input.Remark))
            {
                input.Remark = "表建表";
            }
        }

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

        foreach (var codeGen in codeGens)
        {
            await GenerateCodeAsync(codeGen);
        }
        FileHelper.OpenFolder(ProjectConstants.CodeGenPath);

        return true;
    }

    [NonAction]
    private async Task GenerateCodeAsync(CodeGenDto genTable)
    {
        var nameModel = await GetNameModelAsync(genTable);
        var templateItems = GetTemplateItems(nameModel);

        GenerateCodeByTemplate(templateItems);
    }


    #region Get template items
    private List<CodeGenTemplateItem> GetTemplateItems(CodeGenNameModel nameModel)
    {
        var templateItems = new List<CodeGenTemplateItem>();

        if (nameModel.CodeGen.EntityFromTable)
        {
            templateItems = GetTemplateItems_NewEntity(nameModel);
        }
        else
        {
            //if (genTable.UseCustomTemplate)
            templateItems = GetTemplateItems_Custom(nameModel);
        }

        return templateItems;
    }
    private List<CodeGenTemplateItem> GetTemplateItems_NewEntity(CodeGenNameModel nameModel)
    {
        var codeGenDto = nameModel.CodeGen;
        string appName = ProjectConstants.AppName;
        string baseGenPath = ProjectConstants.CodeGenPath;
        var templatePath = Path.Combine(env.WebRootPath, "Template");

        //\Modules\Sys
        var modulePath = Path.Combine(baseGenPath, nameModel.Module);
        //\Modules\Sys\Gardener.Sys
        var baseModulePath = Path.Combine(modulePath, appName + "." + nameModel.Module);
        //\Modules\Sys\Gardener.Sys.Client
        var clientModulePath = Path.Combine(modulePath, appName + "." + nameModel.Module + ".Client");
        //\Modules\Sys\Gardener.Sys.Server
        var serverModulePath = Path.Combine(modulePath, appName + "." + nameModel.Module + ".Server");

        var itemList = new List<CodeGenTemplateItem>();

        // Model
        itemList.Add(new CodeGenTemplateItem(nameModel)
        {
            TemplatePath = Path.Combine(templatePath, "Entity.cs.razor"),
            GenPath = Path.Combine(serverModulePath, "Model", codeGenDto.ClassName + ".cs")
        });

        return itemList;
    }

    private List<CodeGenTemplateItem> GetTemplateItems_Custom(CodeGenNameModel nameModel)
    {
        string appName = ProjectConstants.AppName;
        string baseGenPath = ProjectConstants.CodeGenPath;
        var templatePath = Path.Combine(env.WebRootPath, "Template");
        var codeGenDto = nameModel.CodeGen;

        //\Modules\Sys
        var modulePath = Path.Combine(baseGenPath, nameModel.Module);
        //\Modules\Sys\Gardener.Sys
        var baseModulePath = Path.Combine(modulePath, appName + "." + nameModel.Module);
        //\Modules\Sys\Gardener.Sys.Client
        var clientModulePath = Path.Combine(modulePath, appName + "." + nameModel.Module + ".Client");
        //\Modules\Sys\Gardener.Sys.Server
        var serverModulePath = Path.Combine(modulePath, appName + "." + nameModel.Module + ".Server");

        var itemList = new List<CodeGenTemplateItem>();

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
            GenPath = Path.Combine(baseModulePath, "Dto", "SearchDto",
                codeGenDto.ClassName + "SearchDto.cs")
        });

        // IBaseController
        if (codeGenDto.GenerateBaseClass)
        { 
            itemList.Add(new CodeGenTemplateItem(nameModel)
            {
                TemplatePath = Path.Combine(templatePath, "IBaseController.cs.razor"),
                GenPath = Path.Combine(baseModulePath, "I" + codeGenDto.Module + "BaseController.cs")
            });
        }

        // IController
        if(codeGenDto.GenerateService)
        {
            itemList.Add(new CodeGenTemplateItem(nameModel)
            {
                TemplatePath = Path.Combine(templatePath, "IController.cs.razor"),
                GenPath = Path.Combine(baseModulePath, "IController", "I" + codeGenDto.ClassName + "Controller.cs")
            });
        }

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
        if (codeGenDto.GenerateBaseClass)
        { 
            itemList.Add(new CodeGenTemplateItem(nameModel)
            {
                TemplatePath = Path.Combine(templatePath, "BaseClientController.cs.razor"),
                GenPath = Path.Combine(clientModulePath, codeGenDto.Module + "BaseClientController.cs")
            });
        }

        // controller
        if (codeGenDto.GenerateService)
        {
            itemList.Add(new CodeGenTemplateItem(nameModel)
            {
                TemplatePath = Path.Combine(templatePath, "ClientController.cs.razor"),
                GenPath = Path.Combine(clientModulePath, "Controller", codeGenDto.ClassName + "ClientController.cs")
            });
        }

        // module base edit: XxxBaseEdit
        if (codeGenDto.GenerateBaseClass)
        {
            itemList.Add(new CodeGenTemplateItem(nameModel)
            {
                TemplatePath = Path.Combine(templatePath, "BaseEdit.cs.razor"),
                GenPath = Path.Combine(clientModulePath, codeGenDto.Module + "BaseEdit.cs")
            });
        }

        // module base table: XxxBaseTable
        if (codeGenDto.GenerateBaseClass)
        { 
            itemList.Add(new CodeGenTemplateItem(nameModel)
            {
                TemplatePath = Path.Combine(templatePath, "BaseTable.cs.razor"),
                GenPath = Path.Combine(clientModulePath, codeGenDto.Module + "BaseTable.cs")
            });
        }

        // views/imports
        if (codeGenDto.GenerateBaseClass)
        { 
            itemList.Add(new CodeGenTemplateItem(nameModel)
            {
                TemplatePath = Path.Combine(templatePath, "ClientImports.razor"),
                GenPath = Path.Combine(clientModulePath, "Views",
                    "_Imports.razor")
            });
        }

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
        //itemList.Add(new CodeGenTemplateItem(nameModel)
        //{
        //    TemplatePath = Path.Combine(templatePath, "SqlServer - Insert Menu.sql.razor"),
        //    GenPath = Path.Combine(modulePath, codeGenDto.ClassName + " - SqlServer Insert Menu.sql")
        //});

        // Swagger settings
        if (codeGenDto.GenerateBaseClass)
        {
            itemList.Add(new CodeGenTemplateItem(nameModel)
            {
                TemplatePath = Path.Combine(templatePath, "SwaggerSetting.razor"),
                GenPath = Path.Combine(modulePath, codeGenDto.Module + " - SwaggerSetting.Add.json")
            });
        }

        #endregion

        #region Server module
        bool generateIService = false;

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

        // IBase Service
        if (generateIService)
        {
            itemList.Add(new CodeGenTemplateItem(nameModel)
            {
                TemplatePath = Path.Combine(templatePath, "IBaseService.cs.razor"),
                GenPath = Path.Combine(serverModulePath, "I" + codeGenDto.Module + "BaseService.cs")
            });
        }

        // Base Service
        if (codeGenDto.GenerateBaseClass)
        {
            itemList.Add(new CodeGenTemplateItem(nameModel)
            {
                TemplatePath = Path.Combine(templatePath, "BaseService.cs.razor"),
                GenPath = Path.Combine(serverModulePath, codeGenDto.Module + "BaseService.cs")
            });
        }

        //IService
        if (generateIService)
        {
            itemList.Add(new CodeGenTemplateItem(nameModel)
            {
                TemplatePath = Path.Combine(templatePath, "IService.cs.razor"),
                GenPath = Path.Combine(serverModulePath, "IService", "I" + codeGenDto.ClassName + "Service.cs")
            });
        }

        //Service
        if (codeGenDto.GenerateService)
        {
            itemList.Add(new CodeGenTemplateItem(nameModel)
            {
                TemplatePath = Path.Combine(templatePath, "Service.cs.razor"),
                GenPath = Path.Combine(serverModulePath, "Service", codeGenDto.ClassName + "Service.cs")
            });
        }

        // base controller
        if (codeGenDto.GenerateBaseClass)
        {
            itemList.Add(new CodeGenTemplateItem(nameModel)
            {
                TemplatePath = Path.Combine(templatePath, "BaseController.cs.razor"),
                GenPath = Path.Combine(serverModulePath, codeGenDto.Module + "BaseController.cs")
            });
        }

        //Controller
        if (codeGenDto.GenerateService)
        {
            itemList.Add(new CodeGenTemplateItem(nameModel)
            {
                TemplatePath = Path.Combine(templatePath, "Controller.cs.razor"),
                GenPath = Path.Combine(serverModulePath, "Controller", codeGenDto.ClassName + "Controller.cs")
            });
        }
        #endregion

        #region Locale
        if (codeGenDto.GenerateLocaleFile
                && nameModel.LocaleItems.Count() > 1)
        {
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
                GenPath = Path.Combine(modulePath, "Locale", "ZH", // CH -> ZH, App.zh.resx
                    codeGenDto.ClassName + " - DBLocale.CH.txt")
            });
        }
        #endregion

        return itemList;
    }

    private List<CodeGenTemplateItem> GetTemplateItems_Default(CodeGenNameModel nameModel, string baseGenPath)
    {
        //TODO: Gardener 默认模板
        throw new NotImplementedException();
    }
    #endregion
    [NonAction]
    private async Task<CodeGenNameModel> GetNameModelAsync(CodeGenDto codeGenDto)
    {
        string appName = ProjectConstants.AppName;
        string baseGenPath = ProjectConstants.CodeGenPath;

        // 表建表时，搜索母表模块下的多语言文件
        var localeFileModule = codeGenDto.Module;
        if (codeGenDto.EntityFromTable)
        {
            localeFileModule = codeGenDto.OriginModule;
        }
        // Locale file path: Gardener\src\Modules\XXX\Gardener.XXX\DB\DB Locale
        var localeFolder = GetLocaleFileFolder(localeFileModule);
        //var localePath = Path.Combine(env.WebRootPath, "DB Locale");

        //\Modules\XXX
        var modulePath = Path.Combine(baseGenPath, codeGenDto.Module);
        //\Modules\Sys\Gardener.Sys
        var baseModulePath = Path.Combine(modulePath, appName + "." + codeGenDto.Module);

        #region Name model
        // CodeGenConfig
        var codeGenConfigs = await codeGenConfigRepository
            .Where(u => u.CodeGenId == codeGenDto.Id)
            .ToListAsync();
        var codeGenConfigDtos = codeGenConfigs.MapTo<CodeGenConfigDto>();

        #region Entity Attributes
        // EF CodeFirst 官方文档：
        // https://learn.microsoft.com/zh-cn/ef/core/modeling/entity-properties?tabs=data-annotations%2Cwithout-nrt
        foreach (var column in codeGenConfigDtos)
        {
            // EF CodeFirst默认string为Unicode，非Unicode设置[Unicode(false)]

            #region MaxLengthText
            var maxLengthStr = (column.NetType.Contains("string")
                && column.Length != null
                && column.Length > 0) ? // Max length 不用设置[MaxLength]
                "[MaxLength(" + column.Length.ToString() + ")]"
                : "";
            column.MaxLengthText = maxLengthStr;
            #endregion

            #region PrecisionText
            // [Precision(0)]
            if (column.NetType.Contains(NetTypeRaw._DateTime))
            {
                // TODO
            }
            #endregion

            #region DbDataTypeText, 不支持多库
            //[Column("Username", TypeName = "Nvarchar(20)")]
            if (codeGenDto.GenerateDbDataTypeText)
            {
                if (true)// sql server
                {
                    // string
                    // 这里处理string类型和长度，例如SqlServer的varchar，nvarchar,
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
                    if (column.DbDataType.Contains("datetime"))
                    {
                        column.DbDataTypeText = column.DbDataType.FirstToUpper();
                    }
                }
            }

            #endregion
        }

        #endregion

        #region Get Locale from file
        var readLocaleItems = new List<CodeGenLocaleItem>();
        var tableLocale = new CodeGenLocaleItem();
        var columnLocales = new List<CodeGenLocaleItem>();

        var localeFilePath = Path.Combine(localeFolder, //genTable.Module,
            codeGenDto.TableName + ExcelHelper.Extension);
        if (File.Exists(localeFilePath))
        {
            // Read locale items
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

            #region Set column comment
            // 数据库中一般只有英文备注，这里设置中英文备注
            // 如果没有备注默认用的NetColumnName
            // 这里的操作也可以直接在DeleteAndAddList里的Comment部分设置
            foreach (var configDto in codeGenConfigDtos)
            {
                var matchLocale = columnLocales
                    .Where(it => it.Name == configDto.ColumnName)
                    .FirstOrDefault();
                if (matchLocale != null)
                {
                    // 如果未设置描述
                    if (configDto.ColumnDescription == configDto.NetColumnName)
                    {
                        // TODO: 这里直接把数据库的列备注设置成了EN，或许可以响应codeGenDto.UseChineseKey
                        // 设置中文，也可以新增一个选项
                        configDto.ColumnDescription = matchLocale.ValueEN;
                    }
                    
                    // 如果未设置备注
                    if (configDto.ColumnSummary == configDto.NetColumnName
                        || codeGenDto.UseChineseSummary)
                    {
                        if (matchLocale.ValueEN == matchLocale.ValueCH)
                        {
                            configDto.ColumnSummary = matchLocale.ValueEN;
                        }
                        else
                        {
                            configDto.ColumnSummary = matchLocale.ValueEN + " / " + matchLocale.ValueCH;
                        }
                    }
                }
            }
            #endregion
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

        #region Generate a new locale file
        if (codeGenDto.GenerateLocaleFile
            || !File.Exists(localeFilePath)
            || codeGenDto.EntityFromTable)//表建表模式
        {
            var newLocaleItems = new List<CodeGenLocaleItem>();
            newLocaleItems.Add(tableLocale);

            // 表建表处理, 修改表名
            if (codeGenDto.EntityFromTable)
            {
                newLocaleItems.FirstOrDefault().Name = codeGenDto.NewTableName ?? codeGenDto.ClassName;
                newLocaleItems.FirstOrDefault().Key = codeGenDto.ClassName;
            }

            // 多语言使用C#Class名、列名作Key
            if (codeGenDto.UseNetColumnAsKey)
            {
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

                // 这里循环取一下原始多语言文件中的列，如果不存在则新建一条
                var matchLocale = columnLocales
                    .Where(it => it.Name == configDto.ColumnName)
                    .FirstOrDefault();
                if (matchLocale != null)
                {
                    // 多语言使用C#Class名、列名作Key
                    if (codeGenDto.UseNetColumnAsKey)
                    {
                        matchLocale.Key = configDto.NetColumnName;
                    }
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

            // Sys\Gardener.Sys
            //var filePath = Path.Combine(modulePath,
            //    newLocaleItems.FirstOrDefault().Name + ExcelHelper.Extension);

            // Sys\Gardener.Sys\DB\DB Locale
            var filePath = Path.Combine(baseModulePath, "DB", "DB Locale",
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
        // 设置使用的Locale Key
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

        // url path: _sys.tool -> sys/tool 
        codeGenDto.ModuleToUrl = ToUrlPath(codeGenDto.Module);
        codeGenDto.ModuleUpper = codeGenDto.Module.ToUpperCamel();
        codeGenDto.ModuleLower = codeGenDto.Module.ToLowerCamel();
        // Primary key
        //codeGenDto.HasPrimarykey = codeGenConfigs
        //    .Where(it => it.IsPrimaryKey == true)
        //    .ToList().Any();
        codeGenDto.PrimaryKeyType = await GetPrimaryKeyType(codeGenDto);
        if (codeGenDto.PrimaryKeyName != "Id")
        {
            codeGenDto.EditFormInherits = "BaseEdit";
            codeGenDto.MainTableInherits = "BaseMainTable";
        }

        // TableName
        var tableName = codeGenDto.TableName;
        // 表建表处理, 修改表名
        if (codeGenDto.EntityFromTable)
        {
            tableName = codeGenDto.NewTableName ?? codeGenDto.ClassName;
        }

        // TableDesc
        var tableDesc = codeGenDto.ClassName;
        if (!string.IsNullOrEmpty(codeGenDto.TableDescriptionEN))
        {
            tableDesc = codeGenDto.TableDescriptionEN;
        }

        // TableSummary
        var tableSummary = tableDesc;
        if (codeGenDto.TableName != codeGenDto.ClassName)
        {
            tableSummary += "\r\n/// Table: " + tableName;
        }

        CodeGenNameModel nameModel = new CodeGenNameModel()
        {
            TableName = tableName,
            ClassName = codeGenDto.ClassName,
            ClassNameLower = codeGenDto.ClassNameLower,
            Module = codeGenDto.Module,
            ModuleToUrl = codeGenDto.ModuleToUrl,
            ModuleUpper = codeGenDto.ModuleUpper,
            ModuleLower = codeGenDto.ModuleLower,
            TableDesc = tableDesc,
            TableSummary = tableSummary,

            CodeGen = codeGenDto,
            CodeGenConfigs = codeGenConfigDtos
        };
        nameModel.AppName = appName;

        // Locale
        nameModel.TableLocaleKey = tableLocale.Key;
        // Menu name
        // menu name set, add a new locale key
        if (!string.IsNullOrEmpty(codeGenDto.MenuNameEN))
        {
            nameModel.LocaleItems.Add(new CodeGenLocaleItem()
            {
                Name = codeGenDto.MenuNameEN,
                Key = codeGenDto.MenuNameEN,//ToUpperCamel(), 现在Menu直接用的Resource.Name作为Key, 也可以设置MenuLocaleKey字段
                ValueEN = codeGenDto.MenuNameEN,
                ValueCH = codeGenDto.MenuNameCH,
            });
        }
        else // menu name not set, use locale key as menu name
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
            .Where(c => c.EditComponentType == ClientComponentType.RemoteImage)
            .ToList().Any())
        {
            nameModel.HasRemoteImage = true;
        }

        // Menu
        nameModel.Menus = GetMenus(nameModel);
        #endregion

        return nameModel;
    }

    /// <summary>
    /// Gardener\src\Modules\XXX\Gardener.XXX\DB\DB Locale
    /// </summary>
    /// <param name="moduleName">XXX</param>
    /// <returns></returns>
    private string GetLocaleFileFolder(string moduleName)
    {
        return Path.Combine(
            FileHelper.GetParentDirectory(App.HostEnvironment.ContentRootPath),
            "Modules",
            moduleName,
            ProjectConstants.AppName + "." + moduleName,
            "DB",
            "DB Locale");
    }

    /// <summary>
    /// url path: _sys.tool -> sys/tool
    /// </summary>
    /// <param name="module"></param>
    /// <returns></returns>
    private string ToUrlPath(string module)
    {
        return module.Replace(".", "/").Replace("_", "");
    }

    // 这里也可以传入CodeGenDto,
    // GetNameModel里对CodeGenDto.MenuNameEN的操作就要搬到CodeGen的Insert和Update里面,
    // 或者这里手动设置一下MenuNameEN：如果为空 -> _Module._ClassName
    private List<ResourceDto> GetMenus(CodeGenNameModel nameModel)
    {
        CodeGenDto codeGenDto = nameModel.CodeGen;

        #region Menu
        var iconName = "Icon Name";
        if (!string.IsNullOrEmpty(codeGenDto.IconName))
        {
            iconName = codeGenDto.IconName;
        }

        List<ResourceDto> menus = new();
        menus.Add(new ResourceDto()
        {
            Id = Guid.NewGuid(),
            ParentId = codeGenDto.MenuParentId,
            Name = codeGenDto.MenuNameEN,
            Key = codeGenDto.Module + "_" + codeGenDto.ClassName,
            Path = string.Format("/{0}/{1}", ToUrlPath(codeGenDto.Module), codeGenDto.ClassName),
            Icon = iconName,
            Order = 0,
            Type = ResourceType.Menu,
            CreatedTime = DateTimeOffset.Now,
            IsLocked = false,
            IsDeleted = false,
        });
        #endregion

        #region Buttons
        // Menu Buttons / Actions
        menus.Add(NewAction(AuthItems.Search, menus.First(), codeGenDto));
        menus.Add(NewAction(AuthItems.Add, menus.First(), codeGenDto));
        menus.Add(NewAction(AuthItems.Edit, menus.First(), codeGenDto));
        menus.Add(NewAction(AuthItems.Delete, menus.First(), codeGenDto));
        menus.Add(NewAction(AuthItems.Lock, menus.First(), codeGenDto));

        // 这里根据类批量生成Menu button, XxxAuthItems: AuthItems
        var baseAuthItem = App.EffectiveTypes
            .Where(a => !a.IsAbstract
                && a.IsClass
                && a.Name.Equals(codeGenDto.Module + nameof(AuthItems)))
            .FirstOrDefault();
        if (baseAuthItem != null)
        {
            var fieldInfos = baseAuthItem.GetFields
                (BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.IsLiteral && !x.IsInitOnly)
                .ToList();

            foreach (var item in fieldInfos)
            {
                menus.Add(NewAction(item.GetValue(null).ToString(), menus.First(), codeGenDto));
            }
        }
        #endregion

        return menus;
    }

    private async Task<string> GetPrimaryKeyType(CodeGenDto codeGenDto)
    {
        var pkType = "int";

        var codeGenConfigs = await codeGenConfigRepository
            .Where(it => it.CodeGenId == codeGenDto.Id)
            .ToListAsync();

        // primary key
        var codeGenConfig = codeGenConfigs
            .Where(it => it.NetColumnName == codeGenDto.PrimaryKeyName)
            .FirstOrDefault();

        if (codeGenConfig != null)
        {
            pkType = codeGenConfig.NetType.Replace("?", "");
        }
        else
        {
            throw Oops.Oh($"指定的主键{codeGenDto.PrimaryKeyName}在表中不存在");
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
                throw Oops.Oh(ExceptionCode.NO_INCLUD_FILE, "文件不存在：" + genItem.TemplatePath);
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
                IdUtil.GetGuid32() + model.ClassName,
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

        // 返回准确的类型
        if (type == "byte")
        {
            type = NetTypeRaw._byte;// 确保数据库只有0和1的话也可以用 "bool"
        }
        else if (type == "short")
        {
            type = NetTypeRaw._short; // "Int16";
        }
        else if (type == "Int32")
        {
            type = NetTypeRaw._int;
        }
        else if (type == "String")
        {
            type = NetTypeRaw._string;
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

    public async Task<bool> GenerateMenu(int codeGenId)
    {
        var codeGenDto = await Get(codeGenId);
        if (!codeGenDto.MenuParentId.HasValue)
        {
            throw Oops.Oh("未设置父级菜单");
        }

        var nameModel = await GetNameModelAsync(codeGenDto);
        var menus = GetMenus(nameModel);

        // delete old
        var newMenusKeys = menus.Select(it => it.Key).ToList();
        var allMenus = await resourceService.GetAllUsable();
        var oldMenus = allMenus.Where(it => newMenusKeys.Contains(it.Key)).ToList();
        await resourceService.Deletes(oldMenus.Select(it => it.Id).ToArray());

        // insert new
        foreach (var item in menus)
        {
            await resourceService.Insert(item);
        }

        return true;
    }

    /// <summary>
    /// 导入多语言
    /// </summary>
    /// <param name="codeGenId"></param>
    /// <returns></returns>
    public async Task<bool> GenerateLocale(int codeGenId)
    {
        var codeGenDto = await Get(codeGenId);

        var nameModel = await GetNameModelAsync(codeGenDto);
        if (nameModel.LocaleItems.Count() <= 1)
        {
            throw Oops.Oh("不存在多语言Excel文件");
        }

        var localeItems = nameModel.LocaleItems;
        var localeXmlPath = Path.Combine
            (FileHelper.GetParentDirectory(App.HostEnvironment.ContentRootPath),
            "Client",
            "Gardener.Client.Entry", // WPF...
            "Resources");
        var localeXmlEN = Path.Combine(localeXmlPath, "App.en.resx");
        var localeXmlCH = Path.Combine(localeXmlPath, "App.zh.resx");

        // TODO
        // xml to data table?
        //https://blog.csdn.net/qq_32915337/article/details/83896547

        return true;
    }


    // End
}
