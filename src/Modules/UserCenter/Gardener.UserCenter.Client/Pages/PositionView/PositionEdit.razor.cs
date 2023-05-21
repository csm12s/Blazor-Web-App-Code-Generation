// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Utils;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.PositionView
{
    public partial class PositionEdit : EditOperationDialogBase<PositionDto,int, UserCenterResource>
    {
        private IEnumerable<CodeDto>? grades;
        protected override async Task OnInitializedAsync()
        {
            await StartLoading();
            await base.OnInitializedAsync();
            grades= CodeUtil.GetCodesFromCache<PositionDto>(() => _editModel.Grade);
            await StopLoading();
        }

    }
}
