﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 资源功能信息
    /// </summary>
    [Description("资源功能信息")]
    public class ResourceFunction : IEntity
    {
        /// <summary>
        /// 权限Id
        /// </summary>
        [Required]
        [DisplayName("资源编号")]
        public Guid ResourceId { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        [DisplayName("资源")]
        public Resource Resource { get; set; }

        /// <summary>
        /// 功能Id
        /// </summary>
        [Required]
        [DisplayName("功能编号")]
        public Guid FunctionId { get; set; }

        /// <summary>
        /// 功能
        /// </summary>
        [DisplayName("功能")]
        public Function Function { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;



    }
}
