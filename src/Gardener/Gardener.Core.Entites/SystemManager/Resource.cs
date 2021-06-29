// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 资源表
    /// </summary>
    [Description("资源信息")]
    public class Resource : GardenerEntityBase<Guid>, IEntitySeedData<Resource>, IEntityTypeBuilder<Resource>
    {
        /// <summary>
        /// 资源名称
        /// </summary>
        [Required, MaxLength(100)]
        [DisplayName("名称")]
        public string Name { get; set; }

        /// <summary>
        /// 资源名称简写-唯一
        /// 内部鉴权使用
        /// </summary>
        [Required, MaxLength(100)]
        [DisplayName("唯一标示")]
        public string Key { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        [DisplayName("备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 资源地址 菜单：页面路由地址
        /// </summary>
        [MaxLength(200)]
        [DisplayName("路径")]
        public string Path { get; set; }

        /// <summary>
        /// 资源图标
        /// </summary>
        [MaxLength(50)]
        [DisplayName("图标")]
        public string Icon { get; set; }

        /// <summary>
        /// 资源排序
        /// </summary>
        [Required, DefaultValue(0)]
        [DisplayName("排序")]
        public int Order { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        [DisplayName("父级编号")]
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 父级
        /// </summary>
        public Resource Parent { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public ICollection<Resource> Children { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        [Required, DefaultValue(ResourceType.Menu)]
        public ResourceType Type { get; set; }

        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<Role> Roles { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<RoleResource> RoleResources { get; set; }

        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<Function> Functions { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<ResourceFunction> ResourceFunctions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<Resource> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder
               .HasMany(x => x.Children)
               .WithOne(x => x.Parent)
               .HasForeignKey(x => x.ParentId)
               .OnDelete(DeleteBehavior.ClientSetNull); // 必须设置这一行

            entityBuilder
               .HasMany(x => x.Functions)
               .WithMany(x => x.Resources)
               .UsingEntity<ResourceFunction>(
                   x => x.HasOne(r => r.Function).WithMany(r => r.ResourceFunctions).HasForeignKey(r => r.FunctionId),
                   x => x.HasOne(r => r.Resource).WithMany(r => r.ResourceFunctions).HasForeignKey(r => r.ResourceId),
                   x => x.HasKey(t => new { t.ResourceId, t.FunctionId })
               );
        }
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<Resource> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]
            {

 new Resource(){Id=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),Name="根节点",Icon="apartment",Remark="根根节点不能删除，不能改变类型！！。",Key="root",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)0,Order=0},
 new Resource(){Id=Guid.Parse("371b335b-29e5-4846-b6de-78c9cc691717"),ParentId=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),Name="首页",Icon="home",Remark="",Key="admin_home",Path="/",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=0},
 new Resource(){Id=Guid.Parse("d88fbd6d-00ce-4687-9440-a7ffd5aab2aa"),ParentId=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),Name="用户权限",Icon="verified",Remark="用户权限",Key="user_auth",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=1},
 new Resource(){Id=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),ParentId=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),Name="系统管理",Icon="setting",Remark="系统管理",Key="system_manager",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=2},
 new Resource(){Id=Guid.Parse("2dd1a78c-f725-461b-8bc6-66112a7e156c"),ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Name="审计管理",Icon="eye",Remark="审计管理",Key="system_manager_audit",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=1},
 new Resource(){Id=Guid.Parse("925c3162-155c-4644-8ca2-075f9fc76235"),ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Name="附件管理",Icon="file",Remark="附件管理",Key="system_manager_attachment",Path="/system_manager/attachment",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=3},
 new Resource(){Id=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),ParentId=Guid.Parse("d88fbd6d-00ce-4687-9440-a7ffd5aab2aa"),Name="用户管理",Icon="user",Remark="用户管理",Key="user_auth_user",Path="/user_auth/user",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=0},
 new Resource(){Id=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),ParentId=Guid.Parse("d88fbd6d-00ce-4687-9440-a7ffd5aab2aa"),Name="角色管理",Icon="control",Remark="",Key="user_auth_role",Path="/user_auth/role",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=1},
 new Resource(){Id=Guid.Parse("25dcaaca-6f97-45f3-952d-05112f07c677"),ParentId=Guid.Parse("d88fbd6d-00ce-4687-9440-a7ffd5aab2aa"),Name="资源管理",Icon="api",Remark="资源管理",Key="user_auth_resource",Path="/user_auth/resource",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=2},
 new Resource(){Id=Guid.Parse("1efd01cf-42f2-45c7-95f2-84be55e65646"),ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Name="刷新角色",Icon="",Remark="",Key="user_auth_role_refresh",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=3},
 new Resource(){Id=Guid.Parse("476cf96a-0e18-4c30-a760-e8b9c615bb99"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="删除选中用户",Icon="",Remark="删除选中",Key="user_auth_user_delete_selected",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"),ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Name="角色分配资源",Icon="",Remark="",Key="user_auth_role_set_resource",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=5},
 new Resource(){Id=Guid.Parse("67501fd4-4fbf-48c2-b383-f3a2085268ed"),ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Name="添加角色",Icon="",Remark="",Key="user_auth_role_add",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=2},
 new Resource(){Id=Guid.Parse("13e7d01e-93ca-429c-b412-ff6fa5b6a026"),ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Name="编辑角色",Icon="",Remark="",Key="user_auth_role_edit",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=4},
 new Resource(){Id=Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),ParentId=Guid.Parse("2dd1a78c-f725-461b-8bc6-66112a7e156c"),Name="数据审计",Icon="",Remark="数据审计",Key="system_manager_audit_entity",Path="/system_manager/audit-entity",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=2},
 new Resource(){Id=Guid.Parse("d982a072-4681-45d9-8489-7a14218adb04"),ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Name="删除角色",Icon="",Remark="",Key="user_auth_role_delete",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=1},
 new Resource(){Id=Guid.Parse("b71bbc5f-83a3-4065-b561-cb4b69b4a507"),ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Name="锁定角色",Icon="",Remark="",Key="user_auth_role_lock",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=7},
 new Resource(){Id=Guid.Parse("a468499c-7115-44f1-ad38-2c5f696891d4"),ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Name="删除选中角色",Icon="",Remark="",Key="user_auth_role_delete_selected",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("ea0fb035-1f06-4f61-9946-8df027a7462d"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="编辑用户头像-列表中",Icon="",Remark="编辑用户头像-列表中",Key="user_auth_user_list_edit_avatar",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=8},
 new Resource(){Id=Guid.Parse("0aa9b237-dab8-472e-b2e6-af9c0af9f916"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="编辑用户",Icon="",Remark="",Key="user_auth_user_edit",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=4},
 new Resource(){Id=Guid.Parse("e44bb45d-514c-4217-bfba-452c0bd38f28"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="刷新用户",Icon="",Remark="",Key="user_auth_user_refresh",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=3},
 new Resource(){Id=Guid.Parse("4dd77af7-1947-4166-9006-ee4e9fe84472"),ParentId=Guid.Parse("25dcaaca-6f97-45f3-952d-05112f07c677"),Name="编辑资源",Icon="",Remark="",Key="user_auth_resource_edit",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=3},
 new Resource(){Id=Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),ParentId=Guid.Parse("2dd1a78c-f725-461b-8bc6-66112a7e156c"),Name="操作审计",Icon="",Remark="操作审计",Key="system_manager_audit_operation",Path="/system_manager/audit-operation",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=1},
 new Resource(){Id=Guid.Parse("5dfbc04e-1e77-4c41-9ef2-0ed97b6c7882"),ParentId=Guid.Parse("925c3162-155c-4644-8ca2-075f9fc76235"),Name="刷新附件",Icon="null",Remark="刷新",Key="system_manager_attachment_refresh",Path="null",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=2},
 new Resource(){Id=Guid.Parse("ce3992ba-a9df-4114-a38b-615525b53dfd"),ParentId=Guid.Parse("925c3162-155c-4644-8ca2-075f9fc76235"),Name="删除附件",Icon="null",Remark="删除附件",Key="system_manager_attachment_delete",Path="null",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=1},
 new Resource(){Id=Guid.Parse("d998802f-776e-4137-bc63-d8d818464f98"),ParentId=Guid.Parse("925c3162-155c-4644-8ca2-075f9fc76235"),Name="删除选中附件",Icon="null",Remark="删除选中附件",Key="system_manager_attachment_delete_selected",Path="null",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("b01446db-3659-4cc5-a1f9-5008461f751e"),ParentId=Guid.Parse("25dcaaca-6f97-45f3-952d-05112f07c677"),Name="展开/收起资源",Icon="",Remark="",Key="user_auth_resource_switch",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=4},
 new Resource(){Id=Guid.Parse("4f6e13ee-cc52-4fe2-9d2c-db86281ee004"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="查询单条用户信息",Icon="",Remark="",Key="user_auth_user_detail_api",Path="api/user/{id}",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)3000,Order=-1},
 new Resource(){Id=Guid.Parse("87377abe-785d-426c-b052-f706a2c7173d"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="锁定用户",Icon="",Remark="",Key="user_auth_user_lock",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=7},
 new Resource(){Id=Guid.Parse("d5756ad0-6a8b-4462-907f-1c52a1e11369"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="删除用户",Icon="",Remark="",Key="user_auth_user_delete",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=1},
 new Resource(){Id=Guid.Parse("99b6dcf1-1eae-4653-b30d-423c9c8dc95c"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="添加用户",Icon="",Remark="",Key="user_auth_user_add",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=2},
 new Resource(){Id=Guid.Parse("bc41e1b6-148a-4f67-a036-272393e5cf1f"),ParentId=Guid.Parse("25dcaaca-6f97-45f3-952d-05112f07c677"),Name="添加资源",Icon="",Remark="",Key="user_auth_resource_add",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("a6fc1966-3e63-40c2-a475-e164691f3dca"),ParentId=Guid.Parse("25dcaaca-6f97-45f3-952d-05112f07c677"),Name="删除资源",Icon="",Remark="",Key="user_auth_resource_delete",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=1},
 new Resource(){Id=Guid.Parse("ec53d5a0-42f5-4e49-aece-e69ca36f2a26"),ParentId=Guid.Parse("25dcaaca-6f97-45f3-952d-05112f07c677"),Name="刷新资源",Icon="",Remark="",Key="user_auth_resource_refresh",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=2},
 new Resource(){Id=Guid.Parse("46cad808-0d0b-42bb-a134-3ad6db8ebf54"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="用户分配角色",Icon="",Remark="",Key="user_auth_user_role_edit",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=5},
 new Resource(){Id=Guid.Parse("1c377037-13b4-4ef2-8010-d914a40fdbb3"),ParentId=Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),Name="删除操作审计",Icon="",Remark="删除操作审计",Key="system_manager_audit_operation_delete",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=3},
 new Resource(){Id=Guid.Parse("cc23917b-930a-4e34-9717-be71b9fd2dd5"),ParentId=Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),Name="刷新操作审计",Icon="",Remark="刷新操作审计",Key="system_manager_audit_operation_refresh",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=1},
 new Resource(){Id=Guid.Parse("5591c5b9-9ee0-44ae-a4fa-39234b95afa4"),ParentId=Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),Name="删除选中操作审计",Icon="",Remark="",Key="system_manager_audit_operation_delete_selected",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=2},
 new Resource(){Id=Guid.Parse("286dc779-f58d-439a-bb9b-1333ff2b111b"),ParentId=Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),Name="删除数据审计",Icon="",Remark="删除数据审计",Key="system_manager_audit_entity_delete",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=3},
 new Resource(){Id=Guid.Parse("8158e1a6-335d-4a29-9177-0f30e86fa8ec"),ParentId=Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),Name="删除选中数据审计",Icon="",Remark="删除选中数据审计",Key="system_manager_audit_entity_delete_selected",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=2},
 new Resource(){Id=Guid.Parse("7f772fcb-fe68-4edb-9f7a-6ef520aa25f1"),ParentId=Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),Name="刷新数据审计",Icon="",Remark="刷新数据审计",Key="system_manager_audit_entity_refresh",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=1},
 new Resource(){Id=Guid.Parse("3f8d700a-bc26-4d5c-9622-d98bf9359159"),ParentId=Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),Name="查询数据审计详情",Icon="",Remark="查询数据审计详情",Key="system_manager_audit_entity_detail",Path="",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=4},
 new Resource(){Id=Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Name="资源管理",Icon="api",Remark="",Key="system_manager_resource",Path="/system_manager/resource",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=4},
 new Resource(){Id=Guid.Parse("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Name="接口管理",Icon="api",Remark="",Key="system_manager_function",Path="/system_manager/function",CreatedTime=DateTimeOffset.Now,IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=5}
            };
            //return null;
        }
    }
}