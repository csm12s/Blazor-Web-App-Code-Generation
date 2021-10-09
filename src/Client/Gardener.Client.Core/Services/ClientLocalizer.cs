// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Microsoft.Extensions.Localization;

namespace Gardener.Client.Core.Services
{
    public class ClientLocalizer<T> : IClientLocalizer
    {
        private readonly IStringLocalizer<T> localizer;

        public ClientLocalizer(IStringLocalizer<T> localizer)
        {
            this.localizer = localizer;
        }

        public string this[string name] => localizer[name].Value;

        public string Combination(params string[] names)
        {
            if (names.Length == 0) {
                return string.Empty;
            }
            System.Text.StringBuilder msg = new System.Text.StringBuilder();
            for (int i=0;i<names.Length;i++)
            {
                msg.Append(localizer[names[i]].Value);
                msg.Append(' ');
            }
            return msg.ToString().TrimEnd(' ');
        }
        
    }
}
