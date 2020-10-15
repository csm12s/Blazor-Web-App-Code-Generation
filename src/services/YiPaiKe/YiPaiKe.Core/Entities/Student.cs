using Fur.DatabaseAccessor;
using System.ComponentModel.DataAnnotations;

namespace YiPaiKe.Core.Entities
{
    /// <summary>
    /// 学生表
    /// </summary>
    public class Student:Entity
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
    }
}
