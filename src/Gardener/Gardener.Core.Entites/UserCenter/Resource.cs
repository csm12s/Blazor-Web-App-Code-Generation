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
    public class Resource : Entity<Guid>, IEntitySeedData<Resource>
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
        [DisplayName("名称")]
        public string Remark { get; set; }
        /// <summary>
        /// 资源地址
        /// 菜单：页面路由地址
        /// API:接口路由地址
        /// </summary>
        [MaxLength(200)]
        [DisplayName("路径")]
        public string Path { get; set; }
        /// <summary>
        /// 接口请求方法
        /// </summary>
        [DisplayName("请求方法")]
        public HttpMethodType? Method { get; set; }
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
        [Required, DefaultValue(ResourceType.Api)]
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
        [DisplayName("是否锁定")]
        public bool IsLocked { get; set; }
        /// <summary>
        /// 启用审计
        /// </summary>
        [DisplayName("启用审计")]
        public bool EnableAudit { get; set; } = true;
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
                new Resource(){Id=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),Name="根节点",Icon="apartment",Remark="根根节点不能删除，不能改变类型！！。",Key="root",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)0,Order=0},
new Resource(){Id=Guid.Parse("de8658ed-e997-4c07-861e-721c6275ca38"),ParentId=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),Name="系统权限相关接口",Icon="",Remark="",Key="system_auth_api",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)0,Type=(ResourceType)3000,Order=-1},
new Resource(){Id=Guid.Parse("371b335b-29e5-4846-b6de-78c9cc691717"),ParentId=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),Name="首页",Icon="home",Remark="",Key="admin_home",Path="/",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=0},
new Resource(){Id=Guid.Parse("d88fbd6d-00ce-4687-9440-a7ffd5aab2aa"),ParentId=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),Name="用户权限",Icon="verified",Remark="用户权限",Key="user_auth",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=1},
new Resource(){Id=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),ParentId=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),Name="系统管理",Icon="setting",Remark="系统管理",Key="system_manager",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=2},
new Resource(){Id=Guid.Parse("c8540d46-fe88-4858-8ab7-f8b427695e77"),ParentId=Guid.Parse("de8658ed-e997-4c07-861e-721c6275ca38"),Name="Token 刷新",Icon="",Remark="",Key="system_auth_api_refresh_token",Path="api/authorize/refresh-token",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)1,Type=(ResourceType)3000,Order=1},
new Resource(){Id=Guid.Parse("8b78e71e-bd7f-4264-80cd-0ec2964b4f63"),ParentId=Guid.Parse("de8658ed-e997-4c07-861e-721c6275ca38"),Name="查看用户角色",Icon="",Remark="",Key="system_auth_api_current_user_roles",Path="api/authorize/current-user-roles",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)0,Type=(ResourceType)3000,Order=2},
new Resource(){Id=Guid.Parse("825e4fbd-c88c-4028-b864-a7d7363e9550"),ParentId=Guid.Parse("de8658ed-e997-4c07-861e-721c6275ca38"),Name="获取当前用户信息",Icon="",Remark="",Key="system_auth_api_current_user",Path="api/authorize/current-user",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)0,Type=(ResourceType)3000,Order=3},
new Resource(){Id=Guid.Parse("ba6dc63f-dff8-4899-922c-38f2b4ce415d"),ParentId=Guid.Parse("de8658ed-e997-4c07-861e-721c6275ca38"),Name="获取当前用户资源",Icon="",Remark="",Key="system_auth_api_current_user_resources",Path="api/authorize/current-user-resources",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)1,Type=(ResourceType)3000,Order=4},
new Resource(){Id=Guid.Parse("8910d2d6-784b-4331-a5bc-22e2a943aa9f"),ParentId=Guid.Parse("de8658ed-e997-4c07-861e-721c6275ca38"),Name="获取当前用户的所有菜单",Icon="",Remark="",Key="system_auth_api_current_user_menus",Path="api/authorize/current-user-menus",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)0,Type=(ResourceType)3000,Order=5},
new Resource(){Id=Guid.Parse("ed0b45b7-03be-44d3-98cd-c2b9447f7014"),ParentId=Guid.Parse("de8658ed-e997-4c07-861e-721c6275ca38"),Name="删除当前用户的刷新token",Icon="",Remark="",Key="system_auth_api_delete_current_user_refresh_token",Path="api/authorize/current-user-refresh-token",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)3,Type=(ResourceType)3000,Order=6},
new Resource(){Id=Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Name="数据审计",Icon="eye",Remark="数据审计",Key="system_manager_audit_entity",Path="/system_manager/audit-entity",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=2},
new Resource(){Id=Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Name="操作审计",Icon="eye",Remark="操作审计",Key="system_manager_audit_operation",Path="/system_manager/audit-operation",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=1},
new Resource(){Id=Guid.Parse("925c3162-155c-4644-8ca2-075f9fc76235"),ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Name="附件管理",Icon="file",Remark="附件管理",Key="system_manager_attachment",Path="/system_manager/attachment",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=3},
new Resource(){Id=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),ParentId=Guid.Parse("d88fbd6d-00ce-4687-9440-a7ffd5aab2aa"),Name="用户管理",Icon="user",Remark="用户管理",Key="user_auth_user",Path="/user_auth/user",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=0},
new Resource(){Id=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),ParentId=Guid.Parse("d88fbd6d-00ce-4687-9440-a7ffd5aab2aa"),Name="角色管理",Icon="control",Remark="",Key="user_auth_role",Path="/user_auth/role",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=1},
new Resource(){Id=Guid.Parse("25dcaaca-6f97-45f3-952d-05112f07c677"),ParentId=Guid.Parse("d88fbd6d-00ce-4687-9440-a7ffd5aab2aa"),Name="资源管理",Icon="api",Remark="资源管理",Key="user_auth_resource",Path="/user_auth/resource",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=2},
new Resource(){Id=Guid.Parse("7034e707-93fc-453c-9dfe-20a0cb58297d"),ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Name="系统日志",Icon="alert",Remark="系统日志",Key="system_manager_log",Path="/system_manager/log",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=0},
new Resource(){Id=Guid.Parse("a468499c-7115-44f1-ad38-2c5f696891d4"),ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Name="删除选中角色",Icon="",Remark="",Key="user_auth_role_delete_selected",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
new Resource(){Id=Guid.Parse("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"),ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Name="角色分配资源",Icon="",Remark="",Key="user_auth_role_set_resource",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=5},
new Resource(){Id=Guid.Parse("13e7d01e-93ca-429c-b412-ff6fa5b6a026"),ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Name="编辑角色",Icon="",Remark="",Key="user_auth_role_edit",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=4},
new Resource(){Id=Guid.Parse("1efd01cf-42f2-45c7-95f2-84be55e65646"),ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Name="刷新角色",Icon="",Remark="",Key="user_auth_role_refresh",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=3},
new Resource(){Id=Guid.Parse("67501fd4-4fbf-48c2-b383-f3a2085268ed"),ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Name="添加角色",Icon="",Remark="",Key="user_auth_role_add",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=2},
new Resource(){Id=Guid.Parse("d982a072-4681-45d9-8489-7a14218adb04"),ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Name="删除角色",Icon="",Remark="",Key="user_auth_role_delete",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=1},
new Resource(){Id=Guid.Parse("0b133140-4c89-4463-aea6-75f7f0ddebdc"),ParentId=Guid.Parse("925c3162-155c-4644-8ca2-075f9fc76235"),Name="附件列表数据查询",Icon="null",Remark="附件列表数据查询",Key="system_manager_attachment_list_api",Path="api/attachment/search",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)1,Type=(ResourceType)3000,Order=-1},
new Resource(){Id=Guid.Parse("d56889de-4008-42ed-9166-b21cdc0c7fcf"),ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Name="角色列表数据查询",Icon="",Remark="",Key="user_auth_role_list_api",Path="api/role/search/{pageindex}/{pagesize}",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)0,Type=(ResourceType)3000,Order=-1},
new Resource(){Id=Guid.Parse("bc41e1b6-148a-4f67-a036-272393e5cf1f"),ParentId=Guid.Parse("25dcaaca-6f97-45f3-952d-05112f07c677"),Name="添加资源",Icon="",Remark="",Key="user_auth_resource_add",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
new Resource(){Id=Guid.Parse("87377abe-785d-426c-b052-f706a2c7173d"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="锁定用户",Icon="",Remark="",Key="user_auth_user_lock",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=7},
new Resource(){Id=Guid.Parse("46cad808-0d0b-42bb-a134-3ad6db8ebf54"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="用户分配角色",Icon="",Remark="",Key="user_auth_user_role_edit",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=5},
new Resource(){Id=Guid.Parse("0aa9b237-dab8-472e-b2e6-af9c0af9f916"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="编辑用户",Icon="",Remark="",Key="user_auth_user_edit",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=4},
new Resource(){Id=Guid.Parse("e44bb45d-514c-4217-bfba-452c0bd38f28"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="刷新用户",Icon="",Remark="",Key="user_auth_user_refresh",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=3},
new Resource(){Id=Guid.Parse("99b6dcf1-1eae-4653-b30d-423c9c8dc95c"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="添加用户",Icon="",Remark="",Key="user_auth_user_add",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=2},
new Resource(){Id=Guid.Parse("d5756ad0-6a8b-4462-907f-1c52a1e11369"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="删除用户",Icon="",Remark="",Key="user_auth_user_delete",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=1},
new Resource(){Id=Guid.Parse("476cf96a-0e18-4c30-a760-e8b9c615bb99"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="删除选中用户",Icon="",Remark="删除选中",Key="user_auth_user_delete_selected",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
new Resource(){Id=Guid.Parse("dc7cf259-5c60-47c9-a02b-1fc9b04c9582"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="用户列表数据查询",Icon="",Remark="列表数据查询",Key="user_auth_user_list_api",Path="api/user/search/{pageindex}/{pagesize}",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)0,Type=(ResourceType)3000,Order=-1},
new Resource(){Id=Guid.Parse("a6fc1966-3e63-40c2-a475-e164691f3dca"),ParentId=Guid.Parse("25dcaaca-6f97-45f3-952d-05112f07c677"),Name="删除资源",Icon="",Remark="",Key="user_auth_resource_delete",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=1},
new Resource(){Id=Guid.Parse("ec53d5a0-42f5-4e49-aece-e69ca36f2a26"),ParentId=Guid.Parse("25dcaaca-6f97-45f3-952d-05112f07c677"),Name="刷新资源",Icon="",Remark="",Key="user_auth_resource_refresh",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=2},
new Resource(){Id=Guid.Parse("4dd77af7-1947-4166-9006-ee4e9fe84472"),ParentId=Guid.Parse("25dcaaca-6f97-45f3-952d-05112f07c677"),Name="编辑资源",Icon="",Remark="",Key="user_auth_resource_edit",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=3},
new Resource(){Id=Guid.Parse("b01446db-3659-4cc5-a1f9-5008461f751e"),ParentId=Guid.Parse("25dcaaca-6f97-45f3-952d-05112f07c677"),Name="展开/收起资源",Icon="",Remark="",Key="user_auth_resource_switch",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=4},
new Resource(){Id=Guid.Parse("e6e477f3-8dd8-4657-adde-cc2c34017775"),ParentId=Guid.Parse("25dcaaca-6f97-45f3-952d-05112f07c677"),Name="资源数据详情",Icon="",Remark="",Key="user_auth_resource_get_detail_api",Path="api/resource/{id}",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)0,Type=(ResourceType)3000,Order=-2},
new Resource(){Id=Guid.Parse("5dfbc04e-1e77-4c41-9ef2-0ed97b6c7882"),ParentId=Guid.Parse("925c3162-155c-4644-8ca2-075f9fc76235"),Name="刷新附件",Icon="null",Remark="刷新",Key="system_manager_attachment_refresh",Path="null",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=2},
new Resource(){Id=Guid.Parse("ce3992ba-a9df-4114-a38b-615525b53dfd"),ParentId=Guid.Parse("925c3162-155c-4644-8ca2-075f9fc76235"),Name="删除附件",Icon="null",Remark="删除附件",Key="system_manager_attachment_delete",Path="null",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=1},
new Resource(){Id=Guid.Parse("d998802f-776e-4137-bc63-d8d818464f98"),ParentId=Guid.Parse("925c3162-155c-4644-8ca2-075f9fc76235"),Name="删除选中附件",Icon="null",Remark="删除选中附件",Key="system_manager_attachment_delete_selected",Path="null",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
new Resource(){Id=Guid.Parse("b71bbc5f-83a3-4065-b561-cb4b69b4a507"),ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Name="锁定角色",Icon="",Remark="",Key="user_auth_role_lock",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=7},
new Resource(){Id=Guid.Parse("e30bbf62-d6d3-4e72-ac1a-abb285587632"),ParentId=Guid.Parse("25dcaaca-6f97-45f3-952d-05112f07c677"),Name="资源列表数据查询",Icon="",Remark="列表数据查询",Key="user_auth_get_resource_tree_api",Path="api/resource/tree",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)0,Type=(ResourceType)3000,Order=-1},
new Resource(){Id=Guid.Parse("23544f6f-ce09-4de5-b496-d283c7935f01"),ParentId=Guid.Parse("67501fd4-4fbf-48c2-b383-f3a2085268ed"),Name="添加角色",Icon="",Remark="",Key="user_auth_role_add_api",Path="api/role",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=true,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)1,Type=(ResourceType)3000,Order=0},
new Resource(){Id=Guid.Parse("b11e633f-0253-4345-b811-e96ced174011"),ParentId=Guid.Parse("bc41e1b6-148a-4f67-a036-272393e5cf1f"),Name="添加资源",Icon="",Remark="",Key="user_auth_resource_add_api",Path="api​/resource",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=true,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)1,Type=(ResourceType)3000,Order=0},
new Resource(){Id=Guid.Parse("74068df7-300e-4f3a-ab2c-f78b93a78d74"),ParentId=Guid.Parse("b71bbc5f-83a3-4065-b561-cb4b69b4a507"),Name="锁定角色",Icon="",Remark="",Key="user_auth_role_lock_api",Path="api/resource/{id}/lock/{islocked}",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=true,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)2,Type=(ResourceType)3000,Order=0},
new Resource(){Id=Guid.Parse("ba7c2ed3-e1e9-44de-b48f-092f02a9d811"),ParentId=Guid.Parse("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"),Name="查询所有资源",Icon="",Remark="",Key="user_auth_get_resource_tree_api",Path="api/resource/tree",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)0,Type=(ResourceType)3000,Order=2},
new Resource(){Id=Guid.Parse("8e7f90eb-c5d5-40f7-bf62-cf0d3f1d8c55"),ParentId=Guid.Parse("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"),Name="获取角色所有资源",Icon="",Remark="",Key="user_auth_role_get_resource_api",Path="api/role/{roleid}/resource",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)0,Type=(ResourceType)3000,Order=1},
new Resource(){Id=Guid.Parse("976aab97-4a39-4d6a-896c-f909f62d4728"),ParentId=Guid.Parse("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"),Name="为角色分配权限（重置）",Icon="",Remark="",Key="user_auth_role_set_resource_api",Path="api/role/{roleid}/resource",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=true,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)1,Type=(ResourceType)3000,Order=0},
new Resource(){Id=Guid.Parse("ba063442-52c6-4f51-a87f-44bb22c75de9"),ParentId=Guid.Parse("13e7d01e-93ca-429c-b412-ff6fa5b6a026"),Name="编辑角色",Icon="",Remark="",Key="user_auth_role_edit_api",Path="api/role",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=true,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)2,Type=(ResourceType)3000,Order=0},
new Resource(){Id=Guid.Parse("3201dcea-913b-4b30-a596-523c25311cb1"),ParentId=Guid.Parse("d998802f-776e-4137-bc63-d8d818464f98"),Name="物理删除多个附件",Icon="null",Remark="物理删除多个附件",Key="system_manager_attachment_delete_selected_api",Path="api/attachment/deletes",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=true,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)1,Type=(ResourceType)3000,Order=0},
new Resource(){Id=Guid.Parse("468ebc0c-01ef-4ddb-8369-5807112c8b51"),ParentId=Guid.Parse("d982a072-4681-45d9-8489-7a14218adb04"),Name="逻辑删除单个角色",Icon="",Remark="",Key="user_auth_role_delete_api",Path="api/role/fake-delete/{id}",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=true,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)3,Type=(ResourceType)3000,Order=0},
new Resource(){Id=Guid.Parse("d33194df-544a-4886-9130-60fb78a1fcb5"),ParentId=Guid.Parse("a6fc1966-3e63-40c2-a475-e164691f3dca"),Name="逻辑删除子级资源",Icon="",Remark="",Key="user_auth_resource_deletes_api",Path="api/resource/fake-deletes",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=true,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)1,Type=(ResourceType)3000,Order=0},
new Resource(){Id=Guid.Parse("b330d71f-d64c-49c5-b5c9-54986e60b4e7"),ParentId=Guid.Parse("a468499c-7115-44f1-ad38-2c5f696891d4"),Name="逻辑删除多个角色",Icon="",Remark="",Key="user_auth_role_delete_selected_api",Path="api/role/fake-deletes",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=true,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)1,Type=(ResourceType)3000,Order=0},
new Resource(){Id=Guid.Parse("c7e92b64-0434-48e3-bab3-c13b8a536580"),ParentId=Guid.Parse("87377abe-785d-426c-b052-f706a2c7173d"),Name="锁定用户",Icon="",Remark="",Key="user_auth_user_lock_api",Path="api/user/{id}/lock/{islocked}",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=true,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)2,Type=(ResourceType)3000,Order=0},
new Resource(){Id=Guid.Parse("02ae4ca7-8a18-49c0-a98b-3dbc823760b5"),ParentId=Guid.Parse("46cad808-0d0b-42bb-a134-3ad6db8ebf54"),Name="查询用户角色",Icon="",Remark="",Key="user_auth_user_get_roles_api",Path="api/user/{userid}/roles",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)0,Type=(ResourceType)3000,Order=2},
new Resource(){Id=Guid.Parse("2e7a9334-5f50-4d11-999b-8c31f3db2a17"),ParentId=Guid.Parse("46cad808-0d0b-42bb-a134-3ad6db8ebf54"),Name="角色查询可用",Icon="",Remark="",Key="user_auth_role_get_effective_roles_api",Path="api/role/effective",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=false,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)0,Type=(ResourceType)3000,Order=1},
new Resource(){Id=Guid.Parse("36465515-40c7-4e16-94fc-14be8b03e247"),ParentId=Guid.Parse("46cad808-0d0b-42bb-a134-3ad6db8ebf54"),Name="用户设置角色",Icon="",Remark="",Key="user_auth_user_role_edit_api",Path="api/user/{userid}/role",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=true,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)0,Type=(ResourceType)3000,Order=0},
new Resource(){Id=Guid.Parse("6e0add35-dcb1-4d39-b336-1c732ce811e5"),ParentId=Guid.Parse("0aa9b237-dab8-472e-b2e6-af9c0af9f916"),Name="编辑用户",Icon="",Remark="",Key="user_auth_user_edit_api",Path="api/user",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=true,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)2,Type=(ResourceType)3000,Order=0},
new Resource(){Id=Guid.Parse("7f6eb4a1-b046-438e-a50a-0311da7488b6"),ParentId=Guid.Parse("99b6dcf1-1eae-4653-b30d-423c9c8dc95c"),Name="添加用户",Icon="",Remark="",Key="user_auth_user_add_api",Path="api/user",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=true,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)1,Type=(ResourceType)3000,Order=0},
new Resource(){Id=Guid.Parse("c876dd56-06fe-4a9b-9cd2-d2f47e867496"),ParentId=Guid.Parse("d5756ad0-6a8b-4462-907f-1c52a1e11369"),Name="逻辑删除单个用户",Icon="",Remark="",Key="user_auth_user_delete_api",Path="api​/user​/fake-delete​/{id}",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=true,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)3,Type=(ResourceType)3000,Order=0},
new Resource(){Id=Guid.Parse("0bdf7864-f155-47a0-b2cf-bb05a2016f9e"),ParentId=Guid.Parse("476cf96a-0e18-4c30-a760-e8b9c615bb99"),Name="逻辑删除多个用户",Icon="",Remark="",Key="user_auth_user_delete_selected_api",Path="api/user/fake-deletes",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=true,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)1,Type=(ResourceType)3000,Order=0},
new Resource(){Id=Guid.Parse("c034c322-420f-455c-9b0d-6b4aee46218c"),ParentId=Guid.Parse("ce3992ba-a9df-4114-a38b-615525b53dfd"),Name="物理删除单个附件",Icon="null",Remark="物理删除单个附件",Key="system_manager_attachment_delete_api",Path="api/attachment/{id}",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=true,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)3,Type=(ResourceType)3000,Order=0},
new Resource(){Id=Guid.Parse("b1d51f43-1977-4f46-a07d-14ac19090286"),ParentId=Guid.Parse("4dd77af7-1947-4166-9006-ee4e9fe84472"),Name="资源编辑",Icon="",Remark="",Key="user_auth_resource_edit_api",Path="api/resource",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:07:05"),EnableAudit=true,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)2,Type=(ResourceType)3000,Order=0},
new Resource(){Id=Guid.Parse("b000974d-36e2-4487-a163-5408cec5198e"),ParentId=Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),Name="操作审计列表数据查询",Icon="",Remark="操作审计获取列表数据",Key="system_manager_audit_operation_search_list_api",Path="api/audit-operation/search",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:12:55"),EnableAudit=false,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)1,Type=(ResourceType)3000,Order=0},
new Resource(){Id=Guid.Parse("cc23917b-930a-4e34-9717-be71b9fd2dd5"),ParentId=Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),Name="刷新操作审计",Icon="",Remark="刷新操作审计",Key="system_manager_audit_operation_refresh",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:19:03"),EnableAudit=true,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=1},
new Resource(){Id=Guid.Parse("5591c5b9-9ee0-44ae-a4fa-39234b95afa4"),ParentId=Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),Name="删除选中操作审计",Icon="",Remark="",Key="system_manager_audit_operation_delete_selected",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:20:23"),EnableAudit=true,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=2},
new Resource(){Id=Guid.Parse("1c377037-13b4-4ef2-8010-d914a40fdbb3"),ParentId=Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),Name="删除操作审计",Icon="",Remark="删除操作审计",Key="system_manager_audit_operation_delete",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:31:51"),EnableAudit=true,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=3},
new Resource(){Id=Guid.Parse("87409b8d-28d3-42ca-9838-c30408b32331"),ParentId=Guid.Parse("1c377037-13b4-4ef2-8010-d914a40fdbb3"),Name="逻辑删除单个操作审计",Icon="",Remark="",Key="system_manager_audit_operation_delete_api",Path="api/attachment/fake-delete/{id}",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:34:28"),EnableAudit=true,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)3,Type=(ResourceType)3000,Order=0},
new Resource(){Id=Guid.Parse("3fadb096-7fdb-40fc-b6d7-2b23399e2df8"),ParentId=Guid.Parse("5591c5b9-9ee0-44ae-a4fa-39234b95afa4"),Name="逻辑删除多个操作审计",Icon="",Remark="逻辑删除多个操作审计",Key="system_manager_audit_operation_delete_selected_api",Path="api/attachment/fake-deletes",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:36:01"),EnableAudit=true,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)1,Type=(ResourceType)3000,Order=0},
new Resource(){Id=Guid.Parse("9c02b28b-8f69-4da4-987e-74bf824c3520"),ParentId=Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),Name="数据审计列表数据查询",Icon="",Remark="",Key="system_manager_audit_entity_search_list_api",Path="api/audit-entity/search",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:38:34"),EnableAudit=false,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)1,Type=(ResourceType)3000,Order=0},
new Resource(){Id=Guid.Parse("7f772fcb-fe68-4edb-9f7a-6ef520aa25f1"),ParentId=Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),Name="刷新数据审计",Icon="",Remark="刷新数据审计",Key="system_manager_audit_entity_refresh",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:40:20"),EnableAudit=true,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=1},
new Resource(){Id=Guid.Parse("8158e1a6-335d-4a29-9177-0f30e86fa8ec"),ParentId=Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),Name="删除选中数据审计",Icon="",Remark="删除选中数据审计",Key="system_manager_audit_entity_delete_selected",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:41:15"),EnableAudit=true,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=2},
new Resource(){Id=Guid.Parse("286dc779-f58d-439a-bb9b-1333ff2b111b"),ParentId=Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),Name="删除数据审计",Icon="",Remark="删除数据审计",Key="system_manager_audit_entity_delete",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:41:48"),EnableAudit=true,IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=3},
new Resource(){Id=Guid.Parse("0fd709b8-309e-405f-8b6f-7462f4e6a1c6"),ParentId=Guid.Parse("8158e1a6-335d-4a29-9177-0f30e86fa8ec"),Name="逻辑删除多个数据审计",Icon="",Remark="",Key="system_manager_audit_entity_delete_selected_api",Path="api/audit-entity/fake-deletes",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:44:58"),EnableAudit=true,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)1,Type=(ResourceType)3000,Order=0},
new Resource(){Id=Guid.Parse("296a8af5-c95b-4689-8ec9-e57b5f777123"),ParentId=Guid.Parse("286dc779-f58d-439a-bb9b-1333ff2b111b"),Name="逻辑删除单个数据审计",Icon="",Remark="",Key="system_manager_audit_entity_delete_",Path="api/audit-entity/fake-delete/{id}",CreatedTime=DateTimeOffset.Parse("2021-01-02 23:45:29"),EnableAudit=true,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)3,Type=(ResourceType)3000,Order=0},
new Resource(){Id=Guid.Parse("05ad7e32-3101-465f-bec1-01ef8fab70bf"),ParentId=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),Name="基础服务接口",Icon="",Remark="基础服务接口",Key="system_base_service",Path="",CreatedTime=DateTimeOffset.Parse("2021-01-03 12:08:53"),EnableAudit=false,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)0,Type=(ResourceType)3000,Order=-1},
new Resource(){Id=Guid.Parse("26341713-0afe-4030-a373-ef2980df2599"),ParentId=Guid.Parse("05ad7e32-3101-465f-bec1-01ef8fab70bf"),Name="附件上传",Icon="",Remark="附件上传",Key="system_base_service_attachment_upload_api",Path="api/attachment/upload",CreatedTime=DateTimeOffset.Parse("2021-01-03 12:21:32"),EnableAudit=true,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)1,Type=(ResourceType)3000,Order=0},
new Resource(){Id=Guid.Parse("4f6e13ee-cc52-4fe2-9d2c-db86281ee004"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="查询单条用户信息",Icon="",Remark="",Key="user_auth_user_detail_api",Path="api/user/{id}",CreatedTime=DateTimeOffset.Parse("2021-01-03 12:27:37"),EnableAudit=false,IsDeleted=false,IsLocked=false,Method=(HttpMethodType)0,Type=(ResourceType)3000,Order=-1},
            };
            //return null;
        }
    }
}