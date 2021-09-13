﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Authorization.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class PositionDto : BaseDto<int>
    {
        /// <summary>
        /// 岗位名称
        /// </summary>
        [DisplayName("名称")]
        [Required(ErrorMessage = "岗位名称")]
        public string Name { get; set; }

        /// <summary>
        /// 设置该岗位的目标
        /// </summary>
        [DisplayName("目标")]
        public string Target { get; set; }

        /// <summary>
        /// 职责
        /// </summary>
        [DisplayName("职责")]
        public string Duty { get; set; }

        /// <summary>
        /// 权利
        /// </summary>
        [DisplayName("权利")]
        public string Right { get; set; }

        /// <summary>
        /// 岗位等级
        /// </summary>
        [DisplayName("岗位等级")]
        public string Grade { get; set; }

        /// <summary>
        /// 岗位薪资
        /// </summary>
        [DisplayName("岗位薪资")]
        public string Salary { get; set; }


        /// <summary>
        /// 任职资格
        /// </summary>
        [DisplayName("任职资格")]
        public string Qualifications { get; set; }
    }
}
