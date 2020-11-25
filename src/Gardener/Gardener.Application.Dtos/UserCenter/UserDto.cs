// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Core.Dtos
{
    /// <summary>
    /// 用户数据转换实体
    /// </summary>
    public class UserDto
    {

        public UserDto()
        {
            UserExtension = new UserExtensionDto();
        }
        /// <summary>
        /// 用户id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>        
        public DateTimeOffset CreatedTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTimeOffset? UpdatedTime { get; set; }
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "不能为空"), StringLength(32, ErrorMessage = "最大长度不能大于{1}")]
        public string UserName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [StringLength(32, ErrorMessage = "最大长度不能大于{1}")]
        public string NickName { get; set; }
        /// <summary>
        /// 密码加密后的
        /// </summary>
        //[Required(ErrorMessage = "不能为空")]
        [StringLength(32, ErrorMessage = "最大长度不能大于{1}")]
        public string Password { get; set; }
        ///// <summary>
        ///// 密码加密Key
        ///// </summary>
        //[Required(ErrorMessage = "不能为空"), StringLength(32, ErrorMessage = "最大长度为32")]
        //public string PasswordEncryptKey { get; set; }
        ///// <summary>
        /// 头像
        /// </summary>
        [MaxLength(100, ErrorMessage = "最大长度不能大于{1}")]
        public string Avatar { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(50, ErrorMessage = "最大长度不能大于{1}")]
        public string Email { get; set; }
        /// <summary>
        /// 邮箱是否确认
        /// </summary>
        public bool EmailConfirmed { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        [MaxLength(20, ErrorMessage = "最大长度不能大于{1}")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 手机是否确认
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Required(ErrorMessage = "不能为空"), DefaultValue(Gender.Male)]
        public Gender Gender { get; set; }
        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<RoleDto> Roles { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<UserRoleDto> UserRoles { get; set; }

        /// <summary>
        /// 用户扩展信息
        /// </summary>
        public UserExtensionDto UserExtension { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }
    }
}
