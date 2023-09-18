// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.Base;
using Gardener.UserCenter.Resources;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Gardener.UserCenter.Client.Pages.AccountView.SettingsView
{
    public partial class Index : OperationDialogBase<int, bool, UserCenterResource>
    {
        private readonly Dictionary<string, string> _menuMap = new Dictionary<string, string>
        {
            {"account_center_settings_base", "BasicSettings"},
            {"account_center_settings_security", "SecuritySettings"},
            {"account_center_settings_binding", "AccountBinding"},
        };
        [Inject]
        private IAuthenticationStateManager authenticationStateManager { get; set; } = null!;
        private string _selectKey = "account_center_settings_base";
        private void SelectKey(MenuItem item)
        {
            _selectKey = item.Key;
        }
    }
}
