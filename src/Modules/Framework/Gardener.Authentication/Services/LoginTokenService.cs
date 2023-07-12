// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Authentication.Domains;
using Gardener.Authentication.Dtos;
using Gardener.Base;
using Gardener.Base.Entity;
using Gardener.EntityFramwork;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Authentication.Services
{
    /// <summary>
    /// 用户登录TOKEN服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class LoginTokenService : ServiceBase<LoginToken, LoginTokenDto, Guid, GardenerMultiTenantDbContextLocator>, ILoginTokenService
    {
        private readonly IRepository<LoginToken, GardenerMultiTenantDbContextLocator> _loginTokenRepository;
        /// <summary>
        /// 用户登录TOKEN服务
        /// </summary>
        /// <param name="repository"></param>
        public LoginTokenService(IRepository<LoginToken, GardenerMultiTenantDbContextLocator> repository) : base(repository)
        {
            _loginTokenRepository = repository;
        }
        
        /// <summary>
        /// 搜索
        /// </summary>
        /// <remarks>
        /// 搜索数据
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public override async Task<PagedList<LoginTokenDto>> Search(PageRequest request)
        {
            IQueryable<LoginToken> queryable = base.GetSearchQueryable(request)
                .Where(u => u.IsDeleted == false);
            return await queryable
                .OrderConditions(request.OrderConditions.ToArray())
                .Select(x => x.Adapt<LoginTokenDto>())
                .ToPageAsync(request.PageIndex, request.PageSize);
        }
    }
}
