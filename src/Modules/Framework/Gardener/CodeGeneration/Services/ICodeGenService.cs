// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.CodeGeneration.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.CodeGeneration.Services
{
    /// <summary>
    /// 代码生成服务, DB First
    /// </summary>
    public interface ICodeGenService: IServiceBase<CodeGenDto, Guid>
    {
        /// <summary>
        /// GenerateCode
        /// </summary>
        /// <param name="codeGenIds"></param>
        /// <returns></returns>
        Task<bool> GenerateCode(Guid[] codeGenIds);
        /// <summary>
        /// GenerateLocale
        /// </summary>
        /// <param name="codeGenId"></param>
        /// <returns></returns>
        Task<bool> GenerateLocale(Guid codeGenId);
        /// <summary>
        /// GenerateMenu
        /// </summary>
        /// <param name="codeGenId"></param>
        /// <returns></returns>
        Task<bool> GenerateMenu(Guid codeGenId);
        /// <summary>
        /// GetTableListAsync
        /// </summary>
        /// <returns></returns>
        Task<List<TableOutput>> GetTableListAsync();
        /// <summary>
        /// OpenCodeGenFolder
        /// </summary>
        /// <returns></returns>
        Task<bool> OpenCodeGenFolder();
    }
}
