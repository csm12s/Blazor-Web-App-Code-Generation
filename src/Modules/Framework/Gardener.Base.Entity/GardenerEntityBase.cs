// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using System.ComponentModel;
using Gardener.Base.Entity;
using Furion.DependencyInjection;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Base
{

    /*
     * 主键+预置字段 继承这些：
     * GardenerEntityBase
     * GardenerEntityBase<TKey>
     * GardenerEntityBase<TKey,TDbContextLocator1,...>
     * 
     * 主键 继承这些：
     * GardenerEntityBaseEmpty
     * GardenerEntityBaseEmpty<TKey>
     * GardenerEntityBaseEmpty<TKey,TDbContextLocator1,...>
     *  
     *  
     * 预置字段 继承这些：
     * GardenerEntityBaseNoKey 
     * GardenerEntityBaseNoKey<TDbContextLocator1,...>
     *
     * 不需要任何 继承这些：
     * GardenerEntityBaseNoKeyAndEmpty 
     * GardenerEntityBaseNoKeyAndEmpty<TDbContextLocator1,...>
     * 
     * 
     * 多租户+主键+预置字段 继承这些：
     * GardenerTenantEntityBase
     * GardenerTenantEntityBase<TKey>
     * GardenerTenantEntityBase<TKey,TDbContextLocator1,...>
     * 
     * 多租户+主键 继承这些：
     * GardenerTenantEntityBaseEmpty
     * GardenerTenantEntityBaseEmpty<TKey>
     * GardenerTenantEntityBaseEmpty<TKey,TDbContextLocator1,...>
     *  
     *  
     * 多租户+预置字段 继承这些：
     * GardenerTenantEntityBaseNoKey 
     * GardenerTenantEntityBaseNoKey<TDbContextLocator1,...>
     *
     * 多租户 继承这些：
     * GardenerTenantEntityBaseNoKeyAndEmpty 
     * GardenerTenantEntityBaseNoKeyAndEmpty<TDbContextLocator1,...>
     *
     */

    #region Entity基础信息 基础此类只能继承属性和字段，还需实现 IDBEntityBase 接口
    /// <summary>
    /// Entity基础预置信息
    /// </summary>
    /// <remarks>
    /// 基础此类只能继承属性和字段，还需实现 IEntityBase 接口
    /// </remarks>
    [SuppressSniffer]
    public abstract class EntityBaseInfoNoKey : BaseDto
    {

    }
    /// <summary>
    /// 一个主键的Entity基础信息
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <remarks>
    /// 基础此类只能继承属性和字段，还需实现 IEntityBase 接口
    /// </remarks>
    [SuppressSniffer]
    public abstract class EntityBaseInfo<TKey> : EntityBaseInfoNoKey, IModelId<TKey>
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TKey Id { get; set; } = default!;
    }
    /// <summary>
    /// 一个主键且没有其他字段的Entity基础信息
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <remarks>
    /// 基础此类只能继承属性和字段，还需实现 IEntityBase 接口
    /// </remarks>
    [SuppressSniffer]
    public abstract class EntityBaseInfoEmpty<TKey> : IModelId<TKey>
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TKey Id { get; set; } = default!;

    }
    /// <summary>
    /// 多租户Entity基础信息
    /// </summary>
    /// <remarks>
    /// 基础此类只能继承属性和字段，还需实现 IEntityBase 接口
    /// </remarks>
    [SuppressSniffer]
    public abstract class TenantEntityBaseInfoNoKey : EntityBaseInfoNoKey, IModelTenant
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        [DisplayName("租户编号")]
        public virtual Guid? TenantId { get; set; }

        /// <summary>
        /// 租户
        /// </summary>
        [DisplayName("Tenant")]
        [NotMapped]
        public virtual ITenant? Tenant { get; set; }
    }
    /// <summary>
    /// 多租户空Entity基础信息
    /// </summary>
    /// <remarks>
    /// 基础此类只能继承属性和字段，还需实现 IEntityBase 接口
    /// </remarks>
    [SuppressSniffer]
    public abstract class TenantEntityBaseInfoNoKeyAndEmpty : IModelTenant
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        [DisplayName("租户编号")]
        public virtual Guid? TenantId { get; set; }

        /// <summary>
        /// 租户
        /// </summary>
        [DisplayName("Tenant")]
        [NotMapped]
        public virtual ITenant? Tenant { get; set; }
    }
    /// <summary>
    /// 多租户一个主键的Entity基础信息
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <remarks>
    /// 基础此类只能继承属性和字段，还需实现 IEntityBase 接口
    /// </remarks>
    [SuppressSniffer]
    public abstract class TenantEntityBaseInfo<TKey> : TenantEntityBaseInfoNoKey, IModelId<TKey>
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TKey Id { get; set; } = default!;
    }
    /// <summary>
    /// 多租户一个主键且没有其他字段的Entity基础信息
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <remarks>
    /// 基础此类只能继承属性和字段，还需实现 IEntityBase 接口
    /// </remarks>
    [SuppressSniffer]
    public abstract class TenantEntityBaseInfoEmpty<TKey> : TenantEntityBaseInfoNoKeyAndEmpty, IModelId<TKey>
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TKey Id { get; set; } = default!;
    }

    #endregion


    #region 无主键基类

    #region 普通-无主键
    /// <summary>
    /// 无主键基类
    /// </summary>
    [SuppressSniffer]
    public abstract class GardenerEntityBaseNoKey :
        EntityBaseInfoNoKey,
        IEntityBase<MasterDbContextLocator>
    { }
    /// <summary>
    /// 无主键基类
    /// </summary>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <remarks>
    /// 无主键基类,包含预置字段<see cref="EntityBaseInfoNoKey"/>
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerEntityBaseNoKey<TDbContextLocator1> :
        EntityBaseInfoNoKey,
        IEntityBase<TDbContextLocator1>
        where TDbContextLocator1 : class, IDbContextLocator
    { }
    /// <summary>
    /// 无主键基类
    /// </summary>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <remarks>
    /// 无主键基类,包含预置字段<see cref="EntityBaseInfoNoKey"/>
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerEntityBaseNoKey<TDbContextLocator1, TDbContextLocator2> :
        EntityBaseInfoNoKey,
        IEntityBase<TDbContextLocator1, TDbContextLocator2>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
    { }
    /// <summary>
    /// 无主键基类
    /// </summary>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <typeparam name="TDbContextLocator3"></typeparam>
    /// <remarks>
    /// 无主键基类,包含预置字段<see cref="EntityBaseInfoNoKey"/>
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerEntityBaseNoKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> :
        EntityBaseInfoNoKey,
        IEntityBase<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
    { }
    /// <summary>
    /// 无主键基类
    /// </summary>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <typeparam name="TDbContextLocator3"></typeparam>
    /// <typeparam name="TDbContextLocator4"></typeparam>
    /// <remarks>
    /// 无主键基类,包含预置字段<see cref="EntityBaseInfoNoKey"/>
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerEntityBaseNoKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4> :
        EntityBaseInfoNoKey,
        IEntityBase<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator3>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
    { }
    #endregion

    #region 普通-无主键-无内置字段
    /// <summary>
    /// 无主键空基类
    /// </summary>
    /// <remarks>
    /// 无主键空基类，不包含任何内容
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerEntityBaseNoKeyAndEmpty :
        IEntityBase<MasterDbContextLocator>
    { }
    /// <summary>
    /// 无主键基类
    /// </summary>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <remarks>
    /// 无主键空基类，不包含任何内容
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerEntityBaseNoKeyAndEmpty<TDbContextLocator1> :
        IEntityBase<TDbContextLocator1>
        where TDbContextLocator1 : class, IDbContextLocator
    { }
    /// <summary>
    /// 无主键基类
    /// </summary>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <remarks>
    /// 无主键空基类，不包含任何内容
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerEntityBaseNoKeyAndEmpty<TDbContextLocator1, TDbContextLocator2> :
        IEntityBase<TDbContextLocator1, TDbContextLocator2>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
    { }
    /// <summary>
    /// 无主键基类
    /// </summary>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <typeparam name="TDbContextLocator3"></typeparam>
    /// <remarks>
    /// 无主键空基类，不包含任何内容
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerEntityBaseNoKeyAndEmpty<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> :
        IEntityBase<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
    { }
    /// <summary>
    /// 无主键基类
    /// </summary>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <typeparam name="TDbContextLocator3"></typeparam>
    /// <typeparam name="TDbContextLocator4"></typeparam>
    /// <remarks>
    /// 无主键空基类，不包含任何内容
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerEntityBaseNoKeyAndEmpty<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4> :
        IEntityBase<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator3>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
    { }
    #endregion

    #region 多租户-无主键
    /// <summary>
    /// 多租户无主键基类
    /// </summary>
    /// <remarks>
    /// 多租户无主键基类,包含预置字段<see cref="TenantEntityBaseInfo"/>
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerTenantEntityBaseNoKey :
        TenantEntityBaseInfoNoKey,
        IEntityBase<MasterDbContextLocator>
    { }
    /// <summary>
    /// 多租户无主键基类
    /// </summary>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <remarks>
    /// 多租户无主键基类,包含预置字段<see cref="TenantEntityBaseInfo"/>
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerTenantEntityBaseNoKey<TDbContextLocator1> :
        TenantEntityBaseInfoNoKey,
        IEntityBase<TDbContextLocator1>
        where TDbContextLocator1 : class, IDbContextLocator
    { }
    /// <summary>
    /// 多租户无主键基类
    /// </summary>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <remarks>
    /// 多租户无主键基类,包含预置字段<see cref="TenantEntityBaseInfo"/>
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerTenantEntityBaseNoKey<TDbContextLocator1, TDbContextLocator2> :
        TenantEntityBaseInfoNoKey,
        IEntityBase<TDbContextLocator1, TDbContextLocator2>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
    { }
    /// <summary>
    /// 多租户无主键基类
    /// </summary>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <typeparam name="TDbContextLocator3"></typeparam>
    /// <remarks>
    /// 多租户无主键基类,包含预置字段<see cref="TenantEntityBaseInfo"/>
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerTenantEntityBaseNoKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> :
        TenantEntityBaseInfoNoKey,
        IEntityBase<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
    { }
    /// <summary>
    /// 多租户无主键基类
    /// </summary>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <typeparam name="TDbContextLocator3"></typeparam>
    /// <typeparam name="TDbContextLocator4"></typeparam>
    /// <remarks>
    /// 多租户无主键基类,包含预置字段<see cref="TenantEntityBaseInfo"/>
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerTenantEntityBaseNoKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4> :
        TenantEntityBaseInfoNoKey,
        IEntityBase<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator3>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
    { }
    #endregion

    #region 多租户-无主键-无内置字段
    /// <summary>
    /// 多租户无主键基类
    /// </summary>
    /// <remarks>
    /// 多租户无主键基类,包含预置字段<see cref="TenantEntityBaseInfoNoKeyAndEmpty"/>
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerTenantEntityBaseNoKeyAndEmpty :
        TenantEntityBaseInfoNoKeyAndEmpty,
        IEntityBase<MasterDbContextLocator>
    { }
    /// <summary>
    /// 多租户无主键基类
    /// </summary>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <remarks>
    /// 多租户无主键基类,包含预置字段<see cref="TenantEntityBaseInfoNoKeyAndEmpty"/>
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerTenantEntityBaseNoKeyAndEmpty<TDbContextLocator1> :
        TenantEntityBaseInfoNoKeyAndEmpty,
        IEntityBase<TDbContextLocator1>
        where TDbContextLocator1 : class, IDbContextLocator
    { }
    /// <summary>
    /// 多租户无主键基类
    /// </summary>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <remarks>
    /// 多租户无主键基类,包含预置字段<see cref="TenantEntityBaseInfoNoKeyAndEmpty"/>
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerTenantEntityBaseNoKeyAndEmpty<TDbContextLocator1, TDbContextLocator2> :
        TenantEntityBaseInfoNoKeyAndEmpty,
        IEntityBase<TDbContextLocator1, TDbContextLocator2>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
    { }
    /// <summary>
    /// 多租户无主键基类
    /// </summary>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <typeparam name="TDbContextLocator3"></typeparam>
    /// <remarks>
    /// 多租户无主键基类,包含预置字段<see cref="TenantEntityBaseInfoNoKeyAndEmpty"/>
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerTenantEntityBaseNoKeyAndEmpty<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> :
        TenantEntityBaseInfoNoKeyAndEmpty,
        IEntityBase<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
    { }
    /// <summary>
    /// 多租户无主键基类
    /// </summary>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <typeparam name="TDbContextLocator3"></typeparam>
    /// <typeparam name="TDbContextLocator4"></typeparam>
    /// <remarks>
    /// 多租户无主键基类,包含预置字段<see cref="TenantEntityBaseInfoNoKeyAndEmpty"/>
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerTenantEntityBaseNoKeyAndEmpty<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4> :
        TenantEntityBaseInfoNoKeyAndEmpty,
        IEntityBase<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator3>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
    { }
    #endregion

    #endregion

    #region 单主键

    #region 普通-单主键
    /// <summary>
    /// 单主键基类
    /// </summary>
    /// <remarks>
    /// 单主键<see cref="int"/>基类,包含预置字段<see cref="EntityBaseInfoNoKey"/>
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerEntityBase :
        EntityBaseInfo<int>,
        IEntityBase<MasterDbContextLocator>
    { }
    /// <summary>
    /// 单主键基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <remarks>
    /// 单主键<see cref="TKey"/>基类,包含预置字段<see cref="EntityBaseInfoNoKey"/>
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerEntityBase<TKey> :
        EntityBaseInfo<TKey>,
        IEntityBase<MasterDbContextLocator>
    { }
    /// <summary>
    /// 单主键基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <remarks>
    /// 单主键<see cref="TKey"/>基类,包含预置字段<see cref="EntityBaseInfoNoKey"/>
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerEntityBase<TKey, TDbContextLocator1> :
        EntityBaseInfo<TKey>,
        IEntityBase<TDbContextLocator1>
        where TDbContextLocator1 : class, IDbContextLocator
    { }
    /// <summary>
    /// 单主键基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <remarks>
    /// 单主键<see cref="TKey"/>基类,包含预置字段<see cref="EntityBaseInfoNoKey"/>
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerEntityBase<TKey, TDbContextLocator1, TDbContextLocator2> :
        EntityBaseInfo<TKey>,
        IEntityBase<TDbContextLocator1, TDbContextLocator2>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
    { }
    /// <summary>
    /// 单主键基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <typeparam name="TDbContextLocator3"></typeparam>
    /// <remarks>
    /// 单主键<see cref="TKey"/>基类,包含预置字段<see cref="EntityBaseInfoNoKey"/>
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerEntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> :
        EntityBaseInfo<TKey>,
        IEntityBase<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
    { }
    /// <summary>
    /// 单主键基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <typeparam name="TDbContextLocator3"></typeparam>
    /// <typeparam name="TDbContextLocator4"></typeparam>
    /// <remarks>
    /// 单主键<see cref="TKey"/>基类,包含预置字段<see cref="EntityBaseInfoNoKey"/>
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerEntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4> :
        EntityBaseInfo<TKey>,
        IEntityBase<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator3>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
    { }
    #endregion

    #region 普通-单主键基类-无内置字段
    /// <summary>
    /// 单主键基类-无内置字段
    /// </summary>
    /// <remarks>
    /// 单主键<see cref="int"/>基类，无预置字段
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerEntityBaseEmpty :
        EntityBaseInfoEmpty<int>,
        IEntityBase<MasterDbContextLocator>
    { }
    /// <summary>
    /// 单主键基类-无内置字段
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <remarks>
    /// 单主键<see cref="TKey"/>基类，无预置字段
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerEntityBaseEmpty<TKey> :
        EntityBaseInfoEmpty<TKey>,
        IEntityBase<MasterDbContextLocator>
    { }
    /// <summary>
    /// 单主键基类-无内置字段
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <remarks>
    /// 单主键<see cref="TKey"/>基类，无预置字段
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerEntityBaseEmpty<TKey, TDbContextLocator1> :
        EntityBaseInfoEmpty<TKey>,
        IEntityBase<TDbContextLocator1>
        where TDbContextLocator1 : class, IDbContextLocator
    { }
    /// <summary>
    /// 单主键基类-无内置字段
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <remarks>
    /// 单主键<see cref="TKey"/>基类，无预置字段
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerEntityBaseEmpty<TKey, TDbContextLocator1, TDbContextLocator2> :
        EntityBaseInfoEmpty<TKey>,
        IEntityBase<TDbContextLocator1, TDbContextLocator2>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
    { }
    /// <summary>
    /// 单主键基类-无内置字段
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <typeparam name="TDbContextLocator3"></typeparam>
    /// <remarks>
    /// 单主键<see cref="TKey"/>基类，无预置字段
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerEntityBaseEmpty<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> :
        EntityBaseInfoEmpty<TKey>,
        IEntityBase<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
    { }
    /// <summary>
    /// 单主键基类-无内置字段
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <typeparam name="TDbContextLocator3"></typeparam>
    /// <typeparam name="TDbContextLocator4"></typeparam>
    /// <remarks>
    /// 单主键<see cref="TKey"/>基类，无预置字段
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerEntityBaseEmpty<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4> :
        EntityBaseInfoEmpty<TKey>,
        IEntityBase<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator3>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
    { }
    #endregion

    #region 多租户-单主键
    /// <summary>
    /// 多租户单主键基类
    /// </summary>
    /// <remarks>
    /// 多租户单主键<see cref="int"/>基类，包含预置字段<see cref="TenantEntityBaseInfoNoKey"/>
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerTenantEntityBase :
        TenantEntityBaseInfo<int>,
        IEntityBase<MasterDbContextLocator>
    { }
    /// <summary>
    /// 多租户单主键基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <remarks>
    /// 多租户单主键<see cref="TKey"/>基类，包含预置字段<see cref="TenantEntityBaseInfoNoKey"/>
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerTenantEntityBase<TKey> :
        TenantEntityBaseInfo<TKey>,
        IEntityBase<MasterDbContextLocator>
    { }
    /// <summary>
    /// 多租户单主键基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <remarks>
    /// 多租户单主键<see cref="TKey"/>基类，包含预置字段<see cref="TenantEntityBaseInfoNoKey"/>
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerTenantEntityBase<TKey, TDbContextLocator1> :
        TenantEntityBaseInfo<TKey>,
        IEntityBase<TDbContextLocator1>
        where TDbContextLocator1 : class, IDbContextLocator
    { }
    /// <summary>
    /// 多租户单主键基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <remarks>
    /// 多租户单主键<see cref="TKey"/>基类，包含预置字段<see cref="TenantEntityBaseInfoNoKey"/>
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerTenantEntityBase<TKey, TDbContextLocator1, TDbContextLocator2> :
        TenantEntityBaseInfo<TKey>,
        IEntityBase<TDbContextLocator1, TDbContextLocator2>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
    { }
    /// <summary>
    /// 多租户单主键基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <typeparam name="TDbContextLocator3"></typeparam>
    /// <remarks>
    /// 多租户单主键<see cref="TKey"/>基类，包含预置字段<see cref="TenantEntityBaseInfoNoKey"/>
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerTenantEntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> :
        TenantEntityBaseInfo<TKey>,
        IEntityBase<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
    { }
    /// <summary>
    /// 多租户单主键基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <typeparam name="TDbContextLocator3"></typeparam>
    /// <typeparam name="TDbContextLocator4"></typeparam>
    /// <remarks>
    /// 多租户单主键<see cref="TKey"/>基类，包含预置字段<see cref="TenantEntityBaseInfoNoKey"/>
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerTenantEntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4> :
        TenantEntityBaseInfo<TKey>,
        IEntityBase<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator3>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
    { }
    #endregion

    #region 多租户-单主键-无内置字段
    /// <summary>
    /// 多租户单主键无内置字段基类
    /// </summary>
    /// <remarks>
    /// 多租户单主键<see cref="int"/>基类，不包含预置字段
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerTenantEntityBaseEmpty :
        TenantEntityBaseInfoEmpty<int>,
        IEntityBase<MasterDbContextLocator>
    { }
    /// <summary>
    /// 多租户单主键无内置字段基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <remarks>
    /// 多租户单主键<see cref="TKey"/>基类，不包含预置字段
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerTenantEntityBaseEmpty<TKey, TDbContextLocator1> :
        TenantEntityBaseInfoEmpty<TKey>,
        IEntityBase<TDbContextLocator1>
        where TDbContextLocator1 : class, IDbContextLocator
    { }
    /// <summary>
    /// 多租户单主键无内置字段基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <remarks>
    /// 多租户单主键<see cref="TKey"/>基类，不包含预置字段
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerTenantEntityBaseEmpty<TKey, TDbContextLocator1, TDbContextLocator2> :
        TenantEntityBaseInfoEmpty<TKey>,
        IEntityBase<TDbContextLocator1, TDbContextLocator2>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
    { }
    /// <summary>
    /// 多租户单主键无内置字段基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <typeparam name="TDbContextLocator3"></typeparam>
    /// <remarks>
    /// 多租户单主键<see cref="TKey"/>基类，不包含预置字段
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerTenantEntityBaseEmpty<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> :
        TenantEntityBaseInfoEmpty<TKey>,
        IEntityBase<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
    { }
    /// <summary>
    /// 多租户单主键无内置字段基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <typeparam name="TDbContextLocator3"></typeparam>
    /// <typeparam name="TDbContextLocator4"></typeparam>
    /// <remarks>
    /// 多租户单主键<see cref="TKey"/>基类，不包含预置字段
    /// </remarks>
    [SuppressSniffer]
    public abstract class GardenerTenantEntityBaseEmpty<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4> :
        TenantEntityBaseInfoEmpty<TKey>,
        IEntityBase<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator3>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
    { }
    #endregion

    #endregion
}
