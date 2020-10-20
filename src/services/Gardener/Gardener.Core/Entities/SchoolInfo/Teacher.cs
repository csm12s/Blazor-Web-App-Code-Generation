// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Fur.DatabaseAccessor;
using System.ComponentModel.DataAnnotations;
using Gardener.Core.Enums;

namespace Gardener.Core.Entities
{
    /// <summary>
    /// 老师
    /// </summary>
    public  class Teacher:Entity
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [MaxLength(32)]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        [Required]
        public int Age { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Required]
        public Gender Sex { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
    }
}
