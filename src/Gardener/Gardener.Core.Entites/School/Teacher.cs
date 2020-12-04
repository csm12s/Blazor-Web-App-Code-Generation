// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using System.ComponentModel.DataAnnotations;
using Gardener.Enums;
using System;

namespace Gardener.Core.Entites
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
        /// <summary>
        /// 身份证
        /// </summary>
        public string CardID { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? DateOfDeath { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 入校日期
        /// </summary>
        public DateTime DateEntrance { get; set; }

        /// <summary>
        /// 工龄
        /// </summary>
        public string WorkYears { get; set; }
    }
}


