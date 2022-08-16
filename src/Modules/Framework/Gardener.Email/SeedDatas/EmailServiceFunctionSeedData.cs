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

namespace Gardener.Email.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class EmailServiceFunctionSeedData : IEntitySeedData<Function>
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
                new Function() {Group="系统基础服务",Service="邮件服务",Summary="发送邮件",Key="2C72E2117E4F5092A5C6F2C807389D38",Description="发送邮件",Path="/api/email/send",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("7f36ba4f-ec97-4fa9-953b-fa2f1686c448"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
         };
        }
    }

}
