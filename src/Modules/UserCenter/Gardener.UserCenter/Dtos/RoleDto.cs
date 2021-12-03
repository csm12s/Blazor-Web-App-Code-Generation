// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.UserCenter.Dtos
{
    /// <summary>
    /// 角色
    /// </summary>
    public class RoleDto : BaseDto<int>
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [Required(ErrorMessage ="不能为空")]
        [MaxLength(100,ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("名称")]
        public string Name { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        [Required(ErrorMessage = "不能为空")]
        [MaxLength(500, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 是否是超级管理员
        /// 超级管理员拥有所有权限
        /// </summary>
        [DisplayName("是否是超级管理员")]
        public bool IsSuperAdministrator { get; set; }

        /// <summary>
        /// 是否是默认权限
        /// 注册用户时默认设置
        /// </summary>
        [DisplayName("是否是默认角色")]
        public bool IsDefault { get; set; }

        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<ResourceDto> Resources { get; set; }
    }
}