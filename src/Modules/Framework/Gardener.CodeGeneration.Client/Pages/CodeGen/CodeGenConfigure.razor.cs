using AntDesign;
using Gardener.Client.Base;
using Gardener.CodeGeneration.Dtos;
using Gardener.CodeGeneration.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.CodeGeneration.Client.Pages.CodeGen;

public partial class CodeGenConfigure: FeedbackComponent<int, bool>
{
    string _editId;
    int _codeGenId;
    [Inject]
    ICodeGenConfigService codeGenConfigClientService { get; set; }

    protected bool _tableIsLoading = false;
    List<CodeGenConfigDto> _list = new();
    protected int _total = 0;
    protected int _pageIndex = 1;
    protected int _pageSize = 100;//ClientConstant.pageSize;
    protected IEnumerable<CodeGenConfigDto> _selectedRows;

    protected override async Task OnInitializedAsync()
    {
        _tableIsLoading = true;
        // Init
        _codeGenId = this.Options;
        _list = await codeGenConfigClientService.GetCodeGenConfigsByCodeGenId(_codeGenId);

        await base.OnInitializedAsync();
        _tableIsLoading = false;
    }

    async void SaveAll()
    {
        await codeGenConfigClientService.SaveAll(_list);
        await base.FeedbackRef.CloseAsync(true);
    }

    void deleteRow(string id)
    {
        //_list = _list.Where(d => d.Id != id).ToArray();
    }

    void startEdit(string id)
    {
        _editId = id;
    }

    void stopEdit()
    {
        //var editedData = _list.FirstOrDefault(x => x.Id == _editId);
        _editId = null;
    }


}
