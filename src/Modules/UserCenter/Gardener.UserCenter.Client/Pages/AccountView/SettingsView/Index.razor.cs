// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.UserCenter.Resources;
using System.Collections.Generic;

namespace Gardener.UserCenter.Client.Pages.AccountView.SettingsView
{
    public partial class Index : OperationDialogBase<int, bool, UserCenterResource>
    {
        private readonly Dictionary<string, string> _menuMap = new Dictionary<string, string>
        {
            {"base", "BasicSettings"},
            {"security", "SecuritySettings"},
            {"binding", "AccountBinding"},
        };

        private string _selectKey = "base";

        private void SelectKey(MenuItem item)
        {
            _selectKey = item.Key;
        }
    }
}
