// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.FriendlyException;
using Gardener.Authorization.Domains;
using Gardener.Authorization.Dtos;
using Gardener.Authorization.Enums;
using Gardener.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Authorization.Core
{
    public class IdentityPermissionService : IIdentityPermissionService
    {
        /// <summary>
        /// 用户仓储
        /// </summary>
        private readonly IRepository<User> _userRepository;
        /// <summary>
        /// 用户仓储
        /// </summary>
        private readonly IRepository<Client> _clientRepository;

        public async Task<bool> Check(Identity identity, FunctionDto function)
        {
            if (IdentityType.User.Equals(identity.IdentityType))
            {
                return await CurrentUserHaveResource(int.Parse(identity.Id),function.Key);
            }else if (IdentityType.Client.Equals(identity.IdentityType))
            {
                return await CurrentClientHaveResource(identity.Id, function.Key);
            }
            return false;
        }

        /// <summary>
        /// 判断是否拥有该权限
        /// </summary>
        /// <param name="functionKey"></param>
        /// <returns></returns>
        private async Task<bool> CurrentUserHaveResource(int userId,string functionKey)
        {
            return await _userRepository.AsQueryable(false)
                    .Include(u => u.Roles)
                        .ThenInclude(u => u.Resources)
                            .ThenInclude(u => u.Functions)
                    .Where(u => u.Id == userId && u.IsDeleted == false && u.IsLocked == false)
                    .SelectMany(u => u.Roles.Where(x => x.IsDeleted == false && x.IsLocked == false)
                        .SelectMany(u => u.Resources.Where(x => x.IsDeleted == false && x.IsLocked == false)
                                .SelectMany(u => u.Functions.Where(x => x.IsDeleted == false && x.IsLocked == false && x.Key.Equals(functionKey)))
                            )
                        ).AnyAsync();
        }
        /// <summary>
        /// 判断是否拥有该权限
        /// </summary>
        /// <param name="functionKey"></param>
        /// <returns></returns>
        private async Task<bool> CurrentClientHaveResource(string clientId, string functionKey)
        {
            return await _clientRepository.AsQueryable(false)
                    .Include(u => u.Functions)
                    .Where(u => u.Id.Equals(clientId) && u.IsDeleted == false && u.IsLocked == false)
                    .SelectMany(u => u.Functions.Where(x => x.Key.Equals(functionKey) && x.IsDeleted == false && x.IsLocked == false)
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
    }
}
