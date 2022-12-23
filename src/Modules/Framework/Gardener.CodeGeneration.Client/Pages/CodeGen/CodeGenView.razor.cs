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
using Gardener.SystemManager.Services;
using Microsoft.AspNetCore.Components;
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

    [Inject]
    private IResourceService resourceService { get; set; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dialogSettings"></param>
    protected override void SetOperationDialogSettings(OperationDialogSettings dialogSettings)
    {
        dialogSettings.Width = 1000;
    }
    protected override async Task OnInitializedAsync()
    {
        // table select
        var tableInfos = await codeGenClientService.GetTableListAsync();
        _select_TableName = tableInfos.ToSelectItems(it => it.TableName, it => it.TableName);

        await base.OnInitializedAsync();
    }

    private async Task OnClickConfigure(int id)
    {
        await OpenOperationDialogAsync
            <CodeGenConfigView, int, bool>
            (localizer["设置"], id, async result =>
        {
            //await ReLoadTable();
        }, width: 1200);

    }

    private async Task OnClickGenerate(int codeGenId)
    {
        List<int> codeGenIds = new List<int>();
        codeGenIds.Add(codeGenId);

        var success = await codeGenClientService.GenerateCode(codeGenIds.ToArray());
        if (success)
        { 
            await messageService.Success(localizer.Combination("生成","成功"));
        }
        else
        {
            await messageService.Error(localizer.Combination("生成", "失败"));
        }
    }

    private async Task OnClickGenerateMenu(int codeGenId)
    {
        var codeGenDto = await _service.Get(codeGenId);
        var menuKey = codeGenDto.Module + "_" + codeGenDto.ClassName;

        var allMenus = await resourceService.GetAll();
        
        // 这里也可以从后端获取
        var oldMenus = allMenus.Where(it => it.Key == menuKey).ToList();
        var oldMenuButtons = allMenus
            .Where(it => it.Key.StartsWith(menuKey + "_")).ToList();
        oldMenus.AddRange(oldMenuButtons);

        var title = "确认继续";
        var menuInfoStr = "将添加以下菜单：" + menuKey + "\r\n";
        if (oldMenus.Any())
        {
            menuInfoStr += "将删除以下菜单: \r\n";
            foreach (var item in oldMenus)
            {
                menuInfoStr += string.Format("名称：{0}，Key：{1}。\r\n", localizer[item.Name], item.Key);
            }
        }

        // TODO: 文本不能换行
        if (await confirmService.YesNo(title, menuInfoStr) == ConfirmResult.Yes)
        {
            var success = await codeGenClientService.GenerateMenu(codeGenId);
            if (success)
            {
                await messageService.Success(localizer.Combination("生成", "成功"));
            }
            else
            {
                await messageService.Error(localizer.Combination("生成", "失败"));
            }
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
                    await messageService.Success(localizer.Combination("生成", "成功"));
                }
                else
                {
                    await messageService.Error(localizer.Combination("生成", "失败"));
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
