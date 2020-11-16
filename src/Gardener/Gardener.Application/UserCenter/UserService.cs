// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Fur.DatabaseAccessor;
using Gardener.Application.Dtos;
using Gardener.Core;
using Gardener.Core.Entites;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Gardener.Application.UserCenter
{
    /// <summary>
    /// 用户服务
    /// </summary>
    [AppAuthorize, ApiDescriptionSettings("UserAuthorizationServices")]
    public class UserService : ServiceBase<User, UserDto>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationManager _authorizationManager;
        private readonly IRepository<RoleResource> _roleResourceRepository;
        private readonly IRepository<Resource> _resourceRepository;
        /// <summary>
        /// 用户服务
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="authorizationManager"></param>
        /// <param name="roleResourceRepository"></param>
        /// <param name="resourceRepository"></param>
        public UserService(
            IRepository<User> userRepository,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationManager authorizationManager,
            IRepository<RoleResource> roleResourceRepository, 
            IRepository<Resource> resourceRepository) : base(userRepository)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _authorizationManager = authorizationManager;
            _roleResourceRepository = roleResourceRepository;
            _resourceRepository = resourceRepository;
        }

        /// <summary>
        /// 查看用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<RoleDto> GetRoles([ApiSeat(ApiSeats.ActionStart)] int userId)
        {
            var roles = _userRepository
                .DetachedEntities
                .Include(u => u.Roles)
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Roles)
                .ToList();

            return roles.Adapt<List<RoleDto>>();
        }

        /// <summary>
        /// 查看用户权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Resource> GetResources([ApiSeat(ApiSeats.ActionStart)] int userId)
        {
            List<Resource> resources;
            //超级管理员
            if (_authorizationManager.IsSuperAdministrator())
            {
                resources = _resourceRepository.AsEnumerable(false);
            }
            else
            {
                //其他角色
                resources = _userRepository
                   .Include(u => u.Roles, false)
                       .ThenInclude(u => u.Resources)
                   .Where(u => u.Id == userId)
                   .SelectMany(u => u.Roles
                       .SelectMany(u => u.Resources))
                   .ToList();
            }
            return resources;
        }
    }
}
