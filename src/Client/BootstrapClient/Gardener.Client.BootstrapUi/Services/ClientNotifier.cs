// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

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

        public ClientNotifier(IClientLocalizer localizer)
        {
            this.localizer = localizer;
        }

        public Task Error(string description, Exception ex = null)
        {
            throw new NotImplementedException();
        }

        public Task Error(string msg, string description, Exception ex = null)
        {
            throw new NotImplementedException();
        }

        public Task Info(string description)
        {
            throw new NotImplementedException();
        }

        public Task Info(string msg, string description)
        {
            throw new NotImplementedException();
        }

        public Task Success(string description)
        {
            throw new NotImplementedException();
        }

        public Task Success(string msg, string description)
        {
            throw new NotImplementedException();
        }

        public Task Warn(string description, Exception ex = null)
        {
            throw new NotImplementedException();
        }

        public Task Warn(string msg, string description, Exception ex = null)
        {
            throw new NotImplementedException();
        }
    }
}
