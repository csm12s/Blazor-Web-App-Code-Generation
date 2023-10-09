// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;

namespace Gardener.Common
{
    /// <summary>
    /// 日期工具类
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// 格式化日期差值为
        /// dd:HH:mm:ss
        /// </summary>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns></returns>
        public static string FormatDateTimeDiff(DateTimeOffset begin, DateTimeOffset end) 
        {
            TimeSpan ts = (end - begin);
            string sdt2 = string.Format("{0}:{1}:{2}:{3}", ts.Days.ToString().PadLeft(2, '0'), ts.Hours.ToString().PadLeft(2, '0'), ts.Minutes.ToString().PadLeft(2, '0'), ts.Seconds.ToString().PadLeft(2, '0'));
            return sdt2;
        }
        /// <summary>
        /// 时间转时间戳(毫秒级)
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long DateTimeToTimestamp(DateTime time)
        {
            TimeSpan ts = time - new DateTime(1970, 1, 1).ToLocalTime();
            return Convert.ToInt64(ts.TotalMilliseconds);
        }
        /// <summary>
        /// 时间戳(毫秒级)转时间
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime TimestampToDateTime(long timestamp)
        {
            return new DateTime(1970, 1, 1).ToLocalTime().AddMilliseconds(timestamp);
        }
    }
}
