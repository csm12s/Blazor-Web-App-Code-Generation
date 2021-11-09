// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Email.Domains
{
    /// <summary>
    /// 邮件模板信息
    /// </summary>
    [Description("邮件模板信息")]
    public class EmailTemplate : GardenerEntityBase<Guid>, IEntitySeedData<EmailTemplate>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [MaxLength(30), Required]
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("备注")]
        [MaxLength(500)]
        public string Remark { get; set; }
        /// <summary>
        /// 发件人
        /// </summary>
        [DisplayName("发件人")]
        [MaxLength(100)]
        public string FromName { get; set; }
        /// <summary>
        /// 主题模板
        /// </summary>
        [DisplayName("主题模板")]
        [MaxLength(1000)]
        public string SubjectTemplate { get; set; }
        /// <summary>
        /// 内容模板
        /// </summary>
        [DisplayName("内容模板")]
        [MaxLength(5000)]
        public string ContentTemplate { get; set; }
        /// <summary>
        /// 例子
        /// </summary>
        [DisplayName("例子")]
        [MaxLength(1000)]
        public string Example { get; set; }
        /// <summary>
        /// 是否是HTML内容
        /// </summary>
        [DisplayName("是否是HTML内容")]
        public bool IsHtml { get; set; }
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
                SubjectTemplate="@Model.User ，你好，请查收验证码",
                ContentTemplate=@"<p>你好，<span>@Model.User</span></p>
                                  <p>您的验证码是：<b> @Model.Code </b></p>
                                  <P>时间：@(System.DateTime.Now.ToString(""yyyy-MM-dd HH:mm:ss""))</p>",
                Example="{\"Code\":123,\"User\":\"张三\"}",
                IsHtml=true,
                IsDeleted=false,
                IsLocked=false,
                CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636428834)
                }
            };
        }
    }
}
