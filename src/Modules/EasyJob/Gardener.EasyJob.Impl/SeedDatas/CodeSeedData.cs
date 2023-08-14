// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Authentication.Enums;
using Gardener.Base.Entity;
using Microsoft.EntityFrameworkCore;

namespace Gardener.EasyJob.Impl.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class CodeSeedData : IEntitySeedData<Code>
    {
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<Code> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]{
                new Code() {CodeTypeId=4,CodeValue="1",CodeName="Today",Order=0,Id=12,IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-13 13:05:05"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-14 14:54:29"),UpdateBy="3",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Code() {CodeTypeId=4,CodeValue="7",CodeName="A week",Order=5,Id=13,IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-13 13:05:27"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-14 14:54:36"),UpdateBy="3",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Code() {CodeTypeId=4,CodeValue="30",CodeName="A month",Order=10,Id=14,IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-13 13:05:44"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-14 14:54:42"),UpdateBy="3",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Code() {CodeTypeId=4,CodeValue="180",CodeName="Half a year",Order=15,Id=15,IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-13 13:06:09"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-14 14:54:48"),UpdateBy="3",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Code() {CodeTypeId=4,CodeValue="365",CodeName="A year",Order=20,Id=16,IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-13 13:07:44"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-14 14:54:57"),UpdateBy="3",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
         };
        }
    }
}
