// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Gardener.Enums;
using Gardener.Base.Entity;

namespace Gardener.CodeGeneration.SeedDatas
{
    /// <summary>
    /// 接口种子数据
    /// </summary>
    public class CodeGenerationServiceFunctionSeedData : IEntitySeedData<Function>
    {
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<Function> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]{
                new Function() {Group="系统基础服务",Service="代码生成服务",Summary="更新实体的代码生成配置",Key="6AAF93FCBAC80E0FD4329B6852E1741D",Path="/api/code-generation/entity-code-generation-setting",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("a0decf1b-ed7a-4cd4-ac2f-ee85f52e6c95"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="代码生成服务",Summary="添加实体的代码生成配置",Key="FAC62F9BF65D4DD69EE5EDE973F67030",Path="/api/code-generation/entity-code-generation-setting",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("becfbc6e-e75f-4c17-a0f8-d366cc0c0ecb"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="代码生成服务",Summary="获取所有实体定义",Key="EC62EF7FF22A3D75FF0452966175ED6D",Path="/api/code-generation/entity-definitions",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("bfbcb606-6adb-460f-9730-20dbe3b32949"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="代码生成服务",Summary="获取实体的代码生成配置",Key="51CDF306434E8148436781B9BFB4D520",Path="/api/code-generation/entity-code-generation-setting/{entityfullname}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("f9feca89-9856-4c20-aa82-b2260df498a9"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
         };
        }
    }

}
