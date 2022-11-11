using Gardener.Client.Base;
using Gardener.Client.Base.Components;
using Gardener.Client.Base.Constants;
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
    private List<SelectItem> _allTables = new();
    [Inject]
    private ICodeGenService codeGenClientService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        // table select
        var tableInfos = await codeGenClientService.GetTableListAsync();
        _allTables = tableInfos.ToSelectItems(it => it.TableName, it => it.TableName);

        await base.OnInitializedAsync();
    }

    private async Task OnClickConfigure(int id)
    {
        await OpenOperationDialogAsync
            <CodeGenConfigure, int, bool>
            (localizer["Configure"], id, async result =>
        {
            await ReLoadTable();
        }, new OperationDialogSettings() { 
            DialogType = OperationDialogType.Drawer,
            Width = ClientConstant.PageOperationDialogWidth
        });


        // TODO: 原来的 CodeGenConfigView 布局有问题
        //await OpenOperationDialogAsync
        //    <CodeGenConfigView, int, bool>
        //    (localizer["Configure"], id, async result =>
        //{
        //    await ReLoadTable();
        //}, width: ClientConstant.PageOperationDialogWidth);

    }

    private async Task OnClickGenerate(int codeGenId)
    {
        List<int> codeGenIds = new List<int>();
        codeGenIds.Add(codeGenId);

        await codeGenClientService.GenerateCode(codeGenIds.ToArray());
        await messageService.Success("Generate Success");
    }

    private async Task DoSearch()
    {
        _customSearchFilterGroups = GetCustomSearchFilterGroups(_searchDto);

        await ReLoadTable(true);
    }

    private async Task DoClearSearch()
    {
        //todo 
        //_searchDto = new();

        await DoSearch();
    }
}
