// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;

namespace Gardener.UserCenter.Resources
{
    /// <summary>
    /// 用户中心资源-<see cref="UserCenterResource"/>的key
    /// </summary>
    public class UserCenterResourceKeys : SharedLocalResourceKeys
    {
        /// <summary>
        /// 点击修改头像
        /// </summary>
        public const string ClickUpdateAvatar = nameof(UserCenterResource.ClickUpdateAvatar);

        /// <summary>
        /// 用户编号
        /// </summary>
        public const string UserId = nameof(UserCenterResource.UserId);
        /// <summary>
        /// 密码
        /// </summary>
        public const string Password = nameof(UserCenterResource.Password);
        /// <summary>
        /// 登录密码
        /// </summary>
        public const string LoginPassword = nameof(UserCenterResource.LoginPassword);
        /// <summary>
        /// 验证码
        /// </summary>
        public const string VerifyCode = nameof(UserCenterResource.VerifyCode);
        /// <summary>
        /// 自动登录
        /// </summary>
        public const string AutoLogin = nameof(UserCenterResource.AutoLogin);
        /// <summary>
        /// 登录
        /// </summary>
        public const string Login = nameof(UserCenterResource.Login);
        /// <summary>
        /// 账号密码登录
        /// </summary>
        public const string AccountPasswordLogin = nameof(UserCenterResource.AccountPasswordLogin);
        /// <summary>
        /// 没有可用角色，请先添加角色
        /// </summary>
        public const string NoRoleNeedAdd = nameof(UserCenterResource.NoRoleNeedAdd);
        /// <summary>
        /// 联系人
        /// </summary>
        public const string Contacts = nameof(UserCenterResource.Contacts);
        /// <summary>
        /// 电话
        /// </summary>
        public const string Tel = nameof(UserCenterResource.Tel);
        /// <summary>
        /// 邮箱
        /// </summary>
        public const string Email = nameof(UserCenterResource.Email);
        /// <summary>
        /// 私钥
        /// </summary>
        public const string SecretKey = nameof(UserCenterResource.SecretKey);
        /// <summary>
        /// 责任
        /// </summary>
        public const string Duty = nameof(UserCenterResource.Duty);
        /// <summary>
        /// 等级
        /// </summary>
        public const string Grade = nameof(UserCenterResource.Grade);
        /// <summary>
        /// 目标
        /// </summary>
        public const string Target = nameof(UserCenterResource.Target);
        /// <summary>
        /// 权利
        /// </summary>
        public const string Right = nameof(UserCenterResource.Right);
        /// <summary>
        /// 资质
        /// </summary>
        public const string Qualifications = nameof(UserCenterResource.Qualifications);
        /// <summary>
        /// 薪资
        /// </summary>
        public const string Salary = nameof(UserCenterResource.Salary);
        /// <summary>
        /// 超级管理员
        /// </summary>
        public const string IsSuperAdministrator = nameof(UserCenterResource.IsSuperAdministrator);
        /// <summary>
        /// 默认的
        /// </summary>
        public const string IsDefault = nameof(UserCenterResource.IsDefault);
        /// <summary>
        /// 旧密码
        /// </summary>
        public const string OldPassword = nameof(UserCenterResource.OldPassword);
        /// <summary>
        /// 新密码
        /// </summary>
        public const string NewPassword = nameof(UserCenterResource.NewPassword);
        /// <summary>
        /// 确认新密码
        /// </summary>
        public const string ConfirmNewPassword = nameof(UserCenterResource.ConfirmNewPassword);
    }

}
