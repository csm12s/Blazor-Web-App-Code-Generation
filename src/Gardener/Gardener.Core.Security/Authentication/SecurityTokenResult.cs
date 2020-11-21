// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

namespace Gardener.Core.Security.Authentication
{
    public class SecurityTokenResult
    {
        public bool Status { get; set; }
        public string AccessToken { get; set; }
        public long ExpiresIn { get; set; }
        public string TokenType { get; set; }
    }
}
