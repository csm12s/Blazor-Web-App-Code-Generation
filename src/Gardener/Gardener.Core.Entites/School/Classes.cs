// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using System.ComponentModel.DataAnnotations;

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
