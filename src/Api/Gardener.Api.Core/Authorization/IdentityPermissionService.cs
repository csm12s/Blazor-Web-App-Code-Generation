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
using Gardener.Enums;
using Gardener.UserCenter.Impl.Domains;
using Gardener.Base.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Authorization.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class IdentityPermissionService : IIdentityPermissionService
    {

        /// <summary>
        /// 判断审核是否用于api访问权限
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="api"></param>
        /// <returns></returns>
        public async Task<bool> Check(Identity identity, ApiEndpoint api)
        {
            if (api == null)
            {
                return true;
            }
            if (identity == null) 
            {
                return false;
            }
            
            if (IdentityType.User.Equals(identity.IdentityType))
            {
                if (await IsSuperAdministrator(GetUserId(identity)))
                {
                    return true;
                }
                return await CurrentUserHaveResource(int.Parse(identity.Id), api.Key);
            }else if (IdentityType.Client.Equals(identity.IdentityType))
            {
                return await CurrentClientHaveResource(Guid.Parse(identity.Id), api.Key);
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<bool> IsSuperAdministrator(int userId) 
        {
            IRepository<User> _userRepository = Db.GetRepository<User>();
            //超管
            if (await _userRepository.AsQueryable(false)
                 .Include(u => u.Roles)
                 .Where(u => u.Id == userId && u.IsDeleted == false && u.IsLocked == false)
                 .SelectMany(u => u.Roles.Where(x => x.IsDeleted == false && x.IsLocked == false && x.IsSuperAdministrator == true))
                 .AnyAsync())
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 判断是否拥有该权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="functionKey"></param>
        /// <returns></returns>
        private async Task<bool> CurrentUserHaveResource(int userId,string functionKey)
        {
            IRepository<User> _userRepository = Db.GetRepository<User>();
            return await _userRepository.AsQueryable(false)
                    .Include(u => u.Roles)
                        .ThenInclude(u => u.RoleResources)
                            .ThenInclude(u => u.Resource)
                                .ThenInclude(u => u.ResourceFunctions)
                                    .ThenInclude(u=>u.Function)
                    .Where(u => u.Id == userId && u.IsDeleted == false && u.IsLocked == false)
                    .SelectMany(u => u.Roles.Where(x => x.IsDeleted == false && x.IsLocked == false)
                        .SelectMany(u => u.RoleResources.Select(u=>u.Resource).Where(x => x.IsDeleted == false && x.IsLocked == false)
                                .SelectMany(u => u.ResourceFunctions.Select(u=>u.Function).Where(x => x.IsDeleted == false && x.IsLocked == false && x.Key.Equals(functionKey)))
                            )
                        ).AnyAsync();
        }
        /// <summary>
        /// 判断是否拥有该权限
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="functionKey"></param>
        /// <returns></returns>
        private async Task<bool> CurrentClientHaveResource(Guid clientId, string functionKey)
        {
            IRepository<Client> _clientRepository = Db.GetRepository<Client>();

            return await _clientRepository.AsQueryable(false)
                    .Include(u => u.ClientFunctions)
                    .ThenInclude(u=>u.Function)
                    .Where(u => u.Id.Equals(clientId) && u.IsDeleted == false && u.IsLocked == false)
                    .SelectMany(u => u.ClientFunctions.Select(u=>u.Function).Where(x => x.Key.Equals(functionKey) && x.IsDeleted == false && x.IsLocked == false)
                        ).AnyAsync();
        }

        /// <summary>
        /// 
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
            throw Oops.Oh(ExceptionCode.UNAUTHORIZED);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        private int GetUserId(Identity identity)
        {
            if (!IdentityType.User.Equals(identity.IdentityType))
            {
                throw Oops.Oh(ExceptionCode.UNAUTHORIZED);
            }
            int userId = int.Parse(identity.Id);

            return userId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        private Guid GetClientId(Identity identity)
        {
            if (!IdentityType.Client.Equals(identity.IdentityType))
            {
                throw Oops.Oh(ExceptionCode.UNAUTHORIZED);
            }
            Guid clientId = Guid.Parse(identity.Id);

            return clientId;
        }

        /// <summary>
        /// 检测loginId是否可用
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public async Task<bool> CheckLoginIdUsable(string loginId)
        {
            return await Db.GetRepository<LoginToken>().AsQueryable(false).Where(x => x.IsDeleted == false && x.IsLocked == false && x.LoginId.Equals(loginId)).AnyAsync();
        }

    }
}
