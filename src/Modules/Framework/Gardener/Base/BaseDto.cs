// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Attributes;
using Gardener.Authentication.Dtos;
using Gardener.Authentication.Enums;
using System;
using System.ComponentModel;

namespace Gardener.Base
{
    /// <summary>
    /// dto基础类
    /// </summary>
    public abstract class BaseDtoEmptyNoKey
    {
    }

    /// <summary>
    /// dto基础类
    /// </summary>
    public abstract class BaseDtoEmpty<TKey> : BaseDtoEmptyNoKey, IModelId<TKey>
    {
        /// <summary>
        /// 编号
        /// </summary>
        [DisplayName("Id")]
        public TKey Id { get; set; } = default!;
    }

    /// <summary>
    /// dto基础类
    /// </summary>
    public abstract class BaseDto : BaseDtoEmptyNoKey, IModelCreated, IModelLocked, IModelDeleted, IModelUpdated
    {
        /// <summary>
        /// 是否锁定
        /// </summary>
        [DisplayName("IsLocked")]
        public virtual bool IsLocked { get; set; }
        /// <summary>
        /// 是否逻辑删除
        /// </summary>
        [DisplayName("IsDeleted")]
        [DisabledSearchField]
        public virtual bool IsDeleted { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [DisplayName("CreatedTime")]
        public virtual DateTimeOffset CreatedTime { get; set; }
        /// <summary>
        /// 创建者编号
        /// </summary>
        [DisplayName("CreateBy")]
        public virtual string? CreateBy { get; set; }
        /// <summary>
        /// 创建者身份类型
        /// </summary>
        [DisplayName("CreateIdentityType")]
        public virtual IdentityType? CreateIdentityType { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        [DisplayName("UpdatedTime")]
        public virtual DateTimeOffset? UpdatedTime { get; set; }
        /// <summary>
        /// 修改者编号
        /// </summary>
        [DisplayName("UpdateBy")]
        public virtual string? UpdateBy { get; set; }
        /// <summary>
        /// 修改者身份类型
        /// </summary>
        [DisplayName("UpdateIdentityType")]
        public virtual IdentityType? UpdateIdentityType { get; set; }

        /// <summary>
        /// 设置创建者身份
        /// </summary>
        /// <param name="identity"></param>
        public void SetCreatedIdentity(Identity identity)
        {
            this.CreateBy = identity.Id;
            this.CreateIdentityType = identity.IdentityType;
        }
        /// <summary>
        /// 设置更新者身份
        /// </summary>
        /// <param name="identity"></param>
        public void SetUpdatedIdentity(Identity identity)
        {
            this.UpdateBy = identity.Id;
            this.UpdateIdentityType = identity.IdentityType;
        }
    }


    /// <summary>
    /// dto基础类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseDto<TKey> : BaseDto, IModelId<TKey>
    {
        /// <summary>
        /// 编号
        /// </summary>
        [DisplayName("Id")]
        public TKey Id { get; set; } = default!;

    }


    /// <summary>
    /// 多租户Dto基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class TenantBaseDtoEmpty<TKey> : BaseDtoEmpty<TKey>, IModelTenantId
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        [DisplayName("TenantId")]
        public virtual Guid? TenantId { get; set; }
    }
    /// <summary>
    /// 多租户Dto基类
    /// </summary>
    public abstract class TenantBaseDtoEmptyNoKey : BaseDtoEmptyNoKey, IModelTenantId
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        [DisplayName("TenantId")]
        public virtual Guid? TenantId { get; set; }
    }

    /// <summary>
    /// 多租户Dto基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class TenantBaseDto<TKey> : BaseDto<TKey>, IModelTenantId
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        [DisplayName("TenantId")]
        public virtual Guid? TenantId { get; set; }
    }
    /// <summary>
    /// 多租户Dto基类
    /// </summary>
    public abstract class TenantBaseDtoNoKey : BaseDto, IModelTenantId
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        [DisplayName("TenantId")]
        public virtual Guid? TenantId { get; set; }
    }
}
