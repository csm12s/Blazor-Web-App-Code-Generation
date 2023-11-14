// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

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
        private readonly ILogger<DfcfNewsGatherJob> logger;
        private readonly IEventBus eventBus;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="eventBus"></param>
        public DfcfNewsGatherJob(ILogger<DfcfNewsGatherJob> logger, IEventBus eventBus)
        {
            this.logger = logger;
            this.eventBus = eventBus;
        }

        static private long lastNewsId = 0;
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public Task ExecuteAsync(JobExecutingContext context, CancellationToken stoppingToken)
        {
            List<NewsInfo>? resultNews = GetLastNews().Result;
            if (resultNews == null) {
                context.Result = "执行完成";
                return Task.CompletedTask; 
            }
            foreach (NewsInfo newsInfo in resultNews)
            {
                if (stoppingToken.IsCancellationRequested) { break; }
                DongFangCaiFuNewsEvent newsEvent = new();
                newsEvent.Title = newsInfo.title;
                newsEvent.Content = newsInfo.digest;
                eventBus.Publish(newsEvent);
            }
            context.Result = $"执行完成:采集到{resultNews.Count}条记录";
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
