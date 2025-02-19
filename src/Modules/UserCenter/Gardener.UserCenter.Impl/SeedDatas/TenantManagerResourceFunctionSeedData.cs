﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Base.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Gardener.UserCenter.Impl.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class TenantManagerResourceFunctionSeedData : IEntitySeedData<ResourceFunction>
    {
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<ResourceFunction> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]{
                new ResourceFunction() {ResourceId=Guid.Parse("07af05b1-6f3e-49fa-9959-463e246346df"),FunctionId=Guid.Parse("22f82d98-9176-4391-a950-1d1d01e105fd"),CreatedTime=DateTimeOffset.Parse("2023-05-10 16:16:49"),},
                new ResourceFunction() {ResourceId=Guid.Parse("4db9a237-1343-4c4a-91f6-9a40fb9f0e2a"),FunctionId=Guid.Parse("a9ec32a6-286b-4fe4-b69e-79cca6243e00"),CreatedTime=DateTimeOffset.Parse("2023-05-10 16:12:37"),},
                new ResourceFunction() {ResourceId=Guid.Parse("8b2007b4-821b-49fc-aa5d-35ebc4dbe3c9"),FunctionId=Guid.Parse("687b278e-aa4e-4d1c-95fd-f148eeb2a658"),CreatedTime=DateTimeOffset.Parse("2023-05-10 16:10:03"),},
                new ResourceFunction() {ResourceId=Guid.Parse("b4072d45-f643-4bdb-a63e-7286cfa9c62b"),FunctionId=Guid.Parse("22f82d98-9176-4391-a950-1d1d01e105fd"),CreatedTime=DateTimeOffset.Parse("2023-05-10 16:16:22"),},
                new ResourceFunction() {ResourceId=Guid.Parse("b4072d45-f643-4bdb-a63e-7286cfa9c62b"),FunctionId=Guid.Parse("74a1a6b4-4559-48fc-8987-3287e5a4e056"),CreatedTime=DateTimeOffset.Parse("2023-05-10 16:14:56"),},
                new ResourceFunction() {ResourceId=Guid.Parse("d45effb9-67a8-4459-83ac-c3852c8b4f1f"),FunctionId=Guid.Parse("8db56ca8-b0e3-45c6-9c73-15893dd616f6"),CreatedTime=DateTimeOffset.Parse("2023-05-10 16:08:56"),},
                new ResourceFunction() {ResourceId=Guid.Parse("d92268ec-6b51-4514-9487-52cb3fb0d850"),FunctionId=Guid.Parse("a6939609-708f-4231-a726-6da1903ef69a"),CreatedTime=DateTimeOffset.Parse("2023-05-10 16:23:10"),},
                new ResourceFunction() {ResourceId=Guid.Parse("efbcc18b-c193-42cc-b315-cde07f51b496"),FunctionId=Guid.Parse("cbdf4c22-b54f-4943-b28e-2bb563720fa2"),CreatedTime=DateTimeOffset.Parse("2023-05-10 16:07:38"),},
                new ResourceFunction() {ResourceId=Guid.Parse("62e874c8-d286-4b28-831b-90d0c49f0908"),FunctionId=Guid.Parse("0837b06f-e1d7-4ed4-9204-0c237aac6978"),CreatedTime=DateTimeOffset.Parse("2023-05-11 17:19:01"),},
                new ResourceFunction() {ResourceId=Guid.Parse("62e874c8-d286-4b28-831b-90d0c49f0908"),FunctionId=Guid.Parse("22f82d98-9176-4391-a950-1d1d01e105fd"),CreatedTime=DateTimeOffset.Parse("2023-05-11 17:19:01"),},
                new ResourceFunction() {ResourceId=Guid.Parse("62e874c8-d286-4b28-831b-90d0c49f0908"),FunctionId=Guid.Parse("cb4fbf67-4747-43d5-a355-7300f007cce3"),CreatedTime=DateTimeOffset.Parse("2023-05-11 17:19:01"),},
                new ResourceFunction() {ResourceId=Guid.Parse("3a2c9195-9a5c-42c7-b5dc-7300bbc66e8c"),FunctionId=Guid.Parse("23cd301c-ebbb-4715-a96d-72232d9a6927"),CreatedTime=DateTimeOffset.Parse("2023-05-16 18:11:08"),},
                new ResourceFunction() {ResourceId=Guid.Parse("3a2c9195-9a5c-42c7-b5dc-7300bbc66e8c"),FunctionId=Guid.Parse("e38c1619-0f84-4e55-81c2-0f47992ee33d"),CreatedTime=DateTimeOffset.Parse("2023-05-16 18:11:27"),},
                new ResourceFunction() {ResourceId=Guid.Parse("807029ec-be10-4faa-a332-bcb1021ff966"),FunctionId=Guid.Parse("8663473b-4a13-46ce-8187-a850215151fe"),CreatedTime=DateTimeOffset.Parse("2023-05-16 18:11:56"),},
         };
        }
    }

}
