// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.FriendlyException;
using Gardener.Authentication.Domains;
using Gardener.Authentication.Dtos;
using Gardener.Authentication.Enums;
using Gardener.Authorization.Dtos;
using Gardener.Base.Entity;
using Gardener.Enums;
using Gardener.UserCenter.Impl.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Authorization.Core
{
    /// <summary>
    /// 身份权限服务
    /// </summary>
    public class IdentityPermissionService : IIdentityPermissionService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<RoleResource> _roleResourceRepository;
        private readonly IRepository<Resource> _resourceRepository;
        private readonly IRepository<ResourceFunction> _resourceFunctionRepository;
        private readonly IRepository<Function> _functionRepository;
        private readonly IRepository<Client> _clientRepository;
        /// <summary>
        /// 身份权限服务
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="roleRepository"></param>
        /// <param name="roleResourceRepository"></param>
        /// <param name="resourceFunctionRepository"></param>
        /// <param name="functionRepository"></param>
        /// <param name="clientRepository"></param>
        /// <param name="resourceRepository"></param>
        public IdentityPermissionService(IRepository<User> userRepository, IRepository<Role> roleRepository, IRepository<RoleResource> roleResourceRepository, IRepository<ResourceFunction> resourceFunctionRepository, IRepository<Function> functionRepository, IRepository<Client> clientRepository, IRepository<Resource> resourceRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _roleResourceRepository = roleResourceRepository;
            _resourceFunctionRepository = resourceFunctionRepository;
            _functionRepository = functionRepository;
            _clientRepository = clientRepository;
            _resourceRepository = resourceRepository;
        }

        /// <summary>
        /// 判断审核是否用于api访问权限
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="api"></param>
        /// <returns></returns>
        public async Task<bool> Check(Identity? identity, ApiEndpoint? api)
        {
            if (api == null)
            {
                return true;
            }
            if (identity == null)
            {
                return false;
            }
            if (await IsSuperAdministrator(identity))
            {
                return true;
            }
            if (IdentityType.User.Equals(identity.IdentityType))
            {

                return await CurrentUserHaveFunction(int.Parse(identity.Id), api.Key);
            }
            else if (IdentityType.Client.Equals(identity.IdentityType))
            {
                return await CurrentClientHaveFunction(Guid.Parse(identity.Id), api.Key);
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Task<bool> IsSuperAdministrator(Identity? identity)
        {
            if (identity == null)
            {
                //身份信息缺失
                throw Oops.Oh(ExceptionCode.Unauthorized);
            }
            if (IdentityType.User.Equals(identity.IdentityType))
            {
                int userId = int.Parse(identity.Id);
                //超管
                return _userRepository.AsQueryable(false)
                     .Include(u => u.Roles)
                     .Where(u => u.Id == userId && u.IsDeleted == false && u.IsLocked == false)
                     .SelectMany(u => u.Roles.Where(x => x.IsDeleted == false && x.IsLocked == false && x.IsSuperAdministrator == true))
                     .AnyAsync();
            }
            else
            {
                //其他身份无法判断是否是超级管理
                throw Oops.Oh(ExceptionCode.Unauthorized);
            }

        }
        /// <summary>
        /// 判断是否拥有该权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="functionKey"></param>
        /// <returns></returns>
        private async Task<bool> CurrentUserHaveFunction(int userId, string functionKey)
        {
            //分步查询，减少表关联
            var user = await _userRepository.FindOrDefaultAsync(userId);
            if (user == null || user.IsDeleted || user.IsLocked)
            {
                return false;
            }
            var function = await _functionRepository.AsQueryable(false).Where(x => !x.IsDeleted && !x.IsLocked && x.Key.Equals(functionKey)).FirstOrDefaultAsync();
            if (function == null)
            {
                return false;
            }
            var roleIds = await _roleRepository.AsQueryable(false)
                .Include(x => x.UserRoles)
                .Where(x => !x.IsDeleted && !x.IsLocked)
                .SelectMany(x => x.UserRoles.Where(r => r.UserId == userId && !r.IsDeleted && !r.IsLocked)).Select(x => x.RoleId).ToListAsync();
            if (roleIds == null || !roleIds.Any())
            {
                return false;
            }
            var resourceIds = await _roleResourceRepository.AsQueryable(false).Where(x => !x.IsDeleted && !x.IsLocked && roleIds.Contains(x.RoleId)).Select(x => x.ResourceId).ToListAsync();
            if (resourceIds == null || !resourceIds.Any())
            {
                return false;
            }
            var functionResourceIds = await _resourceFunctionRepository.AsQueryable(false).Where(x => x.FunctionId.Equals(function.Id)).Select(x => x.ResourceId).ToListAsync();
            if (functionResourceIds == null || !functionResourceIds.Any())
            {
                return false;
            }
            return resourceIds.Intersect(functionResourceIds).Any();
            //一步到位，减少请求次数
            //return await _userRepository.AsQueryable(false)
            //        .Include(u => u.Roles)
            //            .ThenInclude(u => u.RoleResources)
            //                .ThenInclude(u => u.Resource)
            //                    .ThenInclude(u => u.ResourceFunctions)
            //                        .ThenInclude(u => u.Function)
            //        .Where(u => u.Id == userId && u.IsDeleted == false && u.IsLocked == false)
            //        .SelectMany(u => u.Roles.Where(x => x.IsDeleted == false && x.IsLocked == false)
            //            .SelectMany(u => u.RoleResources.Select(u => u.Resource).Where(x => x.IsDeleted == false && x.IsLocked == false)
            //                    .SelectMany(u => u.ResourceFunctions.Select(u => u.Function).Where(x => x.IsDeleted == false && x.IsLocked == false && x.Key.Equals(functionKey)))
            //                )
            //            ).AnyAsync();
        }

        /// <summary>
        /// 判断是否拥有该权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        private async Task<bool> CurrentUserHaveResource(int userId, string resourceKey)
        {
            //分步查询，减少表关联

            var user = await _userRepository.FindOrDefaultAsync(userId);
            if (user == null || user.IsDeleted || user.IsLocked)
            {
                return false;
            }
            var resource = await _resourceRepository.Where(x => !x.IsDeleted && !x.IsLocked && x.Key.Equals(resourceKey)).FirstOrDefaultAsync();
            if (resource == null)
            {
                return false;
            }
            var roleIds = await _roleRepository.AsQueryable(false)
                .Include(x => x.UserRoles)
                .Where(x => !x.IsDeleted && !x.IsLocked)
                .SelectMany(x => x.UserRoles.Where(r => r.UserId == userId && !r.IsDeleted && !r.IsLocked)).Select(x => x.RoleId).ToListAsync();
            if (roleIds == null || !roleIds.Any())
            {
                return false;
            }
            return await _roleResourceRepository.AsQueryable(false).Where(x => !x.IsDeleted && !x.IsLocked && roleIds.Contains(x.RoleId) && x.ResourceId.Equals(resource.Id)).AnyAsync();
        }
        /// <summary>
        /// 判断是否拥有该权限
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="functionKey"></param>
        /// <returns></returns>
        private async Task<bool> CurrentClientHaveFunction(Guid clientId, string functionKey)
        {
            return await _clientRepository.AsQueryable(false)
                    .Include(u => u.ClientFunctions)
                    .ThenInclude(u => u.Function)
                    .Where(u => u.Id.Equals(clientId) && u.IsDeleted == false && u.IsLocked == false)
                    .SelectMany(u => u.ClientFunctions.Select(u => u.Function).Where(x => x.Key.Equals(functionKey) && x.IsDeleted == false && x.IsLocked == false)
                        ).AnyAsync();
        }

        /// <summary>
        /// 获取身份Id
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public object GetIdentityId(Identity identity)
        {
            if (IdentityType.User.Equals(identity.IdentityType))
            {
                return GetUserId(identity);
            }
            else if (IdentityType.Client.Equals(identity.IdentityType))
            {
                return GetClientId(identity);
            }
            throw Oops.Oh(ExceptionCode.Unauthorized);
        }

        /// <summary>
        /// 获取用户身份id
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        private int GetUserId(Identity identity)
        {
            if (!IdentityType.User.Equals(identity.IdentityType))
            {
                throw Oops.Oh(ExceptionCode.Unauthorized);
            }
            int userId = int.Parse(identity.Id);

            return userId;
        }

        /// <summary>
        /// 获取client身份id
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        private Guid GetClientId(Identity identity)
        {
            if (!IdentityType.Client.Equals(identity.IdentityType))
            {
                throw Oops.Oh(ExceptionCode.Unauthorized);
            }
            Guid clientId = Guid.Parse(identity.Id);

            return clientId;
        }

        /// <summary>
        /// 检测是否有该资源的使用权限
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public async Task<bool> Check(Identity? identity, string resourceKey)
        {
            if (identity == null)
            {
                return false;
            }
            if (await IsSuperAdministrator(identity))
            {
                return true;
            }
            if (IdentityType.User.Equals(identity.IdentityType))
            {
                return await CurrentUserHaveResource(int.Parse(identity.Id), resourceKey);
            }
            else if (IdentityType.Client.Equals(identity.IdentityType))
            {
                //client 暂时没有绑定resource功能
                return false;
            }
            return false;
        }
    }
}
