// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.CodeGeneration.Dtos;
using Gardener.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gardener.Base;

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

        //TODO: 其他数据库 可以参考OpenAuth.Core：_dbExtension.GetDbTableStructure(obj.TableName)

        if (true)// Sql Server, 文档：https://learn.microsoft.com/en-us/sql/t-sql/data-types/data-types-transact-sql?view=sql-server-ver16
        {
            switch (dbDataType)
            {
                case "uniqueidentifier":
                    return "guid";
                case "tinyint"://1字节，从0 到255 之间的所有正整数
                    return "byte";
                case "smallint"://-2的15次方（ -32， 768） 到2的15次方-1（ 32 ，767 ）之间的所有正负整数
                    return "Int16";
                case "int":
                    return "int"; // Int32
                case "bigint":// 8字节，-2^63 （-9 ，223， 372， 036， 854， 775， 807） 到2^63-1（ 9， 223， 372， 036 ，854 ，775， 807） 之间的所有正负整数
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
                case "smalldatetime":
                    return "DateTime";
                case "smallmoney":
                case "decimal":
                case "numeric":
                case "money":
                    return "decimal";
                case "float":// 8bit, 可精确到第15 位小数，其范围为从-1.79e -308 到1.79e +308
                    return NetType._double;
                case "real":// 4字节，可精确到第7 位小数，其范围为从-3.40e -38 到3.40e +38
                    return NetType._float; //这里参考https://www.cnblogs.com/kunlunmountain/p/5663357.html

                    default:
                    return "string";
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
/// C# Net data type, TODO
/// </summary>
public static class NetType
{
    public static string _string = "string";

    // 4字节，单精度，可精确到第7 位小数，其范围为从-3.40e -38 到3.40e +38
    public static string _float = "float";

    // 8bit, 双精度类型, 可精确到第15 位小数，其范围为从-1.79e -308 到1.79e +308
    public static string _double = "double";

    // decimal
    
}
