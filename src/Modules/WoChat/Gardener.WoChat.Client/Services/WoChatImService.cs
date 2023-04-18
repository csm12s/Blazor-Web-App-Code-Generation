// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.Base;
using Gardener.UserCenter.Dtos;
using Gardener.WoChat.Dtos;
using Gardener.WoChat.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.WoChat.Client.Services
{
    /// <summary>
    /// im 服务
    /// </summary>
    [ScopedService]
    public class WoChatImService : IWoChatImService
    {
        private readonly string controller = "wo-chat-im";

        private readonly IApiCaller apiCaller;

        public WoChatImService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public Task<Guid?> AddMyImSession(ImSessionAddInput input)
        {
            return apiCaller.PostAsync<ImSessionAddInput, Guid?>($"{controller}/my-im-session", input);
        }

        public Task<IEnumerable<ImSessionDto>> GetImGroupSessions()
        {
            return apiCaller.GetAsync<IEnumerable<ImSessionDto>>($"{controller}/im-group-sessions");
        }

        public Task<IEnumerable<ImSessionDto>> GetMyImSessions()
        {
            return apiCaller.GetAsync<IEnumerable<ImSessionDto>>($"{controller}/my-im-sessions");
        }

        public Task<IEnumerable<ImSessionMessageDto>> GetMySessionMessages(Guid imSessionId, DateTimeOffset? maxDateTime = null, int pageSize = 100)
        {
            IDictionary<string, object?> queryString = new Dictionary<string, object?>();
            queryString.Add("imSessionId", imSessionId);
            queryString.Add("maxDateTime", maxDateTime);
            queryString.Add("pageSize", pageSize);

            return apiCaller.GetAsync<IEnumerable<ImSessionMessageDto>>($"{controller}/my-session-messages", queryString);
        }

        public Task<bool> QuitMyImSession(Guid imSessionId)
        {
            return apiCaller.PostWithoutBodyAsync<bool>($"{controller}/quit-im-session/{imSessionId}");
        }

        public Task<bool> RemoveMyImSession(Guid imSessionId)
        {
            return apiCaller.PostWithoutBodyAsync<bool>($"{controller}/my-im-session/{imSessionId}");
        }

        public Task<bool> SendMessage(ImSessionMessageDto message)
        {
            return apiCaller.PostAsync<ImSessionMessageDto, bool>($"{controller}/send-message", message);
        }
    }
}
