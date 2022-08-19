// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Base.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Gardener.SysTimer.Impl.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class ResourceFunctionSeedData : IEntitySeedData<ResourceFunction>
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
                new ResourceFunction() {ResourceId=Guid.Parse("2b0d5eb7-4626-4273-b124-8816259a2902"),FunctionId=Guid.Parse("588829d2-fae6-40cd-bdfa-c0758e7f89fb"),CreatedTime=DateTimeOffset.Parse("2022-08-16 14:51:43"),},
                new ResourceFunction() {ResourceId=Guid.Parse("3d93eb77-2a72-4b4f-aa79-5da1fc794300"),FunctionId=Guid.Parse("a2504e15-4b43-4a6a-bc1a-9c06effa672c"),CreatedTime=DateTimeOffset.Parse("2022-08-16 14:48:49"),},
                new ResourceFunction() {ResourceId=Guid.Parse("3f7f572f-1df2-4d20-b323-489e44196ad0"),FunctionId=Guid.Parse("4d664ef2-a462-494d-9c5c-453880f44017"),CreatedTime=DateTimeOffset.Parse("2022-08-16 14:53:29"),},
                new ResourceFunction() {ResourceId=Guid.Parse("51991266-0a62-4f8b-ab7a-3bdc48595ea0"),FunctionId=Guid.Parse("c96a611f-555b-4b96-8ee5-83a87ee03a6e"),CreatedTime=DateTimeOffset.Parse("2022-08-16 14:52:08"),},
                new ResourceFunction() {ResourceId=Guid.Parse("7c711d3c-c755-419b-827a-e8e7087984b8"),FunctionId=Guid.Parse("31e5a68d-916b-4b74-8e59-da733724b322"),CreatedTime=DateTimeOffset.Parse("2022-08-16 14:50:43"),},
                new ResourceFunction() {ResourceId=Guid.Parse("7c711d3c-c755-419b-827a-e8e7087984b8"),FunctionId=Guid.Parse("b83c620b-e964-43bb-8590-d8d32277aa00"),CreatedTime=DateTimeOffset.Parse("2022-08-16 14:50:11"),},
                new ResourceFunction() {ResourceId=Guid.Parse("a8a4b47a-7bdc-4b03-8c46-d3cd240ae8c9"),FunctionId=Guid.Parse("99c24403-1417-4c04-b1ef-0c17243215e0"),CreatedTime=DateTimeOffset.Parse("2022-08-16 14:49:22"),},
                new ResourceFunction() {ResourceId=Guid.Parse("a8a4b47a-7bdc-4b03-8c46-d3cd240ae8c9"),FunctionId=Guid.Parse("b2ffcf41-7c74-4815-a367-d55c9a536b22"),CreatedTime=DateTimeOffset.Parse("2022-08-16 14:51:19"),},
                new ResourceFunction() {ResourceId=Guid.Parse("d0d6f112-73a4-44ba-82a4-a3ad8bdb6978"),FunctionId=Guid.Parse("a2504e15-4b43-4a6a-bc1a-9c06effa672c"),CreatedTime=DateTimeOffset.Parse("2022-08-16 14:52:29"),},
                new ResourceFunction() {ResourceId=Guid.Parse("df132f66-027e-4791-af7a-26e496dc8e5a"),FunctionId=Guid.Parse("81b4bb91-1f42-4043-9acb-dac756ce729b"),CreatedTime=DateTimeOffset.Parse("2022-08-16 14:53:56"),},
                new ResourceFunction() {ResourceId=Guid.Parse("f884f2f9-f884-4440-8a2c-7a883ac54660"),FunctionId=Guid.Parse("433d4ad9-7ae0-48ea-851e-c4e594c8e19a"),CreatedTime=DateTimeOffset.Parse("2022-08-16 14:53:01"),},
                new ResourceFunction() {ResourceId=Guid.Parse("32809cde-b1bb-4076-a37b-6c9375e84aac"),FunctionId=Guid.Parse("ee8b1345-e0c0-47a7-9e85-cb0bd7ede472"),CreatedTime=DateTimeOffset.Parse("2022-08-19 18:45:07"),},
         };
        }
    }
}
