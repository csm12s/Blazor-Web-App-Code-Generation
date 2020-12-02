// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

namespace Microsoft.AspNetCore.Authorization
{
    public class ClientPageAuthorizeAttribute: AuthorizeAttribute
    {
        public ClientPageAuthorizeAttribute() : base(AuthConstant.ClientPageResourcePolicy)
        { 
        
        }
    }
}
