// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Client.Core.EventBus.Handlers
{
    /// <summary>
    /// 刷新token失败事件处理器
    /// </summary>
    [TransientService]
    public class RefreshTokenErrorEventHandler : IEventHandler<RefreshTokenErrorEvent>
    {
        private readonly IAuthenticationStateManager authenticationStateManager;

        public RefreshTokenErrorEventHandler(IAuthenticationStateManager authenticationStateManager)
        {
            this.authenticationStateManager = authenticationStateManager;
        }

        public async Task Handler(RefreshTokenErrorEvent e)
        {
            await authenticationStateManager.CleanUserInfo();
        }
    }
}
