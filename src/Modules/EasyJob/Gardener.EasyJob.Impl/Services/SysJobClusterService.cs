﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DatabaseAccessor;
using Gardener.EasyJob.Dtos;
using Gardener.EasyJob.Enums;
using Gardener.EasyJob.Impl.Domains;
using Gardener.EasyJob.Services;
using Gardener.EntityFramwork;
using Medallion.Threading;
using Microsoft.AspNetCore.Mvc;

namespace Gardener.EasyJob.Impl.Services
{
    /// <summary>
    /// 定时任务-集群服务
    /// </summary>
    [ApiDescriptionSettings("EasyJobServices")]
    public class SysJobClusterService : ServiceBase<SysJobCluster, SysJobClusterDto, int>, ISysJobClusterService
    {
        private readonly IRepository<SysJobCluster, MasterDbContextLocator> sysJobClusterRep;
        private readonly IDistributedLockProvider distributedLockProvider;
        private readonly Random rd = new(DateTime.Now.Millisecond);
        /// <summary>
        /// 定时任务-集群服务
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="distributedLockProvider"></param>
        public SysJobClusterService(IRepository<SysJobCluster, MasterDbContextLocator> repository, IDistributedLockProvider distributedLockProvider) : base(repository)
        {
            this.sysJobClusterRep = repository;
            this.distributedLockProvider = distributedLockProvider;
        }

        /// <summary>
        /// 当前作业调度器启动通知
        /// </summary>
        /// <param name="context">作业集群服务上下文</param>
        public async Task<bool> Start(JobClusterContext context)
        {
            // 在作业集群表中，如果 clusterId 不存在，则新增一条（否则更新一条），并设置 status 为 ClusterStatus.Waiting
            if (await sysJobClusterRep.AnyAsync(u => u.ClusterId == context.ClusterId))
            {
                sysJobClusterRep.Where(u => u.ClusterId == context.ClusterId).ToList().ForEach(x =>
                {
                    x.Status = ClusterStatus.Waiting;
                    sysJobClusterRep.Update(x);
                });
            }
            else
            {
                await sysJobClusterRep.InsertAsync(new SysJobCluster { ClusterId = context.ClusterId, Status = ClusterStatus.Waiting });
            }
            return true;
        }

        /// <summary>
        /// 等待被唤醒
        /// </summary>
        /// <param name="context">作业集群服务上下文</param>
        /// <returns><see cref="Task"/></returns>
        public async Task<bool> Waiting(JobClusterContext context)
        {
            var clusterId = context.ClusterId;

            while (true)
            {
                // 控制集群心跳频率（放在头部为了防止 IsAnyAsync continue 没sleep占用大量IO和CPU）
                await Task.Delay(3000 + rd.Next(500, 1000)); // 错开集群同时启动

                try
                {
                    //使用分布式锁
                    using (distributedLockProvider.AcquireLock("lock:JobClusterServer:WaitingForAsync", TimeSpan.FromSeconds(1)))
                    {
                        // 在这里查询数据库，根据以下两种情况处理
                        // 1) 如果作业集群表已有 status 为 ClusterStatus.Working 则继续循环
                        // 2) 如果作业集群表中还没有其他服务或只有自己，则插入一条集群服务或调用 await WorkNowAsync(clusterId); 之后 return;
                        // 3) 如果作业集群表中没有 status 为 ClusterStatus.Working 的，调用 await WorkNowAsync(clusterId); 之后 return;
                        if (await sysJobClusterRep.AnyAsync(u => u.Status == ClusterStatus.Working))
                            continue;

                        await WorkNowAsync(clusterId);
                        break;
                    }
                }
                catch
                {

                }
            }
            return true;
        }

        /// <summary>
        /// 当前作业调度器停止通知
        /// </summary>
        /// <param name="context">作业集群服务上下文</param>
        public Task<bool> Stop(JobClusterContext context)
        {
            // 在作业集群表中，更新 clusterId 的 status 为 ClusterStatus.Crashed
            sysJobClusterRep.Where(u => u.ClusterId == context.ClusterId).ToList().ForEach(x =>
            {
                x.Status = ClusterStatus.Crashed;
                sysJobClusterRep.Update(x);
            });
            return Task.FromResult(true);
        }

        /// <summary>
        /// 当前作业调度器宕机
        /// </summary>
        /// <param name="context">作业集群服务上下文</param>
        public Task<bool> Crash(JobClusterContext context)
        {
            // 在作业集群表中，更新 clusterId 的 status 为 ClusterStatus.Crashed
            sysJobClusterRep.Where(u => u.ClusterId == context.ClusterId).ToList().ForEach(x =>
            {
                x.Status = ClusterStatus.Crashed;
                sysJobClusterRep.Update(x);
            });
            return Task.FromResult(true);
        }

        /// <summary>
        /// 指示集群可以工作
        /// </summary>
        /// <param name="clusterId">集群 Id</param>
        /// <returns></returns>
        private Task<bool> WorkNowAsync(string clusterId)
        {
            // 在作业集群表中，更新 clusterId 的 status 为 ClusterStatus.Working
            sysJobClusterRep.Where(u => u.ClusterId == clusterId).ToList().ForEach(x =>
            {
                x.Status = ClusterStatus.Working;
                sysJobClusterRep.Update(x);
            });
            return Task.FromResult(true);
        }
    }
}
