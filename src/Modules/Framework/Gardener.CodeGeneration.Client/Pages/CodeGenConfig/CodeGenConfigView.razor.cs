using Gardener.Base;
using Gardener.Client.Base;
using Gardener.Client.Base.Components;
using Gardener.CodeGeneration.Client.Services;
using Gardener.CodeGeneration.Dtos;
using Gardener.CodeGeneration.Services;
using Gardener.Common;
using Gardener.UserCenter.Dtos;
using Microsoft.AspNetCore.Components;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.CodeGeneration.Client.Pages.CodeGenConfig;

public partial class CodeGenConfigView : ListTableBase<CodeGenConfigDto, int>
{
    [Inject]
    private ICodeGenService codeGenService { get; set; }
    [Inject]
    private ICodeGenConfigService codeGenConfigClientService { get; set; }

    private int _codeGenId { get; set; }

    protected bool _saveAllBtnLoading = false;

    protected override async Task OnInitializedAsync()
    {
        // CodeGenId 传入 ConfigurationPageRequest
        _codeGenId = this.Options;

        await ReLoadTable(true);
        await base.OnInitializedAsync();
    }

    protected override void ConfigurationPageRequest(PageRequest pageRequest)
    {
        // code gen id
        pageRequest.FilterGroups
            .Add(new FilterGroup()
            .AddRule(new FilterRule(nameof(CodeGenConfigDto.CodeGenId),
            _codeGenId, FilterOperate.Equal)));
    }

    protected virtual async Task OnClickSaveAll()
    {
        _saveAllBtnLoading = true;

        // TODO: 这里_datas是单页的数据，有没有选项设成所有数据
        var success = await codeGenConfigClientService.SaveAll(_datas.ToList());
        if (success)
        {
            await messageService.Success("保存成功");
        }
        else
        { 
            await messageService.Error("保存失败");
        }

        _saveAllBtnLoading = false;
    }

    protected virtual async Task OnClickSaveAllAndClose()
    {
        _saveAllBtnLoading = true;

        var success = await codeGenConfigClientService.SaveAll(_datas.ToList());
        if (success)
        {
            await messageService.Success("保存成功");
            await base.FeedbackRef.CloseAsync(true);
        }
        else
        { 
            await messageService.Error("保存失败");
        }


        _saveAllBtnLoading = false;
    }
}
