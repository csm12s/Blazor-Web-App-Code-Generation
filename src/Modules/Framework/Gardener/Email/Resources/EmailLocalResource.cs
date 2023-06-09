// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;

namespace Gardener.Email.Resources
{
    /// <summary>
    /// 邮件本地化资源
    /// </summary>
    public class EmailLocalResource : SharedLocalResource
    {
       
        /// <summary>
        /// 来自于邮箱
        /// </summary>
        public const string FromEmail = nameof(FromEmail);
        /// <summary>
        /// 启用SSL
        /// </summary>
        public const string EnableSsl = nameof(EnableSsl);
        /// <summary>
        /// 发件人
        /// </summary>
        public const string FromName = nameof(FromName);
        /// <summary>
        /// 目标邮箱
        /// </summary>
        public const string ToEmail = nameof(ToEmail);
        /// <summary>
        /// 邮箱服务配置
        /// </summary>
        public const string EmailServerConfig= nameof(EmailServerConfig);
        /// <summary>
        /// 邮件模板
        /// </summary>
        public const string EmailTemplate = nameof(EmailTemplate);
        /// <summary>
        /// 示例
        /// </summary>
        public const string Example = nameof(Example);
        /// <summary>
        /// 主题模板
        /// </summary>
        public const string SubjectTemplate = nameof(SubjectTemplate);
        /// <summary>
        /// 内容模板
        /// </summary>
        public const string ContentTemplate = nameof(ContentTemplate);
        /// <summary>
        /// 是HTML
        /// </summary>
        public const string IsHtml = nameof(IsHtml);
        /// <summary>
        /// 模板编号
        /// </summary>
        public const string TemplateId = nameof(TemplateId);
    }
}
