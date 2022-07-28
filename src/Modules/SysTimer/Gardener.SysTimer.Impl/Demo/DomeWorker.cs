// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.RemoteRequest.Extensions;
using Furion.TaskScheduler;
using Gardener.NotificationSystem.Core;
using Gardener.NotificationSystem.Dtos.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.SysTimer.Impl.Demo
{
    /// <summary>
    /// 测试定时任务
    /// </summary>
    /// <remarks>定时抓取财经新闻，作为聊天数据推送到客户端</remarks>
    public class DomeWorker : ISpareTimeWorker
    {
        private static string lastNewsId ="";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timer"></param>
        /// <param name="count"></param>
        [SpareTime(100,workerName: "测试定时任务")]
        public void DoSomething(SpareTimer timer, long count) 
        {
            
            NewsInfo newsInfo= GetLastNews().Result;
            if (newsInfo == null) 
            {
                return;
            }
            ChatDemoNotificationData chatNotification = new ChatDemoNotificationData();
            chatNotification.Avatar = "./assets/logo.png";
            chatNotification.NickName = "系统";

            chatNotification.Message = $"{newsInfo.digest}";
            ISystemNotificationService systemNotificationService=App.GetService<ISystemNotificationService>();
            systemNotificationService.SendToAllClient(chatNotification);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<NewsInfo> GetLastNews()
        {
            Random random = new Random();
            string api = $"https://newsapi.eastmoney.com/kuaixun/v1/getlist_102__50_1_.html?r={random.Next()}&_={DateTime.Now.Millisecond}";

            var response =await api.GetAsync();
            string json = await response.Content.ReadAsStringAsync();
            NewsResult result= System.Text.Json.JsonSerializer.Deserialize<NewsResult>(json);

            if (result != null && result.LivesList != null && result.LivesList.Count>0) 
            {
                NewsInfo newsInfo= result.LivesList[0];
                if (newsInfo.newsid.Equals(lastNewsId)) 
                {
                    return null;
                }
                lastNewsId = newsInfo.newsid;
                return newsInfo;
            }

            return null;

        }
    }

    public class NewsResult
    { 
    
        public List<NewsInfo> LivesList { get; set; }


    }

    public class NewsInfo
    {
        public string newsid { get; set; }
        public string url_w { get; set; }
        public string title { get; set; }
        public string digest { get; set; }
        public string showtime { get; set; }

    }
}
