// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Fur.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiPaiKe.Core.Entities
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
