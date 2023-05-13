// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;

namespace Gardener.Base
{
    /// <summary>
    /// 租户基础字段
    /// </summary>
    public interface ITenant: IModelId<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
