// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.SystemManager.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Base.Domains
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







 new Resource(){Id=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),Name="后台根节点",Icon="apartment",Remark="根根节点不能删除，不能改变类型！！。",Key="admin_root",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)0,Order=0},
 new Resource(){Id=Guid.Parse("f4239a53-b5e1-49bd-99c6-967a86f07cdc"),Name="前台根节点",Icon="apartment",Remark="根根节点不能删除，不能改变类型！！。",Key="front_root",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)0,Order=1},
 new Resource(){Id=Guid.Parse("371b335b-29e5-4846-b6de-78c9cc691717"),ParentId=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),Name="首页",Icon="home",Remark="",Key="admin_home",Path="/",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=10},
 new Resource(){Id=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),ParentId=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),Name="系统管理",Icon="setting",Remark="系统管理",Key="system_manager",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=20},
 new Resource(){Id=Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),ParentId=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),Name="登录",Icon="",Remark="登录系统",Key="system_login",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("bd892fb3-47b4-469e-ba14-7c0eb703e164"),ParentId=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),Name="用户中心",Icon="apartment",Remark="用户中心",Key="user_center",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=15},
 new Resource(){Id=Guid.Parse("6dc2b297-7110-462a-b402-9e9736abf292"),ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Name="邮件工具",Icon="mail",Remark="邮件工具",Key="system_manager_email_tool",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=80},
 new Resource(){Id=Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Name="资源管理",Icon="menu",Remark="",Key="system_manager_resource",Path="/system_manager/resource",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=30},
 new Resource(){Id=Guid.Parse("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),ParentId=Guid.Parse("bd892fb3-47b4-469e-ba14-7c0eb703e164"),Name="部门管理",Icon="team",Remark="",Key="user_center_dept",Path="/user_center/dept",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=0},
 new Resource(){Id=Guid.Parse("925c3162-155c-4644-8ca2-075f9fc76235"),ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Name="附件管理",Icon="file",Remark="附件管理",Key="system_manager_attachment",Path="/system_manager/attachment",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=50},
 new Resource(){Id=Guid.Parse("2dd1a78c-f725-461b-8bc6-66112a7e156c"),ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Name="审计管理",Icon="audit",Remark="审计管理",Key="system_manager_audit",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=60},
 new Resource(){Id=Guid.Parse("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Name="客户端管理",Icon="cloud-server",Remark="客户端管理",Key="system_manager_client",Path="/system_manager/client",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=45},
 new Resource(){Id=Guid.Parse("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Name="接口管理",Icon="api",Remark="",Key="system_manager_function",Path="/system_manager/function",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=40},
 new Resource(){Id=Guid.Parse("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"),ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Name="登录管理",Icon="idcard",Remark="",Key="system_manager_login_token",Path="/system_manager/login-token",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=70},
 new Resource(){Id=Guid.Parse("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),ParentId=Guid.Parse("bd892fb3-47b4-469e-ba14-7c0eb703e164"),Name="岗位管理",Icon="crown",Remark="",Key="user_center_position",Path="/user_center/position",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=5},
 new Resource(){Id=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),ParentId=Guid.Parse("bd892fb3-47b4-469e-ba14-7c0eb703e164"),Name="角色管理",Icon="user-switch",Remark="",Key="user_center_role",Path="/user_center/role",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=20},
 new Resource(){Id=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),ParentId=Guid.Parse("bd892fb3-47b4-469e-ba14-7c0eb703e164"),Name="用户管理",Icon="user",Remark="用户管理",Key="user_center_user",Path="/user_center/user",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=10},
 new Resource(){Id=Guid.Parse("365fc5c4-404e-408a-88dc-7614dffad91b"),ParentId=Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),Name="刷新资源",Icon="",Remark="",Key="system_manager_resource_refresh",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("08ae2764-e551-45d2-9da7-49648481a8e0"),ParentId=Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),Name="删除选中",Icon="",Remark="删除选中",Key="system_manager_resource_delete_selected",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("c18d4928-35d2-4085-aec9-379d00bcfd8f"),ParentId=Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),Name="添加子资源",Icon="",Remark="",Key="system_manager_resource_add_children",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("859aa714-67c7-4414-bc96-9de5b7aec2c4"),ParentId=Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),Name="导出种子数据",Icon="",Remark="",Key="system_manager_resource_download_seed_data",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("97a7d440-b7fe-4af6-a8a1-18846c48828b"),ParentId=Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),Name="删除资源",Icon="",Remark="删除资源",Key="system_manager_resource_delete",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("d83c05a0-4d23-4b2b-ba87-284793bf3eba"),ParentId=Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),Name="查看资源",Icon="",Remark="查看资源",Key="system_manager_resource_detail",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("e252c0c6-0f19-4768-954c-c0d83fb96d74"),ParentId=Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),Name="关联资源接口",Icon="",Remark="",Key="system_manager_resource_show_function",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("46cad808-0d0b-42bb-a134-3ad6db8ebf54"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="用户分配角色",Icon="",Remark="",Key="user_center_user_role_edit",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=5},
 new Resource(){Id=Guid.Parse("dec04485-3dab-4251-b7b8-1044e749a51e"),ParentId=Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),Name="编辑资源",Icon="",Remark="",Key="system_manager_resource_edit",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("a1958e51-06d4-4b29-9533-eae9d86c41d1"),ParentId=Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),Name="锁定资源",Icon="",Remark="",Key="system_manager_resource_lock",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("d5756ad0-6a8b-4462-907f-1c52a1e11369"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="删除用户",Icon="",Remark="",Key="user_center_user_delete",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=1},
 new Resource(){Id=Guid.Parse("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),ParentId=Guid.Parse("6dc2b297-7110-462a-b402-9e9736abf292"),Name="邮件模板",Icon="copy",Remark="邮件模板",Key="system_manager_email_temaplate",Path="/system_manager/email_temaplate",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=20},
 new Resource(){Id=Guid.Parse("99c74c8b-e343-43bc-86e3-bca825b6a270"),ParentId=Guid.Parse("6dc2b297-7110-462a-b402-9e9736abf292"),Name="邮件服务器",Icon="setting",Remark="邮件服务器配置",Key="system_manager_email_server_config",Path="/system_manager/email_server_config",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=10},
 new Resource(){Id=Guid.Parse("99b6dcf1-1eae-4653-b30d-423c9c8dc95c"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="添加用户",Icon="",Remark="",Key="user_center_user_add",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=2},
 new Resource(){Id=Guid.Parse("0f16cfba-bbf5-42c5-83a4-0ac03a1ce5f2"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="查看用户",Icon="",Remark="查看用户",Key="user_center_user_detail",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("8ab307de-ad4b-462f-b61d-7f1d53b82f3d"),ParentId=Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),Name="添加资源",Icon="",Remark="添加资源",Key="system_manager_resource_add",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("94d2c383-03b6-475c-a744-637dd87a5fdc"),ParentId=Guid.Parse("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),Name="锁定岗位",Icon="",Remark="锁定岗位",Key="user_center_position_lock",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("ba89c7b7-552c-415c-b4be-085262dc76b0"),ParentId=Guid.Parse("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),Name="查看岗位",Icon="",Remark="查看岗位",Key="user_center_position_detail",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("f4fa035f-27ae-4eee-b006-3cbfac3d2172"),ParentId=Guid.Parse("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),Name="刷新岗位",Icon="",Remark="",Key="user_center_position_refresh",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("87377abe-785d-426c-b052-f706a2c7173d"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="锁定用户",Icon="",Remark="",Key="user_center_user_lock",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=7},
 new Resource(){Id=Guid.Parse("2c1c895c-6434-4f14-91f2-144e48457101"),ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Name="查看角色详情",Icon="",Remark="查看角色详情",Key="user_center_role_detail",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("bf05ffe8-c3ff-402d-bef1-3e95d202fd03"),ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Name="获取种子数据",Icon="",Remark="",Key="user_center_role_resource_download_seed_data",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("13e7d01e-93ca-429c-b412-ff6fa5b6a026"),ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Name="编辑角色",Icon="",Remark="",Key="user_center_role_edit",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=4},
 new Resource(){Id=Guid.Parse("1efd01cf-42f2-45c7-95f2-84be55e65646"),ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Name="刷新角色",Icon="",Remark="",Key="user_center_role_refresh",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=3},
 new Resource(){Id=Guid.Parse("67501fd4-4fbf-48c2-b383-f3a2085268ed"),ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Name="添加角色",Icon="",Remark="",Key="user_center_role_add",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=2},
 new Resource(){Id=Guid.Parse("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"),ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Name="角色分配资源",Icon="",Remark="",Key="user_center_role_set_resource",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=5},
 new Resource(){Id=Guid.Parse("b71bbc5f-83a3-4065-b561-cb4b69b4a507"),ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Name="锁定角色",Icon="",Remark="",Key="user_center_role_lock",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=7},
 new Resource(){Id=Guid.Parse("d982a072-4681-45d9-8489-7a14218adb04"),ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Name="删除角色",Icon="",Remark="",Key="user_center_role_delete",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=1},
 new Resource(){Id=Guid.Parse("a468499c-7115-44f1-ad38-2c5f696891d4"),ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Name="删除选中角色",Icon="",Remark="",Key="user_center_role_delete_selected",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("e44bb45d-514c-4217-bfba-452c0bd38f28"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="刷新用户",Icon="",Remark="",Key="user_center_user_refresh",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=3},
 new Resource(){Id=Guid.Parse("4e845d07-33a4-4dc4-ba7f-8568f88b9d68"),ParentId=Guid.Parse("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),Name="删除岗位",Icon="",Remark="",Key="user_center_position_delete",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("25535592-81a1-42dd-8a55-509f2c852ff9"),ParentId=Guid.Parse("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),Name="编辑岗位",Icon="",Remark="",Key="user_center_position_edit",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("0fd84267-ee22-47c4-b41c-ce654eba29d9"),ParentId=Guid.Parse("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),Name="添加岗位",Icon="",Remark="",Key="user_center_position_add",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("3d6e9553-2baf-4d9d-8a82-65de1c7d7ece"),ParentId=Guid.Parse("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),Name="删除选中岗位",Icon="",Remark="",Key="user_center_position_delete_selected",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("f02f906a-7579-478a-9406-3c8fd2c54886"),ParentId=Guid.Parse("925c3162-155c-4644-8ca2-075f9fc76235"),Name="删除附件",Icon="",Remark="",Key="system_manager_attachment_delete",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("f1649263-ef9a-4f42-85ac-16009283efff"),ParentId=Guid.Parse("925c3162-155c-4644-8ca2-075f9fc76235"),Name="刷新附件",Icon="",Remark="",Key="system_manager_attachment_refresh",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("d998802f-776e-4137-bc63-d8d818464f98"),ParentId=Guid.Parse("925c3162-155c-4644-8ca2-075f9fc76235"),Name="删除选中附件",Icon="null",Remark="删除选中附件",Key="system_manager_attachment_delete_selected",Path="null",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("476cf96a-0e18-4c30-a760-e8b9c615bb99"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="删除选中用户",Icon="",Remark="删除选中",Key="user_center_user_delete_selected",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("6e487179-5bb2-4ab5-80e3-58c514c9595f"),ParentId=Guid.Parse("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),Name="锁定接口",Icon="",Remark="",Key="system_manager_function_enable_audit",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("bd7d1a4c-960a-48b2-9c9e-083aa5c5924f"),ParentId=Guid.Parse("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),Name="删除选中接口",Icon="",Remark="",Key="system_manager_function_delete_selected",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("b100a7eb-ef44-4669-bac5-3c5ce52871bb"),ParentId=Guid.Parse("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),Name="删除接口",Icon="",Remark="",Key="system_manager_function_delete",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("cc8a9836-3c4d-4d0b-ae64-a31a6bb36b6f"),ParentId=Guid.Parse("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),Name="查看接口种子数据",Icon="",Remark="查看接口种子数据",Key="system_manager_function_download_seed_data",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("c98160ef-ce87-4a1b-bfb3-09fc79d2a34a"),ParentId=Guid.Parse("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"),Name="刷新登录Token",Icon="",Remark="",Key="system_manager_login_token_refresh",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("0cbb3d40-de41-483e-a76c-3d85682176af"),ParentId=Guid.Parse("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"),Name="锁定登录Token",Icon="",Remark="",Key="system_manager_login_token_lock",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("3d007d84-d209-49e2-94ca-11ad2a3dd91d"),ParentId=Guid.Parse("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"),Name="删除登录Token",Icon="",Remark="",Key="system_manager_login_token_delete",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("f077211f-0e79-44a3-935c-0f704f6a5962"),ParentId=Guid.Parse("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"),Name="删除选中登录Token",Icon="",Remark="",Key="system_manager_login_token_delete_selected",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("1d2fb341-3b69-4d0b-934d-c4c2cd250401"),ParentId=Guid.Parse("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),Name="删除选中部门",Icon="",Remark="",Key="user_center_dept_delete_selected",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("04c237bb-7670-4d66-bbaa-dcd9624d2d90"),ParentId=Guid.Parse("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),Name="添加子级部门",Icon="",Remark="",Key="user_center_dept_add_children",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("b63d694e-205f-44c0-8353-0c9507f44696"),ParentId=Guid.Parse("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),Name="查看部门详情",Icon="",Remark="查看部门详情",Key="user_center_dept_detail",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("316ecba5-5d89-44ae-908f-a54268723bd1"),ParentId=Guid.Parse("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),Name="编辑部门",Icon="",Remark="",Key="user_center_dept_edit",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("186bca5f-cc2c-427e-a58a-dbb81641a296"),ParentId=Guid.Parse("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),Name="刷新部门",Icon="",Remark="",Key="user_center_dept_refresh",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("defa9a78-229f-43a9-b6b8-95dd6fd8a3c3"),ParentId=Guid.Parse("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),Name="添加部门",Icon="",Remark="",Key="user_center_dept_add",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("de62a886-64b2-4a40-b70a-47eb08f23202"),ParentId=Guid.Parse("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),Name="删除部门",Icon="",Remark="",Key="user_center_dept_delete",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("67ad5c3a-8611-4183-ad9e-63cb4c9760fa"),ParentId=Guid.Parse("925c3162-155c-4644-8ca2-075f9fc76235"),Name="查看附件",Icon="",Remark="查看附件",Key="system_manager_attachment_detail",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("50062351-8235-4da1-9f90-4917d0e8abe0"),ParentId=Guid.Parse("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),Name="编辑接口",Icon="",Remark="",Key="system_manager_function_edit",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("749c3a63-6bd8-4755-87ed-c1d455e5b717"),ParentId=Guid.Parse("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),Name="导入接口",Icon="",Remark="",Key="system_manager_function_import",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("92da96d7-c59c-4d4b-8c97-80a9f59e8fa2"),ParentId=Guid.Parse("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),Name="查看接口详情",Icon="",Remark="查看接口详情",Key="system_manager_function_detail",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),ParentId=Guid.Parse("2dd1a78c-f725-461b-8bc6-66112a7e156c"),Name="操作审计",Icon="",Remark="操作审计",Key="system_manager_audit_operation",Path="/system_manager/audit-operation",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=1},
 new Resource(){Id=Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),ParentId=Guid.Parse("2dd1a78c-f725-461b-8bc6-66112a7e156c"),Name="数据审计",Icon="",Remark="数据审计",Key="system_manager_audit_entity",Path="/system_manager/audit-entity",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)1000,Order=2},
 new Resource(){Id=Guid.Parse("ea0fb035-1f06-4f61-9946-8df027a7462d"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="编辑用户头像-列表中",Icon="",Remark="编辑用户头像-列表中",Key="user_center_user_list_edit_avatar",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=8},
 new Resource(){Id=Guid.Parse("8a4e9aee-b116-4822-bd59-b3a98e84b9f3"),ParentId=Guid.Parse("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),Name="查看客户端",Icon="",Remark="查看客户端",Key="system_manager_client_detail",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("a1260e4c-e67c-4d72-a758-560a13e9c496"),ParentId=Guid.Parse("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),Name="删除客户端",Icon="",Remark="删除客户端",Key="system_manager_client_delete",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("7f9c7946-edbf-4ff2-9e2b-a3cd635b0e84"),ParentId=Guid.Parse("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),Name="关联客户端接口关系",Icon="",Remark="关联客户端接口关系",Key="system_manager_client_show_function",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("a7a949b0-ca8e-47a1-a5be-ce0fa3c501e6"),ParentId=Guid.Parse("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),Name="删除选中客户端",Icon="",Remark="删除选中客户端",Key="system_manager_client_delete_selected",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("757fdf0b-0cb9-4f24-92f6-24e18f3defcc"),ParentId=Guid.Parse("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),Name="添加客户端",Icon="",Remark="添加客户端",Key="system_manager_client_add",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("92ed8299-ff26-4fae-b852-fe33f0c01a09"),ParentId=Guid.Parse("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),Name="编辑客户端",Icon="",Remark="编辑客户端",Key="system_manager_client_edit",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("374f7bfd-3c16-40dd-b4dc-a5992a0915cf"),ParentId=Guid.Parse("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),Name="刷新客户端",Icon="",Remark="刷新客户端",Key="system_manager_client_refresh",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("6ac07813-4d10-4b50-9f0c-ecd444041282"),ParentId=Guid.Parse("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),Name="刷新接口",Icon="",Remark="",Key="system_manager_function_refresh",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("0aa9b237-dab8-472e-b2e6-af9c0af9f916"),ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Name="编辑用户",Icon="",Remark="",Key="user_center_user_edit",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=4},
 new Resource(){Id=Guid.Parse("24ace337-41fe-429d-b32e-d9f88bd97aaa"),ParentId=Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),Name="操作审计数据变更详情",Icon="",Remark="操作审计数据变更详情",Key="system_manager_audit_operation_detail",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("86a086a1-0770-4df4-ade3-433ff7226399"),ParentId=Guid.Parse("7f9c7946-edbf-4ff2-9e2b-a3cd635b0e84"),Name="显示已关联接口",Icon="",Remark="显示已关联接口",Key="system_manager_client_show_function_1",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("a7555120-c3e4-4f8d-bdf8-371ac22daa50"),ParentId=Guid.Parse("7f9c7946-edbf-4ff2-9e2b-a3cd635b0e84"),Name="绑定客户端接口关系",Icon="",Remark="绑定资源接口关系",Key="system_manager_client_function_binding",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("106a3a28-3143-4369-9215-cb223d1b0e45"),ParentId=Guid.Parse("99c74c8b-e343-43bc-86e3-bca825b6a270"),Name="编辑邮件服务器配置",Icon="",Remark="编辑邮件服务器配置",Key="system_manager_email_server_config_edit",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("a807706b-ffb3-4f8d-b18d-9a7ee6b88028"),ParentId=Guid.Parse("99c74c8b-e343-43bc-86e3-bca825b6a270"),Name="添加邮件服务器配置",Icon="",Remark="添加邮件服务器配置",Key="system_manager_email_server_config_add",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("d697fda5-28fa-46c3-ba88-a98dd510e09d"),ParentId=Guid.Parse("99c74c8b-e343-43bc-86e3-bca825b6a270"),Name="刷新邮件服务器配置",Icon="",Remark="刷新邮件服务器配置",Key="system_manager_email_server_config_refresh",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("f63a570e-a762-4410-b4b1-764ee5ceb7ae"),ParentId=Guid.Parse("99c74c8b-e343-43bc-86e3-bca825b6a270"),Name="查看邮件服务器配置",Icon="",Remark="查看邮件服务器配置",Key="system_manager_email_server_config_detail",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("286dc779-f58d-439a-bb9b-1333ff2b111b"),ParentId=Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),Name="删除数据审计",Icon="",Remark="删除数据审计",Key="system_manager_audit_entity_delete",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=3},
 new Resource(){Id=Guid.Parse("8158e1a6-335d-4a29-9177-0f30e86fa8ec"),ParentId=Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),Name="删除选中数据审计",Icon="",Remark="删除选中数据审计",Key="system_manager_audit_entity_delete_selected",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=2},
 new Resource(){Id=Guid.Parse("a02edffb-0a63-4106-bac2-ea66f1f65060"),ParentId=Guid.Parse("7f9c7946-edbf-4ff2-9e2b-a3cd635b0e84"),Name="显示可选接口",Icon="",Remark="显示可选接口",Key="system_manager_client_function_add_page_show",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("7f772fcb-fe68-4edb-9f7a-6ef520aa25f1"),ParentId=Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),Name="刷新数据审计",Icon="",Remark="刷新数据审计",Key="system_manager_audit_entity_refresh",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=1},
 new Resource(){Id=Guid.Parse("f2ca3ab7-40da-4828-ad63-06bc9af9b153"),ParentId=Guid.Parse("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"),Name="保存角色资源",Icon="",Remark="保存角色资源",Key="user_center_role_set_resource_save",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("a2b68c70-173f-46fa-8442-e19219a9905b"),ParentId=Guid.Parse("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"),Name="查看角色资源",Icon="",Remark="查看角色资源",Key="user_center_role_resource_select",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)3000,Order=0},
 new Resource(){Id=Guid.Parse("5591c5b9-9ee0-44ae-a4fa-39234b95afa4"),ParentId=Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),Name="删除选中操作审计",Icon="",Remark="",Key="system_manager_audit_operation_delete_selected",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=2},
 new Resource(){Id=Guid.Parse("1c377037-13b4-4ef2-8010-d914a40fdbb3"),ParentId=Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),Name="删除操作审计",Icon="",Remark="删除操作审计",Key="system_manager_audit_operation_delete",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=3},
 new Resource(){Id=Guid.Parse("ca1d4b3a-336b-40a5-b683-0fe0bcbabaf8"),ParentId=Guid.Parse("e252c0c6-0f19-4768-954c-c0d83fb96d74"),Name="绑定资源接口关系",Icon="",Remark="",Key="system_manager_resource_function_binding",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("cc23917b-930a-4e34-9717-be71b9fd2dd5"),ParentId=Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),Name="刷新操作审计",Icon="",Remark="刷新操作审计",Key="system_manager_audit_operation_refresh",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=1},
 new Resource(){Id=Guid.Parse("46b8f9b5-fe41-4b55-b39f-4cb398186d2c"),ParentId=Guid.Parse("99c74c8b-e343-43bc-86e3-bca825b6a270"),Name="删除邮件服务器配置",Icon="",Remark="删除邮件服务器配置",Key="system_manager_email_server_config_delete",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("02337e03-c44f-4029-bbb2-0cc5adf84c29"),ParentId=Guid.Parse("99c74c8b-e343-43bc-86e3-bca825b6a270"),Name="锁定邮件服务器配置",Icon="",Remark="锁定邮件服务器配置",Key="system_manager_email_server_config_lock",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("3f8d700a-bc26-4d5c-9622-d98bf9359159"),ParentId=Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),Name="查询数据审计详情",Icon="",Remark="查询数据审计详情",Key="system_manager_audit_entity_detail",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=4},
 new Resource(){Id=Guid.Parse("4c96cdb4-efc1-4ccc-8ec6-9ca1bc458d8a"),ParentId=Guid.Parse("7f9c7946-edbf-4ff2-9e2b-a3cd635b0e84"),Name="删除选中客户端接口关系",Icon="",Remark="删除选中客户端接口关系",Key="system_manager_client_function_delete_selected",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("1f8605fb-70b3-4929-89eb-4cda69cc305b"),ParentId=Guid.Parse("99c74c8b-e343-43bc-86e3-bca825b6a270"),Name="删除选中邮件服务器配置",Icon="",Remark="删除选中邮件服务器配置",Key="system_manager_email_server_config_delete_selected",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("083fffc4-2600-49bb-87e6-1a92133499ec"),ParentId=Guid.Parse("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),Name="添加邮件模板",Icon="",Remark="添加邮件模板",Key="system_manager_email_template_add",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("b7cdae2b-4f9b-493a-b43b-a3c7ffef3b86"),ParentId=Guid.Parse("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),Name="刷新邮件模板列表",Icon="",Remark="刷新邮件模板列表",Key="system_manager_email_template_refresh",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("08baa5af-4718-4158-9276-1ad1068b9159"),ParentId=Guid.Parse("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),Name="编辑邮件模板",Icon="",Remark="编辑邮件模板",Key="system_manager_email_template_edit",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("7aad6dba-3f13-4982-adfa-525fa94485dd"),ParentId=Guid.Parse("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),Name="查看邮件模板",Icon="",Remark="查看邮件模板",Key="system_manager_email_template_detail",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("b5320a70-11fe-4b7a-9c7e-5bb132e72639"),ParentId=Guid.Parse("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),Name="删除邮件模板",Icon="",Remark="删除邮件模板",Key="system_manager_email_template_delete",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("ef15af79-1be1-4055-82b0-83a6aa8fdd35"),ParentId=Guid.Parse("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),Name="锁定邮件模板",Icon="",Remark="锁定邮件模板",Key="system_manager_email_template_lock",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("af9b9a49-0094-4e1c-97dc-d0580525244f"),ParentId=Guid.Parse("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),Name="发送测试邮件",Icon="",Remark="发送测试邮件",Key="system_manager_email_template_send",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("c4991844-d3b4-4f9a-9c90-c13114515796"),ParentId=Guid.Parse("e252c0c6-0f19-4768-954c-c0d83fb96d74"),Name="显示可选接口",Icon="",Remark="显示可选接口",Key="system_manager_resource_function_add_page_show",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("4f943ed1-997a-485f-9b54-9824b4ac285c"),ParentId=Guid.Parse("e252c0c6-0f19-4768-954c-c0d83fb96d74"),Name="删除选中资源接口关系",Icon="",Remark="",Key="system_manager_resource_function_delete_selected",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("4af87acd-64b4-4d53-8043-cd7ab6b03c77"),ParentId=Guid.Parse("e252c0c6-0f19-4768-954c-c0d83fb96d74"),Name="显示已关联接口",Icon="",Remark="显示已关联接口",Key="system_manager_resource_show_function_1",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("145ec764-6a72-4c4f-85d3-7ad889193970"),ParentId=Guid.Parse("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),Name="删除选中邮件模板",Icon="",Remark="删除选中邮件模板",Key="system_manager_email_template_delete_selected",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0},
 new Resource(){Id=Guid.Parse("3d93eb77-2a72-4b4f-aa79-4da1fc7943c9"),ParentId=Guid.Parse("99c74c8b-e343-43bc-86e3-bca825b6a270"),Name="发送测试邮件",Icon="",Remark="发送测试邮件",Key="system_manager_email_server_config_send",Path="",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=(ResourceType)2000,Order=0}
            };
 
 //return null;
        }
    }
}