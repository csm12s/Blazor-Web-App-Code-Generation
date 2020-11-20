using Gardener.Core.Security;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Gardener.Core.Security
{
    /// <summary>
    /// JWT 加解密
    /// </summary>
    public class JWTHelper
    {
        /// <summary>
        /// 获取基于JWT的Token
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static SecurityTokenResult BuildJwtToken(JWTSettingsOptions options, Dictionary<string, object> payload)
        {
            List<Claim> claims = new List<Claim>();
            foreach (var item in payload)
            {
                claims.Add(new Claim(item.Key, item.Value.ToString()));
            }
            return BuildJwtToken(options, claims);
        }
        /// <summary>
        /// 获取基于JWT的Token
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static SecurityTokenResult BuildJwtToken(JWTSettingsOptions options, List<Claim> claims)
        {
            

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.IssuerSigningKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: options.ValidIssuer,
                audience: options.ValidAudience,
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(options.ExpiredTime.Value),
                signingCredentials: credentials
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new SecurityTokenResult()
            {
                Status = true,
                AccessToken = encodedJwt,
                ExpiresIn = options.ExpiredTime.Value*60,
                TokenType = "Bearer"
            };
            return response;
        }

        /// <summary>
        /// 生成Token验证参数
        /// </summary>
        /// <param name="jwtSettings"></param>
        /// <returns></returns>
        public static TokenValidationParameters CreateTokenValidationParameters(JWTSettingsOptions jwtSettings)
        {
            return new TokenValidationParameters
            {
                // 验证签发方密钥
                ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey.Value,
                // 签发方密钥
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.IssuerSigningKey)),
                // 验证签发方
                ValidateIssuer = jwtSettings.ValidateIssuer.Value,
                // 设置签发方
                ValidIssuer = jwtSettings.ValidIssuer,
                // 验证签收方
                ValidateAudience = jwtSettings.ValidateAudience.Value,
                // 设置接收方
                ValidAudience = jwtSettings.ValidAudience,
                // 验证生存期
                ValidateLifetime = jwtSettings.ValidateLifetime.Value,
                // 过期时间容错值
                ClockSkew = TimeSpan.FromSeconds(jwtSettings.ClockSkew.Value),
            };
        }
    }
}