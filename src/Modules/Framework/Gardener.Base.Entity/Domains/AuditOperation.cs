// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Attributes;
using Gardener.Authentication.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Base.Entity.Domains
{
    /// <summary>
    /// 操作审计信息
    /// </summary>
    [Description("操作审计信息")]
    [IgnoreAudit]
    public class AuditOperation : GardenerTenantEntityBase<Guid, MasterDbContextLocator, GardenerMultiTenantDbContextLocator, GardenerAuditDbContextLocator>
    {
        /// <summary>
        /// 审计操作
        /// </summary>
        public AuditOperation()
        {
            AuditEntities = new List<AuditEntity>();
        }
        /// <summary>
        /// 资源名
        /// </summary>
        [DisplayName("资源名")]
        [MaxLength(100)]
        public string? ResourceName { get; set; }
        /// <summary>
        /// 资源编号
        /// </summary>
        [DisplayName("资源编号")]
        [Required]
        public Guid ResourceId { get; set; }
        /// <summary>
        /// 操作者编号
        /// </summary>
        [DisplayName("操作者编号")]
        [MaxLength(100)]
        public string? OperaterId { get; set; }
        /// <summary>
        /// 操作者名称
        /// </summary>
        [DisplayName("操作者名称")]
        [MaxLength(100)]
        public string? OperaterName { get; set; }
        /// <summary>
        /// 操作者类型
        /// </summary>
        [DisplayName("操作者类型")]
        public IdentityType OperaterType { get; set; }
        /// <summary>
        /// 访问IP
        /// </summary>
        [DisplayName("IP")]
        [MaxLength(20)]
        public string? Ip { get; set; }
        /// <summary>
        /// UserAgent
        /// </summary>
        [DisplayName("UserAgent")]
        [MaxLength(500)]
        public string? UserAgent { get; set; }
        /// <summary>
        /// 请求地址
        /// </summary>
        [DisplayName("请求地址")]
        [MaxLength(500)]
        public string? Path { get; set; }
        /// <summary>
        /// 请求方法
        /// </summary>
        [DisplayName("请求方法")]
        public Gardener.Enums.HttpMethod Method { get; set; }
        /// <summary>
        /// 请求参数
        /// </summary>
        [DisplayName("请求参数")]
        public string? Parameters { get; set; }

        /// <summary>
        /// 审计数据信息集合
        /// </summary>
        public ICollection<AuditEntity> AuditEntities { get; set; }
    }
}
