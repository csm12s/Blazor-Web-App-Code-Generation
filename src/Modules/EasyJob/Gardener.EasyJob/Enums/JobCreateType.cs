using System.ComponentModel;

namespace Gardener.EasyJob.Enums
{
    /// <summary>
    /// 作业创建类型枚举
    /// </summary>
    [Description("作业创建类型枚举")]
    public enum JobCreateType
    {
        /// <summary>
        /// 内置
        /// </summary>
        [Description("BuiltIn")]
        BuiltIn = 0,

        /// <summary>
        /// 脚本
        /// </summary>
        [Description("Script")]
        Script = 1,

        /// <summary>
        /// HTTP请求
        /// </summary>
        [Description("Http")]
        Http = 2,
    }
}
