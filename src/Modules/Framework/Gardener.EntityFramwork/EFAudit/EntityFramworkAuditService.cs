// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Attributes;
using Gardener.Authentication.Core;
using Gardener.Authentication.Enums;
using Gardener.Base.Entity.Domains;
using Gardener.Common;
using Gardener.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.EntityFramwork.EFAudit
{
    /// <summary>
    /// 当前请求的审计数据管理
    /// </summary>
    public class EntityFramworkAuditService<TDbContextLocator> : IOrmAuditService where TDbContextLocator : class, IDbContextLocator
    {
        private readonly ILogger<EntityFramworkAuditService<TDbContextLocator>> _logger;
        private readonly IRepository<AuditOperation, TDbContextLocator> _auditOperationRepository;
        private readonly IRepository<AuditEntity, TDbContextLocator> _auditEntityRepository;
        private readonly IIdentityService _identityService;
        private AuditOperation? _auditOperation;
        private List<AuditEntity>? _auditEntitys;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="auditOperationRepository"></param>
        /// <param name="auditEntityRepository"></param>
        /// <param name="identityService"></param>
        public EntityFramworkAuditService(ILogger<EntityFramworkAuditService<TDbContextLocator>> logger,
            IRepository<AuditOperation, TDbContextLocator> auditOperationRepository,
            IRepository<AuditEntity, TDbContextLocator> auditEntityRepository,
            IIdentityService identityService)
        {
            _logger = logger;
            _auditOperationRepository = auditOperationRepository;
            _auditEntityRepository = auditEntityRepository;
            _identityService = identityService;
        }
        /// <summary>
        /// 保存操作审计
        /// </summary>
        /// <param name="auditOperation"></param>
        public Task SaveAuditOperation(AuditOperation auditOperation)
        {
            if (auditOperation == null)
            {
                return Task.CompletedTask;
            }
            _logger.LogDebug($"写入操作审计信息 {auditOperation.OperaterName} {auditOperation.ResourceName}");
            _auditOperation = auditOperation;
            try
            {
                return _auditOperationRepository.InsertNowAsync(auditOperation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "操作审计写入数据库异常");
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 保存实体审计数据过程
        /// </summary>
        /// <param name="entitys"></param>
        public void SavingChangesEvent(IEnumerable<EntityEntry> entitys)
        {
            try
            {
                if (entitys == null || !entitys.Any())
                {
                    return;
                }
                // 获取当前事件对应上下文
                // 获取所有实体  
                entitys = entitys.Where(w =>
               w.State == EntityState.Added || w.State == EntityState.Modified || w.State == EntityState.Deleted
                );
                if (!entitys.Any()) return;
                var user = _identityService.GetIdentity();
                List<AuditEntity> auditEntities = new List<AuditEntity>();
                foreach (var entity in entitys)
                {
                    // 获取实体的类型
                    var entityType = entity.Entity.GetType();
                    if (entityType == null) { continue; }
                    //跳过
                    if (entityType.CustomAttributes.Any(x => x.AttributeType.Equals(typeof(IgnoreAuditAttribute)))) { continue; }
                    // 获取实体当前的值
                    PropertyValues currentValues = entity.CurrentValues;
                    AuditEntity auditEntity = new AuditEntity()
                    {
                        TypeName = entityType.FullName ?? string.Empty,
                        Name = entityType.GetDescription() ?? string.Empty,
                        OperaterId = user != null ? user.Id.ToString() : string.Empty,
                        OperaterName = user != null ? user.NickName ?? user.Name : string.Empty,
                        OperaterType = user != null ? user.IdentityType : IdentityType.Unknown,
                        OperationId = Guid.NewGuid(),
                        CurrentValues = currentValues,
                        OldValues = entity.GetDatabaseValues(),
                        CreatedTime = DateTimeOffset.Now,
                        CreateBy = user?.Id,
                        CreateIdentityType = user?.IdentityType,
                        TenantId = user?.TenantId
                    };
                    switch (entity.State)
                    {
                        case EntityState.Modified: auditEntity.OperationType = EntityOperateType.Update; break;
                        case EntityState.Added: auditEntity.OperationType = EntityOperateType.Insert; break;
                        case EntityState.Deleted: auditEntity.OperationType = EntityOperateType.Delete; break;
                    }
                    //记录下变化的实体
                    auditEntities.Add(auditEntity);
                }
                _auditEntitys = auditEntities;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "审计日志异常");
            }
        }
        /// <summary>
        /// 保存实体审计数据
        /// </summary>
        /// <returns></returns>
        public void SavedChangesEvent()
        {
            if (_auditEntitys == null)
            {
                return;
            }
            try
            {
                foreach (AuditEntity entity in _auditEntitys)
                {
                    entity.OperationId = _auditOperation?.Id ?? Guid.NewGuid();
                    var (pkValues, auditProperties) = GetAuditProperties(entity.OperationType, entity.CurrentValues, entity.OldValues);
                    entity.DataId = string.Join(',', pkValues);
                    entity.AuditProperties = auditProperties;
                    if (auditProperties != null)
                    {
                        foreach (AuditProperty property in auditProperties)
                        {
                            property.CreateBy = entity.CreateBy;
                            property.CreateIdentityType = entity.CreateIdentityType;
                            property.TenantId = entity.TenantId;
                            property.CreatedTime = DateTimeOffset.Now;
                        }
                    }
                }
                _auditEntityRepository.InsertNow(_auditEntitys);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "数据审计日志入库异常");

            }
        }
        /// <summary>
        /// 获取属性审计信息
        /// </summary>
        /// <param name="operationType"></param>
        /// <param name="currentValues"></param>
        /// <param name="originalValues"></param>
        /// <returns></returns>
        private (List<object>, ICollection<AuditProperty>) GetAuditProperties(EntityOperateType operationType, PropertyValues currentValues, PropertyValues? originalValues)
        {
            ICollection<AuditProperty> auditProperties = new List<AuditProperty>();
            List<object> pkValues = new List<object>();

            // 获取实体的所有属性，排除【NotMapper】属性
            var props = currentValues.Properties;
            // 遍历所有的属性
            foreach (var prop in props)
            {
                //不需要审计
                if (prop.PropertyInfo == null || prop.PropertyInfo.CustomAttributes.Any(x => x.AttributeType.Equals(typeof(IgnoreAuditAttribute)))) continue;
                // 获取属性值
                string propName = prop.Name;
                // 获取属性当前的值
                var newValue = currentValues[propName];

                //添加的时候，空值字段就不记录了
                if (EntityOperateType.Insert.Equals(operationType) && string.IsNullOrEmpty(ValueToString(newValue))) continue;
                object? oldValue = null;
                if (originalValues != null)
                {
                    oldValue = originalValues[propName];
                }
                //是主键
                IKey? pk = prop.FindContainingPrimaryKey();
                if (pk != null && (newValue != null || oldValue != null))
                {
                    if (newValue != null)
                    {
                        pkValues.Add(newValue);
                    }
                    else if (oldValue != null)
                    {
                        pkValues.Add(oldValue);
                    }

                }
                //更新的话需对比到底有没有变化
                if (operationType.Equals(EntityOperateType.Update) &&
                        (
                            newValue == null && oldValue == null
                            ||
                            newValue != null && newValue.Equals(oldValue)
                        )
                    ) continue;
                var property = new AuditProperty()
                {
                    DisplayName = prop.PropertyInfo.GetDescription() ?? string.Empty,
                    FieldName = propName,
                    OriginalValue = ValueToString(oldValue),
                    CreatedTime = DateTimeOffset.Now
                };
                if (!operationType.Equals(EntityOperateType.Delete))
                {
                    property.NewValue = ValueToString(newValue);
                }
                Type fieldType = prop.PropertyInfo.PropertyType;
                if (fieldType.IsNullableType())
                {
                    property.DataType = fieldType.GetGenericArguments()[0].Name;
                }
                else
                {
                    property.DataType = prop.PropertyInfo.PropertyType.Name;
                }
                auditProperties.Add(property);
            }

            return (pkValues, auditProperties);
        }
        /// <summary>
        /// 格式化各种类型的值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string? ValueToString(object? value)
        {
            if (value == null) return null;
            if (value is DateTime)
            {
                if (value.Equals(DateTime.MinValue)) return null;
            }
            else if (value is DateTimeOffset)
            {
                if (value.Equals(DateTimeOffset.MinValue)) return null;
            }
            else if (value is Guid)
            {
                if (value.Equals(Guid.Empty)) return null;
            }
            else if (value.GetType().IsSubclassOf(typeof(Enum)))
            {
                //枚举展示的是Description或名字
                return EnumHelper.GetEnumDescriptionOrName((Enum)value);
            }
            return value.ToString();

        }
    }
}
