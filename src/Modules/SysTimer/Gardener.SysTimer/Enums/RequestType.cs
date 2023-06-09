using System.ComponentModel;

namespace Gardener.SysTimer.Enums
{
    /// <summary>
    /// 执行类型
    /// </summary>
    public enum ExecuteType
    {
        /// <summary>
        /// 内部方法
        /// </summary>
        [Description("LocalMethod")]
        LOCAL_METHOD = 0,

        /// <summary>
        /// HTTP请求
        /// </summary>
        [Description("HTTP")]
        HTTP = 1
    }
}