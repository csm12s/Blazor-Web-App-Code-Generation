using Gardener.Core;
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
using Furion.DataEncryption;
using Microsoft.AspNetCore.Mvc;
using Gardener.Enums;
using Gardener.Core.Entites;
using Gardener.Application.Dtos;
using System.Threading.Tasks;
using Gardener.Application.Dtos;

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
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<Resource> _resourceRepository;
        private readonly IAuthorizationManager _authorizationManager;

        /// <summary>
        /// 角色管理服务
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="userRepository"></param>
        /// <param name="securityRepository"></param>
        /// <param name="authorizationManager"></param>
        /// <param name="userRoleRepository"></param>
        public AuthorizeService(IHttpContextAccessor httpContextAccessor
            , IRepository<User> userRepository
            , IRepository<Resource> securityRepository
            , IAuthorizationManager authorizationManager
, IRepository<UserRole> userRoleRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _resourceRepository = securityRepository;
            _authorizationManager = authorizationManager;
            _userRoleRepository = userRoleRepository;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>管理员：admin/admin；普通用户：Furion/dotnetchina</remarks>
        /// <returns></returns>
        [AllowAnonymous]
        public LoginOutput Login(LoginInput input)
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

            var output = new LoginOutput()
            {
                UserId = user.Id,
                UserName = user.UserName,
                NickName = user.NickName
            };
            var token = CreateToken(user);
            output.AccessToken = token.AccessToken;
            output.AccessTokenExpiresIn = token.AccessTokenExpiresIn;
            // 设置 Swagger 刷新自动授权
            _httpContextAccessor.SigninToSwagger(output.AccessToken);
            return output;
        }

        /// <summary>
        /// 创建token
        /// </summary>
        /// <returns></returns>
        private TokenOutput CreateToken(User user)
        {
            var output = new TokenOutput();
            var tokenResult = _authorizationManager.CreateToken(user.Id, new Dictionary<string, object>() {
                { "UserName",user.UserName},
                { "NickName",user.NickName},
            });
            output.AccessToken = tokenResult.AccessToken;
            output.AccessTokenExpiresIn = tokenResult.ExpiresIn;
            return output;
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <returns></returns>
        public TokenOutput RefreshToken()
        {
            // 获取用户Id
            var userId = _authorizationManager.GetUserId();
            var user = _userRepository.FirstOrDefault(u => u.Id == userId && u.IsDeleted == false, false) ?? throw Oops.Oh(ExceptionCode.USER_NAME_OR_PASSWORD_ERROR);
            if (user.IsLocked) throw Oops.Oh(ExceptionCode.USER_LOCKED);
            var output = CreateToken(user);
            return output;
        }
        /// <summary>
        /// 查看用户角色
        /// </summary>
        public List<RoleDto> GetCurrentUserRoles()
        {
            // 获取用户Id
            var userId = _authorizationManager.GetUserId();

            var roles = _userRepository
                .DetachedEntities
                .Include(u => u.Roles)
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Roles)
                .ToList();

            return roles.Adapt<List<RoleDto>>();
        }
        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<UserDto> GetCurrentUser()
        {
            // 获取用户Id
            var userId = _authorizationManager.GetUserId();

            var user = await _userRepository.FindAsync(userId);

            return user.Adapt<UserDto>();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceTypes"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<ResourceDto>> GetCurrentUserResources(params ResourceType [] resourceTypes)
        {
            resourceTypes = resourceTypes ?? new ResourceType[] { };
            // 获取用户Id
            var userId = _authorizationManager.GetUserId();
            List<Resource> resources;
            //超级管理员
            if (_authorizationManager.IsSuperAdministrator())
            {
                resources = await _resourceRepository.Where(x => resourceTypes.Contains(x.Type)).ToListAsync();
            }
            else
            {
                resources= await _userRoleRepository
                    .Include(x=>x.Role)
                    .ThenInclude(x=>x.Resources)
                    .Where(x=>x.UserId==userId && x.Role.IsDeleted==false && x.Role.IsLocked==false)
                    .SelectMany(x=>x.Role.Resources.Where(x => x.IsDeleted == false && x.IsLocked == false && resourceTypes.Contains(x.Type)))
                    .ToListAsync();
                //其他角色
                //resources =await _userRepository
                //   .Include(u => u.Roles)
                //       .ThenInclude(u => u.Resources)
                //   .Where(u => u.Id == userId)
                //   .SelectMany(u => u.Roles.Where(x => x.IsDeleted == false && x.IsLocked == false)
                //       .SelectMany(u => u.Resources.Where(x => x.IsDeleted == false && x.IsLocked == false && resourceTypes.Contains(x.Type))))
                //   .ToListAsync();
            }
            return resources.Adapt<List<ResourceDto>>();
        }

        /// <summary>
        /// 获取当前用户的所有菜单
        /// </summary>
        /// <returns></returns>
        public async Task<List<ResourceDto>> GetCurrentUserMenus()
        {
            // 获取用户Id
            List<ResourceDto> resources=await GetCurrentUserResources(ResourceType.ROOT,ResourceType.MENU);

            if (resources == null) return new List<ResourceDto>();

            return resources.Where(x => x.Type.Equals(ResourceType.ROOT)).FirstOrDefault()?.Children?.ToList();
        }
    }
}