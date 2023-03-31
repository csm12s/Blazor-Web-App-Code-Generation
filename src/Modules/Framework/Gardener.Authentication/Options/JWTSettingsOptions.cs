﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Authentication.Options
{
    /// <summary>
    /// Jwt 配置
    /// </summary>
    public class JWTSettingsOptions
    {
        /// <summary>
        /// 验证签发方密钥
        /// </summary>
        public bool ValidateIssuerSigningKey { get; set; } = true;

        /// <summary>
        /// 签发方密钥
        /// </summary>
        public string IssuerSigningKey { get; set; } = null!;

        /// <summary>
        /// 验证签发方
        /// </summary>
        public bool ValidateIssuer { get; set; } = false;

        /// <summary>
        /// 签发方
        /// </summary>
        public string ValidIssuer { get; set; } = null!;

        /// <summary>
        /// 验证签收方
        /// </summary>
        public bool ValidateAudience { get; set; }=false;

        /// <summary>
        /// 签收方
        /// </summary>
        public string ValidAudience { get; set; } = null!;

        /// <summary>
        /// 验证生存期
        /// </summary>
        public bool  ValidateLifetime { get; set; }=true;

        /// <summary>
        /// 过期时间容错值，解决服务器端时间不同步问题（秒）
        /// </summary>
        public long ClockSkew { get; set; } = 5;

        /// <summary>
        /// 过期时间（分钟）
        /// </summary>
        public long? ExpiredTime { get; set; }

        /// <summary>
        /// 加密算法
        /// </summary>
        public string Algorithm { get; set; } = null!;

        /// <summary>
        /// 获取或设置 RefreshToken有效期分钟数
        /// </summary>
        public double RefreshExpireMins { get; set; }

        /// <summary>
        /// 获取或设置 RefreshToken是否绝对过期,绝对过期的刷新token在{RefreshExpireMins}分钟后需要重新登录
        /// </summary>
        public bool IsRefreshAbsoluteExpired { get; set; } = true;
    }
}
