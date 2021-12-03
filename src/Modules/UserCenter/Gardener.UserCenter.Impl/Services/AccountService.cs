// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Gardener.Enums;
using System.Threading.Tasks;
using Gardener.Attributes;
using Microsoft.EntityFrameworkCore;
using Gardener.Authorization.Core;
using Gardener.Authorization.Dtos;
using System;
using Gardener.Authentication.Core;
using Gardener.Authentication.Dtos;
using Gardener.Authentication.Enums;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Enums;
using Gardener.UserCenter.Impl.Domains;
using Gardener.UserCenter.Services;
using Gardener.UserCenter.Impl.Core;
using Gardener.VerifyCode.Core;

namespace Gardener.UserCenter.Impl.Services
{
    /// <summary>
    /// 用户账户认证授权服务
    /// </summary>
    [ApiDescriptionSettings("UserCenterServices")]
    public class AccountService : IDynamicApiController, IAccountService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<User> _userRepository;
        private readonly Authorization.Core.IAuthorizationService _authorizationManager;
        private readonly IJwtService _jwtBearerService;
        private readonly IIdentityPermissionService _identityPermissionService;
        /// <summary>
        /// 资源仓储
        /// </summary>
        private readonly IRepository<Resource> _resourceRepository;
        /// <summary>
        /// 角色管理服务
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="userRepository"></param>
        /// <param name="authorizationManager"></param>
        /// <param name="jwtBearerService"></param>
        /// <param name="resourceRepository"></param>
        /// <param name="identityPermissionService"></param>
        public AccountService(
            IHttpContextAccessor httpContextAccessor,
            IRepository<User> userRepository,
            Authorization.Core.IAuthorizationService authorizationManager,
            IJwtService jwtBearerService,
            IRepository<Resource> resourceRepository, 
            IIdentityPermissionService identityPermissionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _authorizationManager = authorizationManager;
            _jwtBearerService = jwtBearerService;
            _resourceRepository = resourceRepository;
            this._identityPermissionService = identityPermissionService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>登录接口</remarks>
        /// <returns></returns>
        [AllowAnonymous, IgnoreAudit]
        [VerifyCodeAutoVerification]
        public async Task<TokenOutput> Login(LoginInput input)
        {
            // 验证用户是否存在
            var user = _userRepository.FirstOrDefault(u => u.UserName.Equals(input.UserName) && u.IsDeleted == false, false) ?? throw Oops.Bah(ExceptionCode.USER_NAME_OR_PASSWORD_ERROR);
            if (user.IsLocked) throw Oops.Bah(ExceptionCode.USER_LOCKED);
            //密码是否正确
            var encryptedPassword = PasswordEncryptHelper.Encrypt(input.Password, user.PasswordEncryptKey);
            if (!encryptedPassword.Equals(user.Password))
            {
                throw Oops.Bah(ExceptionCode.USER_NAME_OR_PASSWORD_ERROR);
            }
            Identity identity = new Identity 
            {
                Id=user.Id.ToString(),
                LoginId=Guid.NewGuid().ToString(),
                LoginClientType=input.LoginClientType,
                IdentityType=IdentityType.User,
                Name=user.UserName,
                GivenName=user.NickName
            };

            var token = await _jwtBearerService.CreateToken(identity);
            // 设置 Swagger 刷新自动授权
            _httpContextAccessor.HttpContext.SigninToSwagger(token.AccessToken);
            return token.Adapt<TokenOutput>();
        }
        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <remarks>
        /// 通过刷新token获取新的token
        /// </remarks>
        /// <returns></returns>
        [AllowAnonymous, IgnoreAudit]
        public async Task<TokenOutput> RefreshToken(RefreshTokenInput input)
        {
            var token = await _jwtBearerService.RefreshToken(input.RefreshToken);
            return token.Adapt<TokenOutput>();
        }
        /// <summary>
        /// 移除当前用户token
        /// </summary>
        /// <remarks>
        /// 移除当前用户token
        /// </remarks>
        /// <returns></returns>
        public async Task<bool> RemoveCurrentUserRefreshToken()
        {
            var identity = _authorizationManager.GetIdentity();
            return await _jwtBearerService.RemoveRefreshToken(identity);
        }
        /// <summary>
        /// 查看用户角色
        /// </summary>
        /// <remarks>
        /// 查看当前用户角色
        /// </remarks>
        /// <returns></returns>
        public async Task<List<RoleDto>> GetCurrentUserRoles()
        {
            var userId = _authorizationManager.GetIdentityId();
            var roles = await _userRepository.AsQueryable(false).Where(x => x.Id.Equals(userId)).SelectMany(x => x.Roles.Where(r => r.IsDeleted == false && r.IsLocked == false)).ToListAsync();

            return roles.Adapt<List<RoleDto>>();
        }
        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <remarks>
        /// 获取当前用户信息
        /// </remarks>
        /// <returns></returns>
        public async Task<UserDto> GetCurrentUser()
        {
            var userId = _authorizationManager.GetIdentityId();
            var user = await _userRepository.FindAsync(userId);

            return user.Adapt<UserDto>();

        }
        /// <summary>
        /// 获取用户资源
        /// </summary>
        /// <param name="resourceTypes">资源类型</param>
        /// <returns></returns>
        public async Task<List<ResourceDto>> GetCurrentUserResources( [FromQuery] params ResourceType[] resourceTypes)
        {
            resourceTypes = resourceTypes ?? new ResourceType[] { };
            List<Resource> resources =await GetUserResources(resourceTypes);
            return resources.Adapt<List<ResourceDto>>();

        }
        /// <summary>
        /// 获取用户资源的key
        /// </summary>
        /// <param name="resourceTypes">资源类型</param>
        /// <returns></returns>
        public async Task<List<string>> GetCurrentUserResourceKeys([FromQuery] params ResourceType[] resourceTypes)
        {
            resourceTypes = resourceTypes ?? new ResourceType[] { };
            List<string> resourceKeys =await GetUserResourceKeys(resourceTypes);
            return resourceKeys;

        }
        
        /// <summary>
        /// 获取当前用户的所有菜单
        /// </summary>
        /// <remarks>
        /// 获取当前用户的所有菜单
        /// </remarks>
        /// <param name="rootKey"></param>
        /// <returns></returns>
        public async Task<List<ResourceDto>> GetCurrentUserMenus([FromQuery] string rootKey = null)
        {
            // 获取用户Id
            List<ResourceDto> resources = await GetCurrentUserResources(ResourceType.Root, ResourceType.Menu);

            if (resources == null) return new List<ResourceDto>();

            return resources.Where(x => x.Type.Equals(ResourceType.Root) && !string.IsNullOrEmpty(rootKey) && x.Key.Equals(rootKey)).FirstOrDefault()?.Children?.ToList();
        }

        #region 私有

        /// <summary>
        /// 判断是否是超级管理员
        /// </summary>
        /// <returns></returns>
        private async Task<bool> CurrentUserIsSuperAdmin()
        {
            List<RoleDto> roleDtos = await GetCurrentUserRoles();
            if (roleDtos.Any(x => x.IsSuperAdministrator))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取用户资源
        /// </summary>
        /// <param name="resourceTypes">资源类型</param>
        /// <returns></returns>
        private async Task<List<Resource>> GetUserResources(params ResourceType[] resourceTypes)
        {
            resourceTypes = resourceTypes ?? new ResourceType[] { };
            var userId = _authorizationManager.GetIdentityId();

            if (await CurrentUserIsSuperAdmin())
            {
                //超级管库有拥有所有资源
                return await _resourceRepository
                    .Where(x => x.IsDeleted == false && x.IsLocked == false && resourceTypes.Contains(x.Type))
                    .OrderBy(x => x.Order).ToListAsync();

            }
            return await _userRepository
                     .Include(u => u.Roles)
                         .ThenInclude(u => u.Resources)
                     .Where(u => u.Id.Equals(userId) && u.IsDeleted == false && u.IsLocked == false)
                     .SelectMany(u => u.Roles.Where(x => x.IsDeleted == false && x.IsLocked == false)
                         .SelectMany(u => u.Resources
                         .Where(x => x.IsDeleted == false && x.IsLocked == false && resourceTypes.Contains(x.Type))
                         )).OrderBy(x => x.Order).ToListAsync();
        }
        /// <summary>
        /// 获取用户资源所有Key
        /// </summary>
        /// <param name="resourceTypes">资源类型</param>
        /// <returns></returns>
        private async Task<List<string>> GetUserResourceKeys(params ResourceType[] resourceTypes)
        {
            resourceTypes = resourceTypes ?? new ResourceType[] { };
            var userId = _authorizationManager.GetIdentityId();

            if (await CurrentUserIsSuperAdmin())
            {
                //超级管库有拥有所有资源
                return await _resourceRepository
                    .Where(x => x.IsDeleted == false && x.IsLocked == false && resourceTypes.Contains(x.Type))
                    .OrderBy(x => x.Order)
                    .Select(x => x.Key)
                    .ToListAsync();

            }
            return await _userRepository
                     .Include(u => u.Roles)
                         .ThenInclude(u => u.Resources)
                     .Where(u => u.Id.Equals(userId) && u.IsDeleted == false && u.IsLocked == false)
                     .SelectMany(u => u.Roles.Where(x => x.IsDeleted == false && x.IsLocked == false)
                         .SelectMany(u => u.Resources
                         .Where(x => x.IsDeleted == false && x.IsLocked == false && resourceTypes.Contains(x.Type))
                         )).OrderBy(x => x.Order).Select(x => x.Key).ToListAsync();
        }

        #endregion
    }
}