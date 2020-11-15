using Fur.Authorization;
using Gardener.Core;
using Fur.DatabaseAccessor;
using Fur.DynamicApiController;
using Fur.FriendlyException;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Fur;
using Fur.DataEncryption;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Controllers;
using Gardener.Core.Enums;
using Microsoft.Extensions.Options;

namespace Gardener.Application
{
    /// <summary>
    /// 用户中心服务
    /// </summary>
    [AppAuthorize, ApiDescriptionSettings("UserAuthorizationServices")]
    public class UserCenterService : IDynamicApiController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<RoleResource> _roleSecurityRepository;
        private readonly IRepository<Resource> _securityRepository;
        private readonly IAuthorizationManager _authorizationManager;
        private readonly SystemOptions systemOptions;

        /// <summary>
        /// 角色管理服务
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="userRepository"></param>
        /// <param name="roleRepository"></param>
        /// <param name="userRoleRepository"></param>
        /// <param name="roleSecurityRepository"></param>
        /// <param name="securityRepository"></param>
        /// <param name="authorizationManager"></param>
        /// <param name="systemOptions"></param>
        public UserCenterService(IHttpContextAccessor httpContextAccessor
            , IRepository<User> userRepository
            , IRepository<Role> roleRepository
            , IRepository<UserRole> userRoleRepository
            , IRepository<RoleResource> roleSecurityRepository
            , IRepository<Resource> securityRepository
            , IAuthorizationManager authorizationManager
            , IOptions<SystemOptions>  systemOptions)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _roleSecurityRepository = roleSecurityRepository;
            _securityRepository = securityRepository;
            _authorizationManager = authorizationManager;
            this.systemOptions = systemOptions.Value;
        }

        /// <summary>
        /// 登录（免授权）
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>管理员：admin/admin；普通用户：Fur/dotnetchina</remarks>
        /// <returns></returns>
        [AllowAnonymous, IfException(1000, ErrorMessage = "用户名或密码错误")]
        public LoginOutput Login(LoginInput input)
        {
            // 验证用户名和密码
            var user = _userRepository.FirstOrDefault(u => u.UserName.Equals(input.UserName) && u.Password.Equals(MD5Encryption.Encrypt(systemOptions.PasswordEncryptKey+ input.Password)), false) ?? throw Oops.Oh(1000);

            var output = user.Adapt<LoginOutput>();

            // 生成 token
            var jwtSettings = App.GetOptions<JWTSettingsOptions>();
            var datetimeOffset = DateTimeOffset.UtcNow;

            output.AccessToken = JWTEncryption.Encrypt(jwtSettings.IssuerSigningKey, new Dictionary<string, object>()
            {
                { "UserId", user.Id },  // 存储Id
                { "Account",user.UserName }, // 存储用户名
                { JwtRegisteredClaimNames.Iat, datetimeOffset.ToUnixTimeSeconds() },
                { JwtRegisteredClaimNames.Nbf, datetimeOffset.ToUnixTimeSeconds() },
                { JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddSeconds(jwtSettings.ExpiredTime.Value*60).ToUnixTimeSeconds() },
                { JwtRegisteredClaimNames.Iss, jwtSettings.ValidIssuer},
                { JwtRegisteredClaimNames.Aud, jwtSettings.ValidAudience }
            });

            // 设置 Swagger 刷新自动授权
            _httpContextAccessor.SigninToSwagger(output.AccessToken);

            return output;
        }

        /// <summary>
        /// 查看用户角色
        /// </summary>
        [ApiSecurityDefine("查看用户角色")]
        public List<RoleDto> ViewRoles()
        {
            // 获取用户Id
            var userId = _authorizationManager.GetUserId<int>();

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
        [ApiSecurityDefine("查看用户权限")]
        public List<Resource> ViewSecuries()
        {
            // 获取用户Id
            var userId = _authorizationManager.GetUserId<int>();
            List<Resource> securities;
            //超级管理员
            if (_authorizationManager.IsSuperAdministrator())
            {
                securities = _securityRepository.AsEnumerable(false);
            }
            else
            {
                //其他角色
                securities = _userRepository
                   .Include(u => u.Roles, false)
                       .ThenInclude(u => u.Resources)
                   .Where(u => u.Id == userId)
                   .SelectMany(u => u.Roles
                       .SelectMany(u => u.Resources))
                   .ToList();
            }
            return securities;
        }

        /// <summary>
        /// 角色列表
        /// </summary>
        [ApiSecurityDefine("查看所有角色")]
        public List<RoleDto> GetRoles()
        {
            return _roleRepository.AsEnumerable(false).Adapt<List<RoleDto>>();
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        [ApiSecurityDefine("新增角色")]
        public void InsertRole(RoleInput input)
        {
            _roleRepository.Insert(input.Adapt<Role>());
        }

        /// <summary>
        /// 为用户分配角色
        /// </summary>
        [ApiSecurityDefine("为用户分配角色")]
        public void GiveUserRole(int[] roleIds)
        {
            // 获取用户Id
            var userId = _authorizationManager.GetUserId<int>();

            roleIds ??= Array.Empty<int>();
            _userRoleRepository.Delete(_userRoleRepository.Where(u => u.UserId == userId, false).ToList());

            var list = new List<UserRole>();
            foreach (var roleid in roleIds)
            {
                list.Add(new UserRole { UserId = userId, RoleId = roleid, CreatedTime = DateTimeOffset.Now });
            }

            _userRoleRepository.Insert(list);
        }

        /// <summary>
        /// 查看系统所有的权限
        /// </summary>
        [ApiSecurityDefine("查看系统所有的权限")]
        public List<Resource> GetSecurities()
        {
            return _securityRepository.AsEnumerable(false);
        }

        /// <summary>
        /// 为角色分配权限
        /// </summary>
        [ApiSecurityDefine("为角色分配权限")]
        public void GiveRoleSecurity(int roleId, int[] securityIds)
        {
            securityIds ??= Array.Empty<int>();
            _roleSecurityRepository.Delete(_roleSecurityRepository.Where(u => u.RoleId == roleId, false).ToList());

            var list = new List<RoleResource>();
            foreach (var securityId in securityIds)
            {
                list.Add(new RoleResource { RoleId = roleId, ResourceId = securityId, CreatedTime = DateTimeOffset.Now });
            }

            _roleSecurityRepository.Insert(list);
        }
        /// <summary>
        /// 初始化系统挨批资源权限
        /// </summary>
        /// <returns></returns>
        [ApiSecurityDefine("初始化资源")]
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
    }
}