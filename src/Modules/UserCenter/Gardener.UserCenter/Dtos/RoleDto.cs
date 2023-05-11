// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.SystemManager.Dtos;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.UserCenter.Dtos
{
    /// <summary>
    /// 角色
    /// </summary>
    public class RoleDto : TenantBaseDto<int>
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [Required(ErrorMessage = "不能为空")]
        [MaxLength(100, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("Name")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 角色描述
        /// </summary>
        [Required(ErrorMessage = "不能为空")]
        [MaxLength(500, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("Remark")]
        public string Remark { get; set; }=null!;

        /// <summary>
        /// 是否是超级管理员
        /// 超级管理员拥有所有权限
        /// </summary>
        [DisplayName("IsSuperAdministrator")]
        public bool IsSuperAdministrator { get; set; }

        /// <summary>
        /// 是否是默认权限
        /// 注册用户时默认设置
        /// </summary>
        [DisplayName("IsDefault")]
        public bool IsDefault { get; set; }

        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<ResourceDto>? Resources { get; set; }
    }
}