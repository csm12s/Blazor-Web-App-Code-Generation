// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using BootstrapBlazor.Components;
using Gardener.Base.Resources;
using Gardener.Client.Base;
using Gardener.Client.BootstrapUi.Base.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Client.BootstrapUi.Services
{
    [ScopedService]
    public class ClientNotifier : IClientNotifier
    {
        private double duration = ClientConstant.ClientNotifierMessageDuration;
        private int msgMaxLength = ClientConstant.ClientNotifierUseMessageMaxLength;
        private readonly IClientLocalizer localizer;
        private readonly ToastService toastService;
        public ClientNotifier(IClientLocalizer localizer, ToastService toastService)
        {
            this.localizer = localizer;
            this.toastService = toastService;
        }

        public void Error(string description, string? title = null, Exception? ex = null)
        {
            ErrorAsync(description, title, ex);
        }

        public Task ErrorAsync(string description, string? title = null, Exception? ex = null)
        {
            return toastService.Error(title ?? localizer[SharedLocalResource.Error], description);
        }

        public void Info(string description, string? title = null)
        {
            InfoAsync(description, title);
        }

        public Task InfoAsync(string description, string? title = null)
        {
            return toastService.Information(title ?? localizer[SharedLocalResource.Info], description);
        }

        public void Success(string description, string? title = null)
        {
            SuccessAsync(description, title);
        }

        public Task SuccessAsync(string description, string? title = null)
        {
            return toastService.Success(title ?? localizer[SharedLocalResource.Success], description);
        }

        public void Warn(string description, string? title = null, Exception? ex = null)
        {
            WarnAsync(description, title, ex);
        }

        public Task WarnAsync(string description, string? title = null, Exception? ex = null)
        {
            return toastService.Warning(title ?? localizer[SharedLocalResource.Warn], description);
        }
    }
}
