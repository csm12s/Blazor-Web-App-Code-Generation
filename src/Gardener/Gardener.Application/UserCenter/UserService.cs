// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
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
using System;
using System.Threading.Tasks;
using Gardener.Common;
using System.Linq.Expressions;
using Furion.FriendlyException;
using Gardener.Enums;

namespace Gardener.Application.UserCenter
{
    /// <summary>
    /// 用户服务
    /// </summary>
    [ApiDescriptionSettings("UserAuthorizationServices")]
    public class UserService : ServiceBase<User, UserDto>, IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<UserExtension> _userExtensionRepository;
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
        /// <param name="userExtensionRepository"></param>
        public UserService(
            IRepository<User> userRepository,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationManager authorizationManager,
            IRepository<RoleResource> roleResourceRepository,
            IRepository<Resource> resourceRepository,
            IRepository<UserExtension> userExtensionRepository, IRepository<UserRole> userRoleRepository, IRepository<Role> roleRepository) : base(userRepository)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _authorizationManager = authorizationManager;
            _roleResourceRepository = roleResourceRepository;
            _resourceRepository = resourceRepository;
            _userExtensionRepository = userExtensionRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
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
                .Include(u => u.Roles.Where(r=>r.IsDeleted==false))
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
              .Include(u=>u.UserExtension, false)
              .Include(u => u.Roles.Where(x=>x.IsDeleted==false && x.IsLocked==false))
              .Where(u => u.IsDeleted == false)
              .Where(!string.IsNullOrEmpty(name), u => u.NickName.Contains(name) || u.NickName.Contains(name))
              .OrderByDescending(x => x.CreatedTime)
              .Select(u => u.Adapt<UserDto>());
            var pageList= users.ToPagedList(pageIndex,pageSize);
            foreach (var item in pageList.Items) 
            {
                item.Password = null;
            }
            return pageList;

        }
        /// <summary>
        /// 更新一条
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<bool> Update(UserDto input)
        {
            var user = input.Adapt<User>();
            user.UpdatedTime = DateTimeOffset.Now;

            List<Expression<Func<User, object>>> exclude = new List<Expression<Func<User, object>>>() 
            { 
                x=>x.CreatedTime
            };
            //传入了密码就进行修改
            if (!string.IsNullOrEmpty(user.Password))
            {
                user.PasswordEncryptKey = Guid.NewGuid().ToString().Replace("-", "");
                user.Password = PasswordEncrypt.Encrypt(user.Password, user.PasswordEncryptKey);
            }
            else 
            {
                //不修改密码时要排除掉
                exclude.Add(x => x.Password);
                exclude.Add(x => x.PasswordEncryptKey);
            }
            //扩展移除
            //user.UserExtension = null;
            //更新
            await user.UpdateExcludeAsync(exclude);

            if (input.UserExtension != null)
            {
                var userExt = input.UserExtension.Adapt<UserExtension>();
                if (await _userExtensionRepository.AnyAsync(x => x.UserId == userExt.UserId,false))
                {
                    userExt.UpdatedTime = DateTimeOffset.Now;
                    await _userExtensionRepository.UpdateExcludeAsync(userExt, x => x.CreatedTime);
                }
                else 
                {
                     userExt.CreatedTime = DateTimeOffset.Now;
                    await _userExtensionRepository.InsertAsync(userExt);
                }
            }
            return true;
        }
        /// <summary>
        /// 新增一条
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<UserDto> Insert(UserDto input)
        {
            if (_userRepository.Any(x => x.UserName.Equals(input.UserName), false))
            {
                throw Oops.Oh(ExceptionCode.USER_NAME_REPEAT);
            }

            //未传入密码时，自动生成密码
            if (string.IsNullOrEmpty(input.Password))
            {
                input.Password = PasswordGenerate.Create(10);
            }
            User user = input.Adapt<User>();
            user.PasswordEncryptKey = Guid.NewGuid().ToString().Replace("-", "");
            user.Password = PasswordEncrypt.Encrypt(input.Password, user.PasswordEncryptKey);
            user.CreatedTime = DateTimeOffset.Now;
            if (user.UserExtension != null)
            {
                user.UserExtension.CreatedTime = DateTimeOffset.Now;
            }

            //查看是否有默认角色
            var defaultRoleIds = await _roleRepository.Where(x => x.IsDeleted == false && x.IsLocked == false && x.IsDefault == true).Select(x => x.Id).ToListAsync();
            if (defaultRoleIds!=null && defaultRoleIds.Any())
            {
                user.UserRoles = defaultRoleIds.Select(x => new UserRole { RoleId = x, CreatedTime = DateTimeOffset.Now }).ToList();
            }
            var newEntity = await _userRepository.InsertNowAsync(user);

            return newEntity.Entity.Adapt<UserDto>();
        }

        /// <summary>
        /// 查询一条
        /// </summary>
        /// <param name="id"></param>
        public override async Task<UserDto> Get(int id)
        {
            var person = await _userRepository
                .Include(x => x.UserExtension, false)
                .Include(x => x.Roles.Where(r => r.IsDeleted == false))
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            return person.Adapt<UserDto>();
        }

       /// <summary>
       /// 设置用户角色
       /// </summary>
       /// <param name="userId"></param>
       /// <param name="roleIds"></param>
       /// <returns></returns>
       [HttpPost]
        public async Task<bool> Role([ApiSeat(ApiSeats.ActionStart)] int userId,int [] roleIds)
        {
            //先删除现有的
            var userRoles= await _userRoleRepository.AsQueryable(x => x.UserId == userId).ToListAsync();
            if (userRoles.Count > 0)
            {
              await _userRoleRepository.DeleteAsync(userRoles);
            }
            if (roleIds?.Length > 0)
            {
                //添加新的
                var newUserRoles = roleIds.Select(x => new UserRole()
                {

                    UserId = userId,
                    RoleId = x,
                    CreatedTime = DateTimeOffset.Now
                });
                await _userRoleRepository.InsertAsync(newUserRoles);
            }
            return true;
        }
    }
}
