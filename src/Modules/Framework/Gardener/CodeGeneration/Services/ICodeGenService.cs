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
        /// 生成代码
        /// </summary>
        /// <param name="codeGenIds"></param>
        /// <returns></returns>
        Task<bool> GenerateCode(Guid[] codeGenIds);
        /// <summary>
        /// 生成语言环境
        /// </summary>
        /// <param name="codeGenId"></param>
        /// <returns></returns>
        Task<bool> GenerateLocale(Guid codeGenId);
        /// <summary>
        /// 生成目录
        /// </summary>
        /// <param name="codeGenId"></param>
        /// <returns></returns>
        Task<bool> GenerateMenu(Guid codeGenId);
        /// <summary>
        /// 异步获取表格列表
        /// string dbContextLocatorName = ""
        /// </summary>
        /// <returns></returns>
        Task<List<TableOutput>> GetTableListAsync();
        /// <summary>
        /// 打开代码生成文件夹
        /// </summary>
        /// <returns></returns>
        Task<bool> OpenCodeGenFolder();
    }
}
