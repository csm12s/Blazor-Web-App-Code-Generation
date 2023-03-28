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
        /// 用户名
        /// </summary>
        public const string UserName = nameof(UserName);
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
    }
}
