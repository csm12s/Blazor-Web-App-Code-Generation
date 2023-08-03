// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.RemoteRequest.Extensions;
using Furion.Schedule;
using Gardener.EasyJob.Dtos;
using Gardener.EventBus;
using Microsoft.Extensions.Logging;

namespace Gardener.EasyJob.Impl.Jobs
{
    /// <summary>
    /// 采集东方财富新闻job
    /// </summary>
    [JobDetail("job_DfcfNewsGather", Description = "采集东方财富新闻任务", GroupName = "we_chat", Concurrent = false)]
    [PeriodSeconds(5, TriggerId = "trigger_DfcfNewsGather", Description = "采集东方财富新闻触发器")]
    public class DfcfNewsGatherJob : IJob
    {
        static private long lastNewsId = 0;
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public Task ExecuteAsync(JobExecutingContext context, CancellationToken stoppingToken)
        {
            ILogger logger = App.GetRequiredService<ILogger<DfcfNewsGatherJob>>();
            List<NewsInfo>? resultNews = GetLastNews().Result;
            if (resultNews == null) { return Task.CompletedTask; }
            foreach (var newsInfo in resultNews)
            {
                if (stoppingToken.IsCancellationRequested) { break; }
                if (newsInfo == null)
                {
                    return Task.CompletedTask;
                }
                IEventBus eventBus = App.GetRequiredService<IEventBus>();
                DongFangCaiFuNewsEvent newsEvent = new();
                newsEvent.Title = newsInfo.title;
                newsEvent.Content = newsInfo.digest;
                eventBus.Publish(newsEvent);
            }
            context.Result = "执行完成";
            return Task.CompletedTask;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<List<NewsInfo>?> GetLastNews()
        {
            Random random = new();
            string api = $"https://newsapi.eastmoney.com/kuaixun/v1/getlist_102__10_1_.html?r={random.Next()}&_={DateTime.Now.Millisecond}";

            var response = await api.GetAsync();
            string json = await response.Content.ReadAsStringAsync();
            NewsResult? result = System.Text.Json.JsonSerializer.Deserialize<NewsResult>(json);

            if (result != null && result.LivesList != null && result.LivesList.Count > 0)
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
                    lastNewsId = resultNews[0].newsidL;
                }

                return resultNews.OrderBy(x => x.newsidL).ToList();
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
