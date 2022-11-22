// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Base.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.UserCenter.Impl.Domains
{
    /// <summary>
    /// 用户和角色关系表
    /// </summary>
    [Description("角色资源信息")]
    public class RoleResource : IEntity, IEntitySeedData<RoleResource>, IEntityTypeBuilder<RoleResource>
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Required]
        [DisplayName("角色编号")]
        public int RoleId { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        [DisplayName("角色")]
        public Role Role { get; set; }
        /// <summary>
        /// 权限Id
        /// </summary>
        [Required]
        [DisplayName("资源编号")]
        public Guid ResourceId { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        [DisplayName("资源")]
        public Resource Resource { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;


        /// <summary>
        /// 配置多对多关系
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<RoleResource> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder
           .HasKey(t => new { t.RoleId, t.ResourceId });

            entityBuilder
                .HasOne(pt => pt.Role)
                .WithMany(t => t.RoleResources)
                .HasForeignKey(pt => pt.RoleId);
        }
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<RoleResource> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new RoleResource[] {

 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("0f16cfba-bbf5-42c5-83a4-0ac03a1ce5f2"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("186bca5f-cc2c-427e-a58a-dbb81641a296"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("1efd01cf-42f2-45c7-95f2-84be55e65646"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("24ace337-41fe-429d-b32e-d9f88bd97aaa"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("2c1c895c-6434-4f14-91f2-144e48457101"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("2dd1a78c-f725-461b-8bc6-66112a7e156c"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("365fc5c4-404e-408a-88dc-7614dffad91b"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("371b335b-29e5-4846-b6de-78c9cc691717"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("374f7bfd-3c16-40dd-b4dc-a5992a0915cf"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("3d93eb77-2a72-4b4f-aa79-5da1fc794300"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("3f8d700a-bc26-4d5c-9622-d98bf9359159"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("4af87acd-64b4-4d53-8043-cd7ab6b03c77"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("67ad5c3a-8611-4183-ad9e-63cb4c9760fa"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("6ac07813-4d10-4b50-9f0c-ecd444041282"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("6dc2b297-7110-462a-b402-9e9736abf292"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("7aad6dba-3f13-4982-adfa-525fa94485dd"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("7f772fcb-fe68-4edb-9f7a-6ef520aa25f1"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("7f9c7946-edbf-4ff2-9e2b-a3cd635b0e84"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("86a086a1-0770-4df4-ade3-433ff7226399"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("8a4e9aee-b116-4822-bd59-b3a98e84b9f3"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("925c3162-155c-4644-8ca2-075f9fc76235"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("92da96d7-c59c-4d4b-8c97-80a9f59e8fa2"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("99c74c8b-e343-43bc-86e3-bca825b6a270"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("a2b68c70-173f-46fa-8442-e19219a9905b"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("b63d694e-205f-44c0-8353-0c9507f44696"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("b7cdae2b-4f9b-493a-b43b-a3c7ffef3b86"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("ba89c7b7-552c-415c-b4be-085262dc76b0"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("bd892fb3-47b4-469e-ba14-7c0eb703e164"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("c98160ef-ce87-4a1b-bfb3-09fc79d2a34a"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("cc23917b-930a-4e34-9717-be71b9fd2dd5"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("d0d6f112-73a4-44ba-82a4-a3ad8bdb6978"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("d697fda5-28fa-46c3-ba88-a98dd510e09d"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("d83c05a0-4d23-4b2b-ba87-284793bf3eba"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("df132f66-027e-4791-af7a-26e496dc8e5a"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("e252c0c6-0f19-4768-954c-c0d83fb96d74"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("e44bb45d-514c-4217-bfba-452c0bd38f28"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("f1649263-ef9a-4f42-85ac-16009283efff"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("f4fa035f-27ae-4eee-b006-3cbfac3d2172"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("f63a570e-a762-4410-b4b1-764ee5ceb7ae"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1660632961)}
            };
        }
    }
}