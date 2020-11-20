// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 课程
    /// </summary>
    public class Curriculum:Entity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(32)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 所属学科
        /// </summary>
        public string SubjectId { get; set; }
    }
}
