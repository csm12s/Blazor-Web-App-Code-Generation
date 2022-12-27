using Gardener.Base;
using Gardener.Client.Base;
using Gardener.Client.Base.Components;
using Gardener.CodeGeneration.Resources;
using Gardener.CodeGeneration.Dtos;
using Gardener.CodeGeneration.Services;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.CodeGeneration.Client.Pages.CodeGenConfig;

public partial class CodeGenConfigView : ListTableBase<CodeGenConfigDto, int, CodeGenLocalResource>
{
    [Inject]
    private ICodeGenService codeGenService { get; set; }
    [Inject]
    private ICodeGenConfigService codeGenConfigClientService { get; set; }

    private int _codeGenId { get; set; }

    private CodeGenDto _codeGenDto { get; set; } = new();
    private bool _hideEntityFromTableFields = true;

    protected bool _saveAllBtnLoading = false;

    protected override async Task OnInitializedAsync()
    {
        // CodeGenId 传入 ConfigurationPageRequest
        _codeGenId = this.Options;

        // 试图隐藏某些列，前端会一直刷新
        //_codeGenDto = await codeGenService.Get(_codeGenId);
        //if (_codeGenDto.EntityFromTable)
        //{
        //    _hideEntityFromTableFields = false;
        //}

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
        var listDto = _datas.ToList();

        #region Handle data
        // TODO：后端SaveAll报错, 这里应该由后端处理，减轻前端的负担
        foreach (var item in listDto)
        {
            var oldConfig = await codeGenConfigClientService.Get(item.Id);

            // IsRequired is changed
            if (item.IsRequired != oldConfig.IsRequired)
            {
                //必填项不可为Null
                if (item.IsRequired) // 改为必填
                {
                    item.IsNullable = false;
                    item.NetType = item.NetType.Replace("?", "");
                }
                else // 改为非必填
                {
                    if (!item.IsPrimaryKey)
                    {
                        item.IsNullable = true;

                        if (!item.NetType.Contains("?"))
                        {
                            item.NetType += "?";
                        }
                    }
                }
            }
        }
        #endregion

        var success = await codeGenConfigClientService.SaveAll(listDto);
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
