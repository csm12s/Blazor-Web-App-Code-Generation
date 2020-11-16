// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Fur.DatabaseAccessor;
using Gardener.Core.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
