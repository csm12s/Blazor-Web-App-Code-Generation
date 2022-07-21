using System.ComponentModel;

namespace Gardener.SysTimer.Enums
{
    /// <summary>
    /// 执行类型
    /// </summary>
    public enum ExecuteType
    {
        /// <summary>
        /// 执行内部方法
        /// </summary>
        [Description("内部方法")]
        LOCAL = 0,

        /// <summary>
        /// HTTP请求
        /// </summary>
        [Description("HTTP")]
        HTTP = 1
    }
}