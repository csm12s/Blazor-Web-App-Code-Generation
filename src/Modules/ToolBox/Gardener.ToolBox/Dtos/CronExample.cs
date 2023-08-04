namespace Gardener.ToolBox.Dtos
{
    /// <summary>
    /// Crons示例
    /// </summary>
    public class CronExample
    {
        /// <summary>
        /// Crons示例
        /// </summary>
        /// <param name="cron"></param>
        /// <param name="description"></param>
        public CronExample(string cron, string description)
        {
            Cron = cron;
            Description = description;
        }

        /// <summary>
        /// Cron
        /// </summary>
        public string Cron { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
