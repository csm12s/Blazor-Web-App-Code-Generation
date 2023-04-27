// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Linq.Expressions;
using System.Reflection;

namespace Gardener.Common
{
    /// <summary>
    /// 表达式工具
    /// </summary>
    public class ExpressionHelper
    {
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MemberInfo? GetReturnMemberInfo(LambdaExpression expression)
        {
            var accessorBody = expression.Body;
            while (true)
            {
                if (accessorBody is UnaryExpression unaryExpression)
                {
                    accessorBody = unaryExpression.Operand;
                }
                else if (accessorBody is ConditionalExpression conditionalExpression)
                {
                    accessorBody = conditionalExpression.IfTrue;
                }
                else if (accessorBody is MethodCallExpression methodCallExpression)
                {
                    accessorBody = methodCallExpression.Object;
                }
                else if (accessorBody is BinaryExpression binaryExpression)
                {
                    accessorBody = binaryExpression.Left;
                }
                else
                {
                    break;
                }
            }

            if (accessorBody is not MemberExpression memberExpression)
            {
                return null;
            }

            return memberExpression.Member;
        }
    }
}
