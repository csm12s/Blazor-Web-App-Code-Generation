using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Threading.Tasks;

namespace Gardener.Client.Core
{
    public class AuthProvider : AuthenticationStateProvider
    {

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            throw new NotImplementedException();
        }
      
    }
}
