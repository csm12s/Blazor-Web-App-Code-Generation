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

namespace Gardener.VerifyCode.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class EmailVerifyCodeServiceFunctionSeedData : IEntitySeedData<Function>
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
                new Function() {Group="系统基础服务",Service="邮件验证码服务",Summary="移除验证码",Key="88BF07EAB2CA231DE36CF2C1A2D2546D",Path="/api/email-verify-code/{key}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("3896ea42-a5ed-4bc5-8dc5-21e0e5adb2fa"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:01:44"),},
                new Function() {Group="系统基础服务",Service="邮件验证码服务",Summary="获取验证码",Key="E23CF3B8D86A5D0E1F13759117676687",Path="/api/email-verify-code",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("ddeeea7e-09e3-42c1-b536-0ff16393db1c"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:01:43"),},
                new Function() {Group="系统基础服务",Service="邮件验证码服务",Summary="验证验证码",Key="1113744E52468C0ED06582D699F77B87",Path="/api/email-verify-code/verify",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("e466c648-4dc5-4ca4-b8f9-826c51b2a462"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:01:45"),},
         };
        }
    }


}
