// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Text.RegularExpressions;

namespace Gardener.Common;

/// <summary>
/// 代码生成工具
/// </summary>
public class CodeGenUtil
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbType"></param>
    /// <returns></returns>
    public static string GetNetTypeBySystemType(string dbType)
    {
        if (string.IsNullOrEmpty(dbType)) return "";
        if (dbType.StartsWith("System.Nullable"))
            dbType = new Regex(@"(?i)(?<=\[)(.*)(?=\])").Match(dbType).Value; // 中括号[]里面的值

        switch (dbType)
        {
            case "System.Guid": return "Guid";
            case "System.String": return "string";
            case "System.Int32": return "int";
            case "System.Int64": return "long";
            case "System.Single": return "float";
            case "System.Double": return "double";
            case "System.Decimal": return "decimal";
            case "System.Boolean": return "bool";
            case "System.DateTime": return "DateTime";
            case "System.DateTimeOffset": return "DateTimeOffset";
            case "System.Byte": return "byte";
            case "System.Byte[]": return "byte[]";
            default:
                break;
        }
        return dbType;
    }

    public static string GetNetTypeByDBType(string dbDataType)
    {
        dbDataType = dbDataType.ToLower();

        // Sql Server, 文档：https://learn.microsoft.com/en-us/sql/t-sql/data-types/data-types-transact-sql?view=sql-server-ver16
        // 仅供参考：https://www.cnblogs.com/kunlunmountain/p/5663357.html
        if (true)
        {
            switch (dbDataType)
            {
                case "uniqueidentifier":
                    return "guid";
                case "tinyint"://1 Byte, 0 to 255
                    return "byte";
                case "smallint"://2 Bytes, -2^15 (-32,768) to 2^15-1 (32,767)
                    return "Int16";
                case "int": // 4 Bytes, 
                    return "int"; // Int32
                case "bigint":// 8 Bytes, -2^63 (-9,223,372,036,854,775,808) to 2^63-1 (9,223,372,036,854,775,807)
                    return "long";
                case "char":
                case "varchar":
                case "nvarchar":
                case "text":
                case "xml":
                case "varbinary":
                case "image":
                    return "string";
                case "bit":
                    return "bool";
                case "time":
                case "date":
                case "datetime":
                case "datetime2": // EF CodeFirst会将datetime自动映射成datetime2
                case "smalldatetime":
                    return "DateTime";
                case "datetimeoffset":
                    return "DateTimeOffset";
                case "smallmoney":
                case "decimal":
                case "numeric":
                case "money":
                    return "decimal";
                case "float":// 8bit, 可精确到第15 位小数，其范围为从-1.79e -308 到1.79e +308
                    return NetTypeRaw._double;
                case "real":// 4字节，可精确到第7 位小数，其范围为从-3.40e -38 到3.40e +38
                    return NetTypeRaw._float;

                    default:
                    return "UnknownDbType_" + dbDataType;// 这里应该提示或报错
                    //return "string";
            }

            // 这里是参考的OpenAuth.Core, 里面大小写不一，项目稳定后可以删除
            //switch (dbDataType)
            //{
            //    case "uniqueidentifier":
            //        return "guid";
            //    case "smallint":
            //    case "INT":
            //        return "int";
            //    case "BIGINT":
            //        return "long";
            //    case "CHAR":
            //    case "VARCHAR":
            //    case "NVARCHAR":
            //    case "text":
            //    case "xml":
            //    case "varbinary":
            //    case "image":
            //        return "string";
            //    case "tinyint":
            //        return "byte";
            //    case "bit":
            //        return "bool";
            //    case "time":
            //    case "date":
            //    case "DATETIME":
            //    case "smallDATETIME":
            //        return "DateTime";
            //    case "smallmoney":
            //    case "DECIMAL":
            //    case "numeric":
            //    case "money":
            //        return "decimal";
            //    case "float":
            //        return "double";

            //    default:
            //        return "string";
            //}
        }
    }
}

/// <summary>
/// C# Net data type without ?
/// C# 类型，不带问号
/// TODO：如果追加新模式类建表/在页面中建类，这里可以在前端选择
/// NetType包含？，前端选择不包含？，可以使用字段NetTypeRaw在前端选择
/// 结合前端选项IsRequired（必填项）判断IsNullable, 前端其他选项：主键，自增
/// </summary>
public static class NetTypeRaw
{
    public const string _Guid = "Guid";
    public const string _string = "string";
    // Int16
    public const string _short = "short";
    // Int32
    public const string _int = "int";
    // Int64
    public const string _long = "long";

    /// <summary>
    /// 4字节，单精度，可精确到第7 位小数，其范围为从-3.40e -38 到3.40e +38
    /// </summary>
    public const string _float = "float";// Single

    /// <summary>
    /// 8bit, 双精度类型, 可精确到第15 位小数，其范围为从-1.79e -308 到1.79e +308
    /// </summary>
    public const string _double = "double";
    public const string _decimal = "decimal";
    public const string _bool = "bool";
    public const string _DateTime = "DateTime";
    public const string _DateTimeOffset = "DateTimeOffset";

    /// <summary>
    /// 0~255
    /// </summary>
    public const string _byte = "byte";
    public const string _byteArray = "byte[]";

    public const string _object = "object";
}
