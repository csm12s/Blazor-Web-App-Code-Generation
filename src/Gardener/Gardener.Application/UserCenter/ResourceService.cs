// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Core.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gardener.Application.UserCenter
{
    /// <summary>
    /// 资源服务
    /// </summary>
    [AppAuthorize, ApiDescriptionSettings("UserAuthorizationServices")]
    public class ResourceService : ServiceBase<Resource, Resource>
    {
        /// <summary>
        /// 资源服务
        /// </summary>
        /// <param name="repository"></param>
        public ResourceService(IRepository<Resource> repository) : base(repository)
        {
        }
    }
}
