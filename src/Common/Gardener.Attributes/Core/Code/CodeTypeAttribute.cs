// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;

namespace Gardener.Attributes
{
    /// <summary>
    /// 字典类型信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
    public class CodeTypeAttribute : Attribute
    {
        /// <summary>
        /// 字典信息
        /// </summary>
        /// <param name="codeValue"></param>
        /// <param name="codeTypeValue"></param>
        public CodeTypeAttribute(string codeTypeValue)
        {
            CodeTypeValue = codeTypeValue;
        }

        /// <summary>
        /// 字典类型值
        /// </summary>
        public string CodeTypeValue { get; private set; }
    }
}
