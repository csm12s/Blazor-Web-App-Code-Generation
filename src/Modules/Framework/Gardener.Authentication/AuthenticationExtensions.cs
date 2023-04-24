// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Constants;
using Gardener.Authentication.Core;
using Gardener.Authentication.Enums;
using Gardener.Authentication.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 安全服务
    /// </summary>
    public static class AuthenticationExtensions
    {
        /// <summary>
        /// 添加安全服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuthen(this IServiceCollection services)
        {
            //配置
            services.AddConfigurableOptions<JWTOptions>();
            //工具
            services.TryAddSingleton<IJwtService, JwtBearerService>();
            services.TryAddSingleton<IIdentityConverter, IdentityConverter>();
            //当前请求身份
            services.TryAddScoped<IIdentityService, IdentityService>();
            //mvc配置
            services.Configure<MvcOptions>(options =>
            {

                AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .AddAuthenticationSchemes(IdentityType.User.ToString(), IdentityType.Client.ToString())
                        .Build();
                //身份认证
                options.Filters.Add(new AuthorizeFilter(policy));
            });
            using var serviceProvider = services.BuildServiceProvider();
            var jwtSettings = serviceProvider.GetRequiredService<IOptions<JWTOptions>>().Value;

            Func<MessageReceivedContext, Task> contextHandle = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                if (!string.IsNullOrEmpty(accessToken))
                {
                    context.Token = accessToken;
                }
                else if (context.Request.Headers.TryGetValue("Authorization", out var value))
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        string token = value.ToString();
                        if (token.StartsWith(GardenerAuthenticationSchemes.User + " ", StringComparison.OrdinalIgnoreCase))
                        {
                            token = token.Substring((GardenerAuthenticationSchemes.User + " ").Length);
                        }
                        else if (token.StartsWith(GardenerAuthenticationSchemes.Client + " ", StringComparison.OrdinalIgnoreCase))
                        {
                            token = token.Substring((GardenerAuthenticationSchemes.Client + " ").Length);
                        }
                        else if (token.StartsWith(JwtBearerDefaults.AuthenticationScheme + " ", StringComparison.OrdinalIgnoreCase))
                        {
                            token = token.Substring((JwtBearerDefaults.AuthenticationScheme + " ").Length);
                        }
                        context.Token = token;
                    }
                }

                return Task.CompletedTask;
            };

            //jwt身份认证配置
            string defaultScheme = GardenerAuthenticationSchemes.User;
            Func<HttpContext, string?> forwardDefaultSelector = context =>
            {
                //根据token 头选择对应验证方案
                if (context.Request.Headers.TryGetValue("Authorization", out var value))
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        string token = value.ToString();
                        if (token.StartsWith(GardenerAuthenticationSchemes.User, StringComparison.OrdinalIgnoreCase))
                        {
                            //User
                            return GardenerAuthenticationSchemes.User;
                        }
                        else if (token.StartsWith(GardenerAuthenticationSchemes.Client, StringComparison.OrdinalIgnoreCase))
                        {
                            //Client
                            return GardenerAuthenticationSchemes.Client;
                        }
                        else if (token.StartsWith(JwtBearerDefaults.AuthenticationScheme, StringComparison.OrdinalIgnoreCase))
                        {
                            //Bearer => User
                            return GardenerAuthenticationSchemes.User;
                        }
                    }
                }
                return defaultScheme;
            };
            services
            .AddAuthentication(defaultScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
           {
               //Bearer => User
               options.ForwardDefaultSelector = forwardDefaultSelector;
           })
            .AddJwtBearer(GardenerAuthenticationSchemes.User, options =>
           {
               //User
               options.TokenValidationParameters = CreateTokenValidationParameters(jwtSettings.Settings[IdentityType.User]);
               options.Events = new JwtBearerEvents
               {
                   OnMessageReceived = contextHandle
               };
               options.ForwardDefaultSelector = forwardDefaultSelector;
           })
           .AddJwtBearer(GardenerAuthenticationSchemes.Client, options =>
           {
               //Client
               options.TokenValidationParameters = CreateTokenValidationParameters(jwtSettings.Settings[IdentityType.Client]);
               options.Events = new JwtBearerEvents
               {
                   OnMessageReceived = contextHandle
               };
               options.ForwardDefaultSelector = forwardDefaultSelector;
           })
           ;
            return services;
        }


        /// <summary>
        /// 生成Token验证参数
        /// </summary>
        /// <param name="jwtSettings"></param>
        /// <returns></returns>
        private static TokenValidationParameters CreateTokenValidationParameters(JWTSettingsOptions jwtSettings)
        {
            return new TokenValidationParameters
            {
                // 验证签发方密钥
                ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                // 签发方密钥
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(jwtSettings.IssuerSigningKey)),
                // 验证签发方
                ValidateIssuer = jwtSettings.ValidateIssuer,
                // 设置签发方
                ValidIssuer = jwtSettings.ValidIssuer,
                // 验证签收方
                ValidateAudience = jwtSettings.ValidateAudience,
                // 设置接收方
                ValidAudience = jwtSettings.ValidAudience,
                // 验证生存期
                ValidateLifetime = jwtSettings.ValidateLifetime,
                // 过期时间容错值
                ClockSkew = TimeSpan.FromSeconds(jwtSettings.ClockSkew),
                ValidAlgorithms = new[] { jwtSettings.Algorithm }
            };
        }
    }
}
