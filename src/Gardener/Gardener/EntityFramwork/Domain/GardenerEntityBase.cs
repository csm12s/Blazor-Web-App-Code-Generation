// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;
using Furion.DatabaseAccessor;

namespace Gardener.EntityFramwork.Domain
{
    /// <summary>
    /// 基类
    /// </summary>
    public abstract class GardenerEntityBase<T, TDbContextLocator1, TDbContextLocator2> :
        Entity<T, TDbContextLocator1, TDbContextLocator2> where TDbContextLocator1 : class, IDbContextLocator
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
    }
    /// <summary>
    /// 基类
    /// </summary>
    public abstract class GardenerEntityBase<T, TDbContextLocator1> :
        GardenerEntityBase<T, TDbContextLocator1, MasterDbContextLocator> where TDbContextLocator1 : class, IDbContextLocator
    {}
    /// <summary>
    /// 基类
    /// </summary>
    public abstract class GardenerEntityBase<T> :
        GardenerEntityBase<T, MasterDbContextLocator>
    {}
    /// <summary>
    /// 基类
    /// </summary>
    public abstract class GardenerEntityBase :
        GardenerEntityBase<int>
    { }
}
