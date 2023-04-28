// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;
using System.Data;

namespace Gardener.UserCenter.Resources
{
    /// <summary>
    /// 用户中心资源
    /// </summary>
    public class UserCenterResource : SharedLocalResource
    {
        /// <summary>
        /// 点击修改头像
        /// </summary>
        public const string ClickUpdateAvatar = nameof(ClickUpdateAvatar);
       
        /// <summary>
        /// 用户编号
        /// </summary>
        public const string UserId = nameof(UserId);
        /// <summary>
        /// 密码
        /// </summary>
        public const string Password = nameof(Password);
        /// <summary>
        /// 登录密码
        /// </summary>
        public const string LoginPassword = nameof(LoginPassword);
        /// <summary>
        /// 验证码
        /// </summary>
        public const string VerifyCode = nameof(VerifyCode);
        /// <summary>
        /// 自动登录
        /// </summary>
        public const string AutoLogin = nameof(AutoLogin);
        /// <summary>
        /// 登录
        /// </summary>
        public const string Login = nameof(Login);
        /// <summary>
        /// 账号密码登录
        /// </summary>
        public const string AccountPasswordLogin = nameof(AccountPasswordLogin);
        /// <summary>
        /// 没有可用角色，请先添加角色
        /// </summary>
        public const string NoRoleNeedAdd = nameof(NoRoleNeedAdd);
        /// <summary>
        /// 联系人
        /// </summary>
        public const string Contacts = nameof(Contacts);
        /// <summary>
        /// 电话
        /// </summary>
        public const string Tel = nameof(Tel);
        /// <summary>
        /// 邮箱
        /// </summary>
        public const string Email = nameof(Email);
        /// <summary>
        /// 私钥
        /// </summary>
        public const string SecretKey = nameof(SecretKey);
        /// <summary>
        /// 责任
        /// </summary>
        public const string Duty = nameof(Duty);
        /// <summary>
        /// 等级
        /// </summary>
        public const string Grade = nameof(Grade);
        /// <summary>
        /// 目标
        /// </summary>
        public const string Target = nameof(Target);
        /// <summary>
        /// 权利
        /// </summary>
        public const string Right = nameof(Right);
        /// <summary>
        /// 资质
        /// </summary>
        public const string Qualifications = nameof(Qualifications);
        /// <summary>
        /// 薪资
        /// </summary>
        public const string Salary = nameof(Salary);
        /// <summary>
        /// 超级管理员
        /// </summary>
        public const string IsSuperAdministrator = nameof(IsSuperAdministrator);
        /// <summary>
        /// 默认的
        /// </summary>
        public const string IsDefault = nameof(IsDefault);
    }
}
