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
    [AppAuthorize, ApiDescriptionSettings("UserAuthorizationServices")]
    public class AuthorizeService : IDynamicApiController, IAuthorizeService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Resource> _securityRepository;
        private readonly IAuthorizationManager _authorizationManager;

        /// <summary>
        /// 角色管理服务
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="userRepository"></param>
        /// <param name="securityRepository"></param>
        /// <param name="authorizationManager"></param>
        public AuthorizeService(IHttpContextAccessor httpContextAccessor
            , IRepository<User> userRepository
            , IRepository<Resource> securityRepository
            , IAuthorizationManager authorizationManager
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _securityRepository = securityRepository;
            _authorizationManager = authorizationManager;
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
        /// 查看用户权限
        /// </summary>
        /// <returns></returns>
        public List<ResourceDto> GetCurrentUserResources()
        {
            // 获取用户Id
            var userId = _authorizationManager.GetUserId();
            List<Resource> resources;
            //超级管理员
            if (_authorizationManager.IsSuperAdministrator())
            {
                resources = _securityRepository.AsEnumerable(false);
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
            return resources.Adapt<List<ResourceDto>>();
        }

        /// <summary>
        /// 初始化系统挨批资源权限
        /// </summary>
        /// <returns></returns>
        public bool InitResource()
        {
            IRepository<Resource> repository = Db.GetRepository<Resource>();
            if (!repository.Where(x => x.Type.Equals(ResourceType.API)).Any())
            {
                List<Resource> apiResources = MyApplicationContext.GetApiResources();
                apiResources.ForEach(x => x.CreatedTime = DateTimeOffset.Now);
                repository.InsertNow(apiResources);
            }
            return true;
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
    }
}