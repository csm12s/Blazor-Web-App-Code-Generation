using AntDesign;
using Gardener.Base;
using Gardener.Client.Base;
using Gardener.Client.Base.Components;
using Gardener.Client.Base.Constants;
using Gardener.CodeGeneration.Client.Pages.CodeGenConfig;
using Gardener.CodeGeneration.Dtos;
using Gardener.CodeGeneration.Services;
using Gardener.Common;
using Gardener.Enums;
using Gardener.SystemManager.Dtos;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.CodeGeneration.Client.Pages.CodeGen;

public partial class CodeGenView : ListTableBase<CodeGenDto, int, CodeGenEdit>
{
    protected bool _generatesBtnLoading = false;

    // Custom search
    protected CodeGenSearchDto _searchDto = new();
    private List<SelectItem> _select_TableName = new();
    [Inject]
    private ICodeGenService codeGenClientService { get; set; }
    [Inject]
    private ICodeGenConfigService codeGenConfigService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        // table select
        var tableInfos = await codeGenClientService.GetTableListAsync();
        _select_TableName = tableInfos.ToSelectItems(it => it.TableName, it => it.TableName);

        await base.OnInitializedAsync();
    }

    private async Task OnClickConfigure(int id)
    {
        // TODO: 在View页面（User、Role等）设置列宽，列固定到左右端，Table水平、垂直滚动条之后布局出错
        // ，有可能是我不熟悉设置有误，建议丰富一下前端组件

        await OpenOperationDialogAsync
            <CodeGenConfigView, int, bool>
            (localizer["Configure"], id, async result =>
        {
            //await ReLoadTable();
        }, new OperationDialogSettings()
        {
            DialogType = OperationDialogType.Drawer,
            Width = ClientConstant.PageOperationDialogWidth
        });

    }

    private async Task OnClickGenerate(int codeGenId)
    {
        List<int> codeGenIds = new List<int>();
        codeGenIds.Add(codeGenId);

        var success = await codeGenClientService.GenerateCode(codeGenIds.ToArray());
        if (success)
        { 
            await messageService.Success("Generate Success");
        }
        else
        {
            await messageService.Error("Generate Error");
        }
    }

    private async Task DoSearch()
    {
        _customSearchFilterGroups = GetCustomSearchFilterGroups(_searchDto);

        await ReLoadTable(true);
    }

    protected virtual async Task OnClickGenerates()
    {
        if (_selectedRows == null || _selectedRows.Count() == 0)
        {
            messageService.Warn(localizer["未选中任何行"]);
        }
        else
        {
            _generatesBtnLoading = true;
            if (await confirmService.YesNo("批量生成", "是否继续") == ConfirmResult.Yes)
            {
                var success = await codeGenClientService.GenerateCode(_selectedRows.Select(x => x.Id).ToArray());
                if (success)
                {
                    await messageService.Success("Generate Success");
                }
                else
                {
                    await messageService.Error("Generate Error");
                }
            }
            _generatesBtnLoading = false;
        }
    }

    private async Task OnDownloadConfigSeedDataClick()
    {
        var codeGens = await _service.GetAll();
        var codeGenIds = codeGens.Select(it=>it.Id).ToList();

        string data = await codeGenConfigService.GenerateSeedData(new PageRequest()
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

        await OpenOperationDialogAsync<ShowSeedDataCode, string, bool>(
                    localizer["种子数据"],
                    data,
                    width: 1300);
    }

    private async Task OpenCodeGenFolder()
    {
        await codeGenClientService.OpenCodeGenFolder();
    }
}
