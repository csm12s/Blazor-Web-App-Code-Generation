// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Base.Entity.Domains;
using Gardener.Base.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Base.Entity
{
    /// <summary>
    /// 资源表
    /// </summary>
    [Description("资源信息")]
    public class Resource : GardenerEntityBase<Guid, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntityTypeBuilder<Resource, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
        /// <summary>
        /// 资源信息
        /// </summary>
        public Resource()
        {
            ResourceFunctions = new List<ResourceFunction>();
        }

        /// <summary>
        /// 资源名称
        /// </summary>
        [Required, MaxLength(100)]
        [DisplayName("名称")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 资源名称简写-唯一
        /// 内部鉴权使用
        /// </summary>
        [Required, MaxLength(100)]
        [DisplayName("唯一标示")]
        public string Key { get; set; } = null!;

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        [DisplayName("备注")]
        public string? Remark { get; set; }

        /// <summary>
        /// 资源地址 菜单：页面路由地址
        /// </summary>
        [MaxLength(200)]
        [DisplayName("路径")]
        public string? Path { get; set; }

        /// <summary>
        /// 资源图标
        /// </summary>
        [MaxLength(50)]
        [DisplayName("图标")]
        public string? Icon { get; set; }

        /// <summary>
        /// 资源排序
        /// </summary>
        [Required, DefaultValue(0)]
        [DisplayName("排序")]
        public int Order { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        [DisplayName("父级编号")]
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 支持多租户
        /// </summary>
        [DisplayName("SupportMultiTenant")]
        public bool SupportMultiTenant { get; set; }

        /// <summary>
        /// 父级
        /// </summary>
        public Resource? Parent { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public ICollection<Resource>? Children { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        [Required, DefaultValue(ResourceType.Menu)]
        public ResourceType Type { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<ResourceFunction> ResourceFunctions { get; set; } = new List<ResourceFunction>();

        /// <summary>
        /// 租户资源关系
        /// </summary>
        public List<SystemTenantResource> TenantResources { get; set; } = new List<SystemTenantResource>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(EntityTypeBuilder<Resource> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex(x => x.Key);
        }
    }
}