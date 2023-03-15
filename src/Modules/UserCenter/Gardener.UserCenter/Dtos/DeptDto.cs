// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.UserCenter.Dtos
{
    /// <summary>
    /// 部门信息
    /// </summary>
    [Description("部门信息")]
    public class DeptDto: BaseDto<int>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [Required(ErrorMessage = "不能为空"), MaxLength(30, ErrorMessage = "最大长度不能大于{1}")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 联系人
        /// </summary>
        [DisplayName("联系人")]
        [MaxLength(20, ErrorMessage = "最大长度不能大于{1}")]
        public string? Contacts { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [DisplayName("电话")]
        [MaxLength(20, ErrorMessage = "最大长度不能大于{1}")]
        public string? Tel { get; set; }

        /// <summary>
        /// 资源排序
        /// </summary>
        [Required(ErrorMessage = "不能为空"), DefaultValue(0)]
        [DisplayName("排序")]
        public int Order { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [MaxLength(100, ErrorMessage = "最大长度不能大于{1}")]
        public string? Remark { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        [DisplayName("父级编号")]
        public int? ParentId { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public ICollection<DeptDto>? Children { get; set; }
    }
}
