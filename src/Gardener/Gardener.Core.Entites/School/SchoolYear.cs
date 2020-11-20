// -----------------------------------------------------------------------------
// 文件头
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
