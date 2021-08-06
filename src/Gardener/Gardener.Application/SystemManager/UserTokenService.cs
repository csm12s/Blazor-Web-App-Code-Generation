// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DatabaseAccessor;
using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Gardener.Common;
using Gardener.Core;
using Gardener.Core.Entites;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Gardener.Application.SystemManager
{
    /// <summary>
    /// 用户登录TOKEN服务
    /// </summary>
    [ApiDescriptionSettings("SystemManagerServices")]
    public class UserTokenService : ApplicationServiceBase<UserToken, UserTokenDto,Guid>, IUserTokenService
    {
        private readonly IRepository<UserToken> _repository;
        /// <summary>
        /// 用户登录TOKEN服务
        /// </summary>
        /// <param name="repository"></param>
        public UserTokenService(IRepository<UserToken> repository) : base(repository)
        {
            _repository = repository;
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
        public override async Task<PagedList<UserTokenDto>> Search(PageRequest request)
        {
            IFilterService filterService = App.GetService<IFilterService>();
            Expression<Func<UserToken, bool>> expression = filterService.GetExpression<UserToken>(request.FilterGroups);

            IQueryable<UserToken> queryable = _repository.Include(x=>x.User)
                .Where(u => u.IsDeleted == false)
                .Where(expression);
            return await queryable
                .OrderConditions(request.OrderConditions.ToArray())
                .Select(x => x.Adapt<UserTokenDto>())
                .ToPageAsync(request.PageIndex, request.PageSize);
        }
    }
}
