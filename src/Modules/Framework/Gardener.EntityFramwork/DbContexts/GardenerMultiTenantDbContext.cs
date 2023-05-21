// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DatabaseAccessor;
using Gardener.Base;
using Gardener.Base.Entity;
using Gardener.EntityFramwork.Core;
using Gardener.EntityFramwork.EFAudit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Gardener.EntityFramwork.DbContexts
{
    /// <summary>
    /// 多租户数据库上下文
    /// </summary>
    [AppDbContext("Default")]
    public class GardenerMultiTenantDbContext : AppDbContext<GardenerMultiTenantDbContext, GardenerMultiTenantDbContextLocator>, IModelBuilderFilter
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="options"></param>
        public GardenerMultiTenantDbContext(DbContextOptions<GardenerMultiTenantDbContext> options) : base(options)
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
            var context = eventData.Context;
            if (context == null) { return; }

            //基础数据初始化
            GlobalEntityEntryHandle.Handle(context.ChangeTracker.Entries(), true);
            IOrmAuditService ormAuditService = App.GetService<IOrmAuditService>();
            ormAuditService.SavingChangesEvent(context.ChangeTracker.Entries());
        }

        /// <summary>
        /// 数据保存后
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        protected override void SavedChangesEvent(SaveChangesCompletedEventData eventData, int result)
        {
            IOrmAuditService ormAuditService = App.GetService<IOrmAuditService>();
            ormAuditService.SavedChangesEvent();
        }
        /// <summary>
        /// 获取当前用户租户编号
        /// </summary>
        /// <returns></returns>
        public Guid GetTenantId()
        {
            return IdentityUtil.GetIdentity()?.TenantId ?? Guid.Empty;
        }
        /// <summary>
        /// 查询时添加租户编号条件
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnCreating(ModelBuilder modelBuilder, EntityTypeBuilder entityBuilder, DbContext dbContext, Type dbContextLocator)
        {

            LambdaExpression? expression = this.BuildTenantQueryFilter(entityBuilder, dbContext, nameof(IModelTenantId.TenantId));
            entityBuilder.HasQueryFilter(expression);

        }


        /// <summary>
        /// 构建基于表租户查询过滤器表达式
        /// </summary>
        /// <param name="entityBuilder">实体类型构建器</param>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="onTableTenantId">多租户Id属性名</param>
        /// <returns>表达式</returns>
        protected override LambdaExpression? BuildTenantQueryFilter(EntityTypeBuilder entityBuilder, DbContext dbContext, string onTableTenantId)
        {
            // 获取实体构建器元数据
            var metadata = entityBuilder.Metadata;
            if (metadata.FindProperty(onTableTenantId) == null) return default;
            //设置索引，方便查询
            entityBuilder.HasIndex(nameof(IModelTenantId.TenantId));
            MethodInfo? method = dbContext.GetType().GetMethod(nameof(GetTenantId));
            if (method == null) return default;
            // 创建表达式元素
            var parameter = Expression.Parameter(metadata.ClrType, "u");
            var properyName = Expression.Constant(onTableTenantId);
            var propertyValue = Expression.Call(Expression.Constant(dbContext), method);
            //当前租户编号如果为空认为不需要限制
            var expressionBody1 = Expression.Equal(Expression.Constant(Guid.Empty), propertyValue);
            var expressionBody2 = Expression.Equal(Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(Guid) }, parameter, properyName), propertyValue);
            var expression = Expression.Lambda(Expression.Or(expressionBody1, expressionBody2), parameter);
            return expression;
        }

        /// <summary>
        /// 
        /// </summary>
        public override Tenant Tenant
        {
            get
            {
                return new Tenant() { TenantId = GetTenantId() };
            }
        }
    }
}
