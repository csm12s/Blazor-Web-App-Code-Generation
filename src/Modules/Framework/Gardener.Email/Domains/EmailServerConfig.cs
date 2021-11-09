using Furion.DatabaseAccessor;
using Gardener.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Email.Domains
{
    /// <summary>
    /// 邮件服务器配置信息
    /// </summary>
    [Description("邮件服务器配置信息")]
    public class EmailServerConfig: GardenerEntityBase<Guid>, IEntitySeedData<EmailServerConfig>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [MaxLength(30), Required]
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("备注")]
        [MaxLength(500)]
        public string Remark { get; set; }
        /// <summary>
        /// 主机
        /// </summary>
        [DisplayName("主机")]
        [MaxLength(100), Required]
        public string Host { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        [DisplayName("端口")]
        [Required]
        public int Port { get; set; }
        /// <summary>
        /// 发件人邮箱
        /// </summary>
        [DisplayName("发件人邮箱")]
        [MaxLength(100), Required]
        public string FromEmail { get; set; }
        /// <summary>
        /// 账户名
        /// </summary>
        [DisplayName("账户名")]
        [MaxLength(50), Required]
        public string AccountName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [DisplayName("密码")]
        [MaxLength(50), Required]
        public string AccountPassword { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        /// <remarks>多个标签，逗号隔开</remarks>
        [DisplayName("标签")]
        public string Tags { get; set; }

        /// <summary>
        /// 是否启用SSL
        /// </summary>
        [DisplayName("是否启用SSL")]
        public bool EnableSsl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<EmailServerConfig> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[] { 
                new EmailServerConfig{
                Id=Guid.Parse("1812E5C1-7BCC-4D51-9B5E-45D610357E0E"),
                Name="QQ Email",
                Remark="QQ Email",
                Host="smtp.qq.com",
                Port=25,
                FromEmail="888888@qq.com",
                AccountName="888888@qq.com",
                AccountPassword="123456",
                Tags="Base,QQ",
                IsLocked=false,
                IsDeleted=false,
                EnableSsl=false,
                CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636428834)
                }

            };
        }
    }
}
