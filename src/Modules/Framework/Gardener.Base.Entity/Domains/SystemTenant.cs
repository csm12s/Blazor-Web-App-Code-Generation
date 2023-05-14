// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Base.Entity.Domains
{
    /// <summary>
    /// 租户
    /// </summary>
    [Description("租户")]
    public class SystemTenant : GardenerEntityBase<Guid>, IEntitySeedData<SystemTenant>, ITenant
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        [DisplayName("租户名称")]
        [Required, MaxLength(50)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [MaxLength(256)]
        [DisplayName("Email")]
        public string? Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(32)]
        [DisplayName("Tel")]
        public string? Tel { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [MaxLength(100)]
        public string? Remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<SystemTenant> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[] {
                new SystemTenant { Id=Guid.Parse("710148B3-0C80-48A2-8F57-4B863BE9859F"),Name = "租户1", Email = "gardener@163.com", Tel = "400-888-8888", Remark = "预设数据。" ,IsDeleted=false,IsLocked=false,CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311)},
                new SystemTenant { Id=Guid.Parse("F416B514-04C8-40CA-91A4-07C5BBF9C8C6"),Name = "租户2", Email = "gardener@163.com", Tel = "400-888-8888", Remark = "预设数据。" ,IsDeleted=false,IsLocked=false,CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311)},
            };
        }
    }
}
