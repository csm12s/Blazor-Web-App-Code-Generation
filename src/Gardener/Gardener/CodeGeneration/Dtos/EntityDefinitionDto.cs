// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Gardener.CodeGeneration.Dtos
{
    /// <summary>
    /// 实体信息
    /// </summary>
    [Description("实体信息")]
    public class EntityDefinitionDto
    {
        /// <summary>
        /// 实体名称
        /// </summary>
        [DisplayName("实体名称")]
        public String Name { get; set; }
        /// <summary>
        /// 实体完整名称
        /// </summary>
        [DisplayName("实体完整名称")]
        public String FullName { get; set; }
        /// <summary>
        /// 实体描述
        /// </summary>
        [DisplayName("实体描述")]
        public string Description { get; set; }
        /// <summary>
        /// 实体属性信息
        /// </summary>
        [DisplayName("实体属性信息")]
        public List<EntityPropertyDto> Properties { get; set; }

    }
}
