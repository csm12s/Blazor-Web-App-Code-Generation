// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using System;
using System.ComponentModel.DataAnnotations;
using Gardener.Enums;

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 学生
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

        /// <summary>
        /// 身份证
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? DateOfDeath { get; set; }

        /// <summary>
        /// 入学日期
        /// </summary>
        public DateTime DateEntrance { get; set; }

        /// <summary>
        /// 是否完成学业毕业
        /// </summary>
        public bool IsFinish { get; set; }
    }
}
