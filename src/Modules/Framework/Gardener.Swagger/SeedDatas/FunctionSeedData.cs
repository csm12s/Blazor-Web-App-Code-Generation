// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Gardener.Base.Domains;
using Gardener.Enums;

namespace Gardener.Swagger.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class FunctionSeedData : IEntitySeedData<Function>
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
                new Function() {Group="系统基础服务",Service="Swagger服务",Summary="解析api json",Key="7E9057E559FB68353DCA5D208B7B2A71",Description="swagger json 文件解析功能",Path="/api/swagger/analysis/{url}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("8d94c826-ddba-47fe-94c9-333880fee187"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:00:45"),},
                new Function() {Group="系统基础服务",Service="Swagger服务",Summary="获取 swagger 配置",Key="945B6A21E0C00F9BB0F7EEE37C671E3E",Description="获取api分组设置",Path="/api/swagger/api-group",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("a53a9c89-7968-4598-9c46-dad4e9188bd0"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:00:46"),},
                new Function() {Group="系统基础服务",Service="Swagger服务",Summary="从json中获取function",Key="187E0857A128187E01EFBBD569C3DE92",Path="/api/swagger/functions-from-json/{url}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("c591c0ca-3305-4684-89bb-278218d13c47"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:00:47"),},
         };
        }
    }

}
