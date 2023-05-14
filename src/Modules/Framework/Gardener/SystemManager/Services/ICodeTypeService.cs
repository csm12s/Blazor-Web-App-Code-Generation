// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.SystemManager.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.SystemManager.Services
{
    /// <summary>
    /// 字段类型服务
    /// </summary>
    public interface ICodeTypeService : IServiceBase<CodeTypeDto,int>
    {
        /// <summary>
        /// 根据多个字典类型获取所有字典的结果
        /// </summary>
        /// <param name="codeTypeIds"></param>
        /// <returns></returns>
        Task<Dictionary<int, IEnumerable<CodeDto>>> GetCodeDic(params int[] codeTypeIds);
        /// <summary>
        /// 根据字典类型获取字典列表
        /// </summary>
        /// <param name="codeTypeId"></param>
        /// <returns></returns>
        Task<IEnumerable<CodeDto>> GetCodes(int codeTypeId);

        /// <summary>
        /// 根据字典类型值获取字典列表
        /// </summary>
        /// <param name="codeTypeValue"></param>
        /// <returns></returns>
        /// <remarks>
        /// 根据字典类型值获取字典列表
        /// </remarks>
        Task<IEnumerable<CodeDto>> GetCodesByValue(string codeTypeValue);
        /// <summary>
        /// 根据多个字典类型编号获取所有字典的结果
        /// </summary>
        /// <param name="codeTypeValues"></param>
        /// <returns></returns>
        /// <remarks>
        /// 根据多个字典类型编号获取所有字典的结果
        /// </remarks>
        Task<Dictionary<string, IEnumerable<CodeDto>>> GetCodeDicByValues(params string[] codeTypeValues);
        /// <summary>
        /// 刷新字典工具缓存
        /// </summary>
        /// <returns></returns>
        Task<bool> RefreshCodeUtilCache();
    }
}
