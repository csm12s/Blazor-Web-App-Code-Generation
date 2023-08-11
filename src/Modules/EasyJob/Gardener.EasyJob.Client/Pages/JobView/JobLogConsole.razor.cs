// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Base;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.AntDesignUi.Base.Constants;
using Gardener.Client.Base;
using Gardener.EasyJob.Dtos;
using Gardener.EasyJob.Resources;
using Gardener.EasyJob.Services;
using Microsoft.AspNetCore.Components;

namespace Gardener.EasyJob.Client.Pages.JobView
{
    /// <summary>
    /// 定时任务控制台输入参数
    /// </summary>
    public class JobLogConsoleInput
    {
        /// <summary>
        /// 定时任务控制台输入参数
        /// </summary>
        /// <param name="jobId"></param>
        public JobLogConsoleInput(string jobId)
        {
            JobId = jobId;
        }

        /// <summary>
        /// 定时任务控制台输入参数
        /// </summary>
        /// <param name="triggerId"></param>
        /// <param name="jobId"></param>
        public JobLogConsoleInput(string triggerId, string jobId)
        {
            TriggerId = triggerId;
            JobId = jobId;
        }

        /// <summary>
        /// 作业触发器编号
        /// </summary>
        public string? TriggerId { get; set; }

        /// <summary>
        /// 作业 Id
        /// </summary>
        public string JobId { get; set; }
    }

    /// <summary>
    /// 定时任务日志控制台
    /// </summary>
    public partial class JobLogConsole : OperationDialogBase<JobLogConsoleInput, bool, EasyJobLocalResource>
    {
        /// <summary>
        /// 日志服务
        /// </summary>
        [Inject]
        private ISysJobLogService JobLogService { get; set; } = null!;
        /// <summary>
        /// 本地化器
        /// </summary>
        [Inject]
        private IClientLocalizer<EasyJobLocalResource> Localizer { get; set; } =null!;

        /// <summary>
        /// 日志内容
        /// </summary>
        private string logContent = string.Empty;
        /// <summary>
        /// 分页数
        /// </summary>
        private int logPageTotal = 0;

        private int pageIndex = 1;

        private int pageSize = 50;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await ReloadLog();
            await base.OnInitializedAsync();
        }

        /// <summary>
        /// 加载日志
        /// </summary>
        /// <returns></returns>
        private async Task ReloadLog()
        {
            PageRequest request = new PageRequest();
            request.PageSize = pageSize;
            request.PageIndex = pageIndex;
            request.OrderConditions = new List<ListSortDirection>()
            {
                new ListSortDirection ()
                {
                    FieldName=$"{nameof(SysJobLogDto.CreatedTime)}",
                    SortType=Gardener.Enums.ListSortType.Desc
                }
            };
            List<FilterRule> rules = new List<FilterRule>()
            {
                new FilterRule() {
                    Field=$"{nameof(SysJobLogDto.JobId)}",
                    Value=this.Options.JobId,
                    Condition=FilterCondition.And,
                    Operate=FilterOperate.Equal
                }
            };
            if (!string.IsNullOrEmpty(this.Options.TriggerId))
            {
                rules.Add(new FilterRule()
                {
                    Field = $"{nameof(SysJobLogDto.TriggerId)}",
                    Value = this.Options.TriggerId,
                    Condition = FilterCondition.And,
                    Operate = FilterOperate.Equal
                });
            }
            request.FilterGroups = new List<FilterGroup>()
            {
                new FilterGroup ()
                {
                    Rules=rules
                }

            };
            PagedList<SysJobLogDto> result = await JobLogService.Search(request);
            logPageTotal = result.TotalCount;

            if (result.Items.Any())
            {
                foreach (var item in result.Items.OrderBy(x=>x.CreatedTime))
                {
                    string status = Localizer[EasyJobLocalResource.Success];
                    if (!item.Succeeded)
                    {
                        status = Localizer[EasyJobLocalResource.Fail];
                    }
                    logContent += $"\n{item.CreatedTime.ToString(ClientConstant.DateTimeFormat)} - [{item.JobId}] - [{item.TriggerId}] - [{item.ElapsedTime} ms] - [{status}]";
                    if(!string.IsNullOrEmpty(item.Result))
                    {
                        logContent += $"\n{item.Result}";
                    }
                    if (!item.Succeeded)
                    {
                        logContent += $"\n{item.ExceptionMessage}";
                        logContent += $"\n{item.Exception}";
                    }
                }
            }
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="args"></param>
        private Task OnPaginationChange(PaginationEventArgs args)
        {
            (pageIndex, pageSize) = args;
            return ReloadLog();
        }
    }
}
