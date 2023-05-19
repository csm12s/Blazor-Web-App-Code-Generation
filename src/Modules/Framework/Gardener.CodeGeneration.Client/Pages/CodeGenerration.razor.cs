// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base.Services;
using Gardener.CodeGeneration.Dtos;
using Gardener.CodeGeneration.Services;
using Mapster;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.CodeGeneration.Client.Pages
{
    public partial class CodeGenerration
    {
        [Inject]
        IClientMessageService messageService { get; set; } = null!;
        [Inject]
        private ICodeGenerationService codeGenerationService { get; set; } = null!;

        private int current { get; set; } = 0;

        private List<EntityDefinitionDto> entityDefinitions=new List<EntityDefinitionDto>();
        private IEnumerable<EntityDefinitionDto> selectedEntityDefinitions=new List<EntityDefinitionDto>();

        private bool entityDefinitionsLoading = false;

        private EntityCodeGenerationSettingDto selectEntityCodeGenerationSettingDto=new EntityCodeGenerationSettingDto();


        /// <summary>
        /// 页面初始化完成
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            entityDefinitions= await codeGenerationService.GetEntityDefinitions();
        }

        private void OnPreClick()
        {
            current--;
        }

        private async Task OnNextClick()
        {
            current++;
            await StepsOnChange();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        private async Task StepsOnChange()
        {
            if (current == 1)
            {
                if (selectedEntityDefinitions.Count() == 0)
                {
                    messageService.Warn("请选择实体");
                    current = 0;
                }
                EntityDefinitionDto? entityDefinition = selectedEntityDefinitions.FirstOrDefault();
                if (entityDefinition != null)
                {
                    //加载配置
                    EntityCodeGenerationSettingDto settingDto = await codeGenerationService.GetEntityCodeGenerationSetting(entityDefinition.FullName);

                    if (settingDto != null)
                    {
                        settingDto.Adapt(selectEntityCodeGenerationSettingDto);
                    }
                }
            }
        }
    }
}
