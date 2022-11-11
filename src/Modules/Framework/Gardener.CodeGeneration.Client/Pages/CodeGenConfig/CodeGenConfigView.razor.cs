using Gardener.Base;
using Gardener.Client.Base.Components;
using Gardener.CodeGeneration.Dtos;
using Gardener.CodeGeneration.Services;
using Gardener.Common;
using Gardener.UserCenter.Dtos;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.CodeGeneration.Client.Pages.CodeGenConfig;

public partial class CodeGenConfigView : ListTableBase<CodeGenConfigDto, int>
{
    [Inject]
    private ICodeGenService codeGenService { get; set; }
    [Inject]
    private ICodeGenConfigService codeGenConfigService { get; set; }

    private int _codeGenId { get; set; }

    protected bool _saveAllBtnLoading = false;

    protected override async Task OnInitializedAsync()
    {
        // CodeGenId
        _codeGenId = this.Options;

        await ReLoadTable(true);
        await base.OnInitializedAsync();
    }

    protected override void ConfigurationPageRequest(PageRequest pageRequest)
    {
        pageRequest.PageIndex = 1;
        pageRequest.PageSize = 100;

        // code gen id
        pageRequest.FilterGroups
            .Add(new FilterGroup()
            .AddRule(new FilterRule(nameof(CodeGenConfigDto.CodeGenId),
            _codeGenId, FilterOperate.Equal)));
    }

    protected virtual async Task OnClickSaveAll()
    {
        _saveAllBtnLoading = true;

        // Save all
        

        await ReLoadTable();

        _saveAllBtnLoading = false;
    }
}
