// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.SystemManager.Client.Services;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Resources;
using Gardener.SystemManager.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.SystemManager.Client.Pages.CodeView
{
    /// <summary>
    /// 字典编辑框
    /// </summary>
    public partial class CodeEdit : EditOperationDialogBase<CodeDto,int, SystemManagerResource>
    {
        [Inject]
        protected ICodeTypeService CodeTypeService { get; set; } = null!;
        /// <summary>
        /// 字典类型
        /// </summary>
        private List<CodeTypeDto>? codeTypeDtos;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            StartLoading();
            await LoadEditModelData();
            codeTypeDtos = await CodeTypeService.GetAllUsable();
            StopLoading();
        }
    }
}
