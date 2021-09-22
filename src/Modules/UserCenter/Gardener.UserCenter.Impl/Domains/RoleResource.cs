// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
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



 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("186bca5f-cc2c-427e-a58a-dbb81641a296"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("1efd01cf-42f2-45c7-95f2-84be55e65646"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("24ace337-41fe-429d-b32e-d9f88bd97aaa"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("2dd1a78c-f725-461b-8bc6-66112a7e156c"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("365fc5c4-404e-408a-88dc-7614dffad91b"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("371b335b-29e5-4846-b6de-78c9cc691717"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("3f8d700a-bc26-4d5c-9622-d98bf9359159"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("46a9084a-0ae2-496e-bda5-e7e02a419a53"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("6ac07813-4d10-4b50-9f0c-ecd444041282"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("7f772fcb-fe68-4edb-9f7a-6ef520aa25f1"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("925c3162-155c-4644-8ca2-075f9fc76235"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("c98160ef-ce87-4a1b-bfb3-09fc79d2a34a"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("cc23917b-930a-4e34-9717-be71b9fd2dd5"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("e252c0c6-0f19-4768-954c-c0d83fb96d74"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("e44bb45d-514c-4217-bfba-452c0bd38f28"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("f1649263-ef9a-4f42-85ac-16009283efff"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("f4fa035f-27ae-4eee-b006-3cbfac3d2172"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)},
 new RoleResource(){RoleId=2,ResourceId = Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689311)}
            };
        }
    }
}