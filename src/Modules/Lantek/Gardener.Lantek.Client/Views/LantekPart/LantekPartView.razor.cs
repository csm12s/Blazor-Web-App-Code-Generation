
using Microsoft.AspNetCore.Components;
using Gardener.Common;
using Gardener.Lantek.Dto;
using Gardener.Lantek.IController;

namespace Gardener.Lantek.Client.Views.LantekPart;

public partial class LantekPartView : LantekBaseTable<LantekPartDto, LantekPartEdit>
{
    #region Init
    protected LantekPartSearchDto _searchDto = new();
         private List<SelectItem> _select_DisMatRef = new();
    [Inject]
    private ILantekPartController lantekPartClientController { get; set; }
    
    public LantekPartView() : base("LantekPart")
    {
    }

    protected override async Task OnInitializedAsync()
    {
        // Init

        await base.OnInitializedAsync();
    }
    #endregion

    #region Custom search
    private async Task DoSearch()
    {
        _customSearchFilterGroups = GetCustomSearchFilterGroups(_searchDto);

        await ReLoadTable(true);
    }
    #endregion

}


