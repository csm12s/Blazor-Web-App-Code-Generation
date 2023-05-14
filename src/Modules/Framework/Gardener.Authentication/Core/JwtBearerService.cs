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
using Gardener.Authentication.Options;
using Gardener.Base;
using Gardener.Enums;
using Gardener.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Furion;
using Microsoft.AspNetCore.Http;

namespace Gardener.Authentication.Core
{
    /// <summary>
    /// JwtBearer服务
    /// </summary>
    public class JwtBearerService : IJwtService
    {
        private readonly JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();

        private readonly JWTOptions jWTOptions;

        private readonly IIdentityConverter identityConverter;

        /// <summary>
        /// 初始化声明
        /// </summary>
        /// <param name="jWTOptions"></param>
        /// <param name="identityConverter"></param>
        public JwtBearerService(IOptions<JWTOptions> jWTOptions, IIdentityConverter identityConverter)
        {
            this.jWTOptions = jWTOptions.Value;
            this.identityConverter = identityConverter;
        }

        /// <summary>
        /// 创建token
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public async Task<JsonWebToken> CreateToken(Identity identity)
        {
            // New Token
            var (accessToken, accessTokenExpires) = CreateToken(identity, JwtTokenType.AccessToken);
            var (refreshToken, refreshTokenExpires) = CreateToken(identity, JwtTokenType.RefreshToken);
            //存储refreshToken
            //写入刷新token
            await Db.GetRepository<LoginToken>().InsertAsync(new LoginToken()
            {
                TenantId = identity.TenantId,
                IdentityId = identity.Id,
                IdentityName = identity.Name,
                IdentityNickName = identity.NickName,
                IdentityType = identity.IdentityType,
                LoginId = identity.LoginId,
                LoginClientType = identity.LoginClientType,
                Value = refreshToken,
                EndTime = refreshTokenExpires,
                CreatedTime = DateTimeOffset.Now,
                Ip = App.HttpContext?.GetRemoteIpAddressToIPv4()

            });
            return new JsonWebToken()
            {
                AccessToken = accessToken,
                AccessTokenExpires = accessTokenExpires.ToUnixTimeSeconds(),
                RefreshToken = refreshToken,
                RefreshTokenExpires = refreshTokenExpires.ToUnixTimeSeconds()
            };
        }

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="oldRefreshToken"></param>
        /// <returns></returns>
        public async Task<JsonWebToken> RefreshToken(string oldRefreshToken)
        {
            Identity identity = ReadToken(oldRefreshToken);
            IRepository<LoginToken> repository = Db.GetRepository<LoginToken>();

            LoginToken? loginToken = repository.AsQueryable(false).Where(x =>
            x.IsDeleted == false
            && x.IsLocked == false
            && x.IdentityId.Equals(identity.Id)
            && x.IdentityType.Equals(identity.IdentityType)
            && x.LoginId.Equals(identity.LoginId)).OrderByDescending(x => x.EndTime).FirstOrDefault();

            //异常token检测
            if (loginToken == null || loginToken.Value != oldRefreshToken || loginToken.EndTime <= DateTimeOffset.Now)
            {
                //token删除
                if (loginToken != null)
                {
                    await repository.FakeDeleteNowByKeyAsync(loginToken.Id);
                }
                throw Oops.Oh(ExceptionCode.REFRESHTOKEN_NO_EXIST_OR_EXPIRE);
            }
            var jwtOpt = GetJWTSettingsOptions(identity.IdentityType);
            //设置非绝对过期，生成新的刷新token
            if (!jwtOpt.IsRefreshAbsoluteExpired)
            {
                var (refreshToken, refreshTokenExpires) = CreateToken(identity, JwtTokenType.RefreshToken);
                //更新刷新token
                loginToken.Value = refreshToken;
                loginToken.EndTime = refreshTokenExpires;
                loginToken.UpdatedTime = DateTimeOffset.Now;
                await repository.UpdateIncludeAsync(loginToken, new string[] { nameof(LoginToken.Value), nameof(LoginToken.EndTime), nameof(LoginToken.UpdatedTime) });
            }
            // New Token
            var (accessToken, accessTokenExpires) = CreateToken(identity, JwtTokenType.AccessToken);
            return new JsonWebToken()
            {
                AccessToken = accessToken,
                AccessTokenExpires = accessTokenExpires.ToUnixTimeSeconds(),
                RefreshToken = loginToken.Value,
                RefreshTokenExpires = loginToken.EndTime.ToUnixTimeSeconds()
            };
        }

        /// <summary>
        /// 移除token自动刷新
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public async Task<bool> RemoveRefreshToken(Identity identity)
        {
            IRepository<LoginToken> repository = Db.GetRepository<LoginToken>();
            var refreshTokens = await repository.AsQueryable(false).Where(x => x.IsDeleted == false && x.IsLocked == false && x.IdentityId.Equals(identity.Id) && x.IdentityType.Equals(identity.IdentityType) && x.LoginId.Equals(identity.LoginId)).ToListAsync();
            await refreshTokens.ForEachAsync(async x => await repository.FakeDeleteByKeyAsync(x.Id));

            return true;
        }
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="jwtTokenType"></param>
        /// <returns></returns>
        private (string, DateTimeOffset) CreateToken(Identity identity, JwtTokenType jwtTokenType)
        {
            ClaimsIdentity claimsIdentity = identityConverter.IdentityToClaimsIdentity(identity, jwtTokenType);
            JWTSettingsOptions jWTSettings = GetJWTSettingsOptions(identity.IdentityType);
            return CreateToken(claimsIdentity, jWTSettings, jwtTokenType);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identityType"></param>
        /// <returns></returns>
        private JWTSettingsOptions GetJWTSettingsOptions(IdentityType identityType)
        {
            if (jWTOptions.Settings != null && jWTOptions.Settings.ContainsKey(identityType))
            {
                JWTSettingsOptions jWTSettings = jWTOptions.Settings[identityType];
                return jWTSettings;
            }
            else
            {
                throw Oops.Oh(identityType + " JWTSettings is no find");
            }
        }
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="claimsIdentity"></param>
        /// <param name="jwtOpt"></param>
        /// <param name="jwtTokenType"></param>
        /// <returns></returns>
        private (string, DateTimeOffset) CreateToken(ClaimsIdentity claimsIdentity, JWTSettingsOptions jwtOpt, JwtTokenType jwtTokenType)
        {
            DateTimeOffset expires;
            DateTimeOffset now = DateTimeOffset.Now;
            string issuerSigningKey = jwtOpt.IssuerSigningKey;

            if (jwtTokenType.Equals(JwtTokenType.RefreshToken))
            {
                expires = now.AddMinutes(jwtOpt.RefreshExpireMins);
            }
            else
            {
                //默认5分钟
                double minutes = jwtOpt.ExpiredTime.HasValue ? jwtOpt.ExpiredTime.Value : 5;
                expires = now.AddMinutes(minutes);
            }
            SecurityKey key = new SymmetricSecurityKey(Convert.FromBase64String(issuerSigningKey));
            SigningCredentials credentials = new SigningCredentials(key, jwtOpt.Algorithm);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
            {
                Subject = claimsIdentity,
                Audience = jwtOpt.ValidAudience,
                Issuer = jwtOpt.ValidIssuer,
                SigningCredentials = credentials,
                NotBefore = now.DateTime,
                IssuedAt = now.DateTime,
                Expires = expires.DateTime
            };
            SecurityToken token = _tokenHandler.CreateToken(descriptor);
            string accessToken = _tokenHandler.WriteToken(token);
            return (accessToken, expires);
        }

        /// <summary>
        /// 从token中读取数据
        /// </summary>
        /// <param name="tokenStr"></param>
        /// <returns></returns>
        private Identity ReadToken(string tokenStr)
        {

            JwtSecurityToken jwtSecurityToken = _tokenHandler.ReadJwtToken(tokenStr);
            Claim? identityTypeCla = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type.Equals(AuthKeyConstants.IdentityType));
            if (identityTypeCla == null)
            {
                throw Oops.Oh(ExceptionCode.TOKEN_INVALID);
            }
            IdentityType identityType = Enum.Parse<IdentityType>(identityTypeCla.Value);
            JWTSettingsOptions jWTSettingsOptions = GetJWTSettingsOptions(identityType);

            TokenValidationParameters parameters = new TokenValidationParameters()
            {
                ValidateLifetime = jWTSettingsOptions.ValidateLifetime,
                ValidIssuer = jWTSettingsOptions.ValidIssuer,
                ValidAudience = jWTSettingsOptions.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(jWTSettingsOptions.IssuerSigningKey))
            };
            ClaimsPrincipal principal = _tokenHandler.ValidateToken(tokenStr, parameters, out _);

            Identity? identity = identityConverter.ClaimsPrincipalToIdentity(principal);
            if (identity == null)
            {
                throw Oops.Oh(ExceptionCode.TOKEN_INVALID);
            }
            identity.IdentityType = identityType;
            return identity;
        }



    }

}
