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

namespace Gardener.VerifyCode.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class ImageVerifyCodeServiceFunctionSeedData : IEntitySeedData<Function>
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
                new Function() {Group="系统基础服务",Service="图片验证码服务",Summary="移除验证码",Key="715A826DCD331B3155650A79BE0015D8",Path="/api/image-verify-code/{key}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("138283bd-f2ee-4b3b-b268-a12185264103"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="图片验证码服务",Summary="验证验证码",Key="A3F388958310A592E004DDD848AB0CB7",Path="/api/image-verify-code/verify",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("4795ae43-0d52-42f1-8aaf-fc6e6412ac1b"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="图片验证码服务",Summary="获取验证码",Key="BA7BDB4454250C19379AD4FABE7A58B6",Path="/api/image-verify-code",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("d7f59d52-a931-4bec-8312-5142d4d37fda"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
         };
        }
    }

}
