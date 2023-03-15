// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Authentication.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Furion.DependencyInjection;

namespace Gardener.Base.Entity
{
    #region Base Entity Refactor

    /// <summary>
    /// Base Entity Refactor
    /// </summary>
    /// <remarks>
    /// 只要实现 IPrivateEntity 接口，就回被认为是Entity
    /// </remarks>
    [SuppressSniffer]
    public abstract class DBEntityBaseEmpty : IPrivateEntity
    {
    }

    /// <summary>
    /// Base Entity Refactor
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    [SuppressSniffer]
    public abstract class DBEntityBaseEmpty<TKey> : DBEntityBaseEmpty
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TKey Id { get; set; } = default!;

    }

    /// <summary>
    /// Base Entity Refactor
    /// </summary>
    [SuppressSniffer]
    public abstract class DBEntityBase : DBEntityBaseEmpty
    {
        /// <summary>
        /// 是否锁定
        /// </summary>
        [DisplayName("是否锁定")]
        public virtual bool IsLocked { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// 创建者编号
        /// </summary>
        [DisplayName("创建者编号")]
        public virtual string? CreateBy { get; set; }

        /// <summary>
        /// 修改者编号
        /// </summary>
        [DisplayName("修改者编号")]
        public virtual string? UpdateBy { get; set; }

        /// <summary>
        /// 创建者身份类型
        /// </summary>
        [DisplayName("创建者身份类型")]
        public virtual IdentityType? CreateIdentityType { get; set; }

        /// <summary>
        /// 修改者身份类型
        /// </summary>
        [DisplayName("修改者身份类型")]
        public virtual IdentityType? UpdateIdentityType { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public virtual DateTimeOffset CreatedTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [DisplayName("更新时间")]
        public virtual DateTimeOffset? UpdatedTime { get; set; }
    }

    /// <summary>
    /// Base Entity Refactor
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <remarks>
    /// </remarks>
    [SuppressSniffer]
    public abstract class DBEntityBase<TKey> : DBEntityBase
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TKey Id { get; set; }=default!;

    }

    /// <summary>
    /// Base Entity Refactor
    /// </summary>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <remarks>
    /// </remarks>
    [SuppressSniffer]
    public abstract class DBEntityBase<TDbContextLocator1, TDbContextLocator2> :
        DBEntityBase
    where TDbContextLocator1 : class, IDbContextLocator
    where TDbContextLocator2 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Base Entity Refactor
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <remarks>
    /// </remarks>
    [SuppressSniffer]
    public abstract class DBEntityBase<TKey, TDbContextLocator1, TDbContextLocator2> :
        DBEntityBase<TKey>
    where TDbContextLocator1 : class, IDbContextLocator
    where TDbContextLocator2 : class, IDbContextLocator
    {
    }

    #endregion
}
