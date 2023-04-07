// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.EntityFramwork;
using Gardener.SystemManager.Domains;
using Gardener.SystemManager.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.SystemManager.Services
{
    /// <summary>
    /// 字典类型管理
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class CodeTypeService : ServiceBase<CodeType, CodeTypeDto>, ICodeTypeService
    {
        /// <summary>
        /// 字典类型管理
        /// </summary>
        /// <param name="repository"></param>
        public CodeTypeService(IRepository<CodeType, MasterDbContextLocator> repository) : base(repository)
        {
        }
    }
}
