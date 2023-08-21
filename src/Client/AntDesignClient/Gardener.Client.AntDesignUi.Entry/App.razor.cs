// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Base.Resources;
using Gardener.Client.Base;
using Gardener.LocalizationLocalizer;
using Microsoft.AspNetCore.Components;

namespace Gardener.Client.AntDesignUi.Entry
{
    public partial class App
    {
        [Inject]
        private ClientModuleContext moduleContext { get; set; } = null!;
        [Inject]
        private NavigationManager Navigation { get; set; } = null!;
        /// <summary>
        /// 表单验证消息配置
        /// </summary>
        private FormConfig formValidateMessagesConfig = new FormConfig
        {
            ValidateMessages = new FormValidateErrorMessages
            {
                Required = Lo.GetValue<SharedLocalResource>("'{0}' is required"),
                Default = Lo.GetValue<SharedLocalResource>("Validation error on field '{0}'"),
                OneOf = Lo.GetValue<SharedLocalResource>("'{0}' must be one of [{1}]"),
                Whitespace = Lo.GetValue<SharedLocalResource>("'{0}' cannot be empty"),
                String = new FormValidateErrorMessages.StringMessage
                {
                    Len = Lo.GetValue<SharedLocalResource>("'{0}' must be exactly {1} characters"),
                    Min = Lo.GetValue<SharedLocalResource>("'{0}' must be at least {1} characters"),
                    Max = Lo.GetValue<SharedLocalResource>("'{0}' cannot be longer than {1} characters"),
                    Range = Lo.GetValue<SharedLocalResource>("'{0}' must be between {1} and {2} characters"),
                },
                Number = new FormValidateErrorMessages.NumberMessage
                {
                    Len = Lo.GetValue<SharedLocalResource>("'{0}' must equal {1}"),
                    Min = Lo.GetValue<SharedLocalResource>("'{0}' cannot be less than {1}"),
                    Max = Lo.GetValue<SharedLocalResource>("'{0}' cannot be greater than {1}"),
                    Range = Lo.GetValue<SharedLocalResource>("'{0}' must be between {1} and {2}")
                },
                Array = new FormValidateErrorMessages.ArrayMessage
                {
                    Len = Lo.GetValue<SharedLocalResource>("'{0}' must be exactly {1} in length"),
                    Min = Lo.GetValue<SharedLocalResource>("'{0}' cannot be less than {1} in length"),
                    Max = Lo.GetValue<SharedLocalResource>("'{0}' cannot be greater than {1} in length"),
                    Range = Lo.GetValue<SharedLocalResource>("'{0}' must be between {1} and {2} in length")
                },
                Pattern = new FormValidateErrorMessages.PatternMessage
                {
                    Mismatch = Lo.GetValue<SharedLocalResource>("'{0}' does not match pattern {1}")
                }
            }
        };
        /// <summary>
        /// 
        /// </summary>
        private void GoHome()
        {
            Navigation.NavigateTo("/");
        }
    }
}
