// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.RemoteRequest.Extensions;
using Furion.TaskScheduler;
using Gardener.EventBus;
using Gardener.NotificationSystem.Dtos.Notification;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.SysTimer.Impl.Demo
{
    /// <summary>
    /// 测试定时任务
    /// </summary>
    /// <remarks>定时抓取财经新闻，作为聊天数据推送到客户端</remarks>
    public class DomeWorker : ISpareTimeWorker
    {
        private static long lastNewsId =0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timer"></param>
        /// <param name="count"></param>
        [SpareTime(100,workerName: "测试定时任务")]
        public void DoSomething(SpareTimer timer, long count) 
        {
            ILogger logger= App.GetRequiredService<ILogger<DomeWorker>>();
            try
            {
                List<NewsInfo> resultNews = GetLastNews().Result;
                if (resultNews == null) { return; }
                foreach (var newsInfo in resultNews)
                {
                    if (newsInfo == null)
                    {
                        return;
                    }
                    IEventBus eventBus = App.GetRequiredService<IEventBus>();
                    ChatDemoNotificationData chatNotification = new ChatDemoNotificationData();
                    chatNotification.Avatar = "./assets/logo.png";
                    chatNotification.NickName = "系统";
                    chatNotification.Message = $"{newsInfo.digest}";
                    eventBus.Publish(chatNotification);
                }
            }
            catch (Exception ex) 
            {
                logger.LogError("测试定时任务执行异常", ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<List<NewsInfo>> GetLastNews()
        {
            Random random = new Random();
            string api = $"https://newsapi.eastmoney.com/kuaixun/v1/getlist_102__10_1_.html?r={random.Next()}&_={DateTime.Now.Millisecond}";

            var response =await api.GetAsync();
            string json = await response.Content.ReadAsStringAsync();
            NewsResult result= System.Text.Json.JsonSerializer.Deserialize<NewsResult>(json);

            if (result != null && result.LivesList != null && result.LivesList.Count>0) 
            {
                List<NewsInfo> resultNews = new List<NewsInfo>();
                foreach (var newsInfo in result.LivesList)
                {
                    if (newsInfo.newsidL > lastNewsId)
                    {
                        resultNews.Add(newsInfo);
                    }
                }
                if (resultNews.Count > 0) 
                {
                    lastNewsId=resultNews[0].newsidL;
                }

                return resultNews.OrderBy(x=>x.newsidL).ToList();
            }

            return null;

        }
    }

    /// <summary>
    /// 新闻结果
    /// </summary>
    public class NewsResult
    { 
        /// <summary>
        /// live列表
        /// </summary>
        public List<NewsInfo> LivesList { get; set; }


    }

    /// <summary>
    /// 新闻信息
    /// </summary>
    public class NewsInfo
    {
        /// <summary>
        /// 新闻id
        /// </summary>
        public string newsid { get; set; }
        /// <summary>
        /// 新闻id列表
        /// </summary>
        public long newsidL { get { return long.Parse(newsid); } }
        /// <summary>
        /// url
        /// </summary>
        public string url_w { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        public string digest { get; set; }
        /// <summary>
        /// 展示时间
        /// </summary>
        public string showtime { get; set; }

    }
}
