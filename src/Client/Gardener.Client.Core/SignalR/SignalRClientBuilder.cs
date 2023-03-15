// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authorization.Dtos;
using Gardener.Client.Base;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Gardener.Client.Core
{
    public class SignalRClientBuilder : ISignalRClientBuilder
    {
        private string _clientName=null!;
        private string _url = null!;
        private Func<Task<string>> _accessTokenProvider = null!;
        private bool _enableAutomaticReconnect = true;
        private IClientLogger _clientLogger;

        private readonly IAuthenticationStateManager _authenticationStateManager;
        private readonly IOptions<ApiSettings> _options;

        public SignalRClientBuilder(IClientLogger clientLogger, IAuthenticationStateManager authenticationStateManager, IOptions<ApiSettings> options)
        {
            _clientLogger = clientLogger;
            _authenticationStateManager = authenticationStateManager;
            _options = options;
        }
        public ISignalRClientBuilder GetInstance()
        {
            return new SignalRClientBuilder(_clientLogger, _authenticationStateManager, _options);
        }
        public ISignalRClient Build()
        {
            if (_clientName == null)
            {
                throw new ArgumentNullException("clientName");
            }
            if (_url == null)
            {
                throw new ArgumentNullException("url");
            }
            if (_accessTokenProvider == null)
            {
                _accessTokenProvider = async () =>
                 {
                     TokenOutput? token = await _authenticationStateManager.GetCurrentToken();
                     if (token != null) 
                     {
                         return token.AccessToken;
                     }
                     throw new Exception("not get token");
                 };
            }
            if (_url.IndexOf(":") == -1)
            {
                _url = $"{_options.Value.BaseAddres}{_url}";
            }
            SignalRClient signalRClient = new SignalRClient(_clientName,_url, _clientLogger, _accessTokenProvider);
            signalRClient.AutomaticReconnect(_enableAutomaticReconnect);
            return signalRClient;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessTokenProvider"></param>
        /// <returns></returns>
        public ISignalRClientBuilder SetAccessTokenProvider(Func<Task<string>> accessTokenProvider)
        {
            this._accessTokenProvider = accessTokenProvider;
            return this;
        }

        public ISignalRClientBuilder SetClientName(string clientName)
        {
            this._clientName = clientName;
            return this;
        }

        public ISignalRClientBuilder SetEnableAutomaticReconnect(bool enableAutomaticReconnect = true)
        {
            this._enableAutomaticReconnect = enableAutomaticReconnect;
            return this;
        }

        public ISignalRClientBuilder SetLogger(IClientLogger clientLogger)
        {
            this._clientLogger = clientLogger;
            return this;
        }

        public ISignalRClientBuilder SetUrl(string url)
        {
            this._url = url;
            return this;
        }
    }
}
