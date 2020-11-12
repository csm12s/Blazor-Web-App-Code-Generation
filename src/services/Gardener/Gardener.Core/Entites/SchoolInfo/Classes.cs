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

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 班级
    /// </summary>
    public class Classes:Entity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(32)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 所属年纪ID
        /// </summary>
        public string GradeId { get; set; }
    }
}
