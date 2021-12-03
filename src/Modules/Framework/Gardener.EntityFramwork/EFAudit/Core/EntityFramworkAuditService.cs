// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DatabaseAccessor;
using Gardener.Attributes;
using Gardener.Authentication.Enums;
using Gardener.Authorization.Core;
using Gardener.Common;
using Gardener.EntityFramwork.Audit.Domains;
using Gardener.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.EntityFramwork.Audit.Core
{
    /// <summary>
    /// 当前请求的审计数据管理
    /// </summary>
    public class EntityFramworkAuditService<TDbContextLocator> : IOrmAuditService where TDbContextLocator:class, IDbContextLocator
    {
        private readonly ILogger<EntityFramworkAuditService<TDbContextLocator>> _logger;
        private readonly IRepository<AuditOperation, TDbContextLocator> _auditOperationRepository;
        private readonly IRepository<AuditEntity, TDbContextLocator> _auditEntityRepository;
        private AuditOperation _auditOperation;
        private List<AuditEntity> _auditEntitys;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="auditOperationRepository"></param>
        /// <param name="auditEntityRepository"></param>
        public EntityFramworkAuditService(ILogger<EntityFramworkAuditService<TDbContextLocator>> logger,
            IRepository<AuditOperation, TDbContextLocator> auditOperationRepository,
            IRepository<AuditEntity, TDbContextLocator> auditEntityRepository)
        {
            _logger = logger;
            _auditOperationRepository = auditOperationRepository;
            _auditEntityRepository = auditEntityRepository;
        }
        /// <summary>
        /// 保存操作审计
        /// </summary>
        /// <param name="auditOperation"></param>
        public async Task SaveAuditOperation(AuditOperation auditOperation)
        {
            if (auditOperation == null) return;
            _logger.LogDebug($"写入操作审计信息 {auditOperation.OperaterName} {auditOperation.ResourceName}");
            _auditOperation = auditOperation;
            try
            {
               await _auditOperationRepository.InsertNowAsync(auditOperation);
            }
            catch (Exception ex)
            {
               _logger.LogError(ex, "操作审计写入数据库异常");
            }
        }

        /// <summary>
        /// 保存实体审计数据
        /// </summary>
        /// <param name="auditEntitys"></param>
        /// <returns></returns>
        private async Task SaveAuditEntitys(List<AuditEntity> auditEntitys)
        {
            if (auditEntitys == null) return;
            if (this._auditOperation != null) 
            {
                auditEntitys.ForEach(x => {
                    x.OperationId = _auditOperation.Id;
                });
            }
            try
            {
               _auditEntityRepository.InsertNow(auditEntitys);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "实体审计写入数据库异常");
            }
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
               (w.State == EntityState.Added || w.State == EntityState.Modified || w.State == EntityState.Deleted)
                );
                if (!entitys.Any()) return;
                var user = App.GetService<IAuthorizationService>().GetIdentity();
                List<AuditEntity> auditEntities = new List<AuditEntity>();
                foreach (var entity in entitys)
                {
                    // 获取实体的类型
                    var entityType = entity.Entity.GetType();
                    //跳过
                    if (entityType.CustomAttributes.Any(x => x.AttributeType.Equals(typeof(IgnoreAuditAttribute)))) { continue; }
                    // 获取实体当前的值
                    var currentValues = entity.CurrentValues;
                    AuditEntity auditEntity = new AuditEntity();
                    auditEntity.TypeName = entityType.FullName;
                    auditEntity.Name = entityType.GetDescription();
                    auditEntity.OperaterId = user != null ? user.Id.ToString() : null;
                    auditEntity.OperaterName = user != null ? (user.GivenName ?? user.GivenName) : null;
                    auditEntity.OperaterType = user != null ? user.IdentityType : IdentityType.Unknown;
                    auditEntity.OperationId = Guid.NewGuid();
                    auditEntity.CurrentValues = currentValues;
                    auditEntity.OldValues = entity.GetDatabaseValues();
                    auditEntity.CreatedTime = DateTimeOffset.Now;
                    switch (entity.State)
                    {
                        case EntityState.Modified: auditEntity.OperationType = EntityOperationType.Update; break;
                        case EntityState.Added: auditEntity.OperationType = EntityOperationType.Add; break;
                        case EntityState.Deleted: auditEntity.OperationType = EntityOperationType.Delete; break;
                    }
                    //记录下变化的实体
                    auditEntities.Add(auditEntity);
                }
                this._auditEntitys = auditEntities;
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
        public async Task SavedChangesEvent()
        {
            try
            {
                List<AuditEntity> auditEntitys = _auditEntitys;
                if (auditEntitys == null) return;

                foreach (var entity in auditEntitys)
                {
                    var (pkValues, auditProperties) = GetAuditProperties(entity.OperationType, entity.CurrentValues, entity.OldValues);
                    entity.DataId = string.Join(',', pkValues);
                    entity.AuditProperties = auditProperties;
                }
                await SaveAuditEntitys(auditEntitys);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "审计日志异常");

            }
        }
        /// <summary>
        /// 获取属性审计信息
        /// </summary>
        /// <param name="operationType"></param>
        /// <param name="currentValues"></param>
        /// <param name="originalValues"></param>
        /// <returns></returns>
        private (List<object>, ICollection<AuditProperty>) GetAuditProperties(EntityOperationType operationType, PropertyValues currentValues, PropertyValues originalValues)
        {
            ICollection<AuditProperty> auditProperties = new List<AuditProperty>();
            List<object> pkValues = new List<object>();

            // 获取实体的所有属性，排除【NotMapper】属性
            var props = currentValues.Properties;
            // 遍历所有的属性
            foreach (var prop in props)
            {
                //不需要审计
                if (prop.PropertyInfo.CustomAttributes.Any(x => x.AttributeType.Equals(typeof(IgnoreAuditAttribute)))) continue;
                // 获取属性值
                var propName = prop.Name;
                // 获取属性当前的值
                var newValue = currentValues[propName];

                //添加的时候，空值字段就不记录了
                if (EntityOperationType.Add.Equals(operationType) && string.IsNullOrEmpty(ValueToString(newValue))) continue;
                object oldValue = null;
                if (originalValues != null)
                {
                    oldValue = originalValues[propName];
                }
                //是主键
                IKey pk = prop.FindContainingPrimaryKey();
                if (pk != null && (newValue != null || oldValue != null))
                {
                    pkValues.Add(newValue ?? oldValue);
                }
                //更新的话需对比到底有没有变化
                if (operationType.Equals(EntityOperationType.Update) &&
                        (
                            (newValue == null && oldValue == null)
                            ||
                            (newValue != null && newValue.Equals(oldValue))
                        )
                    ) continue;
                var property = new AuditProperty()
                {
                    DisplayName = prop.PropertyInfo.GetDescription(),
                    FieldName = propName,
                    OriginalValue = ValueToString(oldValue),
                    CreatedTime = DateTimeOffset.Now
                };
                if (!operationType.Equals(EntityOperationType.Delete))
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
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ValueToString(Object value)
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
                //枚举展示的是Description
                var des = EnumHelper.GetEnumDescription((Enum)value);
                return des ?? value.ToString();
            }
            return value.ToString();

        }
    }
}
