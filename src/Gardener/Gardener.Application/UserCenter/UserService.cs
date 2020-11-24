// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Core.Dtos;
using Gardener.Core;
using Gardener.Core.Entites;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Gardener.Common;

namespace Gardener.Application.UserCenter
{
    /// <summary>
    /// 用户服务
    /// </summary>
    [AppAuthorize, ApiDescriptionSettings("UserAuthorizationServices")]
    public class UserService : ServiceBase<User, UserDto>, IUserService
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
        public List<ResourceDto> GetResources([ApiSeat(ApiSeats.ActionStart)] int userId)
        {
            List<ResourceDto> resources = _userRepository
               .Include(u => u.Roles, false)
                   .ThenInclude(u => u.Resources)
               .Where(_authorizationManager.IsSuperAdministrator(), u => u.Id == userId)
               .Where(u=>u.IsDeleted==false)
               .SelectMany(u => u.Roles
                   .SelectMany(u => u.Resources))
               .ProjectToType<ResourceDto>()
               .ToList();
            return resources;
        }
        /// <summary>
        /// 搜索用户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedList<UserDto> Search([FromQuery] string name,  int pageIndex = 1,int pageSize = 10)
        {
            var users = _userRepository
              .Include(u=>u.UserExtension, false).Include(u => u.Roles)
              .Where(u => u.IsDeleted == false)
              .Where(!string.IsNullOrEmpty(name), u => u.NickName.Contains(name) || u.NickName.Contains(name))
              .OrderByDescending(x => x.CreatedTime)
              .Select(u => u.Adapt<UserDto>());
            return users.ToPagedList(pageIndex,pageSize);
        }
        /// <summary>
        /// 更新一条
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<bool> Update(UserDto input)
        {
            input.UpdatedTime = DateTimeOffset.Now;
            // 更新 排除创建时间 和 密码
            await input.Adapt<User>().UpdateExcludeAsync(
                    x => x.CreatedTime,
                    x => x.Password,
                    x => x.PasswordEncryptKey
                );
            return true;
        }
        /// <summary>
        /// 新增一条
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<UserDto> Insert(UserDto input)
        {
            //未传入密码时，自动生成密码
            if (string.IsNullOrEmpty(input.Password))
            {
                input.Password = PasswordGenerate.Create(10);
            }
            User user = input.Adapt<User>();
            user.PasswordEncryptKey = Guid.NewGuid().ToString();
            user.Password = PasswordEncrypt.Encrypt(input.Password, user.PasswordEncryptKey);
            user.CreatedTime = DateTimeOffset.Now;
            var newEntity = await _userRepository.InsertNowAsync(user);
            return newEntity.Entity.Adapt<UserDto>();
        }
    }
}
