// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Gardener.Common
{
    /// <summary>
    /// 生成种子数据工具
    /// </summary>
    public static class SeedDataGenerateTool
    {
        /// <summary>
        /// 生成种子数据
        /// </summary>
        /// <param name="request"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        /// <remarks>
        /// 根据搜索条叫生成种子数据
        /// </remarks>
        public static string Generate<TEntity>(IEnumerable<TEntity> datas,string? entityName=null)
        {
            if (datas==null || !datas.Any())
            {
                return string.Empty;
            }
            entityName = entityName ?? typeof(TEntity).Name;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("/// <summary>");
            sb.AppendLine("/// 种子数据");
            sb.AppendLine("/// </summary>");
            sb.AppendLine($"public class {entityName}SeedData : IEntitySeedData<{entityName}>");
            sb.AppendLine("{");
            sb.AppendLine("     /// <summary>");
            sb.AppendLine("     /// 种子数据");
            sb.AppendLine("     /// </summary>");
            sb.AppendLine("     /// <param name=\"dbContext\"></param>");
            sb.AppendLine("     /// <param name=\"dbContextLocator\"></param>");
            sb.AppendLine("     /// <returns></returns>");
            sb.AppendLine($"    public IEnumerable<{entityName}> HasData(DbContext dbContext, Type dbContextLocator)");
            sb.AppendLine("     {");
            sb.AppendLine("         return new[]{");

            foreach (var item in datas)
            {
                if (item == null) { continue; }
                Type type = item.GetType();
                PropertyInfo[] properties = type.GetProperties();
                sb.Append($"                new {entityName}()");
                sb.Append(" {");
                foreach (PropertyInfo property in properties)
                {
                    string propertyName = property.Name;
                    var propertyType = property.PropertyType.GetUnNullableType();
                    Object? value = item.GetPropertyValue(propertyName);
                    if (value == null)
                    {
                        continue;
                    }

                    if (propertyType.Equals(typeof(string)) || propertyType.Equals(typeof(char)))
                    {
                        sb.Append($"{propertyName}=\"{value}\"");
                    }
                    else if (propertyType.Equals(typeof(Guid)))
                    {
                        sb.Append($"{propertyName}=Guid.Parse(\"{value}\")");
                    }
                    else if (propertyType.Equals(typeof(short))
                       || propertyType.Equals(typeof(int))
                       || propertyType.Equals(typeof(long))
                       || propertyType.Equals(typeof(float))
                       || propertyType.Equals(typeof(double))
                       || propertyType.Equals(typeof(decimal))
                       || propertyType.Equals(typeof(byte))
                       || propertyType.Equals(typeof(bool))
                       )
                    {
                        sb.Append($"{propertyName}={(value.ToString() ?? "").ToLower()}");
                    }
                    else if (propertyType.Equals(typeof(DateTimeOffset)) || propertyType.Equals(typeof(DateTime)))
                    {
                        if (value != null)
                        {
                            DateTimeOffset time = (DateTimeOffset)value;
                            sb.Append($"{propertyName}={typeof(DateTimeOffset).Name}.Parse(\"{time.ToString("yyyy-MM-dd HH:mm:ss")}\")");
                        }
                    }
                    else if (propertyType.IsEnum)
                    {
                        sb.Append($"{propertyName}=Enum.Parse<{propertyType.Name}>(\"{value.ToString()}\")");
                    }
                    else
                    {
                        continue;
                    }
                    sb.Append(",");

                }
                sb.Append("}");
                sb.Append(",");
                sb.AppendLine("");
            }
            sb.AppendLine("         };");
            sb.AppendLine("     }");
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
