// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Attributes;
using Gardener.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.UserCenter.Dtos
{
    /// <summary>
    /// 岗位
    /// </summary>
    public class PositionDto : BaseDto<int>
    {
        /// <summary>
        /// 岗位名称
        /// </summary>
        [DisplayName("Name")]
        [Required(ErrorMessage = "不能为空")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 设置该岗位的目标
        /// </summary>
        [DisplayName("Target")]
        public string? Target { get; set; }

        /// <summary>
        /// 职责
        /// </summary>
        [DisplayName("Duty")]
        public string? Duty { get; set; }

        /// <summary>
        /// 权利
        /// </summary>
        [DisplayName("Right")]
        public string? Right { get; set; }

        /// <summary>
        /// 岗位等级
        /// </summary>
        [DisplayName("Grade")]
        [CodeType("position-level")]
        public string? Grade { get; set; }

        /// <summary>
        /// 岗位薪资
        /// </summary>
        [DisplayName("Salary")]
        public string? Salary { get; set; }


        /// <summary>
        /// 任职资格
        /// </summary>
        [DisplayName("Qualifications")]
        public string? Qualifications { get; set; }
    }
}
