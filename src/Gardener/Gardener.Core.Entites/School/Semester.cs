// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 学年学期
    /// </summary>
    public class Semester:Entity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(32)]
        [Required]
        public string Name { get; set; }
    }
}
