// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.Schedule;
using Gardener.Authentication.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gardener.Authentication.Jobs
{
    /// <summary>
    /// 清理登录token任务
    /// </summary>
    [JobDetail("job_CleanLoginToken", Description = "清理登录token任务", GroupName = "System", Concurrent = false)]
    [PeriodMinutes(5, TriggerId = "trigger_CleanLoginTokenJob", Description = "清理登录token任务触发器")]
    public class CleanLoginTokenJob : IJob
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        /// <summary>
        /// 清理登录token任务
        /// </summary>
        /// <param name="serviceScopeFactory"></param>
        public CleanLoginTokenJob(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task ExecuteAsync(JobExecutingContext context, CancellationToken stoppingToken)
        {
            DateTimeOffset current = DateTimeOffset.Now;
            using var factory = serviceScopeFactory.CreateScope();
            IRepository<LoginToken> loginTokenRepository = factory.ServiceProvider.GetRequiredService<IRepository<LoginToken>>();
            IEnumerable<LoginToken> tokens = await loginTokenRepository
                .AsQueryable(false)
                .Where(x => x.EndTime.CompareTo(current) <= 0 || x.IsDeleted == true)
                .ToListAsync();
            if (tokens.Any())
            {
                foreach (LoginToken token in tokens)
                {
                    await loginTokenRepository.DeleteAsync(token);
                }
            }
            context.Result = $"执行完成，移除{tokens.Count()}条记录。";
        }
    }
}
