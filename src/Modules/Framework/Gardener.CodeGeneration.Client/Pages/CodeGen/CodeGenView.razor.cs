using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.CodeGeneration.Client.Pages.CodeGenConfig;
using Gardener.CodeGeneration.Resources;
using Gardener.CodeGeneration.Dtos;
using Gardener.CodeGeneration.Services;
using Gardener.Common;
using Gardener.Enums;
using Gardener.SystemManager.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Gardener.Client.AntDesignUi.Base.Components;
using AntDesign;
using Gardener.Client.AntDesignUi.Base;

namespace Gardener.CodeGeneration.Client.Pages.CodeGen;

public partial class CodeGenView : ListOperateTableBase<CodeGenDto, Guid, CodeGenEdit, CodeGenLocalResource>
{
    protected bool _generatesBtnLoading = false;

    // Custom search
    protected CodeGenSearchDto _searchDto = new();
    private List<SelectItem> _select_TableName = new();
    [Inject]
    private ICodeGenService codeGenClientService { get; set; } = null!;
    [Inject]
    private ICodeGenConfigService codeGenConfigService { get; set; } = null!;

    [Inject]
    private IResourceService resourceService { get; set; } = null!;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dialogSettings"></param>
    protected override void SetOperationDialogSettings(OperationDialogSettings dialogSettings)
    {
        dialogSettings.Width = 1200;
    }
    protected override async Task OnInitializedAsync()
    {
        // table select
        List<TableOutput> tableInfos = await codeGenClientService.GetTableListAsync();
        var items = tableInfos.ToSelectItems
            (it => it.TableName, it => it.ClientSelectLabelText ?? string.Empty);
        if (items != null)
        { 
            _select_TableName = items;
        }

        await base.OnInitializedAsync();
    }
    /// <summary>
    /// 设置TableSearch特定参数
    /// </summary>
    /// <param name="tableSearchSettings"></param>
    protected override void SetTableSearchParameters(TableSearchSettings tableSearchSettings, List<Func<List<FilterGroup>?>> tableSearchFilterGroupProviders)
    {
        //设置参数
        tableSearchFilterGroupProviders.Add(() => { return GetCustomSearchFilterGroups(_searchDto); });
    }
    private async Task OnClickConfigure(Guid id)
    {
        await OpenOperationDialogAsync
            <CodeGenConfigView, Guid, bool>
            (Localizer[SharedLocalResource.Setting], id,  result =>
        {
            //await ReLoadTable();
            return Task.CompletedTask;
        }, width: 1800);

    }

    private async Task OnClickGenerate(Guid codeGenId)
    {
        StartTableLoading();
        List<Guid> codeGenIds = new List<Guid>();
        codeGenIds.Add(codeGenId);

        var success = await codeGenClientService.GenerateCode(codeGenIds.ToArray());
        StopTableLoading();

        if (success)
        { 
            await MessageService.SuccessAsync(Localizer.Combination(SharedLocalResource.Generate, SharedLocalResource.Success));
        }
        else
        {
            await MessageService.ErrorAsync(Localizer.Combination(SharedLocalResource.Generate, SharedLocalResource.Fail));
        }
        
    }

    private async Task OnClickGenerateMenu(Guid codeGenId)
    {
        StartTableLoading();

        var codeGenDto = await BaseService.Get(codeGenId);
        var menuKey = codeGenDto.Module + "_" + codeGenDto.ClassName;

        var allMenus = await resourceService.GetAll();
        
        // 这里也可以从后端获取
        var oldMenus = allMenus.Where(it => it.Key == menuKey).ToList();
        var oldMenuButtons = allMenus
            .Where(it => it.Key.StartsWith(menuKey + "_")).ToList();
        oldMenus.AddRange(oldMenuButtons);

        var title = Localizer[CodeGenLocalResource.IsContinue];
        var menuInfoStr = "将添加以下菜单：" + menuKey + "\r\n";
        if (oldMenus.Any())
        {
            menuInfoStr += "将删除以下菜单: \r\n";
            foreach (var item in oldMenus)
            {
                menuInfoStr += string.Format("名称：{0}，Key：{1}。\r\n", Localizer[item.Name], item.Key);
            }
        }

        // TODO: 文本不能换行
        if (await ConfirmService.YesNo(title, menuInfoStr) == ConfirmResult.Yes)
        {
            var success = await codeGenClientService.GenerateMenu(codeGenId);
            
            if (success)
            {
                await MessageService.SuccessAsync(Localizer.Combination(SharedLocalResource.Generate, SharedLocalResource.Success));
            }
            else
            {
                await MessageService.ErrorAsync(Localizer.Combination(SharedLocalResource.Generate, SharedLocalResource.Fail));
            }
           
        }

        StopTableLoading();
    }

    private async Task OnClickGenerateLocale(Guid codeGenId)
    {
        StartTableLoading();

        var codeGenDto = await BaseService.Get(codeGenId);

        var title = Localizer[CodeGenLocalResource.IsContinue];
        var message = Localizer["MlLgWBeImp"];
        if (await ConfirmService.YesNo(title, message) == ConfirmResult.Yes)
        {
            var success = await codeGenClientService.GenerateLocale(codeGenId);
            if (success)
            {
                await MessageService.SuccessAsync(Localizer.Combination(SharedLocalResource.Generate, SharedLocalResource.Success));
            }
            else
            {
                await MessageService.ErrorAsync(Localizer.Combination(SharedLocalResource.Generate, SharedLocalResource.Fail));
            }
        }

        StopTableLoading();
    }
    private async Task DoSearch()
    {
        await ReLoadTable(true);
    }

    protected virtual async Task OnClickGenerates()
    {
        if (_selectedRows == null || _selectedRows.Count() == 0)
        {
            MessageService.Warn(Localizer[SharedLocalResource.NoRowsAreSelected]);
        }
        else
        {
            _generatesBtnLoading = true;
            if (await ConfirmService.YesNo(Localizer[CodeGenLocalResource.BatchGenerate], CodeGenLocalResource.IsContinue) == ConfirmResult.Yes)
            {
                var success = await codeGenClientService.GenerateCode(_selectedRows.Select(x => x.Id).ToArray());
                if (success)
                {
                    await MessageService.SuccessAsync(Localizer.Combination(SharedLocalResource.Generate, SharedLocalResource.Success));
                }
                else
                {
                    await MessageService.ErrorAsync(Localizer.Combination(SharedLocalResource.Generate, SharedLocalResource.Fail));
                }
               
            }
            _generatesBtnLoading = false;
        }
    }

    private async Task OnDownloadConfigSeedDataClick()
    {
        var codeGens = await BaseService.GetAll();
        var codeGenIds = codeGens.Select(it=>it.Id).ToList();

        Task<string> data = codeGenConfigService.GenerateSeedData(new PageRequest()
        {
            PageIndex = 1,
            PageSize = int.MaxValue,
            FilterGroups = new List<FilterGroup>()
                {
                   new FilterGroup().AddRule(new FilterRule()
                   {
                        Field=nameof(CodeGenConfigDto.CodeGenId),
                        Operate=FilterOperate.In,
                        Value=codeGenIds
                   })
                },
            OrderConditions = new List<ListSortDirection>()
                {
                    new ListSortDirection()
                    {
                        FieldName=nameof(CodeGenConfigDto.Id),
                        SortType=ListSortType.Asc
                    }
                }
        });

        await OpenOperationDialogAsync<ShowSeedDataCode, Task<string>, bool>(
                    Localizer[SharedLocalResource.SeedData],
                    data,
                    width: 1500);
    }

    private async Task ShowHelp()
    {
        await ConfirmService.YesNo("使用说明",
            """
            模板位置：
                Gardener.Entry\wwwroot\Template

            多语言文件存放位置：
                放在Gardener.XXX项目下
                参考Gardener.UserCenter

            生成后操作：
            1, 在Gardener.Api.Core项目中添加Gardener.Xxx.Server项目的引用
               在/Setting/ SwaggerSettings.json 添加setting
            2, 在Gardener.Client.Core项目中添加Gardener.Xxx.Client项目的引用
            3, 在Gardener.Client.Entry/wwwroot/appsettings.json中添加配置 ModuleSettings.Dlls
            （前端View代码中报错部分将@@替换为@）

            其他说明：
            XxxBaseModel, XxxBaseDto 请自行修改
            模块命名暂不支持带点：例如Custom1，不支持这种Custom._1
            生成的工程文件可能存在包降级，手动更新一下
            """
            );
    }

    private async Task OpenCodeGenFolder()
    {
        await codeGenClientService.OpenCodeGenFolder();
    }
}
