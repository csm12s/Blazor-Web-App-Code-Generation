// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Gardener.ToolBox.Dtos
{
    /// <summary>
    /// Cron 检验输入参数
    /// </summary>
    public class CronCheckInput
    {
        /// <summary>
        /// Cron 检验输入参数
        /// </summary>
        /// <param name="cron"></param>
        /// <param name="runTimeNum"></param>
        /// <param name="cronStringFormat"></param>
        [JsonConstructor]
        public CronCheckInput(string cron, int runTimeNum, int cronStringFormat) 
        {
            Cron = cron;
            RunTimeNum = runTimeNum;
            CronStringFormat = cronStringFormat;
        }

        /// <summary>
        /// Cron
        /// </summary>
        public string Cron { get; set; }
        /// <summary>
        /// 获取运行时间数量
        /// </summary>
        public int RunTimeNum { get; set; } = 10;
        /// <summary>
        /// Cron格式
        /// </summary>
        /// <remarks>
        /// <para>0:默认格式,书写顺序：分 时 天 月 周</para>
        /// <para>1:带年份格式,书写顺序：分 时 天 月 周 年</para>
        /// <para>2:带秒格式,书写顺序：秒 分 时 天 月 周</para>
        /// <para>3:带秒和年格式,书写顺序：秒 分 时 天 月 周 年</para>
        /// </remarks>
        public int CronStringFormat { get; set; } = 0;
    }
}
