// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Enums;
using Gardener.SystemManager.Dtos;
using System;
using System.Collections.Generic;

namespace Gardener.Client.Base
{
    /// <summary>
    /// TableSearchField
    /// </summary>
    public class TableSearchField
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// 展示的名字
        /// </summary>
        public string? DisplayName { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public Type Type { get; set; } = null!;
        /// <summary>
        /// 单值使用
        /// </summary>
        public string? Value { get; set; }
        /// <summary>
        /// 多值时使用
        /// </summary>
        public IEnumerable<string>? Values { get; set; }
        /// <summary>
        /// 是否有多值
        /// </summary>
        public bool Multiple { get; set; } = false;
        /// <summary>
        /// 是否是字典
        /// </summary>
        public bool IsCode { get; set; }=false;
        /// <summary>
        /// 字典类型值
        /// </summary>
        public string? CodeTypeValue { get; set; }
        /// <summary>
        /// 字典
        /// </summary>
        public List<CodeDto>? Codes { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; } = int.MaxValue-1000;
    }
}
