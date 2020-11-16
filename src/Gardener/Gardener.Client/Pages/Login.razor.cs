// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Application.Dtos;
using Gardener.Client.Apis;
using Gardener.Client.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Gardener.Client.Pages
{
    public partial class Login
    {
        private bool isLoading = false;

        public void Loading()
        {
            isLoading = true;
        }

        public void UnLoading()
        {
            isLoading = false;
        }

        public void SwitchLoading()
        {
            isLoading = !isLoading;
        }

        private LoginInput loginInput = new LoginInput();
        [Inject]
        public MessageService MsgSvr { get; set; }
        [Inject]
        public IAuthorizeService AuthorizeService { get; set; }

        [Inject] public AuthenticationStateProvider AuthProvider { get; set; }

        public void OnLogin()
        {
            SwitchLoading();
            var loginOutResult= AuthorizeService.Login(loginInput);
            SwitchLoading();
            if (loginOutResult.Successed)
            {
                MsgSvr.Success($"登录成功");
                ((AuthProvider)AuthProvider).MarkUserAsAuthenticated(loginOutResult.Data);
            }
            else {
                MsgSvr.Error($"登录失败：{loginOutResult.Errors}");
            }
        }
        public void OnCancel()
        {
            SwitchLoading();
        }
    }
    
}
