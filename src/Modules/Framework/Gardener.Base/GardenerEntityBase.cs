// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Authentication.Enums;
using System.ComponentModel;

namespace Gardener.Base
{
    /// <summary>
    /// 基类
    /// </summary>
    public abstract class GardenerEntityBase<TKey, TDbContextLocator1, TDbContextLocator2> :
        Entity<TKey, TDbContextLocator1, TDbContextLocator2> where TDbContextLocator1 : class, IDbContextLocator
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
        public string CreatorId { get; set; }

        /// <summary>
        /// 创建者身份类型
        /// </summary>
        [DisplayName("创建者身份类型")]
        public IdentityType CreatorIdentityType { get; set; }
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
}
