namespace Gardener.Application.Dtos
{
    /// <summary>
    /// 登录输出参数
    /// </summary>
    public class LoginOutput : TokenOutput
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

    }
}