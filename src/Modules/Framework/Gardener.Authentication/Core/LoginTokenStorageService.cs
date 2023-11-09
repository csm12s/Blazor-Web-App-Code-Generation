// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DatabaseAccessor;
using Gardener.Authentication.Domains;
using Gardener.Authentication.Dtos;
using Gardener.Base.Entity;
using Gardener.Common;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Authentication.Core
{
    /// <summary>
    /// 登录token存储服务
    /// </summary>
    public class LoginTokenStorageService : ILoginTokenStorageService
    {
        private readonly IRepository<LoginToken, GardenerMultiTenantDbContextLocator> _loginTokenRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginTokenRepository"></param>
        public LoginTokenStorageService(IRepository<LoginToken, GardenerMultiTenantDbContextLocator> loginTokenRepository)
        {
            _loginTokenRepository = loginTokenRepository;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> LogOut(Identity identity)
        {
            var refreshTokens = await _loginTokenRepository.AsQueryable(false).Where(x => x.IsDeleted == false && x.IsLocked == false && x.IdentityId.Equals(identity.Id) && x.IdentityType.Equals(identity.IdentityType) && x.LoginId.Equals(identity.LoginId)).ToListAsync();
            //标记为已登出
            await refreshTokens.ForEachAsync(async x =>
            {
                x.LoggedOut = true;
                await _loginTokenRepository.UpdateIncludeAsync(x, new[] { nameof(LoginToken.LoggedOut) });
            });

            return true;
        }
        /// <summary>
        /// 保存token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        public async Task<bool> Save(JsonWebToken token, Identity identity)
        {
            LoginToken loginToken = new LoginToken()
            {
                TenantId = identity.TenantId,
                IdentityId = identity.Id,
                IdentityName = identity.Name,
                IdentityNickName = identity.NickName,
                IdentityType = identity.IdentityType,
                LoginId = identity.LoginId,
                LoginClientType = identity.LoginClientType,
                Value = token.RefreshToken,
                EndTime = DateTimeOffset.FromUnixTimeSeconds(token.RefreshTokenExpires),
                CreatedTime = DateTimeOffset.Now,
                Ip = App.HttpContext?.GetRemoteIpAddressToIPv4()
            };
            await _loginTokenRepository.InsertAsync(loginToken);

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> Verify(Identity identity)
        {
            DateTimeOffset current = DateTimeOffset.Now;
            return _loginTokenRepository
                .AsQueryable(false)
                .Where(x =>
                x.IsDeleted == false &&
                x.IsLocked == false &&
                x.LoggedOut == false &&
                x.LoginId.Equals(identity.LoginId) &&
                x.IdentityId.Equals(identity.Id) &&
                x.IdentityType.Equals(identity.IdentityType) &&
                x.LoginClientType.Equals(identity.LoginClientType) &&
                x.EndTime > current)
                .AnyAsync();
        }

        /// <summary>
        /// 获取可用token
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Task<LoginTokenDto?> GetAvailableToken(Identity identity)
        {
            DateTimeOffset current = DateTimeOffset.Now;
            return _loginTokenRepository
                .AsQueryable(false)
                .Where(x =>
                x.IsDeleted == false &&
                x.IsLocked == false &&
                x.LoggedOut == false &&
                x.LoginId.Equals(identity.LoginId) &&
                x.IdentityId.Equals(identity.Id) &&
                x.IdentityType.Equals(identity.IdentityType) &&
                x.LoginClientType.Equals(identity.LoginClientType) &&
                x.EndTime > current)
                .Select(x => x.Adapt<LoginTokenDto>())
                .FirstOrDefaultAsync();
        }
        /// <summary>
        /// 更新现有token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> Update(LoginTokenDto token)
        {
            await _loginTokenRepository.UpdateIncludeAsync(token.Adapt<LoginToken>(), new[] { nameof(LoginToken.Value), nameof(LoginToken.EndTime), nameof(LoginToken.UpdatedTime) });
            return true;
        }
    }
}
