// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.CodeGeneration.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.CodeGeneration.Services
{
    /// <summary>
    /// 代码生成服务, DB First
    /// </summary>
    public interface ICodeGenService: IServiceBase<CodeGenDto, int>
    {
        Task<bool> GenerateCode(int[] codeGenIds);
        Task<List<TableOutput>> GetTableListAsync();//string dbContextLocatorName = ""
    }
}
