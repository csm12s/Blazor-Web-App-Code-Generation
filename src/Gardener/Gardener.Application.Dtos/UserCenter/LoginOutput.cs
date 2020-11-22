namespace Gardener.Core.Dtos
{
    /// <summary>
    /// 登录输出参数
    /// </summary>
    public class LoginOutput
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public long AccessTokenExpiresIn { get; set; }
    }
}