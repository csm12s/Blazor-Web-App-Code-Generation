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
        if (true)// Sql Server
        {
            switch (dbDataType)
            {
                case "uniqueidentifier":
                    return "guid";
                case "smallint":
                case "int":
                    return "int";
                case "bigint":
                    return "long";
                case "char":
                case "varchar":
                case "nvarchar":
                case "text":
                case "xml":
                case "varbinary":
                case "image":
                    return "string";
                case "tinyint":
                    return "byte";
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
                case "float":
                    return "double";

                default:
                    return "string";
            }

            // 这里是参考的OpenAuth.Core, 里面大小写不一
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
