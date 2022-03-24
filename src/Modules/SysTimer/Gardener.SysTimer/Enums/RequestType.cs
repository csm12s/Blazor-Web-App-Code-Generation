using System.ComponentModel;

namespace Gardener.SysTimer.Enums
{
    /// <summary>
    /// http请求类型
    /// </summary>
    public enum RequestType
    {
        /// <summary>
        /// 执行内部方法
        /// </summary>
        [Description("内部方法")]
        Run = 0,

        /// <summary>
        /// GET请求
        /// </summary>
        [Description("GET")]
        Get = 1,

        /// <summary>
        /// POST请求
        /// </summary>
        [Description("POST")]
        Post = 2,

        /// <summary>
        /// PUT请求
        /// </summary>
        [Description("PUT")]
        Put = 3,

        /// <summary>
        /// DELETE请求
        /// </summary>
        [Description("DELETE")]
        Delete = 4
    }
}