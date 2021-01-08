// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DatabaseAccessor;
using Gardener.Common;
using Gardener.Core;
using Gardener.Core.Audit;
using Gardener.Core.Entites;
using Gardener.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Gardener.EntityFramwork.Core.DbContexts
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    [AppDbContext("GardenerSqlite3ConnectionString")]
    public class GardenerDbContext : AppDbContext<GardenerDbContext>
    {
        public GardenerDbContext(DbContextOptions<GardenerDbContext> options) : base(options)
        {
        }
        /// <summary>
        /// 解决sqlite 不支持datetimeoffset问题（损失很小的精度）
        /// https://blog.dangl.me/archive/handling-datetimeoffset-in-sqlite-with-entity-framework-core/
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                // SQLite does not have proper support for DateTimeOffset via Entity Framework Core, see the limitations
                // here: https://docs.microsoft.com/en-us/ef/core/providers/sqlite/limitations#query-limitations
                // To work around this, when the Sqlite database provider is used, all model properties of type DateTimeOffset
                // use the DateTimeOffsetToBinaryConverter
                // Based on: https://github.com/aspnet/EntityFrameworkCore/issues/10784#issuecomment-415769754
                // This only supports millisecond precision, but should be sufficient for most use cases.
                foreach (var entityType in builder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(DateTimeOffset)
                                                                                || p.PropertyType == typeof(DateTimeOffset?));
                    foreach (var property in properties)
                    {
                        builder
                            .Entity(entityType.Name)
                            .Property(property.Name)
                            .HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
                }
            }
        }
        /// <summary>
        /// 数据保存
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        protected override void SavingChangesEvent(DbContextEventData eventData, InterceptionResult<int> result)
        {
            IAuditDataManager auditDataManager = App.GetService<IAuditDataManager>();
            if (auditDataManager == null) return;
            ILogger<GardenerDbContext> _logger = App.GetService<ILogger<GardenerDbContext>>();
            try
            {
                // 获取当前事件对应上下文
                var dbContext = eventData.Context;
                // 获取所有实体  
                var entitys = dbContext.ChangeTracker.Entries().Where(w =>
                w.Entity.GetType() != typeof(AuditEntity)
                && w.Entity.GetType() != typeof(AuditOperation)
                && w.Entity.GetType() != typeof(AuditProperty)
                && w.Entity.GetType() != typeof(UserToken)
                && (w.State == EntityState.Added || w.State == EntityState.Modified || w.State == EntityState.Deleted)
                );
                if (!entitys.Any()) return;
                var user = App.GetService<IAuthorizationManager>().GetUser();
                List<AuditEntity> auditEntities = new List<AuditEntity>();
                foreach (var entity in entitys)
                {
                    // 获取实体的类型
                    var entityType = entity.Entity.GetType();
                    // 获取实体当前的值
                    var currentValues = entity.CurrentValues;
                    AuditEntity auditEntity = new AuditEntity();
                    auditEntity.TypeName = entityType.FullName;
                    auditEntity.Name = entityType.GetDescription();
                    auditEntity.OperaterId = user != null ? user.Id.ToString() : null;
                    auditEntity.OperaterName = user != null ? (user.NickName ?? user.UserName) : null;
                    auditEntity.OperationId = Guid.NewGuid();
                    auditEntity.CurrentValues = currentValues;
                    auditEntity.OldValues = entity.GetDatabaseValues();
                    switch (entity.State)
                    {
                        case EntityState.Modified: auditEntity.OperationType = OperationType.Update; break;
                        case EntityState.Added: auditEntity.OperationType = OperationType.Add; break;
                        case EntityState.Deleted: auditEntity.OperationType = OperationType.Delete; break;
                    }

                    //添加的时候需要在保存结束时设置主键值和属性变更结果
                    //if (!EntityState.Added.Equals(entity.State))
                    //{
                    //    var (pkValues, auditProperties) = GetAuditProperties(auditEntity.OperationType, currentValues, entity.GetDatabaseValues());
                    //    auditEntity.DataId = string.Join(',', pkValues);
                    //    auditEntity.AuditProperties = auditProperties;
                    //}
                    auditEntities.Add(auditEntity);
                }
                auditDataManager.SetAuditEntitys(auditEntities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "审计日志异常");

            }
        }
        /// <summary>
        /// 数据保存后
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        protected override void SavedChangesEvent(SaveChangesCompletedEventData eventData, int result)
        {
            IAuditDataManager auditDataManager = App.GetService<IAuditDataManager>();
            if (auditDataManager == null) return;
            ILogger<GardenerDbContext> _logger = App.GetService<ILogger<GardenerDbContext>>();
            try
            {
                List<AuditEntity> auditEntitys = auditDataManager.GetAuditEntities();
                if (auditEntitys == null) return;

                foreach (var entity in auditEntitys)
                {
                    //if (!entity.OperationType.Equals(OperationType.Add)) continue;
                    var (pkValues, auditProperties) = GetAuditProperties(entity.OperationType, entity.CurrentValues, entity.OldValues);
                    entity.DataId = string.Join(',', pkValues);
                    entity.AuditProperties = auditProperties;
                }
                auditDataManager.SaveAuditEntitys(auditEntitys);
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
        private (List<object> ,ICollection<AuditProperty>) GetAuditProperties(OperationType operationType,PropertyValues currentValues, PropertyValues originalValues)
        {
            ICollection<AuditProperty> auditProperties = new List<AuditProperty>();
            List<object> pkValues = new List<object>();

            // 获取实体的所有属性，排除【NotMapper】属性
            var props = currentValues.Properties;
            // 遍历所有的属性
            foreach (var prop in props)
            {
                // 获取属性值
                var propName = prop.Name;
                // 获取属性当前的值
                var newValue = currentValues[propName];
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
                if (operationType.Equals(OperationType.Update) && 
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
                    OriginalValue = oldValue == null ? null : oldValue.ToString()
                };
                if (!operationType.Equals(OperationType.Delete))
                {
                    property.NewValue = newValue == null ? null : newValue.ToString();
                }
                Type fieldType = prop.PropertyInfo.PropertyType;
                if (fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(Nullable<>))
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
    }
}
