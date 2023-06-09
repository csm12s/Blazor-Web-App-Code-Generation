// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Audit.Resources
{
    /// <summary>
    /// 审计本地化资源
    /// </summary>
    public class AuditLocalResource : SharedLocalResource
    {
        /// <summary>
        /// 操作者编号
        /// </summary>
        public const string OperaterId = nameof(OperaterId);
        /// <summary>
        /// 操作者名称
        /// </summary>
        public const string OperaterName = nameof(OperaterName);
        /// <summary>
        /// 操作者类型
        /// </summary>
        public const string OperaterType = nameof(OperaterType);
        /// <summary>
        /// 类型名称
        /// </summary>
        public const string TypeName = nameof(TypeName);
        /// <summary>
        /// 数据编号
        /// </summary>
        public const string DataId = nameof(DataId);
        /// <summary>
        /// 操作类型
        /// </summary>
        public const string OperationType = nameof(OperationType);
        /// <summary>
        /// 字段名称
        /// </summary>
        public const string FieldName = nameof(FieldName);
        /// <summary>
        /// 显示名称
        /// </summary>
        public const string DisplayName = nameof(DisplayName);
        /// <summary>
        /// 数据类型
        /// </summary>
        public const string DataType = nameof(DataType);
        /// <summary>
        /// 原值
        /// </summary>
        public const string OriginalValue = nameof(OriginalValue);
        /// <summary>
        /// 新值
        /// </summary>
        public const string NewValue = nameof(NewValue);
        /// <summary>
        /// 资源名称
        /// </summary>
        public const string ResourceName = nameof(ResourceName);
    }
}
