// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Authentication.Enums;
using Gardener.Base.Domains;
using Gardener.Base.Enums;
using Microsoft.EntityFrameworkCore;

namespace Gardener.SystemManager.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class ResourceManagerResourceSeedData : IEntitySeedData<Resource>
    {
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<Resource> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]{
                new Resource() {Name="资源管理",Key="system_manager_resource",Remark="",Path="/system_manager/resource",Icon="menu",Order=30,ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Type=Enum.Parse<ResourceType>("Menu"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),},
                new Resource() {Name="添加资源",Key="system_manager_resource_add",Remark="添加资源",Path="",Icon="",Order=0,ParentId=Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("8ab307de-ad4b-462f-b61d-7f1d53b82f3d"),},
                new Resource() {Name="添加子资源",Key="system_manager_resource_add_children",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("c18d4928-35d2-4085-aec9-379d00bcfd8f"),},
                new Resource() {Name="删除资源",Key="system_manager_resource_delete",Remark="删除资源",Path="",Icon="",Order=0,ParentId=Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("97a7d440-b7fe-4af6-a8a1-18846c48828b"),},
                new Resource() {Name="删除选中",Key="system_manager_resource_delete_selected",Remark="删除选中",Path="",Icon="",Order=0,ParentId=Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("08ae2764-e551-45d2-9da7-49648481a8e0"),},
                new Resource() {Name="查看资源",Key="system_manager_resource_detail",Remark="查看资源",Path="",Icon="",Order=0,ParentId=Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("d83c05a0-4d23-4b2b-ba87-284793bf3eba"),},
                new Resource() {Name="导出种子数据",Key="system_manager_resource_download_seed_data",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("859aa714-67c7-4414-bc96-9de5b7aec2c4"),},
                new Resource() {Name="编辑资源",Key="system_manager_resource_edit",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("dec04485-3dab-4251-b7b8-1044e749a51e"),},
                new Resource() {Name="显示可选接口",Key="system_manager_resource_function_add_page_show",Remark="显示可选接口",Path="",Icon="",Order=0,ParentId=Guid.Parse("e252c0c6-0f19-4768-954c-c0d83fb96d74"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("c4991844-d3b4-4f9a-9c90-c13114515796"),},
                new Resource() {Name="绑定资源接口关系",Key="system_manager_resource_function_binding",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("e252c0c6-0f19-4768-954c-c0d83fb96d74"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("ca1d4b3a-336b-40a5-b683-0fe0bcbabaf8"),},
                new Resource() {Name="删除选中资源接口关系",Key="system_manager_resource_function_delete_selected",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("e252c0c6-0f19-4768-954c-c0d83fb96d74"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("4f943ed1-997a-485f-9b54-9824b4ac285c"),},
                new Resource() {Name="获取种子数据",Key="system_manager_resource_function_download_seed_data",Order=0,ParentId=Guid.Parse("e252c0c6-0f19-4768-954c-c0d83fb96d74"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorId="1",CreatorIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2022-08-16 17:58:18"),Id=Guid.Parse("64346edf-1390-4a90-bc63-93f322ed6c8f"),},
                new Resource() {Name="锁定资源",Key="system_manager_resource_lock",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("a1958e51-06d4-4b29-9533-eae9d86c41d1"),},
                new Resource() {Name="刷新资源",Key="system_manager_resource_refresh",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("365fc5c4-404e-408a-88dc-7614dffad91b"),},
                new Resource() {Name="关联资源接口",Key="system_manager_resource_show_function",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("e252c0c6-0f19-4768-954c-c0d83fb96d74"),},
                new Resource() {Name="显示已关联接口",Key="system_manager_resource_show_function_1",Remark="显示已关联接口",Path="",Icon="",Order=0,ParentId=Guid.Parse("e252c0c6-0f19-4768-954c-c0d83fb96d74"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("4af87acd-64b4-4d53-8043-cd7ab6b03c77"),},
         };
        }
    }


}
