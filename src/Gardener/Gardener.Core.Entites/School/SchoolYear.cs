// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 学年
    /// </summary>
    public class SchoolYear:Entity
    {
        /// <summary>
        /// 年份
        /// </summary>
        [Range(1,int.MaxValue)]
        public int Year { get; set; }
    }
}
