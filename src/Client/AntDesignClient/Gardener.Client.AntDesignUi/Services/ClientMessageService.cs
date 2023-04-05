// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.Base;
using Gardener.Client.Base.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Client.AntDesignUi.Services
{
    [ScopedService]
    public class ClientMessageService : IClientMessageService
    {
        private readonly MessageService messageService;

        public ClientMessageService(MessageService messageService)
        {
            this.messageService = messageService;
        }

        public void Error(string content, double? duration = null, Action? onClose = null)
        {
            messageService.Error(content, duration, onClose);
        }

        public Task ErrorAsync(string content, double? duration = null, Action? onClose = null)
        {
            return messageService.Error(content, duration, onClose);
        }

        public void Info(string content, double? duration = null, Action? onClose = null)
        {
            messageService.Info(content, duration, onClose);
        }

        public Task InfoAsync(string content, double? duration = null, Action? onClose = null)
        {
           return messageService.Info(content, duration, onClose);
        }

        public void Loading(string content, double? duration = null, Action? onClose = null)
        {
            messageService.Loading(content, duration, onClose);
        }

        public Task LoadingAsync(string content, double? duration = null, Action? onClose = null)
        {
            return messageService.Loading(content, duration, onClose);
        }

        public void Success(string content, double? duration = null, Action? onClose = null)
        {
           messageService.Success(content, duration, onClose);
        }

        public Task SuccessAsync(string content, double? duration = null, Action? onClose = null)
        {
           return messageService.Success(content, duration, onClose);
        }

        public void Warn(string content, double? duration = null, Action? onClose = null)
        {
            messageService.Warn(content, duration, onClose);
        }

        public Task WarnAsync(string content, double? duration = null, Action? onClose = null)
        {
            return messageService.Warn(content, duration, onClose);
        }

        public void Warning(string content, double? duration = null, Action? onClose = null)
        {
            messageService.Warning(content, duration, onClose);
        }

        public Task WarningAsync(string content, double? duration = null, Action? onClose = null)
        {
            return messageService.Warning(content, duration, onClose); 
        }
    }
}
