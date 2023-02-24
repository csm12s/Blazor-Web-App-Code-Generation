// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using BootstrapBlazor.Components;
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
        private readonly ToastService? toastService;
        public ClientNotifier(IClientLocalizer localizer, ToastService? toastService)
        {
            this.localizer = localizer;
            this.toastService = toastService;
        }

        public async Task Error(string description, Exception ex = null)
        {
            await toastService.Error("通知", description);
        }

        public async Task Error(string msg, string description, Exception ex = null)
        {
            await toastService.Error(msg, description);
        }

        public async Task Info(string description)
        {
            await toastService.Information("通知", description);
        }

        public async Task Info(string msg, string description)
        {
            await toastService.Information(msg, description);
        }

        public async Task Success(string description)
        {
            await toastService.Success("通知", description);
        }

        public async Task Success(string msg, string description)
        {
            await toastService.Success(msg, description);
        }

        public async Task Warn(string description, Exception ex = null)
        {
            await toastService.Warning("通知", description);
        }

        public async Task Warn(string msg, string description, Exception ex = null)
        {
            await toastService.Warning(msg, description);
        }
    }
}
