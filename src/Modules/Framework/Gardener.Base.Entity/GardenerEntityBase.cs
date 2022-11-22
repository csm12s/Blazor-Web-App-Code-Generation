// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Authentication.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Furion.DependencyInjection;

namespace Gardener.Base
{
    #region Base Entity Refactor
    /// <summary>
    /// Base Entity Refactor
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    [SuppressSniffer]
    public abstract class DBEntity<TKey, TDbContextLocator1, TDbContextLocator2> :
        PrivateDBEntityBase<TKey>
    where TDbContextLocator1 : class, IDbContextLocator
    where TDbContextLocator2 : class, IDbContextLocator
    {
    }
    /// <summary>
    /// Base Entity Refactor
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class PrivateDBEntityBase<TKey> : IPrivateEntity
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TKey Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTimeOffset CreatedTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTimeOffset? UpdatedTime { get; set; }
    }
    #endregion
    /// <summary>
    /// 基类, TODO 1: 这里改成继承DBEntity，取消对TenantId的继承
    /// 避免初始化Entity时EF反射获取不到TenantId（好像因为写了两个TenantId，一个Virtual一个重写）, 这里的写法参考Admin.Net
    /// TODO 2: 所有数据库表是不是应该加上前缀, 例如Sys_
    /// </summary>
    public abstract class GardenerEntityBase<TKey, TDbContextLocator1, TDbContextLocator2> :
        DBEntity<TKey, TDbContextLocator1, TDbContextLocator2> where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
    {
        /// <summary>
        /// 是否锁定
        /// </summary>
        [DisplayName("是否锁定")]
        public bool IsLocked { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建者编号
        /// </summary>
        [DisplayName("创建者编号")]
        public string? CreateBy { get; set; } // CreateBy / CreateUserId

        /// <summary>
        /// 修改者编号
        /// </summary>
        [DisplayName("修改者编号")]
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 创建者身份类型
        /// </summary>
        [DisplayName("创建者身份类型")]
        public IdentityType? CreateIdentityType { get; set; }


        /// <summary>
        /// 修改者身份类型
        /// </summary>
        [DisplayName("修改者身份类型")]
        public IdentityType? UpdateIdentityType { get; set; }
    }
    /// <summary>
    /// 基类
    /// </summary>
    public abstract class GardenerEntityBase<TKey, TDbContextLocator1> :
        GardenerEntityBase<TKey, TDbContextLocator1, MasterDbContextLocator> where TDbContextLocator1 : class, IDbContextLocator
    {}
    /// <summary>
    /// 基类
    /// </summary>
    public abstract class GardenerEntityBase<TKey> :
        GardenerEntityBase<TKey, MasterDbContextLocator>
    {}
    /// <summary>
    /// 基类
    /// </summary>
    public abstract class GardenerEntityBase :
        GardenerEntityBase<int>
    { }

    /// <summary>
    /// Tenant base entity
    /// </summary>
    public abstract class GardenerTenantEntityBase<TKey, TKey_TenantId> :
        GardenerEntityBase<TKey>
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        public virtual TKey_TenantId TenantId { get; set; }
    }
    /// <summary>
    /// Tenant base entity
    /// </summary>
    public abstract class GardenerTenantEntityBase :
        GardenerTenantEntityBase<int, Guid>
    {
    }
}
