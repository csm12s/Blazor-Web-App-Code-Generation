// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.NotificationSystem.Dtos.Notification;
using Gardener.NotificationSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.NotificationSystem.Client.Core.Services
{
    /// <summary>
    /// 聊天示例服务
    /// </summary>
    [ScopedService]
    public class ChatDemoService : IChatDemoService
    {
        private readonly string controller = "chat-demo";
        private readonly IApiCaller apiCaller;
        public ChatDemoService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }
        public async Task<IEnumerable<ChatDemoNotificationData >> GetHistory()
        {
            return await apiCaller.GetAsync<IEnumerable<ChatDemoNotificationData >>($"{controller}/history");
        }
    }
}
