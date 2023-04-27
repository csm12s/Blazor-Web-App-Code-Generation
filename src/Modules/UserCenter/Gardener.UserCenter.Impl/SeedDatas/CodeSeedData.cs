// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Authentication.Enums;
using Gardener.Base.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Gardener.UserCenter.Impl.SeedDatas
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
                new Code() {CodeTypeId=3,CodeValue="p1",CodeName="P1",Order=10,Id=7,IsLocked=false,IsDeleted=false,CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-25 09:30:30"),},
                new Code() {CodeTypeId=3,CodeValue="p2",CodeName="P2",Order=20,Id=8,IsLocked=false,IsDeleted=false,CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-25 09:30:50"),},
                new Code() {CodeTypeId=3,CodeValue="p3",CodeName="P3",Order=30,Id=9,IsLocked=false,IsDeleted=false,CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-25 09:31:03"),},
                new Code() {CodeTypeId=3,CodeValue="p4",CodeName="P4",Order=40,Id=10,IsLocked=false,IsDeleted=false,CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-25 09:31:22"),},
                new Code() {CodeTypeId=3,CodeValue="p5",CodeName="P5",Order=50,Id=11,IsLocked=false,IsDeleted=false,CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-25 09:31:36"),},
                new Code() {CodeTypeId=2,CodeValue="pingpang",CodeName="乒乓球",Order=10,Id=3,IsLocked=false,IsDeleted=false,CreateBy="6",UpdateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-24 18:10:35"),UpdatedTime=DateTimeOffset.Parse("2023-04-24 18:11:29"),},
                new Code() {CodeTypeId=2,CodeValue="basketball",CodeName="篮球",Order=20,Id=4,IsLocked=false,IsDeleted=false,CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-24 18:11:18"),},
                new Code() {CodeTypeId=2,CodeValue="swimming",CodeName="游泳",Order=30,Id=5,IsLocked=false,IsDeleted=false,CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-24 18:11:58"),},
                new Code() {CodeTypeId=2,CodeValue="football",CodeName="足球",Order=40,Id=6,IsLocked=false,IsDeleted=false,CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-24 18:13:04"),},
                new Code() {CodeTypeId=1,CodeValue="1",CodeName="难过",Order=1,Id=1,IsLocked=false,IsDeleted=false,CreateBy="2",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-10 15:41:07"),},
                new Code() {CodeTypeId=1,CodeValue="2",CodeName="高兴",Order=2,Id=2,IsLocked=false,IsDeleted=false,CreateBy="2",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-10 15:40:54"),},
         };
        }
    }
}
