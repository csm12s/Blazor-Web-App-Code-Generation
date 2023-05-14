// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.RemoteRequest.Extensions;
using Furion.TaskScheduler;
using Gardener.EventBus;
using Gardener.SysTimer.Dtos;
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
        static private long lastNewsId = 0;
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
                List<NewsInfo>? resultNews = GetLastNews().Result;
                if (resultNews == null) { return; }
                foreach (var newsInfo in resultNews)
                {
                    if (newsInfo == null)
                    {
                        return;
                    }
                    IEventBus eventBus = App.GetRequiredService<IEventBus>();
                    DongFangCaiFuNewsEvent newsEvent = new();
                    newsEvent.Title = newsInfo.title;
                    newsEvent.Content = newsInfo.digest;
                    eventBus.Publish(newsEvent);
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
        private async Task<List<NewsInfo>?> GetLastNews()
        {
            Random random = new();
            string api = $"https://newsapi.eastmoney.com/kuaixun/v1/getlist_102__10_1_.html?r={random.Next()}&_={DateTime.Now.Millisecond}";

            var response =await api.GetAsync();
            string json = await response.Content.ReadAsStringAsync();
            NewsResult? result= System.Text.Json.JsonSerializer.Deserialize<NewsResult>(json);

            if (result != null && result.LivesList != null && result.LivesList.Count>0) 
            {
                List<NewsInfo> resultNews = new();
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
    /// 
    /// </summary>
    public class NewsResult
    { 
    /// <summary>
    /// 
    /// </summary>
        public List<NewsInfo>? LivesList { get; set; }


    }
    /// <summary>
    /// 
    /// </summary>
    public class NewsInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string newsid { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        public long newsidL { get { return long.Parse(newsid); } }
        /// <summary>
        /// 
        /// </summary>
        public string? url_w { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? digest { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? showtime { get; set; }

    }
}
