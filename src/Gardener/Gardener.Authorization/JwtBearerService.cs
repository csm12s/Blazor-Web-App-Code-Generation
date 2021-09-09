// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.Authorization;
using Furion.DatabaseAccessor;
using Furion.DataValidation;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Gardener.Authorization.Domain;
using Gardener.EntityFramwork;
using Gardener.Enums;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Authorization
{
    /// <summary>
    /// jwt
    /// </summary>
    public class JwtBearerService : IJwtBearerService, IScoped
    {
        private JWTSettingsOptions _jwtOptions;
        private JwtRefreshTokenSettings _jwtRefreshTokenOptions;
        IRepository<UserToken> _repository;
        /// <summary>
        /// 请求上下文访问器
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="jwtSettings"></param>
        /// <param name="jwtRefreshTokenOptions"></param>
        /// <param name="httpContextAccessor"></param>
        public JwtBearerService(IRepository<UserToken> repository, IOptions<JWTSettingsOptions> jwtSettings, IOptions<JwtRefreshTokenSettings> jwtRefreshTokenOptions, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _jwtOptions = jwtSettings.Value;
            _jwtRefreshTokenOptions = jwtRefreshTokenOptions.Value;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// 创建token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clientType"></param>
        /// <returns></returns>
        public Task<JsonWebToken> CreateToken(int userId, LoginClientType clientType = LoginClientType.Browser)
        {
            return CreateToken(userId, clientType, null);
        }

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="refreshTokenStr"></param>
        /// <returns></returns>
        public async Task<JsonWebToken> RefreshToken(string refreshTokenStr)
        {
            var (oldRefreshToken, principal) = ReadToken(refreshTokenStr, JwtTokenType.RefreshToken);

            UserToken refreshToken = _repository.AsQueryable(false).Where(x => x.IsDeleted == false && x.IsLocked==false && x.UserId == oldRefreshToken.UserId && x.ClientId.Equals(oldRefreshToken.ClientId)).OrderByDescending(x => x.EndTime).FirstOrDefault();

            //异常token检测
            if (refreshToken == null || refreshToken.Value != refreshTokenStr || refreshToken.EndTime <= DateTimeOffset.UtcNow)
            {
                //过期token删除
                if (refreshToken != null && refreshToken.EndTime <= DateTime.UtcNow)
                {
                    await _repository.FakeDeleteAsync(refreshToken);
                }
                throw Oops.Oh(ExceptionCode.REFRESHTOKEN_NO_EXIST_OR_EXPIRE);
            }
            LoginClientType? clientType = principal.Claims.FirstOrDefault(m => m.Type == AuthKeyConstants.ClientTypeKeyName)?.Value.Adapt<LoginClientType>() ?? LoginClientType.Browser;
            JsonWebToken token = await CreateToken(oldRefreshToken.UserId, clientType.Value, refreshToken);
            return token;
        }
        /// <summary>
        /// 创建token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clientType"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        private async Task<JsonWebToken> CreateToken(int userId, LoginClientType clientType, UserToken refreshToken)
        {
            // New RefreshToken
            string clientId = refreshToken?.ClientId ?? Guid.NewGuid().ToString();
            Claim[] claims =
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(AuthKeyConstants.ClientIdKeyName, clientId),
                new Claim(AuthKeyConstants.ClientTypeKeyName, clientType.ToString())
            };
            //创建刷新token
            var (newRefreshToken, refreshTokenExpires) = CreateToken(claims, JwtTokenType.RefreshToken, refreshToken);
            if (refreshToken == null)
            {
                //写入刷新token
                await _repository.InsertAsync(new UserToken()
                {
                    UserId = userId,
                    ClientId = clientId,
                    LoginClientType = clientType,
                    Value = newRefreshToken,
                    EndTime = refreshTokenExpires,
                    CreatedTime = DateTimeOffset.UtcNow
                });
            }
            else 
            {
                //更新token
                refreshToken.UpdatedTime = DateTimeOffset.Now;
                refreshToken.Value = newRefreshToken;
                refreshToken.EndTime = refreshTokenExpires;
                await _repository.UpdateAsync(refreshToken);
            }
            
            //New AccessToken
            var (newAccessToken, accessTokenExpires) = CreateToken(claims, JwtTokenType.AccessToken);

            return new JsonWebToken()
            {
                AccessToken = newAccessToken,
                AccessTokenExpires = accessTokenExpires.ToUnixTimeSeconds(),
                RefreshToken = newRefreshToken,
                RefreshTokenExpires = refreshTokenExpires.ToUnixTimeSeconds()
            };
        }
        /// <summary>
        /// 创建token
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="tokenType"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        private (string, DateTimeOffset) CreateToken(IEnumerable<Claim> claims, JwtTokenType tokenType, UserToken refreshToken = null)
        {
            DateTimeOffset expires;
            DateTimeOffset now = DateTimeOffset.Now;
            string issuerSigningKey = string.Empty;
            if (tokenType == JwtTokenType.AccessToken)
            {
                double minutes = _jwtOptions.ExpiredTime.HasValue ? _jwtOptions.ExpiredTime.Value : 5; //默认5分钟
                expires = now.AddMinutes(minutes);
                issuerSigningKey = _jwtOptions.IssuerSigningKey;
            }
            else
            {
                issuerSigningKey = _jwtRefreshTokenOptions.IssuerSigningKey;
                if (refreshToken == null || !_jwtRefreshTokenOptions.IsRefreshAbsoluteExpired)
                {
                    double minutes = _jwtRefreshTokenOptions.RefreshExpireMins > 0 ? _jwtRefreshTokenOptions.RefreshExpireMins : 10080; // 默认7天
                    expires = now.AddMinutes(minutes);
                }
                else
                    expires = refreshToken.EndTime;
            }

            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Audience = _jwtOptions.ValidAudience,
                Issuer = _jwtOptions.ValidIssuer,
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
        /// 移除当前用户的刷新token
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RemoveCurrentUserRefreshToken()
        {
            var user = _httpContextAccessor.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated) return false;

            string userIdStr = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr))
            {
                throw new ArgumentNullException(ClaimTypes.NameIdentifier);
            }
            userIdStr.Validate(ValidationTypes.Numeric);
            int userId = int.Parse(userIdStr);

            string clientId = user.FindFirstValue(AuthKeyConstants.ClientIdKeyName);
            if (string.IsNullOrEmpty(clientId))
            {
                throw new ArgumentNullException(AuthKeyConstants.ClientIdKeyName);
            }

            var refreshTokens = await _repository.AsQueryable(false).Where(x => x.IsDeleted == false && x.UserId == userId && x.ClientId.Equals(clientId)).ToListAsync();
            refreshTokens.ForEach(async x => await _repository.FakeDeleteAsync(x));
            return true;
        }

        /// <summary>
        /// 从token中读取数据
        /// </summary>
        /// <param name="tokenStr"></param>
        /// <param name="jwtTokenType"></param>
        /// <returns></returns>
        private (UserToken, ClaimsPrincipal) ReadToken(string tokenStr, JwtTokenType jwtTokenType)
        {
            TokenValidationParameters parameters = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidIssuer = _jwtOptions.ValidIssuer,
                ValidAudience = _jwtOptions.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenType.Equals(JwtTokenType.AccessToken) ? _jwtOptions.IssuerSigningKey : _jwtRefreshTokenOptions.IssuerSigningKey))
            };
            ClaimsPrincipal principal = _tokenHandler.ValidateToken(tokenStr, parameters, out _);
            string userIdStr = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr))
            {
                throw new ArgumentNullException(ClaimTypes.NameIdentifier);
            }
            userIdStr.Validate(ValidationTypes.Numeric);
            int userId = int.Parse(userIdStr);

            string clientId = principal.FindFirstValue(AuthKeyConstants.ClientIdKeyName);
            if (string.IsNullOrEmpty(clientId))
            {
                throw new ArgumentNullException(AuthKeyConstants.ClientIdKeyName);
            }
            return (new UserToken { ClientId = clientId, UserId = userId, Value = tokenStr }, principal);
        }
        /// <summary>
        /// token 类型
        /// </summary>
        private enum JwtTokenType
        {
            /// <summary>
            /// 
            /// </summary>
            AccessToken,
            /// <summary>
            /// 
            /// </summary>
            RefreshToken
        }
    }
}
