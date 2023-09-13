
using AntDesign;
using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.Base.Services;
using Gardener.LocalizationLocalizer;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Resources;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.AccountView.SettingsView
{
    public partial class SecurityView : OperationDialogBase<int, bool, UserCenterResource>
    {
        ColLayoutParam _labelCol = new ColLayoutParam
        {
            Xs = new EmbeddedProperty { Span = 24 },
            Sm = new EmbeddedProperty { Span = 6 },
        };

        ColLayoutParam _wrapperCol = new ColLayoutParam
        {
            Xs = new EmbeddedProperty { Span = 24 },
            Sm = new EmbeddedProperty { Span = 14 },
        };

        private static ChangePasswordInput model = new ChangePasswordInput(string.Empty, string.Empty, string.Empty);

        [Inject]
        private IAccountService accountService { get; set; } = null!;

        [Inject]
        private IClientMessageService clientMessageService { get; set; } = null!;

        private FormValidationRule[] confirmNewPasswordValidatorRules = new FormValidationRule[]{
        new FormValidationRule { Required=true,Validator = (validationContext) => {
                if (!string.Equals(validationContext.Value,model.NewPassword))
                {
                    string errorMessage = Lo.GetValue<UserCenterResource>(nameof(UserCenterResource.ConfirmNewPassword));
                    var result = new ValidationResult(errorMessage, new string[] { validationContext.FieldName });
                    return result;
                }
                return null;
            }
        },
    };

        /// <summary>
        /// 表单完成时
        /// </summary>
        /// <param name="editContext"></param>
        /// <returns></returns>
        protected virtual async Task OnFormFinish(EditContext editContext)
        {
            await StartLoading();
            bool result = await accountService.ChangePassword(model);
            if (result)
            {
                clientMessageService.Success(Localizer.Combination(nameof(SharedLocalResource.Save), nameof(SharedLocalResource.Succeed)));
            }
            await StopLoading();
        }
    }
}