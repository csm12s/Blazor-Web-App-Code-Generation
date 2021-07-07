// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Application.Dtos
{
    /// <summary>
    /// 用户数据转换实体
    /// </summary>
    public class UserDto: BaseDto<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public UserDto()
        {
            UserExtension = new UserExtensionDto();
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTimeOffset? UpdatedTime { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "不能为空"), MaxLength(32, ErrorMessage = "最大长度不能大于{1}"),MinLength(5,ErrorMessage = "最小长度不能小于{1}")]
        [DisplayName("用户名")]
        public string UserName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(32, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("昵称")]
        public string NickName { get; set; }
        /// <summary>
        /// 密码加密后的
        /// </summary>
        //[Required(ErrorMessage = "不能为空")]
        [MaxLength(32, ErrorMessage = "最大长度不能大于{1}"), MinLength(5, ErrorMessage = "最小长度不能小于{1}")]
        [DisplayName("密码")]
        public string Password { get; set; }
        
        /// <summary>
        /// 头像
        /// </summary>
        [MaxLength(100, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("头像")]
        public string Avatar { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(50, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("邮箱")]
        public string Email { get; set; }
        /// <summary>
        /// 邮箱是否确认
        /// </summary>
        [DisplayName("邮箱是否确认")]
        public bool EmailConfirmed { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        [MaxLength(20, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("手机")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 手机是否确认
        /// </summary>
        [DisplayName("手机是否确认")]
        public bool PhoneNumberConfirmed { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Required(ErrorMessage = "不能为空"), DefaultValue(Gender.Male)]
        [DisplayName("性别")]
        public Gender Gender { get; set; }
        /// <summary>
        /// 多对多
        /// </summary>
        [DisplayName("角色")]
        public ICollection<RoleDto> Roles { get; set; }
        /// <summary>
        /// 用户扩展信息
        /// </summary>
        public UserExtensionDto UserExtension { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        [DisplayName("部门编号")]
        public int? DeptId { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public DeptDto Dept { get; set; }
    }
}
