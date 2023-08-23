using Furion.DatabaseAccessor;
using Gardener.Authentication.Enums;
using Gardener.Base.Entity;
using Gardener.Base.Enums;
using Microsoft.EntityFrameworkCore;

namespace Gardener.ToolBox.Impl.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class ToolBoxResourceSeedData : IEntitySeedData<Resource>
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
                new Resource() {Name="开发工具",Key="dev_tools",Icon="tool",Order=200,ParentId=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),SupportMultiTenant=false,Hide=false,Type=Enum.Parse<ResourceType>("Menu"),Id=Guid.Parse("b06dd4ed-7d67-40d4-8370-8d19afd23eae"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-02 16:50:02"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-03 18:20:00"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="颜色",Key="dev_tools_color",Path="/tools/colors",Icon="bg-colors",Order=10,ParentId=Guid.Parse("b06dd4ed-7d67-40d4-8370-8d19afd23eae"),SupportMultiTenant=false,Hide=false,Type=Enum.Parse<ResourceType>("Menu"),Id=Guid.Parse("7440535c-8568-4d1a-be5c-b7a93cb9d282"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-02 16:50:50"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-03 18:20:10"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="Cron",Key="dev_tools_cron",Path="/tools/cron",Icon="code",Order=20,ParentId=Guid.Parse("b06dd4ed-7d67-40d4-8370-8d19afd23eae"),SupportMultiTenant=false,Hide=false,Type=Enum.Parse<ResourceType>("Menu"),Id=Guid.Parse("ceeb4c42-06a6-4635-b94f-8ed4ee026954"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-04 10:02:10"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-04 14:32:01"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
         };
        }
    }

}
