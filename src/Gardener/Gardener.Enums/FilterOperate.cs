// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Attributes;
using System.ComponentModel;

namespace Gardener.Enums
{
    /// <summary>
    /// 筛选操作方式
    /// </summary>
    public enum FilterOperate
    {
        /// <summary>
        /// 等于
        /// </summary>
        [Code("equal")]
        [Description("等于")]
        Equal = 3,

        /// <summary>
        /// 不等于
        /// </summary>
        [Code("notequal")]
        [Description("不等于")]
        NotEqual = 4,

        /// <summary>
        /// 小于
        /// </summary>
        [Code("less")]
        [Description("小于")]
        Less = 5,

        /// <summary>
        /// 小于或等于
        /// </summary>
        [Code("lessorequal")]
        [Description("小于等于")]
        LessOrEqual = 6,

        /// <summary>
        /// 大于
        /// </summary>
        [Code("greater")]
        [Description("大于")]
        Greater = 7,

        /// <summary>
        /// 大于或等于
        /// </summary>
        [Code("greaterorequal")]
        [Description("大于等于")]
        GreaterOrEqual = 8,

        /// <summary>
        /// 以……开始
        /// </summary>
        [Code("startswith")]
        [Description("开始于")]
        StartsWith = 9,

        /// <summary>
        /// 以……结束
        /// </summary>
        [Code("endswith")]
        [Description("结束于")]
        EndsWith = 10,

        /// <summary>
        /// 字符串的包含（相似）
        /// </summary>
        [Code("contains")]
        [Description("包含")]
        Contains = 11,

        /// <summary>
        /// 字符串的不包含
        /// </summary>
        [Code("notcontains")]
        [Description("不包含")]
        NotContains = 12,
        /// <summary>
        /// 包括在
        /// </summary>
        [Code("in")]
        [Description("包括在")]
        In = 13
        //    ,

        ///// <summary>
        ///// 不包括在
        ///// </summary>
        //[Code("notin")]
        //[Description("不包括在")]
        //NotIn = 14
    }
}
