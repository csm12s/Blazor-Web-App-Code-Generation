// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DynamicApiController;
using Furion.TimeCrontab;
using Gardener.ToolBox.Dtos;
using Gardener.ToolBox.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gardener.ToolBox.Impl.Services
{
    /// <summary>
    /// Cron示例服务
    /// </summary>
    [ApiDescriptionSettings("ToolBoxServices")]
    public class CronExampleService : ICronExampleService, IDynamicApiController
    {
        /// <summary>
        /// 检验
        /// </summary>
        /// <param name="checkInput"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<CronCheckResult> Check(CronCheckInput checkInput)
        {
            CronStringFormat format = (CronStringFormat)checkInput.CronStringFormat;


            Crontab crontab = Crontab.TryParse(checkInput.Cron, format);
            if (crontab == null)
            {
                return Task.FromResult(new CronCheckResult() { IsValid = false });
            }
            List<DateTimeOffset> dates = new List<DateTimeOffset>();
            DateTime startDate = DateTime.Now;
            for (int i = 0; i < checkInput.RunTimeNum; i++)
            {
                DateTime date = crontab.GetNextOccurrence(startDate);
                dates.Add(new DateTimeOffset(date));
                startDate = date;
            }

            return Task.FromResult(new CronCheckResult() { IsValid = true, RunTimes = dates });
        }

        /// <summary>
        /// 获取Cron示例列表
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IEnumerable<CronExample>> GetCronExamples()
        {
            List<CronExample> result = new List<CronExample>();
            // 静态属性
            result.Add(new CronExample(Crontab.Secondly.ToString(), "每秒 .0000000"));
            result.Add(new CronExample(Crontab.SecondlyAt(3).ToString(), "每第 3 秒"));
            result.Add(new CronExample(Crontab.SecondlyAt(3, 5, 6).ToString(), "每第 3，5，6 秒"));
            result.Add(new CronExample(Crontab.Minutely.ToString(), "每分钟 00"));
            result.Add(new CronExample(Crontab.MinutelyAt(3).ToString(), "每分钟第 3 秒"));
            result.Add(new CronExample(Crontab.MinutelyAt(3, 5, 6).ToString(), "每分钟第 3，5，6 秒"));
            result.Add(new CronExample(Crontab.Hourly.ToString(), "每小时 00:00"));
            result.Add(new CronExample(Crontab.HourlyAt(3).ToString(), "每小时第 3 分钟"));
            result.Add(new CronExample(Crontab.HourlyAt(3, 5, 6).ToString(), "每小时第 3，5，6 分钟"));
            result.Add(new CronExample(Crontab.Daily.ToString(), "每天 00:00:00"));
            result.Add(new CronExample(Crontab.DailyAt(3).ToString(), "每天第 3 小时整（点）"));
            result.Add(new CronExample(Crontab.DailyAt(3, 5, 6).ToString(), "每天第 3，5，6 小时整（点）"));
            result.Add(new CronExample(Crontab.Monthly.ToString(), "每月 1 号 00:00:00"));
            result.Add(new CronExample(Crontab.MonthlyAt(3).ToString(), "每月第 3 天零点整"));
            result.Add(new CronExample(Crontab.MonthlyAt(3, 5, 6).ToString(), "每月第 3，5，6 天零点整"));
            result.Add(new CronExample(Crontab.Weekly.ToString(), "每周日 00:00:00"));
            result.Add(new CronExample(Crontab.WeeklyAt(3).ToString(), "每周星期 3 零点整"));
            result.Add(new CronExample(Crontab.WeeklyAt("WED").ToString(), "每周星期 3 零点整// SUN（星期天），MON，TUE，WED，THU，FRI，SAT"));
            result.Add(new CronExample(Crontab.WeeklyAt(3, 5, 6).ToString(), "每周星期 3，5，6 零点整"));
            result.Add(new CronExample(Crontab.WeeklyAt("WED", "FRI", "SAT").ToString(), "每周星期 3，5，6 零点整// SUN（星期天），MON，TUE，WED，THU，FRI，SAT"));
            result.Add(new CronExample(Crontab.Yearly.ToString(), "每年 1 月 1 号 00:00:00"));
            result.Add(new CronExample(Crontab.YearlyAt(3).ToString(), "每年第 3 月 1 日零点整"));
            result.Add(new CronExample(Crontab.YearlyAt("MAR").ToString(), "每年第 3 月 1 日零点整// JAN（一月），FEB，MAR，APR，MAY，JUN，JUL，AUG，SEP，OCT，NOV，DEC"));
            result.Add(new CronExample(Crontab.YearlyAt(3, 5, 6).ToString(), "每年第 3，5，6 月 1 日零点整"));
            result.Add(new CronExample(Crontab.YearlyAt("MAR", "MAY", "JUN").ToString(), "每年第 3，5，6 月 1 日零点整// JAN（一月），FEB，MAR，APR，MAY，JUN，JUL，AUG，SEP，OCT，NOV，DEC"));
            result.Add(new CronExample(Crontab.Workday.ToString(), "每周一至周五 00:00:00"));
            return Task.FromResult<IEnumerable<CronExample>>(result);
        }

    }
}
