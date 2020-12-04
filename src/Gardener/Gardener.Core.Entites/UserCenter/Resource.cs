using Furion.DatabaseAccessor;
using Gardener.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 资源表
    /// </summary>
    public class Resource : Entity<Guid, MasterDbContextLocator>, IEntitySeedData<Resource>
    {
        /// <summary>
        /// 资源名称
        /// </summary>
        [Required, MaxLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// 资源名称简写-唯一
        /// 内部鉴权使用
        /// </summary>
        [Required, MaxLength(100)]
        public string Key { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
        /// <summary>
        /// 资源地址
        /// 菜单：页面路由地址
        /// API:接口路由地址
        /// </summary>
        [MaxLength(200)]
        public string Path { get; set; }
        /// <summary>
        /// 接口请求方法
        /// </summary>
        public HttpMethodType? Method { get; set; }
        /// <summary>
        /// 资源图标
        /// </summary>
        [MaxLength(50)]
        public string Icon { get; set; }
        /// <summary>
        /// 资源排序
        /// </summary>
        [Required, DefaultValue(0)]
        public int Order { get; set; }
        /// <summary>
        /// 父级id
        /// </summary>
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
        [Required, DefaultValue(ResourceType.API)]
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
        /// 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }
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

            entityBuilder.HasComment("权限表");

            entityBuilder.Property(e => e.Id).HasComment("权限id");

            entityBuilder.Property(e => e.CreatedTime).IsRequired()
                .HasMaxLength(6)
                .HasComment("创建时间");

            entityBuilder.Property(e => e.IsDeleted).HasComment("是否删除").IsRequired();

            entityBuilder.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasComment("权限名称");
            //.HasCharSet("utf8mb4")
            //.HasCollation("utf8mb4_0900_ai_ci");

            entityBuilder.Property(e => e.Remark)
                .HasColumnType("varchar(500)")
                .HasComment("备注");
            //.HasCharSet("utf8mb4")
            //.HasCollation("utf8mb4_0900_ai_ci");

            entityBuilder.Property(e => e.Id).IsRequired()
                .HasColumnType("varchar(64)")
                .HasComment("权限唯一名称");
            //.HasCharSet("utf8mb4")
            //.HasCollation("utf8mb4_0900_ai_ci");

            entityBuilder.Property(e => e.Path)
                .HasColumnType("varchar(200)")
                .HasComment("资源地址");


            entityBuilder.Property(e => e.Icon)
                .HasColumnType("varchar(50)")
                .HasComment("资源图标");

            entityBuilder.Property(e => e.ParentId)
                .HasComment("父级id");

            entityBuilder.Property(e => e.Order)
                .HasComment("资源排序");

            entityBuilder.Property(e => e.UpdatedTime)
                .HasMaxLength(6)
                .HasComment("更新时间");
            entityBuilder.Property(e => e.Method)
                .HasComment("接口请求方法");
        }

        public IEnumerable<Resource> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]
            {
new Resource{Id=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),Name="根节点",Icon="apartment",Order=0,Path="",Key="root",Remark="根根节点不能删除，不能改变类型！！。",Type=(ResourceType)0,ParentId=null,CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("6e0add35-dcb1-4d39-b336-1c732ce811e5"),Name="编辑用户",Icon=null,Order=0,Path="api/user",Key="user_auth_user_edit_api",Remark=null,Type=(ResourceType)3000,ParentId=Guid.Parse("0aa9b237-dab8-472e-b2e6-af9c0af9f916"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)2},
new Resource{Id=Guid.Parse("ba063442-52c6-4f51-a87f-44bb22c75de9"),Name="编辑角色",Icon=null,Order=0,Path="api/role",Key="user_auth_role_edit_api",Remark=null,Type=(ResourceType)3000,ParentId=Guid.Parse("13e7d01e-93ca-429c-b412-ff6fa5b6a026"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)2},
new Resource{Id=Guid.Parse("d56889de-4008-42ed-9166-b21cdc0c7fcf"),Name="列表数据查询",Icon=null,Order=-1,Path="api/role/search/{pageindex}/{pagesize}",Key="user_auth_role_list_api",Remark=null,Type=(ResourceType)3000,ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)0},
new Resource{Id=Guid.Parse("a468499c-7115-44f1-ad38-2c5f696891d4"),Name="删除选中",Icon=null,Order=0,Path=null,Key="user_auth_role_delete_selected",Remark=null,Type=(ResourceType)2000,ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("d982a072-4681-45d9-8489-7a14218adb04"),Name="删除",Icon=null,Order=1,Path=null,Key="user_auth_role_delete",Remark=null,Type=(ResourceType)2000,ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("67501fd4-4fbf-48c2-b383-f3a2085268ed"),Name="添加",Icon=null,Order=2,Path=null,Key="user_auth_role_add",Remark=null,Type=(ResourceType)2000,ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("1efd01cf-42f2-45c7-95f2-84be55e65646"),Name="刷新",Icon=null,Order=3,Path=null,Key="user_auth_role_refresh",Remark=null,Type=(ResourceType)2000,ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("13e7d01e-93ca-429c-b412-ff6fa5b6a026"),Name="编辑",Icon=null,Order=4,Path=null,Key="user_auth_role_edit",Remark=null,Type=(ResourceType)2000,ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"),Name="分配资源",Icon=null,Order=5,Path=null,Key="user_auth_role_set_resource",Remark=null,Type=(ResourceType)2000,ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("b71bbc5f-83a3-4065-b561-cb4b69b4a507"),Name="锁定",Icon=null,Order=7,Path=null,Key="user_auth_role_lock",Remark=null,Type=(ResourceType)2000,ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("e30bbf62-d6d3-4e72-ac1a-abb285587632"),Name="资源数据查询",Icon=null,Order=-1,Path="api/resource/tree",Key="user_auth_get_resource_tree_api",Remark="列表数据查询",Type=(ResourceType)3000,ParentId=Guid.Parse("25dcaaca-6f97-45f3-952d-05112f07c677"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)0},
new Resource{Id=Guid.Parse("bc41e1b6-148a-4f67-a036-272393e5cf1f"),Name="添加",Icon=null,Order=0,Path=null,Key="user_auth_resource_add",Remark=null,Type=(ResourceType)2000,ParentId=Guid.Parse("25dcaaca-6f97-45f3-952d-05112f07c677"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("a6fc1966-3e63-40c2-a475-e164691f3dca"),Name="删除",Icon=null,Order=1,Path=null,Key="user_auth_resource_delete",Remark=null,Type=(ResourceType)2000,ParentId=Guid.Parse("25dcaaca-6f97-45f3-952d-05112f07c677"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("ec53d5a0-42f5-4e49-aece-e69ca36f2a26"),Name="刷新",Icon=null,Order=2,Path=null,Key="user_auth_resource_refresh",Remark=null,Type=(ResourceType)2000,ParentId=Guid.Parse("25dcaaca-6f97-45f3-952d-05112f07c677"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("4dd77af7-1947-4166-9006-ee4e9fe84472"),Name="编辑",Icon=null,Order=3,Path=null,Key="user_auth_resource_edit",Remark=null,Type=(ResourceType)2000,ParentId=Guid.Parse("25dcaaca-6f97-45f3-952d-05112f07c677"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("b01446db-3659-4cc5-a1f9-5008461f751e"),Name="展开/收起",Icon=null,Order=4,Path=null,Key="user_auth_resource_switch",Remark=null,Type=(ResourceType)2000,ParentId=Guid.Parse("25dcaaca-6f97-45f3-952d-05112f07c677"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("de8658ed-e997-4c07-861e-721c6275ca38"),Name="系统权限相关接口",Icon=null,Order=-1,Path=null,Key="system_auth_api",Remark=null,Type=(ResourceType)3000,ParentId=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)0},
new Resource{Id=Guid.Parse("371b335b-29e5-4846-b6de-78c9cc691717"),Name="首页",Icon="home",Order=0,Path="/",Key="admin_home",Remark=null,Type=(ResourceType)1000,ParentId=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("d88fbd6d-00ce-4687-9440-a7ffd5aab2aa"),Name="用户权限",Icon="verified",Order=1,Path=null,Key="user_auth",Remark="用户权限",Type=(ResourceType)1000,ParentId=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Name="系统管理",Icon="setting",Order=2,Path=null,Key="system_manager",Remark="系统管理",Type=(ResourceType)1000,ParentId=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("36465515-40c7-4e16-94fc-14be8b03e247"),Name="设置用户角色",Icon=null,Order=0,Path="api/user/{userid}/role",Key="user_auth_user_role_edit_api",Remark=null,Type=(ResourceType)3000,ParentId=Guid.Parse("46cad808-0d0b-42bb-a134-3ad6db8ebf54"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)0},
new Resource{Id=Guid.Parse("2e7a9334-5f50-4d11-999b-8c31f3db2a17"),Name="查询可用角色",Icon=null,Order=1,Path="api/role/effective",Key="user_auth_role_get_effective_roles_api",Remark=null,Type=(ResourceType)3000,ParentId=Guid.Parse("46cad808-0d0b-42bb-a134-3ad6db8ebf54"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)0},
new Resource{Id=Guid.Parse("02ae4ca7-8a18-49c0-a98b-3dbc823760b5"),Name="查询用户角色",Icon=null,Order=2,Path="api/user/{userid}/roles",Key="user_auth_user_get_roles_api",Remark=null,Type=(ResourceType)3000,ParentId=Guid.Parse("46cad808-0d0b-42bb-a134-3ad6db8ebf54"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)0},
new Resource{Id=Guid.Parse("0bdf7864-f155-47a0-b2cf-bb05a2016f9e"),Name="逻辑删除多个用户",Icon=null,Order=0,Path="api/user/fake-deletes",Key="user_auth_user_delete_selected_api",Remark=null,Type=(ResourceType)3000,ParentId=Guid.Parse("476cf96a-0e18-4c30-a760-e8b9c615bb99"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)1},
new Resource{Id=Guid.Parse("b1d51f43-1977-4f46-a07d-14ac19090286"),Name="编辑资源",Icon=null,Order=0,Path="api/resource",Key="user_auth_resource_edit_api",Remark=null,Type=(ResourceType)3000,ParentId=Guid.Parse("4dd77af7-1947-4166-9006-ee4e9fe84472"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)2},
new Resource{Id=Guid.Parse("23544f6f-ce09-4de5-b496-d283c7935f01"),Name="添加角色",Icon=null,Order=0,Path="api/role",Key="user_auth_role_add_api",Remark=null,Type=(ResourceType)3000,ParentId=Guid.Parse("67501fd4-4fbf-48c2-b383-f3a2085268ed"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)1},
new Resource{Id=Guid.Parse("976aab97-4a39-4d6a-896c-f909f62d4728"),Name="为角色分配权限（重置）",Icon=null,Order=0,Path="api/role/{roleid}/resource",Key="user_auth_role_set_resource_api",Remark=null,Type=(ResourceType)3000,ParentId=Guid.Parse("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)1},
new Resource{Id=Guid.Parse("8e7f90eb-c5d5-40f7-bf62-cf0d3f1d8c55"),Name="获取角色所有资源",Icon=null,Order=1,Path="api/role/{roleid}/resource",Key="user_auth_role_get_resource_api",Remark=null,Type=(ResourceType)3000,ParentId=Guid.Parse("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)0},
new Resource{Id=Guid.Parse("ba7c2ed3-e1e9-44de-b48f-092f02a9d811"),Name="查询所有资源",Icon=null,Order=2,Path="api/resource/tree",Key="user_auth_get_resource_tree_api",Remark=null,Type=(ResourceType)3000,ParentId=Guid.Parse("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)0},
new Resource{Id=Guid.Parse("c7e92b64-0434-48e3-bab3-c13b8a536580"),Name="锁定用户",Icon=null,Order=0,Path="api/user/{id}/lock/{islocked}",Key="user_auth_user_lock_api",Remark=null,Type=(ResourceType)3000,ParentId=Guid.Parse("87377abe-785d-426c-b052-f706a2c7173d"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)2},
new Resource{Id=Guid.Parse("dc7cf259-5c60-47c9-a02b-1fc9b04c9582"),Name="列表数据查询",Icon=null,Order=-1,Path="api/user/search/{pageindex}/{pagesize}",Key="user_auth_user_list_api",Remark="列表数据查询",Type=(ResourceType)3000,ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)0},
new Resource{Id=Guid.Parse("476cf96a-0e18-4c30-a760-e8b9c615bb99"),Name="删除选中",Icon=null,Order=0,Path=null,Key="user_auth_user_delete_selected",Remark="删除选中",Type=(ResourceType)2000,ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("d5756ad0-6a8b-4462-907f-1c52a1e11369"),Name="删除",Icon=null,Order=1,Path=null,Key="user_auth_user_delete",Remark=null,Type=(ResourceType)2000,ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("99b6dcf1-1eae-4653-b30d-423c9c8dc95c"),Name="添加",Icon=null,Order=2,Path=null,Key="user_auth_user_add",Remark=null,Type=(ResourceType)2000,ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("e44bb45d-514c-4217-bfba-452c0bd38f28"),Name="刷新",Icon=null,Order=3,Path=null,Key="user_auth_user_refresh",Remark=null,Type=(ResourceType)2000,ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("0aa9b237-dab8-472e-b2e6-af9c0af9f916"),Name="编辑",Icon=null,Order=4,Path=null,Key="user_auth_user_edit",Remark=null,Type=(ResourceType)2000,ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("46cad808-0d0b-42bb-a134-3ad6db8ebf54"),Name="分配角色",Icon=null,Order=5,Path=null,Key="user_auth_user_role_edit",Remark=null,Type=(ResourceType)2000,ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("87377abe-785d-426c-b052-f706a2c7173d"),Name="锁定",Icon=null,Order=7,Path=null,Key="user_auth_user_lock",Remark=null,Type=(ResourceType)2000,ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("7f6eb4a1-b046-438e-a50a-0311da7488b6"),Name="添加用户",Icon=null,Order=0,Path="api/user",Key="user_auth_user_add_api",Remark=null,Type=(ResourceType)3000,ParentId=Guid.Parse("99b6dcf1-1eae-4653-b30d-423c9c8dc95c"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)1},
new Resource{Id=Guid.Parse("b330d71f-d64c-49c5-b5c9-54986e60b4e7"),Name="逻辑删除多个角色",Icon=null,Order=0,Path="api/role/fake-deletes",Key="user_auth_role_delete_selected_api",Remark=null,Type=(ResourceType)3000,ParentId=Guid.Parse("a468499c-7115-44f1-ad38-2c5f696891d4"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)1},
new Resource{Id=Guid.Parse("d33194df-544a-4886-9130-60fb78a1fcb5"),Name="逻辑删除子级资源",Icon=null,Order=0,Path="api/resource/fake-deletes",Key="user_auth_resource_deletes_api",Remark=null,Type=(ResourceType)3000,ParentId=Guid.Parse("a6fc1966-3e63-40c2-a475-e164691f3dca"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)1},
new Resource{Id=Guid.Parse("74068df7-300e-4f3a-ab2c-f78b93a78d74"),Name="锁定角色",Icon=null,Order=0,Path="api/resource/{id}/lock/{islocked}",Key="user_auth_role_lock_api",Remark=null,Type=(ResourceType)3000,ParentId=Guid.Parse("b71bbc5f-83a3-4065-b561-cb4b69b4a507"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)2},
new Resource{Id=Guid.Parse("b11e633f-0253-4345-b811-e96ced174011"),Name="添加资源",Icon=null,Order=0,Path="api​/resource",Key="user_auth_resource_add_api",Remark=null,Type=(ResourceType)3000,ParentId=Guid.Parse("bc41e1b6-148a-4f67-a036-272393e5cf1f"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)1},
new Resource{Id=Guid.Parse("7034e707-93fc-453c-9dfe-20a0cb58297d"),Name="系统日志",Icon="alert",Order=0,Path="/system_manager/log",Key="system_manager_log",Remark="系统日志",Type=(ResourceType)1000,ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),Name="操作审计",Icon="eye",Order=1,Path="/system_manager/audit",Key="system_manager_audit",Remark="操作审计",Type=(ResourceType)1000,ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("c876dd56-06fe-4a9b-9cd2-d2f47e867496"),Name="逻辑删除单个用户",Icon=null,Order=0,Path="api​/user​/fake-delete​/{id}",Key="user_auth_user_delete_api",Remark=null,Type=(ResourceType)3000,ParentId=Guid.Parse("d5756ad0-6a8b-4462-907f-1c52a1e11369"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)3},
new Resource{Id=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="用户管理",Icon="user",Order=0,Path="/user_auth/user",Key="user_auth_user",Remark="用户管理",Type=(ResourceType)1000,ParentId=Guid.Parse("d88fbd6d-00ce-4687-9440-a7ffd5aab2aa"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Name="角色管理",Icon="control",Order=1,Path="/user_auth/role",Key="user_auth_role",Remark=null,Type=(ResourceType)1000,ParentId=Guid.Parse("d88fbd6d-00ce-4687-9440-a7ffd5aab2aa"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("25dcaaca-6f97-45f3-952d-05112f07c677"),Name="资源管理",Icon="api",Order=2,Path="/user_auth/resource",Key="user_auth_resource",Remark="资源管理",Type=(ResourceType)1000,ParentId=Guid.Parse("d88fbd6d-00ce-4687-9440-a7ffd5aab2aa"),CreatedTime=DateTimeOffset.Now,Method=null},
new Resource{Id=Guid.Parse("468ebc0c-01ef-4ddb-8369-5807112c8b51"),Name="逻辑删除单个角色",Icon=null,Order=0,Path="api/role/fake-delete/{id}",Key="user_auth_role_delete_api",Remark=null,Type=(ResourceType)3000,ParentId=Guid.Parse("d982a072-4681-45d9-8489-7a14218adb04"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)3},
new Resource{Id=Guid.Parse("c8540d46-fe88-4858-8ab7-f8b427695e77"),Name="Token 刷新",Icon=null,Order=1,Path="api/authorize/refresh-token",Key="system_auth_api_refresh_token",Remark=null,Type=(ResourceType)3000,ParentId=Guid.Parse("de8658ed-e997-4c07-861e-721c6275ca38"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)1},
new Resource{Id=Guid.Parse("8b78e71e-bd7f-4264-80cd-0ec2964b4f63"),Name="查看用户角色",Icon=null,Order=2,Path="api/authorize/current-user-roles",Key="system_auth_api_current_user_roles",Remark=null,Type=(ResourceType)3000,ParentId=Guid.Parse("de8658ed-e997-4c07-861e-721c6275ca38"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)0},
new Resource{Id=Guid.Parse("825e4fbd-c88c-4028-b864-a7d7363e9550"),Name="获取当前用户信息",Icon=null,Order=3,Path="api/authorize/current-user",Key="system_auth_api_current_user",Remark=null,Type=(ResourceType)3000,ParentId=Guid.Parse("de8658ed-e997-4c07-861e-721c6275ca38"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)0},
new Resource{Id=Guid.Parse("ba6dc63f-dff8-4899-922c-38f2b4ce415d"),Name="获取当前用户资源",Icon=null,Order=4,Path="api/authorize/current-user-resources",Key="system_auth_api_current_user_resources",Remark=null,Type=(ResourceType)3000,ParentId=Guid.Parse("de8658ed-e997-4c07-861e-721c6275ca38"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)1},
new Resource{Id=Guid.Parse("8910d2d6-784b-4331-a5bc-22e2a943aa9f"),Name="获取当前用户的所有菜单",Icon=null,Order=5,Path="api/authorize/current-user-menus",Key="system_auth_api_current_user_menus",Remark=null,Type=(ResourceType)3000,ParentId=Guid.Parse("de8658ed-e997-4c07-861e-721c6275ca38"),CreatedTime=DateTimeOffset.Now,Method=(HttpMethodType)0}

            };
        }
    }
}