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
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Gardener.Enums;
using Gardener.Core.Entites;
using Gardener.Application.Dtos;
using System.Threading.Tasks;
using Gardener.Core;
using Gardener.Application.Interfaces;

namespace Gardener.Application
{
    /// <summary>
    /// 用户中心服务
    /// </summary>
    [ApiDescriptionSettings("UserAuthorizationServices")]
    public class AuthorizeService : IDynamicApiController, IAuthorizeService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<User> _userRepository;
        private readonly IAuthorizationManager _authorizationManager;
        private readonly IJwtBearerService _jwtBearerService;
        private readonly IRepository<Resource> _resourceRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        /// <summary>
        /// 角色管理服务
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="userRepository"></param>
        /// <param name="authorizationManager"></param>
        /// <param name="jwtBearerService"></param>
        public AuthorizeService(
            IHttpContextAccessor httpContextAccessor,
            IRepository<User> userRepository,
            IAuthorizationManager authorizationManager,
            IJwtBearerService jwtBearerService
, IRepository<Resource> resourceRepository, IRepository<UserRole> userRoleRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _authorizationManager = authorizationManager;
            _jwtBearerService = jwtBearerService;
            _resourceRepository = resourceRepository;
            _userRoleRepository = userRoleRepository;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>管理员：admin/admin；普通用户：testuser/testuser</remarks>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<TokenOutput> Login(LoginInput input)
        {
            // 验证用户是否存在
            var user = _userRepository.FirstOrDefault(u => u.UserName.Equals(input.UserName) && u.IsDeleted == false, false) ?? throw Oops.Oh(ExceptionCode.USER_NAME_OR_PASSWORD_ERROR);
            if (user.IsLocked) throw Oops.Oh(ExceptionCode.USER_LOCKED);
            //密码是否正确
            var encryptedPassword = PasswordEncrypt.Encrypt(input.Password, user.PasswordEncryptKey);
            if (!encryptedPassword.Equals(user.Password))
            {
                throw Oops.Oh(ExceptionCode.USER_NAME_OR_PASSWORD_ERROR);
            }
            var token = await _jwtBearerService.CreateToken(user.Id, input.LoginClientType);
            // 设置 Swagger 刷新自动授权
            _httpContextAccessor.SigninToSwagger(token.AccessToken);
            return token.Adapt<TokenOutput>();
        }
        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<TokenOutput> RefreshToken(RefreshTokenInput input)
        {
            var token = await _jwtBearerService.RefreshToken(input.RefreshToken);
            return token.Adapt<TokenOutput>();
        }
        /// <summary>
        /// 移除当前用户token
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RemoveCurrentUserRefreshToken()
        {
            return await _jwtBearerService.RemoveCurrentUserRefreshToken();
        }
        /// <summary>
        /// 查看用户角色
        /// </summary>
        public async Task<List<RoleDto>> GetCurrentUserRoles()
        {
            var user = _authorizationManager.GetUser();

            var roles = user.Roles;

            return roles.Adapt<List<RoleDto>>();
        }
        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<UserDto> GetCurrentUser()
        {
            var user = _authorizationManager.GetUser();

            return user.Adapt<UserDto>();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceTypes"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<ResourceDto>> GetCurrentUserResources(params ResourceType[] resourceTypes)
        {
            resourceTypes = resourceTypes ?? new ResourceType[] { };
            // 获取用户Id
            var userId = _authorizationManager.GetUserId();
            List<Resource> resources =await _authorizationManager.GetUserResources().Where(x => resourceTypes.Contains(x.Type)).OrderBy(x=>x.Order).ToListAsync();
            return resources.Adapt<List<ResourceDto>>();

        }

        /// <summary>
        /// 获取当前用户的所有菜单
        /// </summary>
        /// <returns></returns>
        public async Task<List<ResourceDto>> GetCurrentUserMenus()
        {
            // 获取用户Id
            List<ResourceDto> resources = await GetCurrentUserResources(ResourceType.Root, ResourceType.Menu);

            if (resources == null) return new List<ResourceDto>();

            return resources.Where(x => x.Type.Equals(ResourceType.Root)).FirstOrDefault()?.Children?.ToList();
        }
    }
}