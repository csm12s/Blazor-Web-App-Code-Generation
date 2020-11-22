// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

namespace Gardener.Core.Security
{
    public class SecurityTokenResult
    {
        public string AccessToken { get; set; }
        public long ExpiresIn { get; set; }
        public string TokenType { get; set; }

    }
}
