// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Application.Dtos;
using Gardener.Core.Entites;
using Mapster;
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
using Gardener.Core;
using Gardener.Application.Interfaces;

namespace Gardener.Application
{
    /// <summary>
    /// 用户服务
    /// </summary>
    [ApiDescriptionSettings("UserCenterServices")]
    public class UserService : ApplicationServiceBase<User, UserDto>, IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<UserExtension> _userExtensionRepository;
        /// <summary>
        /// 用户服务
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="userExtensionRepository"></param>
        /// <param name="userRoleRepository"></param>
        /// <param name="roleRepository"></param>
        public UserService(
            IRepository<User> userRepository,
            IRepository<UserExtension> userExtensionRepository,
            IRepository<UserRole> userRoleRepository, 
            IRepository<Role> roleRepository) : base(userRepository)
        {
            _userRepository = userRepository;
            _userExtensionRepository = userExtensionRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// 查看用户角色
        /// </summary>
        /// <remarks>
        /// 查看用户角色
        /// </remarks>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<RoleDto>> GetRoles([ApiSeat(ApiSeats.ActionStart)] int userId)
        {
            var roles = await _userRepository
                .DetachedEntities
                .Include(u => u.Roles.Where(r => r.IsDeleted == false))
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Roles)
                .ToListAsync();

            return roles.Adapt<List<RoleDto>>();
        }

        /// <summary>
        /// 查看用户权限
        /// </summary>
        /// <remarks>
        /// 查看用户权限
        /// </remarks>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<ResourceDto>> GetResources([ApiSeat(ApiSeats.ActionStart)] int userId)
        {
            return await _userRepository
               .Include(u => u.Roles, false)
                   .ThenInclude(u => u.Resources)
               .Where(u => u.Id == userId)
               .Where(u => u.IsDeleted == false)
               .SelectMany(u => u.Roles
                   .SelectMany(u => u.Resources))
               .ProjectToType<ResourceDto>()
               .ToListAsync();
        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <remarks>
        /// 搜索用户数据
        /// </remarks>
        /// <param name="name"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Dtos.PagedList<UserDto>> Search([FromQuery] string name, int pageIndex = 1, int pageSize = 10)
        {
            var users = _userRepository
              .Include(u => u.UserExtension, false)
              .Include(u => u.Roles.Where(x => x.IsDeleted == false && x.IsLocked == false))
              .Where(u => u.IsDeleted == false)
              .Where(!string.IsNullOrEmpty(name), u => u.NickName.Contains(name) || u.NickName.Contains(name))
              .OrderByDescending(x => x.CreatedTime)
              .Select(u => u.Adapt<UserDto>());
            var pageList = await users.ToPagedListAsync(pageIndex, pageSize);
            foreach (var item in pageList.Items)
            {
                item.Password = null;
            }
            return pageList;

        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <remarks>
        /// 更新用户
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<bool> Update(UserDto input)
        {
            //不操作角色关系
            input.Roles = null;
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
            user.UserExtension = null;

            //更新
            await _userRepository.UpdateExcludeAsync(user,exclude);

            if (input.UserExtension != null)
            {
                var userExt = input.UserExtension.Adapt<UserExtension>();
                if (await _userExtensionRepository.AnyAsync(x => x.UserId == userExt.UserId, false))
                {
                    userExt.UpdatedTime = DateTimeOffset.Now;
                    await _userExtensionRepository.UpdateExcludeAsync(userExt, new[] { nameof(UserExtension.CreatedTime) });
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
        /// 新增
        /// </summary>
        /// <remarks>
        /// 新增用户
        /// </remarks>
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
            if (user.UserExtension == null)
            {
                user.UserExtension = new UserExtension();
            }
            user.UserExtension.CreatedTime = DateTimeOffset.Now;

            //查看是否有默认角色
            var defaultRoleIds = await _roleRepository.Where(x => x.IsDeleted == false && x.IsLocked == false && x.IsDefault == true).Select(x => x.Id).ToListAsync();
            if (defaultRoleIds != null && defaultRoleIds.Any())
            {
                user.UserRoles = defaultRoleIds.Select(x => new UserRole { RoleId = x, CreatedTime = DateTimeOffset.Now }).ToList();
            }
            var newEntity = await _userRepository.InsertAsync(user);

            return newEntity.Entity.Adapt<UserDto>();
        }

        /// <summary>
        /// 根据主键获取用户
        /// </summary>
        /// <remarks>
        /// 根据主键获取用户
        /// </remarks>
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
        /// 设置角色
        /// </summary>
        /// <remarks>
        /// 给用户设置角色
        /// </remarks>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Role([ApiSeat(ApiSeats.ActionStart)] int userId, int[] roleIds)
        {
            //先删除现有的
            var userRoles = await _userRoleRepository.AsQueryable(x => x.UserId == userId).ToListAsync();
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

        /// <summary>
        /// 更新头像
        /// </summary>
        /// <remarks>
        /// 更新用户的头像
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAvatar(UserUpdateAvatarInput input)
        {
            await _userRepository.UpdateIncludeAsync(new User {
                Id = input.Id, 
                Avatar = input.Avatar,
                UpdatedTime = DateTime.Now 
            }, new[] { 
                nameof(User.Avatar), 
                nameof(User.UpdatedTime) });
            return true;

        }

        
    }
}
