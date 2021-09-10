// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.EntityFramwork.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Authorization.Domains
{
    /// <summary>
    /// 客户端信息
    /// </summary>
    [Description("客户端信息")]
    public class Client : GardenerEntityBase<Guid>, IEntitySeedData<Client>, IEntityTypeBuilder<Client>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [Required, MaxLength(30)]
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(500), Required]
        [DisplayName("备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [DisplayName("联系人")]
        [MaxLength(20)]
        public string Contacts { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [DisplayName("电话")]
        [MaxLength(20)]
        public string Tel { get; set; }

        /// <summary>
        /// 加密Key
        /// </summary>
        [Required, StringLength(64)]
        [DisplayName("加密KEY")]
        public string EncryptKey { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(50)]
        [DisplayName("邮箱")]
        public string Email { get; set; }

        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<Function> Functions { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<ClientFunction> ClientFunctions { get; set; }

        public void Configure(EntityTypeBuilder<Client> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder
                .HasMany(x=>x.Functions)
                .WithMany(x=>x.Clients)
                .UsingEntity<ClientFunction>(
                    x => x.HasOne(r => r.Function).WithMany(r => r.ClientFunctions).HasForeignKey(r => r.FunctionId),
                    x => x.HasOne(r => r.Client).WithMany(r => r.ClientFunctions).HasForeignKey(r => r.ClientId),
                    x => x.HasKey(t => new { t.ClientId, t.FunctionId })
                );
        }

        public IEnumerable<Client> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[] {
                new Client{
                Id=Guid.Parse("96c0eec0-861f-4ed2-a183-5604b20bdff9"),
                Name="测试client1",
                Contacts="园丁",
                CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311),
                Email="qq@qq.com",
                EncryptKey="9f700cec-b787-4e23-a2da-9e45b3bd6cbb",
                Remark="用于测试",
                Tel="13838888888"
                }
            };
        }
    }
}