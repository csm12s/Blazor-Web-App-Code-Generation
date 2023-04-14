using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.WoChat.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.WoChat.Client.Components
{
    /// <summary>
    /// wo chat 聊天
    /// </summary>
    public partial class WoChat : OperationDialogBase<Guid?,bool, WoChatResource>
    {
        /// <summary>
        /// 加载
        /// </summary>
        /// <returns></returns>
        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }
    }
}
