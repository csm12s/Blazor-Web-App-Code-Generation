// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using System.ComponentModel;
using Gardener.Base.Entity;
using Furion.DependencyInjection;

namespace Gardener.Base
{
    #region 无主键
    /// <summary>
    /// 无主键基类
    /// </summary>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    [SuppressSniffer]
    public abstract class GardenerEntityBaseNoKey<TDbContextLocator1, TDbContextLocator2> :
        DBEntityBase<TDbContextLocator1, TDbContextLocator2> where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
    {}
    /// <summary>
    /// 无主键基类
    /// </summary>
    [SuppressSniffer]
    public abstract class GardenerEntityBaseNoKey<TDbContextLocator1> :
        GardenerEntityBaseNoKey<TDbContextLocator1, MasterDbContextLocator> where TDbContextLocator1 : class, IDbContextLocator
    {}
    /// <summary>
    /// 无主键基类
    /// </summary>
    [SuppressSniffer]
    public abstract class GardenerEntityBaseNoKey :
        GardenerEntityBaseNoKey<MasterDbContextLocator>
    {}

    /// <summary>
    /// 多租户无主键基类
    /// </summary>
    [SuppressSniffer]
    public abstract class GardenerTenantEntityBaseNoKey :
        GardenerEntityBaseNoKey
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        [DisplayName("租户编号")]
        public virtual Guid? TenantId { get; set; }
    }
    #endregion

    #region 单主键
    /// <summary>
    /// 单主键基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <remarks>
    /// 应该是最常用的基类
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerEntityBase<TKey, TDbContextLocator1, TDbContextLocator2> :
        DBEntityBase<TKey, TDbContextLocator1, TDbContextLocator2> where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
    {
    }
    /// <summary>
    /// 单主键基类
    /// </summary>
    [SuppressSniffer]
    public abstract class GardenerEntityBase<TKey, TDbContextLocator1> :
        GardenerEntityBase<TKey, TDbContextLocator1, MasterDbContextLocator> where TDbContextLocator1 : class, IDbContextLocator
    {}
    /// <summary>
    /// 单主键基类
    /// </summary>
    [SuppressSniffer]
    public abstract class GardenerEntityBase<TKey> :
        GardenerEntityBase<TKey, MasterDbContextLocator>
    {}
    /// <summary>
    /// 单主键基类
    /// </summary>
    [SuppressSniffer]
    public abstract class GardenerEntityBase :
        GardenerEntityBase<int>
    { }

    /// <summary>
    /// 多租户单主键基类
    /// </summary>
    [SuppressSniffer]
    public abstract class GardenerTenantEntityBase<TKey> :
        GardenerEntityBase<TKey>
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        [DisplayName("租户编号")]
        public virtual Guid? TenantId { get; set; }
    }
    /// <summary>
    /// 多租户单主键基类
    /// </summary>
    [SuppressSniffer]
    public abstract class GardenerTenantEntityBase :
        GardenerTenantEntityBase<int>
    {
    }
    #endregion
}
