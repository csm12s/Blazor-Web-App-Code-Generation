﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.EntityFramwork.Domains;
using Gardener.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.UserCenter.Impl.Domains
{
    /// <summary>
    /// 功能信息
    /// </summary>
    [Description("功能信息")]
    public class Function : GardenerEntityBase<Guid>, IEntitySeedData<Function>, IEntityTypeBuilder<Function>
    {
        /// <summary>
        /// 分组
        /// </summary>
        [MaxLength(200)]
        [DisplayName("分组")]
        public string Group { get; set; }

        /// <summary>
        /// 服务
        /// </summary>
        [MaxLength(200)]
        [DisplayName("服务")]
        public string Service { get; set; }

        /// <summary>
        /// 概要
        /// </summary>
        [MaxLength(100)]
        [DisplayName("概要")]
        public string Summary { get; set; }

        /// <summary>
        /// 唯一键
        /// </summary>
        [Required, MaxLength(100)]
        [DisplayName("唯一键")]
        public string Key { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(500)]
        [DisplayName("描述")]
        public string Description { get; set; }

        /// <summary>
        /// API路由地址
        /// </summary>
        [Required, MaxLength(200)]
        [DisplayName("地址")]
        public string Path { get; set; }

        /// <summary>
        /// 接口请求方法
        /// </summary>
        [DisplayName("请求方法")]
        public HttpMethod Method { get; set; }

        /// <summary>
        /// 启用审计
        /// </summary>
        [DisplayName("启用审计")]
        public bool EnableAudit { get; set; }

        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<Resource> Resources { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<ResourceFunction> ResourceFunctions { get; set; }
        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<Client> Clients { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<ClientFunction> ClientFunctions { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<Function> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<Function> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new Function[]
            {



 new Function(){Id = Guid.Parse("3e2f4464-6b69-4a00-acfb-d39184729cdd"),EnableAudit=false,Group="系统管理",Service="用户服务",Summary="分页查询",Key="5FFF46E52DE5943FA225B0F6E29A338D",Description="根据分页参数，分页获取数据",Path="/api/user/page/{pageindex}/{pagesize}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("94f22c97-ae4a-40e0-95cd-d0a6347eacd7"),EnableAudit=true,Group="系统管理",Service="角色服务",Summary="根据角色编号删除所有资源",Key="DECA4ECA67D27FC9932271EE3B0AC5DD",Description="根据角色编号删除所有资源",Path="/api/role/{roleid}/resource",Method=(HttpMethod)3,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("01944b79-bfe5-4304-ade0-9c66e038d5d4"),EnableAudit=true,Group="系统管理",Service="角色服务",Summary="更新",Key="3005F52703299DD4885D51C80CA3B370",Description="更新一条数据",Path="/api/role",Method=(HttpMethod)2,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("39812ff6-2c40-4017-9d11-fd7b13fe2a6b"),EnableAudit=false,Group="系统管理",Service="权限认证服务",Summary="查看用户角色",Key="9F7F841076617DA5F308E11132F7E666",Description="查看当前用户角色",Path="/api/account/current-user-roles",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("03f2bbda-d0d4-429f-9c95-03345d00c2cd"),EnableAudit=false,Group="系统管理",Service="权限认证服务",Summary="获取当前用户信息",Key="8B52F19B65946082EBFB93CE354018DE",Description="获取当前用户信息",Path="/api/account/current-user",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("2eb943e9-5b65-4572-b76e-4ef2de07bcd3"),EnableAudit=false,Group="系统管理",Service="权限认证服务",Summary="获取用户资源",Key="58B28787804A078C4A724AF9A151DEDC",Description="",Path="/api/account/current-user-resources",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("ef62671e-4d35-4993-83c4-4dcdf7cbf0d0"),EnableAudit=true,Group="系统管理",Service="附件服务",Summary="批量删除",Key="33524956F6EC6C08F348500B3E2D9E9C",Description="根据多个主键批量删除",Path="/api/attachment/deletes",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("070ae0e4-0193-4ce0-8ba6-b8c344086ced"),EnableAudit=true,Group="系统管理",Service="附件服务",Summary="逻辑删除",Key="4A29177C50844829451B9ABBFA5DAFAC",Description="根据主键逻辑删除",Path="/api/attachment/fake-delete/{id}",Method=(HttpMethod)3,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("a53a9c89-7968-4598-9c46-dad4e9188bd0"),EnableAudit=false,Group="系统管理",Service="Swagger服务",Summary="获取哦 swagger 配置",Key="945B6A21E0C00F9BB0F7EEE37C671E3E",Description="获取api分组设置",Path="/api/swagger/api-group",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("d22007c6-fada-4ef1-bafa-08455b767883"),EnableAudit=false,Group="系统管理",Service="岗位管理服务",Summary="分页查询",Key="F543F08AB768F7D444481F5D7EB52373",Description="根据分页参数，分页获取数据",Path="/api/position/page/{pageindex}/{pagesize}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("9d9233d8-df0a-43b7-929a-65b9bd532c8c"),EnableAudit=false,Group="系统管理",Service="角色服务",Summary="分页查询",Key="5CF48BAB60B771300975D93C49925CA0",Description="根据分页参数，分页获取数据",Path="/api/role/page/{pageindex}/{pagesize}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("7120bd2f-4491-41ac-bef3-7cd86615da14"),EnableAudit=false,Group="系统管理",Service="用户服务",Summary="搜索",Key="E0587A70B59848C9664BE0BF58E13A17",Description="搜索用户数据",Path="/api/user/search/{pageindex}/{pagesize}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("a4a2536b-1cc6-438c-ba00-054e16fc2c7c"),EnableAudit=true,Group="系统管理",Service="角色服务",Summary="删除",Key="1A6C9AC4F4D71B0FC154AD8CE6FE6D29",Description="根据主键删除一条数据",Path="/api/role/{id}",Method=(HttpMethod)3,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("38c69230-1ed0-413e-9ae6-05bc1ef989e0"),EnableAudit=true,Group="系统管理",Service="角色服务",Summary="分配权限",Key="2BBD7196A51542F56FAC25FF3D760D21",Description="分配权限（重置）",Path="/api/role/{roleid}/resource",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("cba739f0-9f8a-40c2-afff-d66c3382e096"),EnableAudit=false,Group="系统管理",Service="角色服务",Summary="根据主键获取",Key="CC8DA87E574A106E9B14287FEC850037",Description="根据主键查找一条数据",Path="/api/role/{id}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("3790cc0d-dc3a-4669-acba-3a90812c6386"),EnableAudit=false,Group="系统管理",Service="用户服务",Summary="查看用户角色",Key="652940681CC97C52299C95242AB1E858",Description="查看用户角色",Path="/api/user/{userid}/roles",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("9c6cefe2-d57d-490c-8b0f-70749bc5cdfa"),EnableAudit=true,Group="系统管理",Service="附件服务",Summary="删除",Key="085CB1560C82B28FE4C8C5F28EA31A59",Description="根据主键删除",Path="/api/attachment/{id}",Method=(HttpMethod)3,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("6a9763c9-c40f-44f3-a248-a3b1e3d1f586"),EnableAudit=false,Group="系统管理",Service="附件服务",Summary="搜索",Key="0C58B2617EA08ED81F14B53C00C678D7",Description="搜索附件数据",Path="/api/attachment/search",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("0f372dde-1e65-441a-b002-eee8b2e1a1f9"),EnableAudit=true,Group="系统管理",Service="审计数据服务",Summary="批量删除",Key="13457B9CA71646A02E6F004CE877A0E6",Description="根据多个主键批量删除",Path="/api/audit-entity/deletes",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("9ebd4172-5191-4931-9b22-4c339be4a816"),EnableAudit=true,Group="系统管理",Service="用户服务",Summary="更新",Key="8C82B0DF3A0F5EB8DFED7794B16DA9A5",Description="更新用户",Path="/api/user",Method=(HttpMethod)2,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("814304bb-22fe-4a33-82e1-8ad7c64bab4a"),EnableAudit=false,Group="系统管理",Service="资源服务",Summary="获取所有子资源",Key="C5668FD7C42E9FB532AB9CB2E1480E1F",Description="获取所有子资源",Path="/api/resource/{id}/children",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("ffef6a8e-3f80-4a39-97c6-5b2b81582830"),EnableAudit=true,Group="系统管理",Service="资源与接口关系服务",Summary="删除资源与接口关系",Key="FE150D4F1EE3DDDE5BD78C718100A247",Description="",Path="/api/resource-function/{resourceid}/{functionid}",Method=(HttpMethod)3,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("c1e7fa06-b759-4bb0-9545-7265e3798d28"),EnableAudit=true,Group="系统管理",Service="资源与接口关系服务",Summary="添加资源与接口关系",Key="43844F96A173330CECD6470FD62A8A76",Description="",Path="/api/resource-function",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("45dd0581-3394-4c0a-bb8e-c9e0074d5611"),EnableAudit=true,Group="系统管理",Service="资源服务",Summary="更新",Key="CE39C474540DD96EAF373115B164EDC7",Description="更新一条数据",Path="/api/resource",Method=(HttpMethod)2,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("3402d3b2-cf24-4634-a65c-534f96e2991a"),EnableAudit=true,Group="系统管理",Service="用户服务",Summary="批量删除",Key="5C9E8B48C5C77A0CEB8E6A853D56A808",Description="根据多个主键批量删除",Path="/api/user/deletes",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("0c6f2138-e984-4fba-ad2a-2890716a7259"),EnableAudit=true,Group="系统管理",Service="用户服务",Summary="更新头像",Key="FEBD6097BE29268FDFDC295C98A9AD9F",Description="更新用户的头像",Path="/api/user/avatar",Method=(HttpMethod)2,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("0d2e0194-2238-457b-aab0-9b3259cc4ed9"),EnableAudit=true,Group="系统管理",Service="用户服务",Summary="设置角色",Key="A843DEF0CDD97A394996DCF7C5E80F5B",Description="给用户设置角色",Path="/api/user/{userid}/role",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("5eb48cf2-6c45-47c2-a68b-84284a389c69"),EnableAudit=true,Group="系统管理",Service="资源服务",Summary="批量删除",Key="F74128AC93B49FC04CB29781E17E5302",Description="根据多个主键批量删除",Path="/api/resource/deletes",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("e7e8c401-2ff1-45ee-adfd-cebe90117575"),EnableAudit=true,Group="系统管理",Service="资源服务",Summary="逻辑删除",Key="FEB756C21615385FC3C747ACB240DC2D",Description="根据主键逻辑删除",Path="/api/resource/fake-delete/{id}",Method=(HttpMethod)3,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("7f0d7abb-06a4-4a35-b4e3-7798b21e37fa"),EnableAudit=true,Group="系统管理",Service="资源服务",Summary="批量逻辑删除",Key="9C888321143AC3E991B72D3B32193A35",Description="根据多个主键批量逻辑删除",Path="/api/resource/fake-deletes",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("3204c8c0-2c00-47ea-b2b3-711c0e7a2c70"),EnableAudit=false,Group="系统管理",Service="权限认证服务",Summary="获取当前用户的所有菜单",Key="B181FD356A433DD14FE2F54E4115AAC0",Description="获取当前用户的所有菜单",Path="/api/account/current-user-menus",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("bdecc6f3-86f4-4818-af34-5e61001bdeeb"),EnableAudit=true,Group="系统管理",Service="权限认证服务",Summary="移除当前用户token",Key="CFCB833E52DBB2A3C4B4F5EAD96701DC",Description="移除当前用户token",Path="/api/account/current-user-refresh-token",Method=(HttpMethod)3,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("7602df12-8a81-4ab1-8314-df9ce948a876"),EnableAudit=true,Group="系统管理",Service="权限认证服务",Summary="刷新Token",Key="943DE09097CAE8DED8AAAF2C489E30D5",Description="通过刷新token获取新的token",Path="/api/account/refresh-token",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("04ad3c68-6e35-4175-a8ff-564d4bf51e91"),EnableAudit=true,Group="系统管理",Service="资源服务",Summary="添加资源",Key="56C72854CD92865B84133D0D791DEC22",Description="添加资源",Path="/api/resource",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("6aea8a77-edd2-444b-b8be-901d78321a49"),EnableAudit=true,Group="系统管理",Service="用户服务",Summary="批量逻辑删除",Key="3E010DCA7BAD6C3FCCCA32FB77F050F0",Description="根据多个主键批量逻辑删除",Path="/api/user/fake-deletes",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("c56d6a82-abc8-4b17-bc28-27b1904116c9"),EnableAudit=false,Group="系统管理",Service="资源与接口关系服务",Summary="获取种子数据",Key="DDE05A70BD80F948C9AEAFB9708090F3",Description="",Path="/api/resource-function/seed-data",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("c9e572ab-6363-49a4-9c74-d6e21553e45d"),EnableAudit=false,Group="系统管理",Service="功能服务",Summary="获取种子数据",Key="49CCD72DC5A379DF8AC6925CF391FC54",Description="获取种子数据",Path="/api/function/seed-data",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("2c3ec3c9-76c7-4d29-953f-e7430f22577b"),EnableAudit=true,Group="系统管理",Service="角色服务",Summary="逻辑删除",Key="BBF7B9CA0FE646DBAE2923B70DA8A7A4",Description="根据主键逻辑删除",Path="/api/role/fake-delete/{id}",Method=(HttpMethod)3,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("383c5aaf-a3e1-44d1-a1c8-3074abe55f95"),EnableAudit=true,Group="系统管理",Service="角色服务",Summary="批量删除",Key="91B03FFD3080A9684592C45A15C826A5",Description="根据多个主键批量删除",Path="/api/role/deletes",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("c591c0ca-3305-4684-89bb-278218d13c47"),EnableAudit=false,Group="系统管理",Service="Swagger服务",Summary="从json中获取function",Key="187E0857A128187E01EFBBD569C3DE92",Description="",Path="/api/swagger/functions-from-json/{url}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("cdd3c605-ed1d-4d94-a482-16430b729541"),EnableAudit=true,Group="系统管理",Service="资源服务",Summary="锁定",Key="BC8D1127FE54019A5476079400388CF3",Description="根据主键锁定或解锁数据",Path="/api/resource/{id}/lock/{islocked}",Method=(HttpMethod)2,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("868fc0df-7cdf-4b56-873e-16dd3e0aa528"),EnableAudit=true,Group="系统管理",Service="角色服务",Summary="锁定",Key="439ED218846E25C27A388B09904AABC8",Description="根据主键锁定或解锁数据",Path="/api/role/{id}/lock/{islocked}",Method=(HttpMethod)2,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("cb9f6387-5817-4fd6-b9eb-6553dcaf5e87"),EnableAudit=false,Group="系统管理",Service="角色服务",Summary="查询所有",Key="8D8980AD32B8E49FB140F9DCE14B897C",Description="查找到所有数据",Path="/api/role/all",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("2428c3c3-740e-45fc-9047-5a2be3c9cd70"),EnableAudit=true,Group="系统管理",Service="用户服务",Summary="删除",Key="FBAC1FD6280B05C7EAFD6BD24F0DE077",Description="根据主键删除一条数据",Path="/api/user/{id}",Method=(HttpMethod)3,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("b38fb0cc-4275-4d1f-8bb7-6f5a962bcc35"),EnableAudit=false,Group="系统管理",Service="用户服务",Summary="根据主键获取用户",Key="011AC4559477AB1F24A281BDC1033AAB",Description="根据主键获取用户",Path="/api/user/{id}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("7a3399b3-6003-4aae-8e24-2e478992630e"),EnableAudit=true,Group="系统管理",Service="岗位管理服务",Summary="添加",Key="1EB184263BA127C79364162F4E75E660",Description="添加一条数据",Path="/api/position",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("23f69ca2-fcef-4cc7-93ac-484a1e38ba22"),EnableAudit=false,Group="系统管理",Service="用户登录TOKEN服务",Summary="搜索",Key="6707D74A420C1B835267E8F89B2A733C",Description="搜索数据",Path="/api/user-token/search",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("5eb3ac07-56a4-401f-86c5-686a512663ce"),EnableAudit=true,Group="系统管理",Service="用户登录TOKEN服务",Summary="添加",Key="B8C5050A9AF405D498549684AFBE6BA5",Description="添加一条数据",Path="/api/user-token",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("36843f91-b2dd-4be2-81bb-98ae3ca02905"),EnableAudit=true,Group="系统管理",Service="用户登录TOKEN服务",Summary="更新",Key="20A3A8A905329855BDB1538D1FC4952E",Description="更新一条数据",Path="/api/user-token",Method=(HttpMethod)2,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("994bcc67-2758-4b1d-894c-1ff8aa234aa9"),EnableAudit=true,Group="系统管理",Service="用户登录TOKEN服务",Summary="删除",Key="F246AC8F372EBBB4DBA7FD9E387C78CF",Description="根据主键删除一条数据",Path="/api/user-token/{id}",Method=(HttpMethod)3,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("29d09bb6-202c-43d4-b223-1bab9a8110c7"),EnableAudit=false,Group="系统管理",Service="用户登录TOKEN服务",Summary="根据主键获取",Key="F644EA520FFD15E462A5C058F5B034B3",Description="根据主键查找一条数据",Path="/api/user-token/{id}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("ca7391b0-2691-4bb9-87c5-1230c5f1e00e"),EnableAudit=true,Group="系统管理",Service="用户登录TOKEN服务",Summary="批量删除",Key="B63C74AC690A1F5B2E8C23CD2F4C4A0B",Description="根据多个主键批量删除",Path="/api/user-token/deletes",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("98fdbccd-fde2-414d-9cfe-0d6cf3339d58"),EnableAudit=true,Group="系统管理",Service="用户登录TOKEN服务",Summary="逻辑删除",Key="FF4F009CAC7CE2915AA92B3864ADD4CF",Description="根据主键逻辑删除",Path="/api/user-token/fake-delete/{id}",Method=(HttpMethod)3,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("453f751d-70d5-4725-ac7c-ad083bd5253d"),EnableAudit=true,Group="系统管理",Service="用户登录TOKEN服务",Summary="批量逻辑删除",Key="3176F9202C7FE094C0BCDBA6E428E1F4",Description="根据多个主键批量逻辑删除",Path="/api/user-token/fake-deletes",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("9b54a63a-b157-4bd3-adcc-daa0e248edc6"),EnableAudit=false,Group="系统管理",Service="用户登录TOKEN服务",Summary="查询所有",Key="75D578DDEE9C4085A751D6DD1C66F861",Description="查找到所有数据",Path="/api/user-token/all",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("025150b5-37a7-4f19-b8a2-187cb1717928"),EnableAudit=false,Group="系统管理",Service="用户登录TOKEN服务",Summary="查询所有可以用的",Key="64B675513AC8714610A3F1E35127EFCE",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/user-token/all-usable",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("4d23c991-627c-4a7a-8fa5-267c6682115d"),EnableAudit=false,Group="系统管理",Service="用户登录TOKEN服务",Summary="分页查询",Key="12097C2120D203155FD96BDD7EB37F23",Description="根据分页参数，分页获取数据",Path="/api/user-token/page/{pageindex}/{pagesize}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("a0e6ea28-fcd8-4c9b-b937-35a2afa10b86"),EnableAudit=true,Group="系统管理",Service="用户登录TOKEN服务",Summary="锁定",Key="DC9409CFC9E16F58BB9584E1445317AD",Description="根据主键锁定或解锁数据（必须有IsLock才能生效）",Path="/api/user-token/{id}/lock/{islocked}",Method=(HttpMethod)2,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("4d51608e-5988-4d3d-8f5e-00e0c0c07b02"),EnableAudit=false,Group="系统管理",Service="资源服务",Summary="查询所有",Key="7971E7E4FDCB5CBA6EE06E7DFE3F199E",Description="查找到所有数据",Path="/api/resource/all",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("424fd96a-a889-4ff9-910a-25a59204d2ec"),EnableAudit=false,Group="系统管理",Service="资源服务",Summary="返回根节点",Key="34CFCB2759472E91321739C5D43B00D0",Description="返回根节点资源",Path="/api/resource/root",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("65a3c1ee-f5cf-48eb-9bf0-3d4db44257e4"),EnableAudit=true,Group="系统管理",Service="岗位管理服务",Summary="逻辑删除",Key="BB0B0620A9F5665B13ADC8D8C8B8F98A",Description="根据主键逻辑删除",Path="/api/position/fake-delete/{id}",Method=(HttpMethod)3,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("715a2905-da23-405d-98a0-1a1222f7d101"),EnableAudit=false,Group="系统管理",Service="岗位管理服务",Summary="根据主键获取",Key="DF0B66D0FC43BB25047A470707E01EF8",Description="根据主键查找一条数据",Path="/api/position/{id}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("f8ddd5e5-7c20-43c2-a2cf-31ebc3f9971a"),EnableAudit=false,Group="系统管理",Service="资源服务",Summary="分页查询",Key="5848947AEE0064BC746DE38E1AC0E3D2",Description="根据分页参数，分页获取数据",Path="/api/resource/page/{pageindex}/{pagesize}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("b952b41e-b3e9-4c53-9a7d-6b561acf4bc4"),EnableAudit=false,Group="系统管理",Service="功能服务",Summary="根据主键获取",Key="FDD3EAB18820A6CD5C6DA3B17D40EEB9",Description="根据主键查找一条数据",Path="/api/function/{id}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("aed3a535-b700-48a5-a8f5-3657e500e400"),EnableAudit=true,Group="系统管理",Service="审计数据服务",Summary="锁定",Key="882FEEBFEAF1F50D83E0189AA69B9ED0",Description="根据主键锁定或解锁数据",Path="/api/audit-entity/{id}/lock/{islocked}",Method=(HttpMethod)2,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("4b7e7f68-8925-4b5c-b8d2-8a51df917b0c"),EnableAudit=false,Group="系统管理",Service="审计数据服务",Summary="分页查询",Key="72DE329278C26111EB3F431ACB89B0A4",Description="根据分页参数，分页获取数据",Path="/api/audit-entity/page/{pageindex}/{pagesize}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("045945e7-94c4-4727-8392-31fc9d99cd9f"),EnableAudit=false,Group="系统管理",Service="审计数据服务",Summary="查询所有",Key="E46343E17F6F09D2DD0BB1B6C78C81F6",Description="查找到所有数据",Path="/api/audit-entity/all",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("f6843cdf-133d-4eb8-92b2-c36fe63ea9d7"),EnableAudit=true,Group="系统管理",Service="岗位管理服务",Summary="锁定",Key="9501F9B0B5D4867FF65611B203B43D69",Description="根据主键锁定或解锁数据（必须有IsLock才能生效）",Path="/api/position/{id}/lock/{islocked}",Method=(HttpMethod)2,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("63d7208e-45d3-406e-a4a1-c87e3afda04d"),EnableAudit=false,Group="系统管理",Service="角色服务",Summary="获取种子数据",Key="72B515FB99A1EFE42DEFCFC12954F93D",Description="获取种子数据",Path="/api/role/role-resource-seed-data",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("39ccceae-2cba-4cd2-a44b-fc8fe8a3f2e4"),EnableAudit=false,Group="系统管理",Service="用户服务",Summary="查看用户权限",Key="FAA3B104E6EBF3B5F16DB92C56836A63",Description="查看用户权限",Path="/api/user/{userid}/resources",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("af79d7de-0141-4338-8c52-05216d1b07ff"),EnableAudit=true,Group="系统管理",Service="用户服务",Summary="新增",Key="7CBF6D43C3F9935BF83629FCEED2FFFB",Description="新增用户",Path="/api/user",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("b56c4126-411c-445e-86aa-a91a5ce816d4"),EnableAudit=false,Group="系统管理",Service="岗位管理服务",Summary="查询所有可以用的",Key="C47AACD68B1EF833AAC0EC90CD878FDD",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/position/all-usable",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("69f70da1-fb4e-443f-9efe-e3d12cc95eed"),EnableAudit=false,Group="系统管理",Service="岗位管理服务",Summary="查询所有",Key="88BAC4E29D23BD095207644BB397E5EE",Description="查找到所有数据",Path="/api/position/all",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("8d94c826-ddba-47fe-94c9-333880fee187"),EnableAudit=false,Group="系统管理",Service="Swagger服务",Summary="解析api json",Key="7E9057E559FB68353DCA5D208B7B2A71",Description="swagger json 文件解析功能",Path="/api/swagger/analysis/{url}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("89954833-64a5-4c87-a717-9c863ca3b263"),EnableAudit=true,Group="系统管理",Service="岗位管理服务",Summary="批量逻辑删除",Key="710C2B0A026A9C3FF0D6235FCD8E0F26",Description="根据多个主键批量逻辑删除",Path="/api/position/fake-deletes",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("ed340c0c-9b63-45f4-942a-c8a14c4491d3"),EnableAudit=true,Group="系统管理",Service="岗位管理服务",Summary="批量删除",Key="C9E5F9B494BBF428A85ECEA53B095285",Description="根据多个主键批量删除",Path="/api/position/deletes",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("5e8adf52-8db2-4d56-9ff3-003cae13e0aa"),EnableAudit=true,Group="系统管理",Service="角色服务",Summary="批量逻辑删除",Key="F19E71A217BEEADDD5EF20B65D93439E",Description="根据多个主键批量逻辑删除",Path="/api/role/fake-deletes",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("61cc62e4-34da-4a0a-9899-488d3ab399fa"),EnableAudit=true,Group="系统管理",Service="岗位管理服务",Summary="删除",Key="34B7575A20F0D8D6B1B2522F9DD7A7B8",Description="根据主键删除一条数据",Path="/api/position/{id}",Method=(HttpMethod)3,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("05153ee4-dc99-4834-b398-5999f7dc8d01"),EnableAudit=true,Group="系统管理",Service="岗位管理服务",Summary="更新",Key="47FEFB8B545A5A813AB9ABA70F02BD49",Description="更新一条数据",Path="/api/position",Method=(HttpMethod)2,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("e8060504-9fce-43a4-a7f0-7818c2de567e"),EnableAudit=false,Group="系统管理",Service="岗位管理服务",Summary="搜索",Key="8B38909A67FDFE704F49AFB6AF35995A",Description="搜索岗位数据",Path="/api/position/search/{pageindex}/{pagesize}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("e38c1619-0f84-4e55-81c2-0f47992ee33d"),EnableAudit=false,Group="系统管理",Service="资源服务",Summary="查询所有资源",Key="6AFF14D9D209CDEEFFC0E4872E060F42",Description="查询所有资源 按树形结构返回",Path="/api/resource/tree",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("a8c06d41-806a-4bf5-8ceb-15995dac08cb"),EnableAudit=false,Group="系统管理",Service="资源服务",Summary="获取种子数据",Key="3A4F1575935BB1B9D3B3F6F407EB43C6",Description="获取种子数据",Path="/api/resource/seed-data",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("0b605fe1-c77c-4735-8320-b8f400163ac9"),EnableAudit=true,Group="系统管理",Service="用户服务",Summary="逻辑删除",Key="02836036DDDF7900E5F5E9762F5E4229",Description="根据主键逻辑删除",Path="/api/user/fake-delete/{id}",Method=(HttpMethod)3,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("7fa014c4-08db-4f96-8132-2bf3db32b256"),EnableAudit=false,Group="系统管理",Service="审计数据服务",Summary="搜索",Key="5A7181978F26890284CE44ED28A2F7AA",Description="搜索数据审计",Path="/api/audit-entity/search",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("84256e5b-2cef-4b16-8fd3-79ff8d47c731"),EnableAudit=true,Group="系统管理",Service="功能服务",Summary="添加",Key="3F9869D1A16CD359E268F2C2DBEFD0E2",Description="添加一条数据",Path="/api/function",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("aeb8b23d-4da3-4ec0-867f-70d2e2ba9550"),EnableAudit=true,Group="系统管理",Service="功能服务",Summary="更新",Key="E2248234B183BA3EEA82273CB03F500C",Description="更新一条数据",Path="/api/function",Method=(HttpMethod)2,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("2a670df1-f01c-4cdb-b084-a46fdb339ced"),EnableAudit=true,Group="系统管理",Service="功能服务",Summary="删除",Key="097D7A323BFFCA32788EAA8C6BDB5157",Description="根据主键删除一条数据",Path="/api/function/{id}",Method=(HttpMethod)3,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("7e5577d4-32b2-4f43-a83f-05410b59b195"),EnableAudit=true,Group="系统管理",Service="审计数据服务",Summary="删除",Key="CCD570BA5C66619052354D738927A007",Description="根据主键删除一条数据",Path="/api/audit-entity/{id}",Method=(HttpMethod)3,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("99264e5a-76d3-4f92-a56a-9c8711067218"),EnableAudit=false,Group="系统管理",Service="角色服务",Summary="搜索",Key="0E961600B0A0BFF9928C779B1C49389D",Description="搜索角色数据",Path="/api/role/search/{pageindex}/{pagesize}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("1d994e50-d40a-465b-8445-646041a8131a"),EnableAudit=false,Group="系统管理",Service="审计操作服务",Summary="根据操作审计ID获取数据审计",Key="26806445F59D861F9FDB9F91B164A1CD",Description="根据操作审计ID获取数据审计",Path="/api/audit-operation/{operationid}/audit-entity",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("a75bd9a7-e3f0-4736-9c27-8763a3d3768b"),EnableAudit=true,Group="系统管理",Service="审计操作服务",Summary="更新",Key="7037CCD6F97FA35692ED560CE1756F86",Description="更新一条数据",Path="/api/audit-operation",Method=(HttpMethod)2,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("a15ce231-80ae-46c6-ada8-49666e81e328"),EnableAudit=false,Group="系统管理",Service="功能服务",Summary="判断是否存在",Key="27693C4354A64289D9A1D3EB50E68E7E",Description="根据 HttpMethod 和 path 判断是否存在",Path="/api/function/exists/{method}/{path}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("416fe54b-6c50-4b1b-bf77-6744cf19fa72"),EnableAudit=false,Group="系统管理",Service="功能服务",Summary="搜索",Key="9A316E9E6A41D1F57870A5F0CDDC93EF",Description="搜索功能数据",Path="/api/function/search",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("056ff2f6-009b-40ff-a1b9-a6983e471967"),EnableAudit=true,Group="系统管理",Service="功能服务",Summary="启用或禁用审计",Key="1DC1B5ECD34759A80CE8C468366A378F",Description="启用或禁用审计",Path="/api/function/{id}/enable-audit/{enableaudit}",Method=(HttpMethod)2,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("eefdb20f-b508-415a-b798-1aa9420a5b62"),EnableAudit=true,Group="系统管理",Service="审计操作服务",Summary="锁定",Key="6999F8BBB5F9BA97658BB99113A381F5",Description="根据主键锁定或解锁数据",Path="/api/audit-operation/{id}/lock/{islocked}",Method=(HttpMethod)2,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("7bb514a5-d62d-4ba1-a9b9-9e7756eaae2d"),EnableAudit=false,Group="系统管理",Service="审计操作服务",Summary="查询所有",Key="D403D5F25D7ACA97A10BEF07B2A816F4",Description="查找到所有数据",Path="/api/audit-operation/all",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("0367ad11-0be0-48dd-a5a9-1d473b78c0bf"),EnableAudit=true,Group="系统管理",Service="附件服务",Summary="上传附件",Key="3BF647BFC6987B8CEA91C97FEE17CC6D",Description="上传单个附件",Path="/api/attachment/upload",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("c39030b8-d207-4c22-a3ba-74b0eccaa2fa"),EnableAudit=true,Group="系统管理",Service="功能服务",Summary="批量逻辑删除",Key="4603BCE62CA130E67C2450C127DD7728",Description="根据多个主键批量逻辑删除",Path="/api/function/fake-deletes",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("590cd04c-025c-4cc1-bdd1-e9cea201bb46"),EnableAudit=false,Group="系统管理",Service="功能服务",Summary="分页查询",Key="67C405B4CBCC02144945800F26CC1F4F",Description="根据分页参数，分页获取数据",Path="/api/function/page/{pageindex}/{pagesize}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("8ae9c253-584e-46e4-b805-6ec90281d6dd"),EnableAudit=true,Group="系统管理",Service="功能服务",Summary="锁定",Key="E7F5596D4D8517C85871566D8EFA0855",Description="根据主键锁定或解锁数据",Path="/api/function/{id}/lock/{islocked}",Method=(HttpMethod)2,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("475207d6-4c0b-4054-a051-7315295694a1"),EnableAudit=true,Group="系统管理",Service="审计数据服务",Summary="添加",Key="44405A33B9DEC6F934920AF5AC6F7111",Description="添加一条数据",Path="/api/audit-entity",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("622c1a11-7dff-4318-9d21-b57fbd1da9ba"),EnableAudit=true,Group="系统管理",Service="用户服务",Summary="锁定",Key="718DFD76BA4C2997D3DDA216BDB98369",Description="根据主键锁定或解锁数据",Path="/api/user/{id}/lock/{islocked}",Method=(HttpMethod)2,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("9d26c715-9b8b-40c6-bbf4-9c51df1193da"),EnableAudit=true,Group="系统管理",Service="审计数据服务",Summary="更新",Key="DE9B14A3BC0E0653399F870F27F24CEF",Description="更新一条数据",Path="/api/audit-entity",Method=(HttpMethod)2,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("4b57474a-88b4-4393-bb49-4b59e8c3c41d"),EnableAudit=true,Group="系统管理",Service="功能服务",Summary="逻辑删除",Key="A401312992835BA902C0CFDC5FEEE1F3",Description="根据主键逻辑删除",Path="/api/function/fake-delete/{id}",Method=(HttpMethod)3,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("fe3c8d2c-02ce-4073-a2b5-0b05168e7fc9"),EnableAudit=true,Group="系统管理",Service="权限认证服务",Summary="登录",Key="BBDF1AED32167D0970DA83229C7A8A03",Description="登录接口",Path="/api/account/login",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("a8211f75-bf19-459a-bf66-9c31c6f334aa"),EnableAudit=true,Group="系统管理",Service="审计操作服务",Summary="批量删除",Key="1C8C95EA831A3D031460A1390DF26E83",Description="根据多个主键批量删除",Path="/api/audit-operation/deletes",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("08d002b9-d320-4410-b9f3-7986ed87ece4"),EnableAudit=false,Group="系统管理",Service="审计操作服务",Summary="根据主键获取",Key="DA5651C09F319A1358B9948735712DCF",Description="根据主键查找一条数据",Path="/api/audit-operation/{id}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("ffbd98b8-8945-4068-b70c-ea58b487bd25"),EnableAudit=true,Group="系统管理",Service="审计操作服务",Summary="删除",Key="AD48018AF04E0A4573815675E555E98D",Description="根据主键删除一条数据",Path="/api/audit-operation/{id}",Method=(HttpMethod)3,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("2f820c7f-4f1c-4737-aae6-329585c75d92"),EnableAudit=false,Group="系统管理",Service="附件服务",Summary="根据主键获取",Key="271DFDC5E142CFE1AF0C4200C6DC060A",Description="根据主键查找一条数据",Path="/api/attachment/{id}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("7cb8921d-0a0c-4e80-8895-604c05480c43"),EnableAudit=false,Group="系统管理",Service="部门服务",Summary="分页查询",Key="0BBAF9866F200FEDE526AB75E03319CC",Description="根据分页参数，分页获取数据",Path="/api/dept/page/{pageindex}/{pagesize}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("0d2df690-6aa7-466b-b1e4-73fa4fda1b5d"),EnableAudit=true,Group="系统管理",Service="部门服务",Summary="锁定",Key="8AED5C0B53588415D98E97119880AC6A",Description="根据主键锁定或解锁数据",Path="/api/dept/{id}/lock/{islocked}",Method=(HttpMethod)2,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("0ac7b7a5-1ca7-4345-b0cd-a328aaa76723"),EnableAudit=false,Group="新闻服务",Service="新闻管理",Summary="查询所有",Key="DA5FD7BA5C124E41186D976D87084C19",Description="查找到所有数据",Path="/api/news/all",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("1562071d-e18c-4d29-a854-12a562961140"),EnableAudit=false,Group="系统管理",Service="用户服务",Summary="查询所有",Key="9B0AD48E75A6C37EDC7101236F93CF77",Description="查找到所有数据",Path="/api/user/all",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("10190ac3-1092-49a9-8ad2-313454b40447"),EnableAudit=true,Group="系统管理",Service="附件服务",Summary="批量逻辑删除",Key="17B55B877E0FB6704577EA356573BBC3",Description="根据多个主键批量逻辑删除",Path="/api/attachment/fake-deletes",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("a3ea9c9f-da6f-48e1-8255-d250bb3e52d5"),EnableAudit=false,Group="系统管理",Service="附件服务",Summary="查询所有",Key="3CBCB4608120758739D941BFCCC09C18",Description="查找到所有数据",Path="/api/attachment/all",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("aedc9e9c-f011-4d46-966e-3b14fd5298c2"),EnableAudit=false,Group="系统管理",Service="附件服务",Summary="分页查询",Key="9BE552AEF35878A71ABE8179B80AA036",Description="根据分页参数，分页获取数据",Path="/api/attachment/page/{pageindex}/{pagesize}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("5604fcc2-595f-4cc5-b0b8-c0d75a4c9351"),EnableAudit=true,Group="系统管理",Service="附件服务",Summary="锁定",Key="ADF53A6D1C062BF2CC40EBDE20D8E841",Description="根据主键锁定或解锁数据",Path="/api/attachment/{id}/lock/{islocked}",Method=(HttpMethod)2,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("8f1c2eeb-248f-41bb-a083-511664f2fd8e"),EnableAudit=true,Group="系统管理",Service="功能服务",Summary="批量删除",Key="717D6057E652BA28D3BF0CE337180E9E",Description="根据多个主键批量删除",Path="/api/function/deletes",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("03c9956e-b832-4202-9c47-55ba3793f606"),EnableAudit=false,Group="系统管理",Service="审计操作服务",Summary="分页查询",Key="778BD549C3ACEF321ECEDF39C80241D0",Description="根据分页参数，分页获取数据",Path="/api/audit-operation/page/{pageindex}/{pagesize}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("46aef5bc-9d0f-4a05-b21d-747753b98569"),EnableAudit=true,Group="系统管理",Service="审计数据服务",Summary="逻辑删除",Key="30759F98C0CF4A34813C280451C2E4CF",Description="根据主键逻辑删除",Path="/api/audit-entity/fake-delete/{id}",Method=(HttpMethod)3,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("73cfe63f-3338-4bd0-a0b9-1b9cc39951ea"),EnableAudit=true,Group="系统管理",Service="审计操作服务",Summary="批量逻辑删除",Key="C53E746377386D224D0941DB8F4CB539",Description="根据多个主键批量逻辑删除",Path="/api/audit-operation/fake-deletes",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("9513e5e1-37ab-4937-94f1-1f6b99a385f7"),EnableAudit=true,Group="系统管理",Service="审计操作服务",Summary="添加",Key="07BC6868FFAD4A5B26193E2372B9821C",Description="添加一条数据",Path="/api/audit-operation",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("cd7db809-50f5-4bf3-a464-89218e24077f"),EnableAudit=false,Group="系统管理",Service="角色服务",Summary="获取角色所有资源",Key="011A2E3F574F9C151E044EFA80A05F29",Description="获取角色所有资源",Path="/api/role/{roleid}/resource",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("a96bb19e-794e-4fe0-ad39-f423df44f633"),EnableAudit=false,Group="系统管理",Service="部门服务",Summary="获取所有部门数据，以树形结构返回",Key="6A85EF9D6FBD3B330E1827AB0949D7E4",Description="",Path="/api/dept/tree",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("85f94b4c-e897-4f3c-b80a-c7ddb8ebf1b5"),EnableAudit=false,Group="系统管理",Service="部门服务",Summary="搜索",Key="E6BAA5C7F35ED0CBD3902A30349A992B",Description="搜索数据",Path="/api/dept/search",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("973edc2c-42e1-473e-9656-a43890663d8a"),EnableAudit=false,Group="系统管理",Service="部门服务",Summary="查询所有可以用的",Key="3D033D8178E68247D2C34E53F00D468F",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/dept/all-usable",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("b3577dc2-dfea-41be-ba8f-bb8efa389f36"),EnableAudit=false,Group="系统管理",Service="审计数据服务",Summary="根据主键获取",Key="A46663EE883A6E5BE0A0C8FE0B3D7A4C",Description="根据主键查找一条数据",Path="/api/audit-entity/{id}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("5c8381ec-7e8a-4060-9c04-83032d18872c"),EnableAudit=true,Group="系统管理",Service="部门服务",Summary="逻辑删除",Key="A5DA0BB6BEA388B99626E5A34BDE68F4",Description="根据主键逻辑删除",Path="/api/dept/fake-delete/{id}",Method=(HttpMethod)3,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("2502e6ae-879b-4674-a557-cd7b4de891a7"),EnableAudit=false,Group="系统管理",Service="部门服务",Summary="根据主键获取",Key="213D1BBDB567A74636ACE841D780F663",Description="根据主键查找一条数据",Path="/api/dept/{id}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("333edf31-c542-4fa1-baca-b770d558a4d7"),EnableAudit=true,Group="系统管理",Service="部门服务",Summary="删除",Key="EA96F9C3B67BB0EB8E3D5337D3482162",Description="根据主键删除一条数据",Path="/api/dept/{id}",Method=(HttpMethod)3,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("e23b555c-600a-4839-9439-2ee0ad0ae4f8"),EnableAudit=true,Group="系统管理",Service="部门服务",Summary="更新",Key="248BF161E6BEB662D259298A8E564433",Description="更新一条数据",Path="/api/dept",Method=(HttpMethod)2,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("16517409-c055-447b-8e91-7155537c6d15"),EnableAudit=true,Group="系统管理",Service="角色服务",Summary="添加",Key="F57997ED31483BE396EB71C98D07B6F5",Description="添加一条数据",Path="/api/role",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("f5c318f6-9230-475a-830e-a404e17506b5"),EnableAudit=true,Group="系统管理",Service="部门服务",Summary="添加",Key="3AB1D0424907EC010DC69F029B4FBD06",Description="添加一条数据",Path="/api/dept",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("ff8621c9-1b88-4e6d-be00-34615c48c69f"),EnableAudit=false,Group="系统管理",Service="部门服务",Summary="获取种子数据",Key="EFFE9D726D7792B023DF91E15AA48C89",Description="",Path="/api/dept/seed-data",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("9caf800a-de55-4d59-a138-675a16924c3c"),EnableAudit=false,Group="系统管理",Service="功能服务",Summary="查询所有",Key="488BDECFA97ADDE5E940446C32C42693",Description="查找到所有数据",Path="/api/function/all",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("fcebd316-c2f3-4f8e-97fc-498dd3a33d4e"),EnableAudit=true,Group="系统管理",Service="部门服务",Summary="批量删除",Key="951D030BDA5FAE619E5A7BB9EFB43F33",Description="根据多个主键批量删除",Path="/api/dept/deletes",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("080dd200-8e8a-489c-86ca-8eb74c417c0b"),EnableAudit=true,Group="系统管理",Service="审计操作服务",Summary="逻辑删除",Key="A20264B6A44D74DBF0C7990CF3FE6DC1",Description="根据主键逻辑删除",Path="/api/audit-operation/fake-delete/{id}",Method=(HttpMethod)3,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("db76ae46-851b-47bc-94be-b2e869043636"),EnableAudit=false,Group="系统管理",Service="审计操作服务",Summary="搜索",Key="704D356B44E6DEA692BA099781A321DD",Description="搜索操作审计",Path="/api/audit-operation/search",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("12dbe1a6-7d23-48a4-bacb-164f0403d0f4"),EnableAudit=true,Group="系统管理",Service="审计数据服务",Summary="批量逻辑删除",Key="87F7F066F0A0605D1DB5CE8B7286E0CB",Description="根据多个主键批量逻辑删除",Path="/api/audit-entity/fake-deletes",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("10fc92a8-30ed-4536-a995-c7af8e5548a1"),EnableAudit=false,Group="系统管理",Service="资源服务",Summary="根据主键获取",Key="6E9E5AA61727C2BD1E4142F0ED0F9DC5",Description="根据主键查找一条数据",Path="/api/resource/{id}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("f4ba1bf6-c07e-4df2-b7de-93b35fb79bf0"),EnableAudit=true,Group="系统管理",Service="资源服务",Summary="删除",Key="8650A7797FF354BBB742C87D0F560844",Description="根据主键删除一条数据",Path="/api/resource/{id}",Method=(HttpMethod)3,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("fff9f1e7-7fd3-42f5-afe7-d40cca07f0ca"),EnableAudit=false,Group="系统管理",Service="资源服务",Summary="根据资源id获取功能信息",Key="B2A11324BCA0A9070B6160AE6B0EE6F2",Description="",Path="/api/resource/{id}/functions",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("51d98131-fb32-4f4a-b9ed-a89ec4c0718a"),EnableAudit=true,Group="新闻服务",Service="新闻管理",Summary="批量删除",Key="900D7247E48D5866026A9DFCDD9758C0",Description="根据多个主键批量删除",Path="/api/news/deletes",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("bd24a2bb-42cf-4a84-b114-7d727464ebd1"),EnableAudit=true,Group="新闻服务",Service="新闻管理",Summary="逻辑删除",Key="964B61937A0A0C000BC8D8D4251188EC",Description="根据主键逻辑删除",Path="/api/news/fake-delete/{id}",Method=(HttpMethod)3,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("337bae83-a083-4e0e-8ceb-2bb21ae22145"),EnableAudit=true,Group="系统管理",Service="部门服务",Summary="批量逻辑删除",Key="32ABBEA6610DE2420AC7B5E7FDAA315E",Description="根据多个主键批量逻辑删除",Path="/api/dept/fake-deletes",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("4e1a2966-bdfd-485a-b0cf-52004e40f6a7"),EnableAudit=false,Group="系统管理",Service="部门服务",Summary="查询所有",Key="0730ED2F37C050E4994609C45BE0C4A4",Description="查找到所有数据",Path="/api/dept/all",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("c715a6d5-cd99-4c94-8760-936817c1e09c"),EnableAudit=false,Group="系统管理",Service="岗位管理服务",Summary="搜索",Key="9A501F3D2F0A3A2D47A17D6F42042CD5",Description="搜索数据",Path="/api/position/search",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("7229563b-7311-41b8-947b-f07d58fa6c87"),EnableAudit=false,Group="系统管理",Service="资源服务",Summary="查询所有可以用的",Key="6C03A3540C36BB4BD1BB9F1606F0F550",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/resource/all-usable",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("1295aed2-ae71-411f-9542-d50f75432840"),EnableAudit=false,Group="系统管理",Service="资源服务",Summary="搜索",Key="24C5B533C5DDC7D494830FF5E28F6EC2",Description="搜索数据",Path="/api/resource/search",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("5efd6ab4-a9d3-4742-9a48-fb54a1b1e463"),EnableAudit=false,Group="系统管理",Service="角色服务",Summary="查询所有可以用的",Key="D4F99E0AE4263D647F3440B66DB7AC7B",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/role/all-usable",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("498638f7-dc92-4d0e-ac5e-26e48cf87a8d"),EnableAudit=false,Group="系统管理",Service="角色服务",Summary="搜索",Key="4B11C588FC856C862E41859F189370C0",Description="搜索数据",Path="/api/role/search",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("cbc8aff4-6dc0-41f2-b684-caba8e0657ac"),EnableAudit=false,Group="系统管理",Service="用户服务",Summary="搜索",Key="04608E487B494D4597BBAD83DF59D2FF",Description="搜索用户数据",Path="/api/user/search",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("f512069b-3c5c-47c6-bb97-5f7e7b71039d"),EnableAudit=true,Group="新闻服务",Service="新闻管理",Summary="批量逻辑删除",Key="CBD3FFEFF6C8E0CF0D8789610DCDB5B5",Description="根据多个主键批量逻辑删除",Path="/api/news/fake-deletes",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("42b3486a-8ea0-4296-a526-7cd3ef9ea73a"),EnableAudit=false,Group="系统管理",Service="附件服务",Summary="查询所有可以用的",Key="6B7B11626AE0ABB28C5331DB67DACAA0",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/attachment/all-usable",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("bdab8953-956d-4b1a-945b-b1806e9ac749"),EnableAudit=false,Group="系统管理",Service="用户服务",Summary="查询所有可以用的",Key="073E6E78B3A88E41DBDC46DCA32C4837",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/user/all-usable",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("4a127124-6348-4db1-aa38-5f3af2c8efdf"),EnableAudit=false,Group="系统管理",Service="审计操作服务",Summary="查询所有可以用的",Key="A99DB9777E1C5C11D2FA6A8957F696E8",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/audit-operation/all-usable",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("b79d2f63-487c-44c8-b7d3-1e882994789b"),EnableAudit=false,Group="系统管理",Service="功能服务",Summary="查询所有可以用的",Key="1CCC2478B5AC5FDDB537DCD33166ABF7",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/function/all-usable",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("0a63566b-0a91-4d79-b417-60b1d8f92aeb"),EnableAudit=false,Group="新闻服务",Service="新闻管理",Summary="根据主键获取",Key="2A6B6013C84E500CF451D706AF4FF7B8",Description="根据主键查找一条数据",Path="/api/news/{id}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("9a4766ee-e624-41ce-98fc-e8abb6ef580a"),EnableAudit=true,Group="新闻服务",Service="新闻管理",Summary="删除",Key="BB2AEA0FA60BE9D7016A8271D23591E3",Description="根据主键删除一条数据",Path="/api/news/{id}",Method=(HttpMethod)3,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("81ee6d06-adc6-42c4-a8cd-5d1496581a6c"),EnableAudit=true,Group="新闻服务",Service="新闻管理",Summary="更新",Key="0A1ABF7F373AA969110F03D1A1977088",Description="更新一条数据",Path="/api/news",Method=(HttpMethod)2,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("c0b7ba65-dd12-4733-b2f5-e7347aa9f301"),EnableAudit=true,Group="新闻服务",Service="新闻管理",Summary="添加",Key="61FD0B99A537BC225735AA062D6A9AEB",Description="添加一条数据",Path="/api/news",Method=(HttpMethod)1,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("a02b5d10-7dc1-474a-9802-781da1172c3f"),EnableAudit=true,Group="新闻服务",Service="新闻管理",Summary="锁定",Key="59F9B25ACB6F71D8E351B43A6443FB6A",Description="根据主键锁定或解锁数据",Path="/api/news/{id}/lock/{islocked}",Method=(HttpMethod)2,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("edcd4871-7520-4437-93b1-3150c63b3486"),EnableAudit=false,Group="新闻服务",Service="新闻管理",Summary="分页查询",Key="2BE2F4CAE116170A84E87A542B7B2E03",Description="根据分页参数，分页获取数据",Path="/api/news/page/{pageindex}/{pagesize}",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)},
 new Function(){Id = Guid.Parse("1fe857c9-c027-4ca3-b8f8-21ec2c1f5cde"),EnableAudit=false,Group="系统管理",Service="审计数据服务",Summary="查询所有可以用的",Key="280713DC4618277C7BF307117835ED7B",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/audit-entity/all-usable",Method=(HttpMethod)0,CreatedTime= DateTimeOffset.FromUnixTimeSeconds(1628689405)}

            };
        }
    }
}