// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Base.Entity;
using Gardener.Email.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Gardener.Email.Domains
{
    /// <summary>
    /// 邮件模板信息
    /// </summary>
    public class EmailTemplate : EmailTemplateDto, IEntityBase, IEntitySeedData<EmailTemplate>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<EmailTemplate> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[] {
                new EmailTemplate{Id=Guid.Parse("90587DB9-3C8D-4EC1-80CC-FF001166FD25"),
                Name="验证码",
                FromName="园丁",
                Remark="发送验证码",
                SubjectTemplate="你好，请查收验证码",
                ContentTemplate=@"<p>您的验证码是：<b> @Model.Code </b></p>
                                  <P>时间：@(System.DateTime.Now.ToString(""yyyy-MM-dd HH:mm:ss""))</p>",
                Example="{\"Code\":123}",
                IsHtml=true,
                IsDeleted=false,
                IsLocked=false,
                CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636428834)
                }
            };
        }
    }
}
