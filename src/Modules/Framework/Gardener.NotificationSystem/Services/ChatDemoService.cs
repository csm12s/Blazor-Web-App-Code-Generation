// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DynamicApiController;
using Gardener.NotificationSystem.Dtos.Notification;
using System.Collections.Concurrent;

namespace Gardener.NotificationSystem.Services
{
    /// <summary>
    /// 聊天示例服务
    /// </summary>
    public class ChatDemoService : IChatDemoService, IDynamicApiController
    {
        private static ConcurrentQueue<ChatNotificationData> concurrentQueue=new ConcurrentQueue<ChatNotificationData>();
        private static object lockObj=new object();
        /// <summary>
        /// 获取聊天历史记录
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IEnumerable<ChatNotificationData>> GetHistory()
        {
            IEnumerable<ChatNotificationData> datas= concurrentQueue.ToList();
            return Task.FromResult(datas);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public static void AddChatMessage(ChatNotificationData data)
        {
            lock (lockObj)
            {
                if (concurrentQueue.Count >= 200)
                {
                    //队列里最多200条
                    concurrentQueue.TryDequeue(out _);
                }
                //入队
                concurrentQueue.Enqueue(data);
            }
            
        }
    }
}
