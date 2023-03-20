
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
    public interface ICodeGenConfigService: IServiceBase<CodeGenConfigDto, Guid>
    {
        /// <summary>
        /// 通过codegenid获取代码生成配置
        /// </summary>
        /// <param name="codeGenId"></param>
        /// <returns></returns>
        Task<List<CodeGenConfigDto>> GetCodeGenConfigsByCodeGenId(Guid codeGenId);
        /// <summary>
        /// 保存全部
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<bool> SaveAll(List<CodeGenConfigDto> list);

        /// <summary>
        /// 删除和新增列表
        /// NoAction
        /// </summary>
        /// <param name="tableColumnOuputs"></param>
        /// <param name="codeGen"></param>
        /// <returns></returns>
        Task DeleteAndAddList(List<TableColumnInfo> tableColumnOuputs, CodeGenDto codeGen);
        /// <summary>
        /// 根据codegenid删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteByCodeGenId(Guid id);
    }
}
