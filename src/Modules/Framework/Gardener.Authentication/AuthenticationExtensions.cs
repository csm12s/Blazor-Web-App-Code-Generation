// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Core;
using Gardener.Authentication.Enums;
using Gardener.Authentication.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
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
            services.TryAddScoped<IIdentityService, IdentityService>();

            services.AddConfigurableOptions<JWTOptions>();
            services.TryAddSingleton<IJwtService, JwtBearerService>();
            services.Configure<MvcOptions>(options =>
            {

                AuthorizationPolicy policy= new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .AddAuthenticationSchemes(IdentityType.User.ToString(), IdentityType.Client.ToString())
                        .Build();
                //身份认证
                options.Filters.Add(new AuthorizeFilter(policy));
            });
            using var serviceProvider = services.BuildServiceProvider();
            var jwtSettings = serviceProvider.GetService<IOptions<JWTOptions>>().Value;

            Func<MessageReceivedContext, Task> contextHandle = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && (path.Value.IndexOf("/ws/") > -1))
                {
                    context.Token = accessToken;
                }

                return Task.CompletedTask;
            };
            //jwt身份认证配置
            services.AddAuthentication(options =>
           {
               options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
               options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

           }).AddJwtBearer(IdentityType.User.ToString(),options =>
           {
               options.TokenValidationParameters = CreateTokenValidationParameters(jwtSettings.Settings[IdentityType.User]);
               options.Events = new JwtBearerEvents
               {
                   OnMessageReceived = contextHandle
               };
           })
           .AddJwtBearer(IdentityType.Client.ToString(), options =>
           {
               options.TokenValidationParameters = CreateTokenValidationParameters(jwtSettings.Settings[IdentityType.Client]);
               options.Events = new JwtBearerEvents
               {
                   OnMessageReceived = contextHandle
               };
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
                ValidAlgorithms=new[] {jwtSettings.Algorithm }
            };
        }
    }
}
