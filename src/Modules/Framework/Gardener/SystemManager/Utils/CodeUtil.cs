// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Attributes;
using Gardener.Common;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace Gardener.SystemManager.Utils
{
    /// <summary>
    /// 字典工具
    /// </summary>
    public static class CodeUtil
    {
        /// <summary>
        /// 字典缓存
        /// </summary>
        private static Dictionary<string, Dictionary<string, CodeDto>> typeValueCodeMap = new Dictionary<string, Dictionary<string, CodeDto>>();
        /// <summary>
        /// 字典类型服务
        /// </summary>
        private static ICodeTypeService? _codeTypeService;
        /// <summary>
        /// 初始化所有Code缓存
        /// </summary>
        /// <param name="codeTypeService"></param>
        /// <returns></returns>
        public static async Task InitAllCode(ICodeTypeService codeTypeService)
        {
            if (codeTypeService != null)
            {
                _codeTypeService = codeTypeService;
            }
            if (_codeTypeService == null)
            {
                throw new ArgumentNullException(nameof(codeTypeService));
            }
            typeValueCodeMap.Clear();
            Dictionary<string, IEnumerable<CodeDto>> keyValues = await _codeTypeService.GetCodeDicByValues(new string[0]);
            foreach (var item in keyValues)
            {
                Dictionary<string, CodeDto> codeValueMap = new Dictionary<string, CodeDto>();
                foreach (var value in item.Value)
                {
                    codeValueMap.TryAdd(value.CodeValue, value);
                }
                typeValueCodeMap.TryAdd(item.Key, codeValueMap);
            }
        }
        /// <summary>
        /// 从缓存获取字典
        /// </summary>
        /// <param name="codeTypeValue"></param>
        /// <param name="codeValue"></param>
        /// <returns></returns>
        public static CodeDto? GetCodeFromCache(string codeTypeValue, string codeValue)
        {
            CodeDto? codeDto = null;
            if (codeTypeValue == null || codeValue == null)
            {
                return codeDto;
            }
            if (typeValueCodeMap.ContainsKey(codeTypeValue))
            {
                typeValueCodeMap[codeTypeValue].TryGetValue(codeValue, out codeDto);
            }
            return codeDto;
        }
        /// <summary>
        /// 从缓存获取字典值
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="fieldExpression">获取字典值的表达式</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static CodeDto? GetCodeFromCache<TDto>(Expression<Func<object?>> fieldExpression)
        {
            var (codeTypeValue, codeValue) = GetCodeTypeAndCodeValue<TDto>(fieldExpression);
            if (codeTypeValue == null || codeValue == null)
            {
                return null;
            }
            return GetCodeFromCache(codeTypeValue, codeValue);
        }
        /// <summary>
        /// 从缓存获取字典
        /// </summary>
        /// <typeparam name="TDto">字典所在类</typeparam>
        /// <returns></returns>
        /// <remarks>
        /// 扫描 TDto 中 各字段，有<see cref="CodeTypeAttribute"/>的字典类型，返回所有已缓存字典
        /// </remarks>
        public static Dictionary<string, IEnumerable<CodeDto>?>? GetCodesFromCache<TDto>()
        {
            List<string> codeTypeValues = GetCodeTypeValues<TDto>();
            return GetCodesFromCache(codeTypeValues.ToArray());
        }
        /// <summary>
        /// 从缓存获取字典
        /// </summary>
        /// <param name="codeTypeValues">字典类型集合</param>
        /// <returns></returns>
        public static Dictionary<string, IEnumerable<CodeDto>?>? GetCodesFromCache(params string[] codeTypeValues)
        {
            if (!codeTypeValues.Any())
            {
                return null;
            }
            Dictionary<string, IEnumerable<CodeDto>?>? result = new Dictionary<string, IEnumerable<CodeDto>?>();
            foreach (var codeTypeValue in codeTypeValues)
            {
                Dictionary<string, CodeDto>? codeMap = null;
                typeValueCodeMap.TryGetValue(codeTypeValue, out codeMap);
                result.Add(codeTypeValue, codeMap == null ? null : codeMap.Values);
            }
            return result;
        }
        /// <summary>
        /// 从缓存获取字典
        /// </summary>
        /// <typeparam name="TDto">字段所在类</typeparam>
        /// <param name="fieldExpression">获取字段表达式</param>
        /// <returns></returns>
        public static IEnumerable<CodeDto>? GetCodesFromCache<TDto>(Expression<Func<object?>> fieldExpression)
        {
            var (codeTypeValue, _) = GetCodeTypeAndCodeValue<TDto>(fieldExpression);
            if (codeTypeValue == null)
            {
                return null;
            }
            return GetCodesFromCache(codeTypeValue);
        }
        /// <summary>
        /// 从缓存获取字典列表
        /// </summary>
        /// <param name="codeTypeValue"></param>
        /// <returns></returns>
        public static IEnumerable<CodeDto>? GetCodesFromCache(string codeTypeValue)
        {
            Dictionary<string, CodeDto>? codeMap = null;
            typeValueCodeMap.TryGetValue(codeTypeValue, out codeMap);
            return codeMap == null ? null : codeMap.Values;
        }
        /// <summary>
        /// 从缓存获取字典名称
        /// </summary>
        /// <param name="codeTypeValue"></param>
        /// <param name="codeValue"></param>
        /// <returns></returns>
        public static string? GetCodeNameFromCache(string codeTypeValue, string codeValue)
        {
            var code = GetCodeFromCache(codeTypeValue, codeValue);
            return code?.CodeName;
        }
        /// <summary>
        /// 从缓存获取字典名称
        /// </summary>
        /// <typeparam name="TDto">字段所在类</typeparam>
        /// <param name="fieldExpression">获取字段表达式</param>
        /// <returns></returns>
        public static string? GetCodeNameFromCache<TDto>(Expression<Func<object?>> fieldExpression)
        {
            var code = GetCodeFromCache<TDto>(fieldExpression);
            return code?.CodeName;
        }
        /// <summary>
        /// 扫描所有字段上的 <see cref="CodeTypeAttribute"/>
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <returns></returns>
        public static List<string> GetCodeTypeValues<TDto>()
        {
            PropertyInfo[] properties = typeof(TDto).GetProperties();
            List<string> codeTypeValues = new List<string>();
            foreach (PropertyInfo property in properties)
            {
                CodeTypeAttribute? codeValue = property.GetCustomAttribute<CodeTypeAttribute>();
                if (codeValue != null)
                {
                    codeTypeValues.Add(codeValue.CodeTypeValue);
                }
            }
            return codeTypeValues;
        }
        /// <summary>
        /// 从表达式获取字典类型和字典值
        /// </summary>
        /// <typeparam name="TDto">字段所在类</typeparam>
        /// <param name="fieldExpression">获取字段表达式</param>
        /// <returns></returns>
        public static (string?, string?) GetCodeTypeAndCodeValue<TDto>(Expression<Func<object?>> fieldExpression)
        {
            object? codeValueObj = fieldExpression.Compile().Invoke();
            string? codeValue = codeValueObj?.ToString();
            if (fieldExpression.Body is not MemberExpression memberExp)
            {
                return (null, codeValue);
            }
            Type type = typeof(TDto);
            var paramExp = Expression.Parameter(type);
            var bodyExp = Expression.MakeMemberAccess(paramExp, memberExp.Member);
            LambdaExpression GetFieldExpression = Expression.Lambda(bodyExp, paramExp);
            var member = ExpressionHelper.GetReturnMemberInfo(GetFieldExpression);
            CodeTypeAttribute? codeType = member?.GetCustomAttribute<CodeTypeAttribute>();
            if (codeType == null)
            {
                return (null, null);
            }
            return (codeType.CodeTypeValue, codeValue);
        }
    }
}
