﻿using Gardener.Client.Base;
using Gardener.Client.Base.Components;
using Gardener.Client.Base.Constants;
using Gardener.CodeGeneration.Client.Pages.CodeGenConfig;
using Gardener.CodeGeneration.Dtos;
using Gardener.CodeGeneration.Services;
using Gardener.Common;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.CodeGeneration.Client.Pages.CodeGen;

public partial class CodeGenView : ListTableBase<CodeGenDto, int, CodeGenEdit>
{
    // Custom search
    protected CodeGenSearchDto _searchDto = new();
    private List<SelectItem> _select_TableName = new();
    [Inject]
    private ICodeGenService codeGenClientService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        // table select
        var tableInfos = await codeGenClientService.GetTableListAsync();
        _select_TableName = tableInfos.ToSelectItems(it => it.TableName, it => it.TableName);

        await base.OnInitializedAsync();
    }

    private async Task OnClickConfigure(int id)
    {
        //await OpenOperationDialogAsync
        //    <CodeGenConfigure, int, bool>
        //    (localizer["Configure"], id, async result =>
        //{
        //    await ReLoadTable();
        //}, new OperationDialogSettings()
        //{
        //    DialogType = OperationDialogType.Drawer,
        //    Width = ClientConstant.PageOperationDialogWidth
        //});


        // TODO1: 原来的 CodeGenConfigView 加了很多列之后布局有问题
        // TODO2: 在View页面（User、Role等）设置列宽，列固定到左右端，Table水平、垂直滚动条之后布局出错，
        // 真正做项目的时候这几项还是要用的

        await OpenOperationDialogAsync
            <CodeGenConfigView, int, bool>
            (localizer["Configure"], id, async result =>
        {
            await ReLoadTable();
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

}
